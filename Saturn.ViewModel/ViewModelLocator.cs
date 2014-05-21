using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Infrastructure;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.DesignViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace EPSILab.SolarSystem.Saturn.ViewModel
{
    public class ViewModelLocator
    {
        #region Static constructor

        /// <summary>
        /// Static constructor. Register all design view-models
        /// </summary>
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register design viewmodels
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IMainViewModel, MainDesignViewModel>();

                SimpleIoc.Default.Register<IDetailsViewModel<Conference>, ConferenceDesignViewModel>();
                SimpleIoc.Default.Register<IDetailsViewModel<Member>, MemberDesignViewModel>();
                SimpleIoc.Default.Register<IDetailsViewModel<News>, NewsDesignViewModel>();
                SimpleIoc.Default.Register<IDetailsViewModel<Project>, ProjectDesignViewModel>();
                SimpleIoc.Default.Register<IDetailsViewModel<Show>, ShowDesignViewModel>();
            }

            // Register models
            ModelBuilder builder = new ModelBuilder();
            Container = builder.Build();
        }

        #endregion

        #region Properties

        /// <summary>
        /// IoC Resolver
        /// </summary>
        public static IContainer Container { get; private set; }

        /// <summary>
        /// Returns the main view-model
        /// </summary>
        public static IMainViewModel MainVM
        {
            get
            {
                if (!SimpleIoc.Default.IsRegistered<IMainViewModel>())
                {
                    SimpleIoc.Default.Register<IMainViewModel, MainViewModel>();
                }

                return SimpleIoc.Default.GetInstance<IMainViewModel>();
            }
        }

        /// <summary>
        /// Returns the one conference view-model
        /// </summary>
        public static IDetailsViewModel<Conference> ConferenceDetailsVM
        {
            get { return GetDetailViewModel<Conference>(); }
        }

        /// <summary>
        /// Returns the conferences list view-model
        /// </summary>
        public static IMasterViewModel<Conference> ConferencesVM
        {
            get { return GetMasterViewModel<Conference>(); }
        }

        /// <summary>
        /// Returns the one news view-model
        /// </summary>
        public static IDetailsViewModel<News> NewsDetailsVM
        {
            get { return GetDetailViewModel<News>(); }
        }

        /// <summary>
        /// Returns the news list view-model
        /// </summary>
        public static IMasterViewModel<News> NewsVM
        {
            get { return GetMasterViewModel<News>(); }
        }

        /// <summary>
        /// Returns the one member view-model
        /// </summary>
        public static IDetailsViewModel<Member> MemberDetailsVM
        {
            get { return GetDetailViewModel<Member>(); }
        }

        /// <summary>
        /// Returns the members list view-model
        /// </summary>
        public static IMasterViewModel<Member> MembersVM
        {
            get { return GetMasterViewModel<Member>(); }
        }

        /// <summary>
        /// Returns the one project view-model
        /// </summary>
        public static IDetailsViewModel<Project> ProjectDetailsVM
        {
            get { return GetDetailViewModel<Project>(); }
        }

        /// <summary>
        /// Returns the projects list view-model
        /// </summary>
        public static IMasterViewModel<Project> ProjectsVM
        {
            get { return GetMasterViewModel<Project>(); }
        }

        /// <summary>
        /// Returns the one show view-model
        /// </summary>
        public static IDetailsViewModel<Show> ShowDetailsVM
        {
            get { return GetDetailViewModel<Show>(); }
        }

        /// <summary>
        /// Returns the shows list view-model
        /// </summary>
        public static IMasterViewModel<Show> ShowsVM
        {
            get { return GetMasterViewModel<Show>(); }
        }

        /// <summary>
        /// Return the search view-model
        /// </summary>
        public static ISearchViewModel SearchVM
        {
            get
            {
                if (!SimpleIoc.Default.IsRegistered<ISearchViewModel>())
                {
                    SimpleIoc.Default.Register<ISearchViewModel, SearchViewModel>();
                }

                return SimpleIoc.Default.GetInstance<ISearchViewModel>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a master view-model
        /// </summary>
        /// <typeparam name="T">Entity from the model</typeparam>
        /// <returns>The associate view-model</returns>
        private static IMasterViewModel<T> GetMasterViewModel<T>()
        {
            if (!SimpleIoc.Default.IsRegistered<IMasterViewModel<T>>())
            {
                SimpleIoc.Default.Register<IMasterViewModel<T>, MasterViewModel<T>>();
            }

            return SimpleIoc.Default.GetInstance<IMasterViewModel<T>>();
        }

        /// <summary>
        /// Returns a details view-model
        /// </summary>
        /// <typeparam name="T">Entity from the model</typeparam>
        /// <returns>The associate view-model</returns>
        private static IDetailsViewModel<T> GetDetailViewModel<T>()
        {
            if (!SimpleIoc.Default.IsRegistered<IDetailsViewModel<T>>())
            {
                SimpleIoc.Default.Register<IDetailsViewModel<T>, DetailsViewModel<T>>();
            }

            return SimpleIoc.Default.GetInstance<IDetailsViewModel<T>>();
        }

        /// <summary>
        /// Clean a master view-model from MVVM Light Messenger
        /// </summary>
        /// <typeparam name="T">Model entity</typeparam>
        public static void CleanMasterVM<T>()
        {
            if (SimpleIoc.Default.IsRegistered<IMasterViewModel<T>>())
            {
                SimpleIoc.Default.GetInstance<IMasterViewModel<T>>().Cleanup();
            }
        }

        /// <summary>
        /// Clean a details view-model from MVVM Light Messenger
        /// </summary>
        /// <typeparam name="T">Model entity</typeparam>
        public static void CleanDetailsVM<T>()
        {
            if (SimpleIoc.Default.IsRegistered<IDetailsViewModel<T>>())
            {
                SimpleIoc.Default.GetInstance<IDetailsViewModel<T>>().Cleanup();
            }
        }

        /// <summary>
        /// Clean the Search view-model from MVVM Light Messenger
        /// </summary>
        public static void CleanSearchVM()
        {
            if (SimpleIoc.Default.IsRegistered<ISearchViewModel>())
            {
                SimpleIoc.Default.GetInstance<ISearchViewModel>().Cleanup();
            }
        }

        /// <summary>
        /// Clean the main view-model
        /// </summary>
        public static void DisposeMainVM()
        {
            if (SimpleIoc.Default.IsRegistered<IMainViewModel>())
            {
                SimpleIoc.Default.GetInstance<IMainViewModel>().Cleanup();
                SimpleIoc.Default.Unregister<IMainViewModel>();
            }
        }

        /// <summary>
        /// Dispose a master view-model
        /// </summary>
        /// <typeparam name="T">Model entity</typeparam>
        public static void DisposeMasterVM<T>()
        {
            if (SimpleIoc.Default.IsRegistered<IMasterViewModel<T>>())
            {
                CleanMasterVM<T>();
                SimpleIoc.Default.Unregister<IMasterViewModel<T>>();
            }
        }

        /// <summary>
        /// Dispose a details view-model
        /// </summary>
        /// <typeparam name="T">Entity from the model</typeparam>
        public static void DisposeDetailsVM<T>()
        {
            if (SimpleIoc.Default.IsRegistered<IDetailsViewModel<T>>())
            {
                CleanDetailsVM<T>();
                SimpleIoc.Default.Unregister<IDetailsViewModel<T>>();
            }
        }

        /// <summary>
        /// Dispose the search view-model
        /// </summary>
        public static void DisposeSearchVM()
        {
            if (SimpleIoc.Default.IsRegistered<ISearchViewModel>())
            {
                SimpleIoc.Default.Unregister<ISearchViewModel>();
            }
        }

        #endregion
    }
}