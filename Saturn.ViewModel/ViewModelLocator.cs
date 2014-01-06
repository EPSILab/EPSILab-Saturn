using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.ViewModel.DesignViewModel;
using SolarSystem.Saturn.ViewModel.Interfaces;

namespace SolarSystem.Saturn.ViewModel
{
    public class ViewModelLocator
    {
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
        }

        #region Properties

        public IMainViewModel MainVMNotStatic
        {
            get { return MainVM; }
        }

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

        public static IDetailsViewModel<Conference> ConferenceDetailsVM
        {
            get { return GiveDetailViewModel<Conference>(); }
        }

        public static IMasterViewModel<Conference> ConferencesVM
        {
            get { return GiveMasterViewModel<Conference>(); }
        }

        public static IDetailsViewModel<News> NewsDetailsVM
        {
            get { return GiveDetailViewModel<News>(); }
        }

        public static IMasterViewModel<News> NewsVM
        {
            get { return GiveMasterViewModel<News>(); }
        }

        public static IDetailsViewModel<Membre> MembreDetailsVM
        {
            get { return GiveDetailViewModel<Membre>(); }
        }

        public static IMasterViewModel<Membre> MembresVM
        {
            get { return GiveMasterViewModel<Membre>(); }
        }

        public static IDetailsViewModel<Projet> ProjetDetailsVM
        {
            get { return GiveDetailViewModel<Projet>(); }
        }

        public static IMasterViewModel<Projet> ProjetsVM
        {
            get { return GiveMasterViewModel<Projet>(); }
        }

        public static IDetailsViewModel<Salon> SalonDetailsVM
        {
            get { return GiveDetailViewModel<Salon>(); }
        }

        public static IMasterViewModel<Salon> SalonsVM
        {
            get { return GiveMasterViewModel<Salon>(); }
        }

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

        private static IMasterViewModel<T> GiveMasterViewModel<T>()
        {
            if (!SimpleIoc.Default.IsRegistered<IMasterViewModel<T>>())
            {
                SimpleIoc.Default.Register<IMasterViewModel<T>, MasterViewModel<T>>();
            }

            return ServiceLocator.Current.GetInstance<IMasterViewModel<T>>();
        }

        private static IDetailsViewModel<T> GiveDetailViewModel<T>()
        {
            if (!SimpleIoc.Default.IsRegistered<IDetailsViewModel<T>>())
            {
                SimpleIoc.Default.Register<IDetailsViewModel<T>, DetailsViewModel<T>>();
            }

            return ServiceLocator.Current.GetInstance<IDetailsViewModel<T>>();
        }

        #endregion

        #region Clean methods

        public static void CleanMainVM(bool unregister)
        {
            if (SimpleIoc.Default.IsRegistered<IMainViewModel>())
            {
                SimpleIoc.Default.GetInstance<IMainViewModel>().Cleanup();
                SimpleIoc.Default.Unregister<IMainViewModel>();
            }
        }

        public static void CleanDetailsVM<T>(bool unregister)
        {
            if (SimpleIoc.Default.IsRegistered<IDetailsViewModel<T>>())
            {
                SimpleIoc.Default.GetInstance<IDetailsViewModel<T>>().Cleanup();

                if (unregister)
                    SimpleIoc.Default.Unregister<IDetailsViewModel<T>>();
            }
        }

        public static void CleanMasterVM<T>(bool unregister)
        {
            if (SimpleIoc.Default.IsRegistered<IMasterViewModel<T>>())
            {
                SimpleIoc.Default.GetInstance<IMasterViewModel<T>>().Cleanup();

                if (unregister)
                    SimpleIoc.Default.Unregister<IMasterViewModel<T>>();
            }
        }

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