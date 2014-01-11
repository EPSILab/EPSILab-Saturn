using NotificationsExtensions.ToastContent;
using SolarSystem.Saturn.Win8.Common;
using SolarSystem.Saturn.Win8.Resources;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
using Windows.Networking.Connectivity;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SolarSystem.Saturn.Win8
{
    /// <summary>
    ///     Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {
        /// <summary>
        ///     Initializes the singleton Application object.  This is the first line of authored code
        ///     executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
            UnhandledException += App_OnUnhandledException;
        }

        private static bool _showNetworkProblemNotification = true;

        /// <summary>
        ///     Determines if network is available
        /// </summary>
        public static bool IsInternetAvailable
        {
            get
            {
                ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();

                // Network available
                if (NetworkInterface.GetIsNetworkAvailable() &&
                    profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                {
                    _showNetworkProblemNotification = true;
                    return true;
                }

                if (_showNetworkProblemNotification)
                {
                    // Network not available
                    IToastText02 templateContent = ToastContentFactory.CreateToastText02();
                    templateContent.TextHeading.Text = MessagesRsxAccessor.GetString("NoConnectionTextHeader");
                    templateContent.TextBodyWrap.Text = MessagesRsxAccessor.GetString("NoConnectionTextBody");

                    ToastNotification toast = templateContent.CreateNotification();
                    ToastNotificationManager.CreateToastNotifier().Show(toast);
                }

                _showNetworkProblemNotification = false;
                return false;
            }
        }

        /// <summary>
        ///     Main application entry point.
        ///     Fire when user launch application from the application icon or from a tile
        /// </summary>
        /// <param name="args">Give details about launch.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                Type page = typeof(MainPage);
                int? id = null;

                // Check if arguments are availables
                if (!string.IsNullOrWhiteSpace(args.Arguments))
                {
                    string arguments = args.Arguments;

                    IDictionary<string, Func<Type>> pages = new Dictionary<string, Func<Type>>
                        {
                            { "News", () => typeof(NewsDetailsPage) },
                            { "Conference", () => typeof(ConferenceDetailsPage) },
                            { "Salon", () => typeof(SalonDetailsPage) },
                        };

                    // Get the page to load
                    string pagename = arguments.Substring(arguments.IndexOf('-') + 1, arguments.LastIndexOf('-'));
                    page = pages[pagename]();

                    // Get the element's ID located after the '-'
                    id = int.Parse(arguments.Substring(arguments.IndexOf('-') + 1));
                }

                SettingsPane.GetForCurrentView().CommandsRequested += OnSettingPane_Opening;

                SearchPane.GetForCurrentView().ShowOnKeyboardInput = true;
                SearchPane.GetForCurrentView().PlaceholderText = MessagesRsxAccessor.GetString("Search");

                if (!rootFrame.Navigate(page, id))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            Window.Current.Activate();
        }

        private void OnSettingPane_Opening(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand command = new SettingsCommand("privacy", MessagesRsxAccessor.GetString("PrivacyStatement"), OnSettingsCommand_Click);
            args.Request.ApplicationCommands.Add(command);
        }

        private async void OnSettingsCommand_Click(IUICommand command)
        {
            await Launcher.LaunchUriAsync(new Uri(ResourcesRsxAccessor.GetString("PrivacyStatement_URL")));
        }

        /// <summary>
        ///     Invoked when application execution is being suspended.  Application state is saved
        ///     without knowing whether the application will be terminated or resumed with the contents
        ///     of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        /// <summary>
        ///     Enable search contract
        /// </summary>
        /// <param name="args">Contains search query</param>
        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            base.OnSearchActivated(args);

            SettingsPane.GetForCurrentView().CommandsRequested += OnSettingPane_Opening;

            Frame frame = CreateOrGetCurrentFrame();
            frame.Navigate(typeof(SearchPage), args.QueryText);
        }

        private Frame CreateOrGetCurrentFrame()
        {
            Frame frame;
            UIElement content = Window.Current.Content;

            if (content is Frame)
            {
                frame = content as Frame;
            }
            else
            {
                frame = new Frame();

                Window.Current.Content = frame;
                Window.Current.Activate();
            }

            return frame;
        }

        /// <summary>
        /// Fired when an unhandled exception occured
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void App_OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageDialog messageDialog = new MessageDialog(MessagesRsxAccessor.GetString("Error"));
            await messageDialog.ShowAsync();

            e.Handled = false;
        }
    }
}