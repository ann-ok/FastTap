using System.Windows;

namespace Fast_Tap
{
    /// <summary>
    /// Логика взаимодействия для Pet.xaml
    /// </summary>
    public partial class PetWindow : Window
    {
        public string petName;

        public PetWindow()
        {
            InitializeComponent();
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            petName = PetNameTB.Text;
            DialogResult = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
