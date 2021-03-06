﻿using PIBand.Common;
using PIBand.Models;
using PIBand.Controllers;
using DataOperations;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Devices.Sensors;
using Windows.UI.Core;

using Windows.Devices.Geolocation;

// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace PIBand
{
    public sealed partial class PivotPage : Page
    {
        private const string FirstGroupName = "FirstGroup";
        private const string SecondGroupName = "SecondGroup";
        private const string SettingsGroupName = "Settings";

        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        private DataClient _viewDataSubscription;
        private PhoneDataController _phoneDataController;

        private DispatcherTimer _timer;
        private DispatcherTimer _checkStatusTimer;
        private Stopwatch _sw;

        private bool _isUserSet;

        public PivotPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>.
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            //var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-1");
            //this.DefaultViewModel[FirstGroupName] = sampleDataGroup;

            

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache. Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/>.</param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
            
        }

        /// <summary>
        /// Adds an item to the list when the app bar button is clicked.
        /// </summary>
        //private void EditAppBarButton_Click(object sender, RoutedEventArgs e)
        //{

            //EnableTextBoxes(true);

            //EditAppBarButton.Visibility = Visibility.Collapsed;
            //SaveAppBarButton.Visibility = Visibility.Visible;
            //BackAppBarButton.Visibility = Visibility.Visible;

            //Frame.Navigate(typeof(EditPage));

            //string groupName = this.pivot.SelectedIndex == 0 ? FirstGroupName : SecondGroupName;
            //var group = this.DefaultViewModel[groupName] as SampleDataGroup;
            //var nextItemId = group.Items.Count + 1;
            //var newItem = new SampleDataItem(
            //    string.Format(CultureInfo.InvariantCulture, "Group-{0}-Item-{1}", this.pivot.SelectedIndex + 1, nextItemId),
            //    string.Format(CultureInfo.CurrentCulture, this.resourceLoader.GetString("NewItemTitle"), nextItemId),
            //    string.Empty,
            //    string.Empty,
            //    this.resourceLoader.GetString("NewItemDescription"),
            //    string.Empty);

            //group.Items.Add(newItem);

            //// Scroll the new item into view.
            //var container = this.pivot.ContainerFromIndex(this.pivot.SelectedIndex) as ContentControl;
            //var listView = container.ContentTemplateRoot as ListView;
            //listView.ScrollIntoView(newItem, ScrollIntoViewAlignment.Leading);
        //}

        //private void EnableTextBoxes(bool isEnable)
        //{
        //    tbUsername.IsEnabled = isEnable;
        //    tbPassword.IsEnabled = isEnable;
        //    tbPIWebAPIServer.IsEnabled = isEnable;
        //    tbAFServer.IsEnabled = isEnable;
        //    tbPIServer.IsEnabled = isEnable;
        //}

        //private void SaveAppBarButton_Click(object sender, RoutedEventArgs e)
        //{
            //AppSettings userSettings = AppSettingsManager.Instance.LocalSettings.Containers["Users"];

            //AppSettings userSettings = localSettings.Settings[tbUsername.Text];

            //Save settings
            //userSettings.AddOrUpdateValue("Username", tbUsername.Text);
            //userSettings.AddOrUpdateValue("Password", tbPassword.Text);
            //userSettings.AddOrUpdateValue("PI Web API Server", tbPIWebAPIServer.Text);
            //userSettings.AddOrUpdateValue("AF Server", tbAFServer.Text);
            //userSettings.AddOrUpdateValue("PI Server", tbPIWebAPIServer.Text);

            //this.DefaultViewModel[SettingsGroupName] = AppSettings.GetUserSettings();

            //Update UI
            //EnableTextBoxes(false);
            //SaveAppBarButton.Visibility = Visibility.Collapsed;
            //BackAppBarButton.Visibility = Visibility.Collapsed;
            //EditAppBarButton.Visibility = Visibility.Visible;

            //AppBarButton editButton = new AppBarButton();
            //editButton.Icon = new SymbolIcon(Symbol.Edit);
            //editButton.Label = "edit";
            //editButton.Click += SaveAppBarButton_Click;

            //bottomAppBar.PrimaryCommands.RemoveAt(0);
            //bottomAppBar.PrimaryCommands.Insert(0, editButton);
        //}

        //private void BackAppBarButton_Click(object sender, RoutedEventArgs e)
        //{
            //this.DefaultViewModel[SettingsGroupName] = AppSettings.GetUserSettings();

            //Update UI
            //EnableTextBoxes(false);
            //SaveAppBarButton.Visibility = Visibility.Collapsed;
            //BackAppBarButton.Visibility = Visibility.Collapsed;
            //EditAppBarButton.Visibility = Visibility.Visible;
        //}

        /// <summary>
        /// Invoked when an item within a section is clicked.
        /// </summary>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(ItemPage), itemId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        /// <summary>
        /// Loads the content for the second pivot item when it is scrolled into view.
        /// </summary>
        private async void SecondPivot_Loaded(object sender, RoutedEventArgs e)
        {
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-2");
            this.DefaultViewModel[SecondGroupName] = sampleDataGroup;
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void pivot_PivotItemLoaded(Pivot sender, PivotItemEventArgs args)
        {

            _isUserSet = AppSettingsManager.Instance.LocalSettings.Containers["UserSettings"].ContainsKey("Username");
            //if (args.Item.Name == "Settings")
            //{
            //string currentUser = AppSettingsManager.Instance.LocalSettings.Containers["Global"].Values["LastUser"] as string;
            //AppSettings curentUserSettings = AppSettingsManager.Instance.LocalSettings.Containers["Users"].Values[currentUser] as AppSettings;

            //if (curentUserSettings == null)
            //{
            //    this.DefaultViewModel[SettingsGroupName] = null;

            //}

            this.DefaultViewModel[SettingsGroupName] = null;

                bottomAppBar.Visibility = Visibility.Visible;

                //Update UI
                //EnableTextBoxes(false);
                //SaveAppBarButton.Visibility = Visibility.Collapsed;
                //BackAppBarButton.Visibility = Visibility.Collapsed;
                //EditAppBarButton.Visibility = Visibility.Visible;

                //AppBarButton editButton = new AppBarButton();
                //editButton.Icon = new SymbolIcon(Symbol.Edit);
                //editButton.Label = "Edit";
                //editButton.Click += EditAppBarButton_Click;
                //bottomAppBar.PrimaryCommands.Insert(0, editButton);
            //}
            if (args.Item.Name == "Data")
            {
                btnStop.IsEnabled = false;
                bottomAppBar.Visibility = Visibility.Visible;
           
                if (_isUserSet)
                {
                    btnStart.IsEnabled = true;
                }
                else
                {
                    btnStart.IsEnabled = false;
                    tbStatus.Text = "Please add a user.";
                }              
            }
            else
            {
                bottomAppBar.Visibility = Visibility.Visible;

                //CommandBar bottomCommandBar = this.BottomAppBar as CommandBar;
                //if (bottomCommandBar != null)
                //{
                //    AppBarButton b = bottomCommandBar.PrimaryCommands[0] as AppBarButton;
                //    if (b == null) return;
                //    // Unregister event handler.
                //    b.Click -= EditAppBarButton_Click;
                //    // Remove edit button.
                //    bottomCommandBar.PrimaryCommands.RemoveAt(0);
                //}
            }
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            SettingsAppBarButton.IsEnabled = false;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;

            _sw = new Stopwatch();

            _sw.Start();
            _timer.Start();

            //_viewDataSubscription = new DataClient();
            //_viewDataSubscription.SubscribeToAccelerometer(OnAccelerometerReading);
            //_viewDataSubscription.SubscribeToGeolocation(OnGeopositionReading);

            AFMetaDataController afMetaDataController = new AFMetaDataController();
            await afMetaDataController.BuildUserAssets();
            afMetaDataController.Close();

            _phoneDataController = new PhoneDataController();
            await _phoneDataController.InitializeAsync();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void _timer_Tick(object sender, object e)
        {
            tbTimer.Text = _sw.Elapsed.ToString(@"hh\:mm\:ss");
        }

        private async void OnGeopositionReading(PositionChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Geoposition geo = args.Position;
                double reading = geo.Coordinate.Timestamp.Second;
                //tbGeo.Text = String.Format("{0,5:0.00}", reading);
            });

            //await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            //{
            //    Geoposition geo = await geoTask;
            //    double reading = geo.Coordinate.Timestamp.Second;
            //    tbGeo.Text = String.Format("{0,5:0.00}", reading);
            //});

        }

        private async void OnAccelerometerReading(AccelerometerReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AccelerometerReading reading = args.Reading;
                //tbAccelX.Text = String.Format("{0,5:0.00}", reading.AccelerationX);
            });

        }

        private async void OnReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AccelerometerReading reading = args.Reading;
                //tbAccelX.Text = String.Format("{0,5:0.00}", reading.AccelerationX);
            });

        }

        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            await _phoneDataController.Close();

            _timer.Stop();
            _sw.Reset();

            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;

            if (_viewDataSubscription != null) _viewDataSubscription.RemoveSubscription();

            SettingsAppBarButton.IsEnabled = true;
        }

        private void SettingsAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isUserSet)
            {
                if (!Frame.Navigate(typeof(SettingsPage)))
                {
                    throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
                }
            }
            else
            {
                if (!Frame.Navigate(typeof(ChangeUserPage)))
                {
                    throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
                }
            }


        }
    }
}
