using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using Windows.UI;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Microsoft.UI.Composition.SystemBackdrops;
using System.Runtime.InteropServices;
using Windows.System;

namespace GameLocker
{
    public sealed partial class MainWindow : Window
    {
        private const string CorrectPassword = "animeisbad";
        private Microsoft.UI.Composition.SystemBackdrops.MicaController m_micaController;

        public MainWindow()
        {
            this.InitializeComponent();

            // Enable Mica backdrop
            if (MicaController.IsSupported())
            {
                Microsoft.UI.Xaml.Media.MicaBackdrop micaBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();
                this.SystemBackdrop = micaBackdrop;
            }

            // Custom title bar
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar); // Assuming you have a UIElement named AppTitleBar for custom title bar. If not, comment this line.

            // Set window size
            var windowHandle = WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32(650, 1000));

            // Apply Mica effect
            ApplyMicaEffect();

            // Extends content into the title bar and optionally sets the title bar to null
            Window window = App.MainWindow;
            this.ExtendsContentIntoTitleBar = true;
         
            // this.SetTitleBar(null); // Uncomment this line if you want to explicitly set the title bar to null.
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

        private void ApplyMicaEffect()
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

            // Apply Mica effect
            appWindow.SetPresenter(AppWindowPresenterKind.Default);

            // Additional customization here if needed
        }
    }
}