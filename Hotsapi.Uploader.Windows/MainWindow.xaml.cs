﻿using Squirrel;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Hotsapi.Uploader.Windows
{
    public partial class MainWindow : Window
    {
        public App App { get { return Application.Current as App; } }
        private bool ShutdownOnClose = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (App.Settings.MinimizeToTray && WindowState == WindowState.Minimized) {
                App.TrayIcon.Visible = true;
                ShutdownOnClose = false;
                Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ShutdownOnClose) {
                App.Shutdown();
            }
        }

        private void ShowLog_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", $@"{App.AppDir}\..\logs");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow() { Owner = this, DataContext = this }.ShowDialog();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            UpdateManager.RestartApp();
        }
    }
}
