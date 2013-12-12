using SolarSystem.Saturn.ViewModel.Helpers;
using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.ObjectModel;

namespace SolarSystem.Saturn.ViewModel.DesignViewModel
{
    class MainDesignViewModel : MainViewModel
    {
        public MainDesignViewModel()
        {
            Menu = new VisualMenu
            {
                Groups = new ObservableCollection<VisualGenericGroup>
                        {
                            new VisualGenericGroup
                                {
                                    Title = AppResourcesHelper.GetString("LBL_NEWS")
                                },
                            new VisualGenericGroup
                                {
                                    Title = AppResourcesHelper.GetString("LBL_BUREAU")
                                },
                            new VisualGenericGroup
                                {
                                    Title = AppResourcesHelper.GetString("LBL_PROJECTS")
                                },
                            new VisualGenericGroup
                                {
                                    Title = AppResourcesHelper.GetString("LBL_CONFERENCES")
                                },
                            new VisualGenericGroup
                                {
                                    Title = AppResourcesHelper.GetString("LBL_SALONS")
                                }
                        }
            };

            SelectedItem = Menu.Groups[0].Items[0];
        }
    }
}
