using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Fast_Tap
{
    /// <summary>
    /// Логика взаимодействия для SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public bool exit, createNewGame;
        private readonly string soundONPath;
        private readonly string soundOFFPath;

        public SettingWindow(string info)
        {
            InitializeComponent();
            exit = false;
            createNewGame = false;
            soundONPath = @"\image\soundON.png";
            soundOFFPath = @"\image\soundOFF.png";

            if (MainWindow.BackMusic.IsMuted == true)
                soundImg.Source = new BitmapImage(new Uri(MainWindow.StartPath + soundOFFPath));
            else
                soundImg.Source = new BitmapImage(new Uri(MainWindow.StartPath + soundONPath));

            this.info.Text += info;
        }

        private void SoundBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.BackMusic.IsMuted == false)
            {
                soundImg.Source = new BitmapImage(new Uri(MainWindow.StartPath + soundOFFPath));
                MainWindow.BackMusic.IsMuted = true;
            }
            else
            {
                soundImg.Source = new BitmapImage(new Uri(MainWindow.StartPath + soundONPath));
                MainWindow.BackMusic.IsMuted = false;
            }
        }

        private void CreateNewHeroBtn_Click(object sender, RoutedEventArgs e)
        {
            createNewGame = true;
            Close();
        }

        private void ExitHeroBtn_Click(object sender, RoutedEventArgs e)
        {
            exit = true;
            Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e) => Close();

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => DialogResult = true;
    }
}
