using Microsoft.UI.Xaml;
using System;

namespace GameLocker
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Create a new instance of your main window.
            MainWindow mainWindow = new MainWindow();

            // Make the window visible.
            mainWindow.Activate();
        }




    }
}
