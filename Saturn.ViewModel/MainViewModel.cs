using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Model.Factory;
using SolarSystem.Saturn.Model.Interface;
using SolarSystem.Saturn.ViewModel.Command;
using SolarSystem.Saturn.ViewModel.Helpers;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Mappers;
using SolarSystem.Saturn.ViewModel.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel
{
    class MainViewModel : MyViewModelBase, IMainViewModel
    {
        public MainViewModel()
        {
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

            LoadMenuCommand = new AsyncDelegateCommand(LoadMenuAsync);
            LoadMoreItemsCommand = new AsyncDelegateCommand<string>(LoadMoreItemsAsync);

            GoToMasterPageCommand = new RelayCommand<VisualGenericGroup>(GoToMasterPage);
            GoToDetailsPageCommand = new RelayCommand<VisualGenericItem>(GoToDetailsPage);
            GoToAboutPageCommand = new RelayCommand<object>(GoToAboutPage);

            GoToSocialPageCommand = new RelayCommand<Uri>(GoToSocialNetworkPage);
            PinCommand = new RelayCommand<PinnableObject>(Pin);
        }

        #region Public commands

        public ICommand LoadMenuCommand { get; private set; }
        public ICommand LoadMoreItemsCommand { get; private set; }

        public ICommand GoToMasterPageCommand { get; private set; }
        public ICommand GoToDetailsPageCommand { get; private set; }
        public ICommand GoToAboutPageCommand { get; private set; }

        public ICommand PinCommand { get; private set; }
        public ICommand GoToSocialPageCommand { get; private set; }

        #endregion

        #region Private methods

        private async Task LoadMenuAsync()
        {
            await LoadNewsAsync();
            await LoadBureauAsync();
            await LoadProjetsAsync();
            await LoadConferencesAsync();
            await LoadSalonsAsync();
        }

        private async Task LoadBureauAsync()
        {
            IsLoading = true;

            IEnumerable<Membre> membres = await _modelMembre.GetAsync(0, 8);

            if (membres != null)
            {
                IList<VisualGenericItem> genericMembres = GenericMembreMapper.Mapper(membres);

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

        private async Task LoadConferencesAsync()
        {
            IsLoading = true;

            IEnumerable<Conference> conferences = await _modelConference.GetAsync(0, 8);

            if (conferences != null)
            {
                IList<VisualGenericItem> genericConferences = ConferenceMapper.Mapper(conferences);

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

        private async Task LoadNewsAsync()
        {
            IsLoading = true;

            IEnumerable<News> news = await _modelNews.GetAsync(0, 8);

            if (news != null)
            {
                IList<VisualGenericItem> genericNews = GenericNewsMapper.Mapper(news);

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

        private async Task LoadProjetsAsync()
        {
            IsLoading = true;

            IEnumerable<Projet> projets = await _modelProjet.GetAsync(0, 8);

            if (projets != null)
            {
                IList<VisualGenericItem> genericProjets = GenericProjetMapper.Mapper(projets);

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

        private async Task LoadSalonsAsync()
        {
            IsLoading = true;

            IEnumerable<Salon> salons = await _modelSalon.GetAsync(0, 8);

            if (salons != null)
            {
                IList<VisualGenericItem> genericSalons = GenericSalonMapper.Mapper(salons);

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
                    newElements = GenericNewsMapper.Mapper(news);
                    break;
                case 2:
                    IEnumerable<Projet> projets = await _modelProjet.GetAsync(alreadyLoaded.Count, 8);
                    newElements = GenericProjetMapper.Mapper(projets);
                    break;
                case 3:
                    IEnumerable<Conference> conferences = await _modelConference.GetAsync(alreadyLoaded.Count, 8);
                    newElements = ConferenceMapper.Mapper(conferences);
                    break;
                case 4:
                    IEnumerable<Salon> salons = await _modelSalon.GetAsync(alreadyLoaded.Count, 8);
                    newElements = GenericSalonMapper.Mapper(salons);
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

        private void GoToMasterPage(VisualGenericGroup group)
        {
            Messenger.Default.Send(group);
        }

        private void GoToDetailsPage(VisualGenericItem item)
        {
            Messenger.Default.Send(item);
        }

        private void GoToAboutPage(object element)
        {
            Messenger.Default.Send(element);
        }

        private void Pin(PinnableObject element)
        {
            Messenger.Default.Send(element);
        }

        private void GoToSocialNetworkPage(Uri uri)
        {
            Messenger.Default.Send(uri);
        }

        #endregion

        #region Public properties

        public VisualMenu Menu { get; protected set; }

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

        #region Private properties

        private VisualGenericItem _selectedItem;

        #endregion

        #region Access to Model

        private readonly IModel<Conference> _modelConference = ModelFactory<Conference>.CreateModel();

        private readonly IModel<Membre> _modelMembre = ModelFactory<Membre>.CreateModel();

        private readonly IModel<News> _modelNews = ModelFactory<News>.CreateModel();

        private readonly IModel<Projet> _modelProjet = ModelFactory<Projet>.CreateModel();

        private readonly IModel<Role> _modelRole = ModelFactory<Role>.CreateModel();

        private readonly IModel<Salon> _modelSalon = ModelFactory<Salon>.CreateModel();

        #endregion
    }
}