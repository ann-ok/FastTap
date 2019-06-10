using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fast_Tap
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            int stage = 555;
            StageLabel.Content = stage;

            HeroHealthPB.Maximum = 500;
            HeroHealthPB.Value = 200;

            HeroImage.Source = new BitmapImage(new Uri("image/wolf.png", UriKind.Relative));

            HeroLvlLavel.Content = 121;
            HeroNameLabel.Content = "Имя";
            HeroBalanceLabel.Content = 550;

            MonsterHealthPB.Maximum = 200;
            MonsterHealthPB.Value = 100;
            MonsterNameLabel.Content = "Монстр";
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsBtn.Content = "Нажали на кнопку";
        }


        private void NextLvlBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RestartBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RewardBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HeroInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PetBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HealthBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsBtn_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void DamageBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProtectionBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BuyPetBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
