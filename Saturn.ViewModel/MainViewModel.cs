using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.Command;
using EPSILab.SolarSystem.Saturn.ViewModel.Helpers;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Mappers;
using EPSILab.SolarSystem.Saturn.ViewModel.Mappers.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EPSILab.SolarSystem.Saturn.ViewModel
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
                _modelMember = scope.Resolve<IReadableMember>();
                _modelNews = scope.Resolve<IReadableLimitable<News>>();
                _modelConference = scope.Resolve<IReadableLimitable<Conference>>();
                _modelProject = scope.Resolve<IReadableLimitable<Project>>();
                _modelShow = scope.Resolve<IReadableLimitable<Show>>();
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
        private readonly IReadableMember _modelMember;

        /// <summary>
        /// Access to news model
        /// </summary>
        private readonly IReadableLimitable<News> _modelNews;

        /// <summary>
        /// Access to projects model
        /// </summary>
        private readonly IReadableLimitable<Project> _modelProject;

        /// <summary>
        /// Access to shows model
        /// </summary>
        private readonly IReadableLimitable<Show> _modelShow;

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
            await LoadProjectsAsync();
            await LoadConferencesAsync();
            await LoadShowsAsync();
        }

        /// <summary>
        /// Load the bureau list from the model
        /// </summary>
        private async Task LoadBureauAsync()
        {
            IsLoading = true;

            IEnumerable<Member> members = await _modelMember.GetBureauAsync();

            if (members != null)
            {
                IMapper<Member> mapper = new GenericMemberMapper();
                IList<VisualGenericItem> genericMembers = mapper.Map(members);

                Menu.Groups.RemoveAt(1);

                Menu.Groups.Insert(1, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_BUREAU"),
                    Items = new ObservableCollection<VisualGenericItem>(genericMembers),
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
        private async Task LoadProjectsAsync()
        {
            IsLoading = true;

            IEnumerable<Project> projets = await _modelProject.GetAsync(0, 8);

            if (projets != null)
            {
                IMapper<Project> mapper = new GenericProjectMapper();
                IList<VisualGenericItem> genericProjects = mapper.Map(projets);

                Menu.Groups.RemoveAt(3);

                Menu.Groups.Insert(3, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_PROJECTS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericProjects),
                    IsFullyLoaded = genericProjects.Count < 8
                });
            }

            IsLoading = false;
        }

        /// <summary>
        /// Load the shows list from the model
        /// </summary>
        private async Task LoadShowsAsync()
        {
            IsLoading = true;

            IEnumerable<Show> salons = await _modelShow.GetAsync(0, 8);

            if (salons != null)
            {
                IMapper<Show> mapper = new GenericShowMapper();
                IList<VisualGenericItem> genericShows = mapper.Map(salons);

                Menu.Groups.RemoveAt(4);

                Menu.Groups.Insert(4, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_SALONS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericShows),
                    IsFullyLoaded = genericShows.Count < 8
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
                    IEnumerable<Project> projets = await _modelProject.GetAsync(alreadyLoaded.Count, 8);
                    IMapper<Project> mapperProjects = new GenericProjectMapper();
                    newElements = mapperProjects.Map(projets);
                    break;
                case 3:
                    IEnumerable<Conference> conferences = await _modelConference.GetAsync(alreadyLoaded.Count, 8);
                    IMapper<Conference> mapperConferences = new GenericConferenceMapper();
                    newElements = mapperConferences.Map(conferences);
                    break;
                case 4:
                    IEnumerable<Show> salons = await _modelShow.GetAsync(alreadyLoaded.Count, 8);
                    IMapper<Show> mapperShows = new GenericShowMapper();
                    newElements = mapperShows.Map(salons);
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
        /// Informs the UI to show an Url on the browser
        /// </summary>
        /// <param name="uri">Url to open in the browser</param>
        private void GoToSocialNetworkPage(Uri uri)
        {
            Messenger.Default.Send(uri);
        }

        #endregion
    }
}