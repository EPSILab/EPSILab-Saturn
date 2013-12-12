﻿using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.WP8.Helpers.BackgroundTask;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SolarSystem.Saturn.WP8
{
    public partial class MainPage
    {
        #region Fields

        private VisualGenericGroup _selectedPanoramaGroup;

        #endregion

        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<object>(this, GoToAboutPage);
            Messenger.Default.Register<VisualGenericItem>(this, ShowDetailsPage);
        }

        #region Methods

        private void ShowDetailsPage(VisualGenericItem item)
        {
            if (item != null)
            {
                if (App.IsInternetAvailable)
                {
                    string url = string.Format("/{0}Page.xaml?Id={1}", item.Type, item.Id);
                    Uri uri = new Uri(url, UriKind.Relative);
                    NavigationService.Navigate(uri);
                }
                else
                {
                    MessageBox.Show("Veuillez vérifier votre connexion à Internet");
                }
            }
        }

        private void GoToAboutPage(object element)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        #endregion

        #region Events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (_selectedPanoramaGroup != null)
            {
                int index = Panorama.Items.Cast<VisualGenericGroup>().TakeWhile(item => item != _selectedPanoramaGroup).Count();
                Panorama.DefaultItem = Panorama.Items[index];
            }

            if (e.NavigationMode == NavigationMode.New)
            {
                BackgroundTaskRegistrationHelper.Register();

                IMainViewModel viewModel = (IMainViewModel)DataContext;

                if (viewModel.LoadMenuCommand.CanExecute(this))
                {
                    viewModel.LoadMenuCommand.Execute(this);
                }
            }
        }

        private void LlsMenu_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Panorama.SelectedItem is PanoramaItem)
            {
                _selectedPanoramaGroup = (VisualGenericGroup)(Panorama.SelectedItem as PanoramaItem).Content;
            }
        }

        #endregion
    }
}