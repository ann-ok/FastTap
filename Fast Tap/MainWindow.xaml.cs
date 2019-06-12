using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using FastTapLibrary;

namespace Fast_Tap
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string startPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;

        private Hero hero;
        private Game game;
        private Monster monster;
        private readonly Dictionary<string, Label> healthPanel, damagePanel, protectionPanel, petPanel;

        public readonly string music = @"\sound\background.mp3";
        private MediaPlayer backMusic = new MediaPlayer();

        private DispatcherTimer monsterAttackTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            backMusic.Open(new Uri(startPath + music));
            backMusic.Play();

            //атака монстра
            monsterAttackTimer.Tick += new EventHandler(MonsterAttackTimer_Tick);
            monsterAttackTimer.Interval = Monster.AttackSpeed;

            healthPanel = new Dictionary<string, Label>()
            {
                {"lvl", LvlHealthLabel },
                {"value", CurrentHealthLabel },
                {"cost", CostHealthLabel },
                {"inc", IncHealthLabel }
            };

            damagePanel = new Dictionary<string, Label>()
            {
                {"lvl", LvlDamageLabel },
                {"value", CurrentDamageLabel },
                {"cost", CostDamageLabel },
                {"inc", IncDamageLabel }
            };

            protectionPanel = new Dictionary<string, Label>()
            {
                {"lvl", LvlProtectionLabel },
                {"value", CurrentProtectionLabel },
                {"cost", CostProtectionLabel },
                {"inc", IncProtectionLabel }
            };

            petPanel = new Dictionary<string, Label>()
            {
                {"lvl", LvlPetLabel },
                {"value", CurrentPetLabel },
                {"cost", CostPetLabel },
                {"inc", IncPetLabel }
            };
        }

        private void MonsterAttackTimer_Tick(object sender, EventArgs e)
        {
            if (hero.Status == Statuses.Inactive && game.prevStage == true)

            //если статус героя неактивный то бросает на этап назад (только 1 раз)
            hero.HealthIndicator -= monster.Attack();
            HeroHealthPB.Value = hero.HealthIndicator;
        }

        //При запуке игры необходимо проверить создан ли персонаж
        //При отсутсвии открываем окно создания персонажа
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //если
            //Новое окно создания персонажа
            CharacterCreationWindow heroWindow = new CharacterCreationWindow();
            //Если персонаж не будет создан выходим из игры
            if (heroWindow.ShowDialog() == false)
            {
                Close();
                return;
            }

            hero = heroWindow.Hero;
            game = new Game();

            monster = CreateMonster();
            monsterAttackTimer.Start();

            DataUpdate();
        }

        private void DataUpdate()
        {
            /*Вносим данные на форму*/

            //Герой
            HeroHealthPB.Maximum = hero.HealthIndicator;
            HeroHealthPB.Value = hero.HealthIndicator;

            HeroNameLabel.Content = hero.Name;
            HeroLvlLabel.Content = hero.Level;
            HeroBalanceLabel.Content = hero.Balance;
            HeroImage.Source = new BitmapImage(new Uri(hero.ImagePath));

            UpdateSkill(hero.Skills.Health);
            UpdateSkill(hero.Skills.Damage);
            UpdateSkill(hero.Skills.Protection);
            UpdateSkill(hero.Skills.Pet);

            //Питомец
            PetBtn.IsEnabled = false;
            PetGrid.Opacity = 0.3;

            //Игра
            StageLabel.Content = game.CurrentStage;

            //Монстр
            MonsterHealthPB.Maximum = monster.HealthIndicator;
            MonsterHealthPB.Value = monster.HealthIndicator;
            MonsterNameLabel.Content = monster.Name;
            MonsterImg.Source = new BitmapImage(monster.Appearance);
            MonsterImg.Width = 150 + game.CurrentStage / 10;

            /*----*/
        }

        private Monster CreateMonster()
        {
            int stage = game.CurrentStage;

            if (stage % 10 == 0)
                return new Boss(stage);
            else if (new Random().NextDouble() <= Monster.BonusChance)
                return new BonusBoss(stage);
            else
                return new Monster(stage);
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

        /// <summary>
        /// Updates values for skill in WPF.
        /// </summary>
        /// <param name="panel">Upgradeable skill.</param>
        private void UpdateSkill(Skill skill)
        {
            Dictionary<string, Label> panel = skill == hero.Skills.Health ? healthPanel
                : skill == hero.Skills.Damage ? damagePanel
                : skill == hero.Skills.Protection ? protectionPanel
                : skill == hero.Skills.Pet ? petPanel
                : null;
                
            if (skill is null)
                throw new Exception("Не удалость распознать навык!");

            panel["lvl"].Content = skill.Level;
            panel["value"].Content = skill.Value;
            panel["cost"].Content = skill.Cost;
            panel["inc"].Content = panel == protectionPanel ? skill.Inc : (int)skill.Inc;
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            backMusic.Position = TimeSpan.Zero;
            backMusic.Play();
        }
    }
}
