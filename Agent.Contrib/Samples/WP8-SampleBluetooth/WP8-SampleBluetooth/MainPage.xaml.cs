using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP8_SampleBluetooth.Bluetooth;
using WP8_SampleBluetooth.Resources;

namespace WP8_SampleBluetooth
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private Connection _connection;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _connection = new Connection("ROB17R", false);
            _connection.OnReceived += _connection_OnReceived;
            _connection.Open();
        }

        void _connection_OnReceived(object Data, Windows.Networking.Sockets.StreamSocket socket, System.ServiceModel.Channels.IChannel channel, DateTime Timestamp)
        {
            ResponseTextBox.Text = (Data as string);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_connection.IsOpen)
            {
                _connection.SendData(InputTextBox.Text);
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}