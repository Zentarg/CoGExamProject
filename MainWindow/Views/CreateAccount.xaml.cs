﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MainWindow;
using MainWindow.Models;
using MainWindow.ViewModels;
using System.ServiceModel.Channels;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MainWindow.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateAccount : Page
    {


        public CreateAccount()
        {
            this.InitializeComponent();
        }


        private void UserNameCheck(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text != "")
            {
                if (AccountHandler.UserNameCheck((sender as TextBox).Text) != 0)
                {
                    FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
                }

            }
        }
        //public event RoutedEventHandler LostFocus;
    }
}
