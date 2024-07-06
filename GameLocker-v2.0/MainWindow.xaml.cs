using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
// Explicitly specify which AppWindow to use
using AppWindow = Microsoft.UI.Windowing.AppWindow;
using WinRT.Interop;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.ApplicationModel;
using Windows.UI;

namespace GameLocker
{
    public sealed partial class MainWindow : Window
    {
        private const string CorrectPassword = "animeisbad";
        private readonly AppWindow appWindow;

        public MainWindow()
        {
            this.InitializeComponent();

            // Set window size
            var windowHandle = WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            // Correctly access the static method
            appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32(500, 1000));

            
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(AppTitleBar);



        }



        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == CorrectPassword)
            {
                GameList.Visibility = Visibility.Visible;
                LaunchButton.Visibility = Visibility.Visible;
                MessageText.Text = "";
            }
            else
            {
                MessageText.Text = "Incorrect password";
            }
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameList.SelectedItem != null)
            {
                string gamePath = "";
                switch (((ListViewItem)GameList.SelectedItem).Content.ToString())
                {
                    case "Celeste":
                        gamePath = @"C:\GAMES\Celeste\cshortcut";
                        break;
                    case "Geometry Dash":
                        gamePath = @"C:\GAMES\GD";
                        break;
                    case "Terraria":
                        gamePath = @"C:\GAMES\Terraria\Terraria.v1.4.4.9.v4\t";
                        break;
                    case "Hollow Knight":
                        gamePath = @"C:\GAMES\Hollow Knight\h";
                        break;
                }

                if (!string.IsNullOrEmpty(gamePath))
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(gamePath) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        MessageText.Text = $"Error launching game: {ex.Message}";
                    }
                }
            }
            else
            {
                MessageText.Text = "Please select a game first";
            }
        }
    }
}
