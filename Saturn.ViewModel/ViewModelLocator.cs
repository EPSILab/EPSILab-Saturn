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

                return ServiceLocator.Current.GetInstance<IMainViewModel>();
            }
        }

        /// <summary>
        /// Returns the one conference view-model
        /// </summary>
        public static IDetailsViewModel<Conference> ConferenceDetailsVM
        {
            get { return GiveDetailViewModel<Conference>(); }
        }

        /// <summary>
        /// Returns the conferences list view-model
        /// </summary>
        public static IMasterViewModel<Conference> ConferencesVM
        {
            get { return GiveMasterViewModel<Conference>(); }
        }

        /// <summary>
        /// Returns the one news view-model
        /// </summary>
        public static IDetailsViewModel<News> NewsDetailsVM
        {
            get { return GiveDetailViewModel<News>(); }
        }

        /// <summary>
        /// Returns the news list view-model
        /// </summary>
        public static IMasterViewModel<News> NewsVM
        {
            get { return GiveMasterViewModel<News>(); }
        }

        /// <summary>
        /// Returns the one member view-model
        /// </summary>
        public static IDetailsViewModel<Membre> MembreDetailsVM
        {
            get { return GiveDetailViewModel<Membre>(); }
        }

        /// <summary>
        /// Returns the members list view-model
        /// </summary>
        public static IMasterViewModel<Membre> MembresVM
        {
            get { return GiveMasterViewModel<Membre>(); }
        }

        /// <summary>
        /// Returns the one project view-model
        /// </summary>
        public static IDetailsViewModel<Projet> ProjetDetailsVM
        {
            get { return GiveDetailViewModel<Projet>(); }
        }

        /// <summary>
        /// Returns the projects list view-model
        /// </summary>
        public static IMasterViewModel<Projet> ProjetsVM
        {
            get { return GiveMasterViewModel<Projet>(); }
        }

        /// <summary>
        /// Returns the one show view-model
        /// </summary>
        public static IDetailsViewModel<Salon> SalonDetailsVM
        {
            get { return GiveDetailViewModel<Salon>(); }
        }

        /// <summary>
        /// Returns the shows list view-model
        /// </summary>
        public static IMasterViewModel<Salon> SalonsVM
        {
            get { return GiveMasterViewModel<Salon>(); }
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

                return ServiceLocator.Current.GetInstance<ISearchViewModel>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a master view-model
        /// </summary>
        /// <typeparam name="T">Entity from the model</typeparam>
        /// <returns>The associate view-model</returns>
        private static IMasterViewModel<T> GiveMasterViewModel<T>()
        {
            if (!SimpleIoc.Default.IsRegistered<IMasterViewModel<T>>())
            {
                SimpleIoc.Default.Register<IMasterViewModel<T>, MasterViewModel<T>>();
            }

            return ServiceLocator.Current.GetInstance<IMasterViewModel<T>>();
        }

        /// <summary>
        /// Returns a details view-model
        /// </summary>
        /// <typeparam name="T">Entity from the model</typeparam>
        /// <returns>The associate view-model</returns>
        private static IDetailsViewModel<T> GiveDetailViewModel<T>()
        {
            if (!SimpleIoc.Default.IsRegistered<IDetailsViewModel<T>>())
            {
                SimpleIoc.Default.Register<IDetailsViewModel<T>, DetailsViewModel<T>>();
            }

            return ServiceLocator.Current.GetInstance<IDetailsViewModel<T>>();
        }

        /// <summary>
        /// Clean the main view-model
        /// </summary>
        public static void CleanMainVM()
        {
            if (SimpleIoc.Default.IsRegistered<IMainViewModel>())
            {
                SimpleIoc.Default.GetInstance<IMainViewModel>().Cleanup();
                SimpleIoc.Default.Unregister<IMainViewModel>();
            }
        }

        /// <summary>
        /// Clean a master view-model from MVVM Light Toolkit Messenger and/or from memory
        /// </summary>
        /// <typeparam name="T">Entity from the model</typeparam>
        /// <param name="unregister">Clean the viewmodel from the memory</param>
        public static void CleanMasterVM<T>(bool unregister)
        {
            if (SimpleIoc.Default.IsRegistered<IMasterViewModel<T>>())
            {
                SimpleIoc.Default.GetInstance<IMasterViewModel<T>>().Cleanup();

                if (unregister)
                    SimpleIoc.Default.Unregister<IMasterViewModel<T>>();
            }
        }

        /// <summary>
        /// Clean a details view-model from MVVM Light Toolkit Messenger and/or from memory
        /// </summary>
        /// <typeparam name="T">Entity from the model</typeparam>
        /// <param name="unregister">Clean the viewmodel from the memory</param>
        public static void CleanDetailsVM<T>(bool unregister)
        {
            if (SimpleIoc.Default.IsRegistered<IDetailsViewModel<T>>())
            {
                SimpleIoc.Default.GetInstance<IDetailsViewModel<T>>().Cleanup();

                if (unregister)
                    SimpleIoc.Default.Unregister<IDetailsViewModel<T>>();
            }
        }

        /// <summary>
        /// Clean the Search view-model from MVVM Light Toolkit Messenger and/or from memory
        /// </summary>
        /// <param name="unregister">Clean the viewmodel from the memory</param>
        public static void CleanSearchVM(bool unregister)
        {
            if (SimpleIoc.Default.IsRegistered<ISearchViewModel>())
            {
                SimpleIoc.Default.GetInstance<ISearchViewModel>().Cleanup();

                if (unregister)
                    SimpleIoc.Default.Unregister<ISearchViewModel>();
            }
        }

        #endregion
    }
}