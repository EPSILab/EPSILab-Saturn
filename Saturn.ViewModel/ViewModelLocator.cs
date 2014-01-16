using Autofac;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SolarSystem.Saturn.Model.Infrastructure;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.ViewModel.DesignViewModel;
using SolarSystem.Saturn.ViewModel.Interfaces;

namespace SolarSystem.Saturn.ViewModel
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
                SimpleIoc.Default.Register<IDetailsViewModel<Membre>, MembreDesignViewModel>();
                SimpleIoc.Default.Register<IDetailsViewModel<News>, NewsDesignViewModel>();
                SimpleIoc.Default.Register<IDetailsViewModel<Projet>, ProjetDesignViewModel>();
                SimpleIoc.Default.Register<IDetailsViewModel<Salon>, SalonDesignViewModel>();
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
        public static IDetailsViewModel<Membre> MembreDetailsVM
        {
            get { return GetDetailViewModel<Membre>(); }
        }

        /// <summary>
        /// Returns the members list view-model
        /// </summary>
        public static IMasterViewModel<Membre> MembresVM
        {
            get { return GetMasterViewModel<Membre>(); }
        }

        /// <summary>
        /// Returns the one project view-model
        /// </summary>
        public static IDetailsViewModel<Projet> ProjetDetailsVM
        {
            get { return GetDetailViewModel<Projet>(); }
        }

        /// <summary>
        /// Returns the projects list view-model
        /// </summary>
        public static IMasterViewModel<Projet> ProjetsVM
        {
            get { return GetMasterViewModel<Projet>(); }
        }

        /// <summary>
        /// Returns the one show view-model
        /// </summary>
        public static IDetailsViewModel<Salon> SalonDetailsVM
        {
            get { return GetDetailViewModel<Salon>(); }
        }

        /// <summary>
        /// Returns the shows list view-model
        /// </summary>
        public static IMasterViewModel<Salon> SalonsVM
        {
            get { return GetMasterViewModel<Salon>(); }
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