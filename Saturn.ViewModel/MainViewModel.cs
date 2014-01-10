using Autofac;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.ViewModel.Command;
using SolarSystem.Saturn.ViewModel.Helpers;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Mappers;
using SolarSystem.Saturn.ViewModel.Mappers.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel
{
    /// <summary>
    /// Main page view-model
    /// </summary>
    class MainViewModel : MyViewModelBase, IMainViewModel
    {
        #region Constructor

        /// <summary>
        /// Constructor. Resolve IoC dependencies, create the menu and the commands
        /// </summary>
        public MainViewModel()
        {
            // Resolve Ioc dependencies
            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
            {
                _modelConference = scope.Resolve<IReadableLimitable<Conference>>();
                _modelMembre = scope.Resolve<IReadableMembre>();
                _modelNews = scope.Resolve<IReadableLimitable<News>>();
                _modelConference = scope.Resolve<IReadableLimitable<Conference>>();
                _modelProjet = scope.Resolve<IReadableLimitable<Projet>>();
                _modelSalon = scope.Resolve<IReadableLimitable<Salon>>();
            }

            // Create the menu
            Menu = new VisualMenu
                {
                    Groups = new ObservableCollection<VisualGenericGroup>
                        {
                            new VisualGenericGroup {Title = AppResourcesHelper.GetString("LBL_NEWS")},
                            new VisualGenericGroup {Title = AppResourcesHelper.GetString("LBL_BUREAU")},
                            new VisualGenericGroup {Title = AppResourcesHelper.GetString("LBL_PROJECTS")},
                            new VisualGenericGroup {Title = AppResourcesHelper.GetString("LBL_CONFERENCES")},
                            new VisualGenericGroup {Title = AppResourcesHelper.GetString("LBL_SALONS")}
                        }
                };

            // Create commands
            LoadMenuCommand = new AsyncDelegateCommand(LoadMenuAsync);
            LoadMoreItemsCommand = new AsyncDelegateCommand<string>(LoadMoreItemsAsync);

            GoToMasterPageCommand = new RelayCommand<VisualGenericGroup>(GoToMasterPage);
            GoToDetailsPageCommand = new RelayCommand<VisualGenericItem>(GoToDetailsPage);
            GoToAboutPageCommand = new RelayCommand<object>(GoToAboutPage);

            GoToSocialPageCommand = new RelayCommand<Uri>(GoToSocialNetworkPage);
            PinCommand = new RelayCommand<PinnableObject>(Pin);
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Selected Item
        /// </summary>
        private VisualGenericItem _selectedItem;

        /// <summary>
        /// Access to conferences model
        /// </summary>
        private readonly IReadableLimitable<Conference> _modelConference;

        /// <summary>
        /// Access to members model
        /// </summary>
        private readonly IReadableMembre _modelMembre;

        /// <summary>
        /// Access to news model
        /// </summary>
        private readonly IReadableLimitable<News> _modelNews;

        /// <summary>
        /// Access to projects model
        /// </summary>
        private readonly IReadableLimitable<Projet> _modelProjet;

        /// <summary>
        /// Access to shows model
        /// </summary>
        private readonly IReadableLimitable<Salon> _modelSalon;

        #endregion

        #region Properties

        /// <summary>
        /// Get the menu
        /// </summary>
        public VisualMenu Menu { get; protected set; }

        /// <summary>
        /// Get the selected Item
        /// </summary>
        public VisualGenericItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Get the "load the menu from model" command
        /// </summary>
        public ICommand LoadMenuCommand { get; private set; }

        /// <summary>
        /// Get the "more items from model" command (for Windows Phone only)
        /// </summary>
        public ICommand LoadMoreItemsCommand { get; private set; }

        /// <summary>
        /// Get the "go to a master page" command (list page)
        /// </summary>
        public ICommand GoToMasterPageCommand { get; private set; }

        /// <summary>
        /// Get the "go to a details page" (one element page)
        /// </summary>
        public ICommand GoToDetailsPageCommand { get; private set; }

        /// <summary>
        /// Get the "go to the about page" command
        /// </summary>
        public ICommand GoToAboutPageCommand { get; private set; }

        /// <summary>
        /// Get the "pin command" (for Windows 8 only)
        /// </summary>
        public ICommand PinCommand { get; private set; }

        /// <summary>
        /// Get the "go to social pages" command (for Windows 8 only)
        /// </summary>
        public ICommand GoToSocialPageCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Load the menu. Call others "Load..." methods
        /// </summary>
        private async Task LoadMenuAsync()
        {
            await LoadNewsAsync();
            await LoadBureauAsync();
            await LoadProjetsAsync();
            await LoadConferencesAsync();
            await LoadSalonsAsync();
        }

        /// <summary>
        /// Load the bureau list from the model
        /// </summary>
        private async Task LoadBureauAsync()
        {
            IsLoading = true;

            IEnumerable<Membre> membres = await _modelMembre.GetAsync();

            if (membres != null)
            {
                IMapper<Membre> mapper = new GenericMembreMapper();
                IList<VisualGenericItem> genericMembres = mapper.Map(membres);

                Menu.Groups.RemoveAt(1);

                Menu.Groups.Insert(1, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_BUREAU"),
                    Items = new ObservableCollection<VisualGenericItem>(genericMembres),
                    IsFullyLoaded = true
                });
            }

            IsLoading = false;
        }

        /// <summary>
        /// Load the conferences list from the model
        /// </summary>
        private async Task LoadConferencesAsync()
        {
            IsLoading = true;

            IEnumerable<Conference> conferences = await _modelConference.GetAsync(0, 8);

            if (conferences != null)
            {
                IMapper<Conference> mapper = new GenericConferenceMapper();
                IList<VisualGenericItem> genericConferences = mapper.Map(conferences);

                Menu.Groups.RemoveAt(2);

                Menu.Groups.Insert(2, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_CONFERENCES"),
                    Items = new ObservableCollection<VisualGenericItem>(genericConferences),
                    IsFullyLoaded = genericConferences.Count < 8
                });
            }

            IsLoading = false;
        }

        /// <summary>
        /// Load the news list from the model
        /// </summary>
        private async Task LoadNewsAsync()
        {
            IsLoading = true;

            IEnumerable<News> news = await _modelNews.GetAsync(0, 8);

            if (news != null)
            {
                IMapper<News> mapper = new GenericNewsMapper();
                IList<VisualGenericItem> genericNews = mapper.Map(news);

                Menu.Groups.RemoveAt(0);

                Menu.Groups.Insert(0, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_NEWS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericNews),
                    IsFullyLoaded = genericNews.Count < 8
                });
            }

            IsLoading = false;
        }

        /// <summary>
        /// Load the projects list from the model
        /// </summary>
        private async Task LoadProjetsAsync()
        {
            IsLoading = true;

            IEnumerable<Projet> projets = await _modelProjet.GetAsync(0, 8);

            if (projets != null)
            {
                IMapper<Projet> mapper = new GenericProjetMapper();
                IList<VisualGenericItem> genericProjets = mapper.Map(projets);

                Menu.Groups.RemoveAt(3);

                Menu.Groups.Insert(3, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_PROJECTS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericProjets),
                    IsFullyLoaded = genericProjets.Count < 8
                });
            }

            IsLoading = false;
        }

        /// <summary>
        /// Load the shows list from the model
        /// </summary>
        private async Task LoadSalonsAsync()
        {
            IsLoading = true;

            IEnumerable<Salon> salons = await _modelSalon.GetAsync(0, 8);

            if (salons != null)
            {
                IMapper<Salon> mapper = new GenericSalonMapper();
                IList<VisualGenericItem> genericSalons = mapper.Map(salons);

                Menu.Groups.RemoveAt(4);

                Menu.Groups.Insert(4, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_SALONS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericSalons),
                    IsFullyLoaded = genericSalons.Count < 8
                });
            }

            IsLoading = false;
        }

        /// <summary>
        /// Load and add more items for a category (News, Projects, Conferences or Shows)
        /// </summary>
        /// <param name="category">Category name</param>
        private async Task LoadMoreItemsAsync(string category)
        {
            IsLoading = true;

            int i = Menu.Groups.TakeWhile(g => g.Title != category).Count();

            IList<VisualGenericItem> alreadyLoaded = Menu.Groups[i].Items;
            IList<VisualGenericItem> newElements = new ObservableCollection<VisualGenericItem>();

            switch (i)
            {
                case 0:
                    IEnumerable<News> news = await _modelNews.GetAsync(alreadyLoaded.Count, 8);
                    IMapper<News> mapperNews = new GenericNewsMapper();
                    newElements = mapperNews.Map(news);
                    break;
                case 2:
                    IEnumerable<Projet> projets = await _modelProjet.GetAsync(alreadyLoaded.Count, 8);
                    IMapper<Projet> mapperProjets = new GenericProjetMapper();
                    newElements = mapperProjets.Map(projets);
                    break;
                case 3:
                    IEnumerable<Conference> conferences = await _modelConference.GetAsync(alreadyLoaded.Count, 8);
                    IMapper<Conference> mapperConferences = new GenericConferenceMapper();
                    newElements = mapperConferences.Map(conferences);
                    break;
                case 4:
                    IEnumerable<Salon> salons = await _modelSalon.GetAsync(alreadyLoaded.Count, 8);
                    IMapper<Salon> mapperSalons = new GenericSalonMapper();
                    newElements = mapperSalons.Map(salons);
                    break;
            }

            // If the list contains less of 8 items, so the list is fully loaded
            if (newElements.Count < 8)
            {
                Menu.Groups[i].IsFullyLoaded = true;
            }

            // Concatenation of new elements and old elements
            Menu.Groups[i].Items = new ObservableCollection<VisualGenericItem>(alreadyLoaded.Concat(newElements));

            IsLoading = false;
        }

        /// <summary>
        /// Informs the UI to show the master page in terms of the passed group
        /// </summary>
        /// <param name="group">Group</param>
        private void GoToMasterPage(VisualGenericGroup group)
        {
            Messenger.Default.Send(group);
        }

        /// <summary>
        /// Informs the UI to show the details page in terms of the passed element
        /// </summary>
        /// <param name="item">Element</param>
        private void GoToDetailsPage(VisualGenericItem item)
        {
            Messenger.Default.Send(item);
        }

        /// <summary>
        /// Informs the UI to show the about page
        /// </summary>
        private void GoToAboutPage(object element)
        {
            Messenger.Default.Send(element);
        }

        /// <summary>
        /// Informs the UI to pin a element
        /// </summary>
        /// <param name="element">Element to pin</param>
        private void Pin(PinnableObject element)
        {
            Messenger.Default.Send(element);
        }

        /// <summary>
        /// Informs the UI to show an URL on the browser
        /// </summary>
        /// <param name="uri">URL to open in the browser</param>
        private void GoToSocialNetworkPage(Uri uri)
        {
            Messenger.Default.Send(uri);
        }

        #endregion
    }
}