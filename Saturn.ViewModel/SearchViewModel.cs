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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EPSILab.SolarSystem.Saturn.ViewModel
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
        /// <param name="modelShows">Model for shows. Resolved by NInject</param>
        public SearchViewModel(ISearchable<News> modelNews, ISearchable<Conference> modelConferences, ISearchable<Show> modelShows)
        {
            _modelNews = modelNews;
            _modelConferences = modelConferences;
            _modelShows = modelShows;

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
        private readonly ISearchable<Show> _modelShows;

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
            await SearchShowsAsync();
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
        private async Task SearchShowsAsync()
        {
            IsLoading = true;

            IList<Show> salons = await _modelShows.SearchAsync(Keywords);

            if (salons != null && salons.Count > 0)
            {
                IMapper<Show> mapper = new GenericShowMapper();
                IList<VisualGenericItem> genericShow = mapper.Map(salons);

                Results.Groups.Insert(Results.Groups.Count, new VisualGenericGroup
                {
                    Title = AppResourcesHelper.GetString("LBL_SALONS"),
                    Items = new ObservableCollection<VisualGenericItem>(genericShow),
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