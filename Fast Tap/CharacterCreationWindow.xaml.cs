using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fast_Tap
{
    /// <summary>
    /// Логика взаимодействия для CharacterCreationWindow.xaml
    /// </summary>
    public partial class CharacterCreationWindow : Window
    {
        private bool exit = true;
        public FastTapLibrary.Hero Hero { get; private set; }

        public CharacterCreationWindow()
        {
            InitializeComponent();
            NameErrorLabel.Visibility = Visibility.Hidden;
            InfoLabel.Content = "Hello traveler!\nChoose your way...";
            HeroImage.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exit && MessageBox.Show("Are you sure you want to quit the game?", "Attention!",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
            else if (exit)
                DialogResult = false;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Image image = new Image();

                switch (HeroImage.SelectedIndex)
                {
                    case 0: image = bearImg; break;
                    case 1: image = wolfImg; break;
                    case 2: image = volfImg; break;
                    case 3: image = lionImg; break;
                    case 4: image = pandaImg; break;
                    case 5: image = foxImg; break;
                    default:
                        new Exception("Недопустимый индекс изображения!"); break;
                }

                string imagePath = image.Source.ToString();

                Hero = new FastTapLibrary.Hero(HeroNameTB.Text, imagePath);

                if (Money.IsChecked == true)
                    Hero.GetReward(true);
                else if (Health.IsChecked == true)
                {
                    Hero.Balance += Hero.Skills.SHealth.Cost;
                    Hero.LevelUp("Health");
                }
                else if (Damage.IsChecked == true)
                {
                    Hero.Balance += Hero.Skills.SDamage.Cost;
                    Hero.LevelUp("Damage");
                }
                else if (Protection.IsChecked == true)
                {
                    Hero.Balance += Hero.Skills.SProtection.Cost;
                    Hero.LevelUp("Protection");
                }

                exit = false;
                DialogResult = true;
            }
            catch(Exception ex) when (ex.Message == "Недопустимый индекс изображения!")
            {
                MessageBox.Show("Не удалось определить изображение!");
            }
            catch
            {
                HeroNameTB.BorderBrush = new SolidColorBrush(Colors.Red);
                NameErrorLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
