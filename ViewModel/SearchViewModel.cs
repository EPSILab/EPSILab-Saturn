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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel
{
    public class SearchViewModel : MyViewModelBase, ISearchViewModel
    {
        public SearchViewModel()
        {
            SearchCommand = new AsyncDelegateCommand<string>(SearchAsync);
            PinCommand = new RelayCommand<PinnableObject>(Pin);
            GoToDetailsPageCommand = new RelayCommand<VisualGenericItem>(GoToDetailsPage);;
        }

        #region Public commands

        public ICommand SearchCommand { get; private set; }
        public ICommand PinCommand { get; private set; }
        public ICommand GoToDetailsPageCommand { get; private set; }

        #endregion

        #region Private methods

        private async Task SearchAsync(string keyword)
        {
            Keyword = keyword;

            Results = new VisualMenu
            {
                Groups = new ObservableCollection<VisualGenericGroup>()
            };

            await SearchNewsAsync();
            await SearchConferencesAsync();
            await SearchSalonsAsync();
        }

        private async Task SearchNewsAsync()
        {
            IsLoading = true;

            IList<News> news = await _modelNews.SearchAsync(Keyword);

            if (news != null && news.Count > 0)
            {
                IList<VisualGenericItem> genericNews = NewsToGenericItemMapper.Mapper(news);

                Results.Groups.Insert(Results.Groups.Count, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_NEWS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericNews),
                    IsFullyLoaded = true
                });
            }

            IsLoading = false;
        }

        private async Task SearchConferencesAsync()
        {
            IsLoading = true;

            IList<Conference> conferences = await _modelConferences.SearchAsync(Keyword);

            if (conferences != null && conferences.Count > 0)
            {
                IList<VisualGenericItem> genericConferences = ConferenceToGenericItemMapper.Mapper(conferences);

                Results.Groups.Insert(Results.Groups.Count, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_CONFERENCES"),
                    Items = new ObservableCollection<VisualGenericItem>(genericConferences),
                    IsFullyLoaded = true
                });
            }

            IsLoading = false;
        }

        private async Task SearchSalonsAsync()
        {
            IsLoading = true;

            IList<Salon> salons = await _modelSalons.SearchAsync(Keyword);

            if (salons != null && salons.Count > 0)
            {
                IList<VisualGenericItem> genericSalon = SalonToGenericItemMapper.Mapper(salons);

                Results.Groups.Insert(Results.Groups.Count, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_SALONS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericSalon),
                    IsFullyLoaded = true
                });
            }

            IsLoading = false;
        }

        private void GoToDetailsPage(VisualGenericItem item)
        {
            Messenger.Default.Send(item);
        }

        private void Pin(PinnableObject element)
        {
            Messenger.Default.Send(element);
        }

        #endregion

        #region Public properties

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                RaisePropertyChanged();
            }
        }

        public VisualMenu Results
        {
            get { return _results; }
            set
            {
                _results = value;
                RaisePropertyChanged();
            }
        }

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

        #region Private attributes

        private string _keyword;
        private VisualMenu _results;
        private VisualGenericItem _selectedItem;

        #endregion

        #region Access to Model

        private readonly IModel<News> _modelNews = ModelFactory<News>.CreateModel();
        private readonly IModel<Conference> _modelConferences = ModelFactory<Conference>.CreateModel();
        private readonly IModel<Salon> _modelSalons = ModelFactory<Salon>.CreateModel();

        #endregion
    }
}