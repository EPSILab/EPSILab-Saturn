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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel
{
    /// <summary>
    /// Search page view-model
    /// </summary>
    public class SearchViewModel : MyViewModelBase, ISearchViewModel
    {
        #region Constructor

        /// <summary>
        /// Constructor. Instanciates all commands
        /// </summary>
        /// <param name="modelNews">Model for news. Resolved by NInject</param>
        /// <param name="modelConferences">Model for conferences. Resolved by NInject</param>
        /// <param name="modelSalons">Model for shows. Resolved by NInject</param>
        public SearchViewModel(ISearchable<News> modelNews, ISearchable<Conference> modelConferences, ISearchable<Salon> modelSalons)
        {
            _modelNews = modelNews;
            _modelConferences = modelConferences;
            _modelSalons = modelSalons;

            SearchCommand = new AsyncDelegateCommand<string>(SearchAsync);
            PinCommand = new RelayCommand<PinnableObject>(Pin);
            GoToDetailsPageCommand = new RelayCommand<VisualGenericItem>(GoToDetailsPage);
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Search keywords
        /// </summary>
        private string _keywords;

        /// <summary>
        /// Results list
        /// </summary>
        private VisualMenu _results;

        /// <summary>
        /// Selected item to pin
        /// </summary>
        private VisualGenericItem _selectedItem;

        /// <summary>
        /// Access to the news model
        /// </summary>
        private readonly ISearchable<News> _modelNews;

        /// <summary>
        /// Access to the conferences model
        /// </summary>
        private readonly ISearchable<Conference> _modelConferences;

        /// <summary>
        /// Access to the shows model
        /// </summary>
        private readonly ISearchable<Salon> _modelSalons;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the search keywords
        /// </summary>
        public string Keywords
        {
            get { return _keywords; }
            private set
            {
                _keywords = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Get or set the results lists
        /// </summary>
        public VisualMenu Results
        {
            get { return _results; }
            private set
            {
                _results = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Get or set the selected item pinnable
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
        /// Launch search command
        /// </summary>
        public ICommand SearchCommand { get; private set; }
        
        /// <summary>
        /// Pin command
        /// </summary>
        public ICommand PinCommand { get; private set; }

        /// <summary>
        /// Show details of the clicked item command
        /// </summary>
        public ICommand GoToDetailsPageCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Launch the search
        /// </summary>
        /// <param name="keyword">Search keywords</param>
        private async Task SearchAsync(string keyword)
        {
            Keywords = keyword;

            Results = new VisualMenu
            {
                Groups = new ObservableCollection<VisualGenericGroup>()
            };

            await SearchNewsAsync();
            await SearchConferencesAsync();
            await SearchSalonsAsync();
        }

        /// <summary>
        /// Search in news
        /// </summary>
        private async Task SearchNewsAsync()
        {
            IsLoading = true;

            IList<News> news = await _modelNews.SearchAsync(Keywords);

            if (news != null && news.Count > 0)
            {
                IMapper<News> mapper = new GenericNewsMapper();
                IList<VisualGenericItem> genericNews = mapper.Map(news);

                Results.Groups.Insert(Results.Groups.Count, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_NEWS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericNews),
                    IsFullyLoaded = true
                });
            }

            IsLoading = false;
        }

        /// <summary>
        /// Search in conferences
        /// </summary>
        private async Task SearchConferencesAsync()
        {
            IsLoading = true;

            IList<Conference> conferences = await _modelConferences.SearchAsync(Keywords);

            if (conferences != null && conferences.Count > 0)
            {
                IMapper<Conference> mapper = new GenericConferenceMapper();
                IList<VisualGenericItem> genericConferences = mapper.Map(conferences);

                Results.Groups.Insert(Results.Groups.Count, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_CONFERENCES"),
                    Items = new ObservableCollection<VisualGenericItem>(genericConferences),
                    IsFullyLoaded = true
                });
            }

            IsLoading = false;
        }

        /// <summary>
        /// Search in shows
        /// </summary>
        private async Task SearchSalonsAsync()
        {
            IsLoading = true;

            IList<Salon> salons = await _modelSalons.SearchAsync(Keywords);

            if (salons != null && salons.Count > 0)
            {
                IMapper<Salon> mapper = new GenericSalonMapper();
                IList<VisualGenericItem> genericSalon = mapper.Map(salons);

                Results.Groups.Insert(Results.Groups.Count, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_SALONS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericSalon),
                    IsFullyLoaded = true
                });
            }

            IsLoading = false;
        }

        /// <summary>
        /// Informs the view the user wants to show selected item's details
        /// </summary>
        /// <param name="item"></param>
        private void GoToDetailsPage(VisualGenericItem item)
        {
            Messenger.Default.Send(item);
        }

        /// <summary>
        /// Informs the view the user wants to pin the selected item
        /// </summary>
        /// <param name="element">Selected item converted</param>
        private void Pin(PinnableObject element)
        {
            Messenger.Default.Send(element);
        }

        #endregion
    }
}