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

        private Game game;
        private readonly Dictionary<string, Label> healthPanel, damagePanel, protectionPanel, petPanel;

        public readonly string music = @"\sound\background.mp3";
        private MediaPlayer backMusic = new MediaPlayer();

        private DispatcherTimer monsterAttackTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            backMusic.Open(new Uri(startPath + music));
            backMusic.MediaEnded += new EventHandler(Media_Ended);
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
            game.MonsterAttack();
            GameViewUpdate();
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

            game = new Game(heroWindow.Hero);

            Hero hero = game.GHero;

            HeroNameLabel.Content = hero.Name;
            HeroImage.Source = new BitmapImage(new Uri(hero.ImagePath));

            monsterAttackTimer.Start();

            GameViewUpdate();
            HeroViewUpdate();
        }

        private void GameViewUpdate()
        {
            /*Вносим данные на форму*/

            //Герой
            HeroHealthPB.Value = game.GHero.HealthIndicator;
            HeroBalanceUpdate();

            //Игра
            StageLabel.Content = game.CurrentStage;

            //Монстр
            Monster monster = game.GMonster;

            MonsterHealthPB.Maximum = (int)monster.Health;
            MonsterHealthPB.Value = monster.HealthIndicator;
            MonsterNameLabel.Content = monster.Name;
            MonsterImg.Source = new BitmapImage(monster.Appearance);
            MonsterImg.Width = 150 + game.CurrentStage / 10;

            //Питомец
            //PetBtn.IsEnabled = false;
            //PetGrid.Opacity = 0.3;

            /*----*/
        }

        private void HeroViewUpdate()
        {
            Hero hero = game.GHero;

            HeroHealthPB.Maximum = hero.Skills.Health;
            //HeroHealthPB.Value = hero.HealthIndicator;

            HeroLvlLabel.Content = hero.Level;

            HeroBalanceUpdate();

            UpdateSkill(hero.Skills.Health);
            UpdateSkill(hero.Skills.Damage);
            UpdateSkill(hero.Skills.Protection);
            UpdateSkill(hero.Skills.Pet);
        }

        private void HeroBalanceUpdate()
        {
            HeroBalanceLabel.Content = game.GHero.Balance;
            int balance = int.Parse(HeroBalanceLabel.Content.ToString());
            HealthBtn.IsEnabled = balance > int.Parse(CostHealthLabel.Content.ToString()) ? true : false;
            DamageBtn.IsEnabled = balance > int.Parse(CostDamageLabel.Content.ToString()) ? true : false;
            ProtectionBtn.IsEnabled = balance > int.Parse(CostProtectionLabel.Content.ToString()) ? true : false;
            PetBtn.IsEnabled = balance > int.Parse(CostPetLabel.Content.ToString()) ? true : false;
        }

        private void NextLvlBtn_Click(object sender, RoutedEventArgs e)
        {
            if (game.prevStage)
                return;

            game.CurrentStage++;
            game.UpdateStage();

            GameViewUpdate();
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
            game.GHero.LevelUp("Health");
            HeroViewUpdate();
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

        private void Window_Closed(object sender, EventArgs e)
        {
            backMusic.Stop();
        }

        private void MonsterField_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            game.HeroAttack();
            GameViewUpdate();
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
            Dictionary<string, Label> panel = skill == game.GHero.Skills.Health ? healthPanel
                : skill == game.GHero.Skills.Damage ? damagePanel
                : skill == game.GHero.Skills.Protection ? protectionPanel
                : skill == game.GHero.Skills.Pet ? petPanel
                : null;
                
            if (skill is null)
                throw new Exception("Не удалость распознать навык!");

            panel["lvl"].Content = skill.Level;
            panel["value"].Content = (int)skill.Value;
            panel["cost"].Content = (int)skill.Cost;
            panel["inc"].Content = panel == protectionPanel ? skill.Inc : (int)skill.Inc;
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            backMusic.Position = TimeSpan.Zero;
            backMusic.Play();
        }
    }
}
