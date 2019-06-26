using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using FastTapLibrary;

namespace Fast_Tap
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly string StartPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;

        private Game game;
        //private readonly string gameFilePath = @"\game.dat";

        public readonly string backMusicPath = @"\sound\background.mp3";
        public readonly string attackMusicPath = @"\sound\attack.mp3";
        public static MediaPlayer BackMusic = new MediaPlayer();
        private readonly MediaPlayer attackMusic = new MediaPlayer();

        private readonly DispatcherTimer monsterAttackTimer = new DispatcherTimer();
        private readonly DispatcherTimer petAttackTimer = new DispatcherTimer();
        private readonly DispatcherTimer heroAttackTimer = new DispatcherTimer();
        private readonly DispatcherTimer petAttackFireTimer = new DispatcherTimer();
        private readonly DispatcherTimer infoDeathTimer = new DispatcherTimer();

        private readonly Dictionary<string, Label> healthPanel, damagePanel, protectionPanel, petPanel;
        private bool noHero = false;

        public MainWindow()
        {
            InitializeComponent();

            BackMusic.Open(new Uri(StartPath + backMusicPath));
            BackMusic.MediaEnded += new EventHandler(Media_Ended);
            BackMusic.Play();

            attackMusic.Open(new Uri(StartPath + attackMusicPath));

            monsterAttackTimer.Tick += MonsterAttackTimer_Tick;
            monsterAttackTimer.Interval = Monster.AttackSpeed;

            petAttackTimer.Tick += PetAttackTimer_Tick;
            petAttackTimer.Interval = Pet.AttackSpeed;

            heroAttackTimer.Tick += Timer_Tick;
            heroAttackTimer.Interval = TimeSpan.FromMilliseconds(0.5);

            petAttackFireTimer.Tick += PetAttackFireTimer_Tick;
            petAttackFireTimer.Interval = TimeSpan.FromSeconds(1);

            infoDeathTimer.Tick += InfoDeathTimer_Tick;
            infoDeathTimer.Interval = TimeSpan.FromSeconds(0.7);

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

        //При запуке игры необходимо проверить создан ли персонаж
        //При отсутсвии открываем окно создания персонажа
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool newGame = true;

            //if (System.IO.File.Exists(StartPath + gameFilePath))
            //{
            //	Game game = Game.Load(StartPath + gameFilePath);
            //	if (game != null)
            //		newGame = false;
            //	else
            //		MessageBox.Show("Мы не смогли загрузить Вашего героя :с\nИгра будет начата сначала...", "Ошибка загрузки", MessageBoxButton.OK);
            //}

            if (newGame)
            {
                CharacterCreationWindow heroWindow = new CharacterCreationWindow();

                //Если персонаж не будет создан выходим из игры
                if (heroWindow.ShowDialog() == false)
                {
                    noHero = true;
                    Close();
                    return;
                }

                game = new Game(heroWindow.Hero);

                //Питомец
                PetCostLabel.Text = Pet.Price.ToString();
                PetGrid.Opacity = 0.3;
                BuyPetBtn.Visibility = Visibility.Visible;
                PetBtn.IsEnabled = false;
            }
            else
            {
                PetNameLabel.Content = game.GPet.Name;
                BuyPetBtn.Visibility = Visibility.Hidden;
                PetBtn.IsEnabled = true;
                petAttackTimer.Start();
            }

            //Герой
            HeroNameLabel.Content = game.GHero.Name;
            HeroImage.Source = new BitmapImage(new Uri(game.GHero.ImagePath));
            HeroLvlLabel.Content = game.GHero.Level;
            HeroHealthPB.Value = game.GHero.HealthIndicator;
            HeroHealthPB.Maximum = (int)game.GHero.Skills.SHealth;

            healthPanel["lvl"].Content = game.GHero.Skills.SHealth.Level;
            healthPanel["value"].Content = (int)game.GHero.Skills.SHealth.Value;
            healthPanel["cost"].Content = (int)game.GHero.Skills.SHealth.Cost;
            healthPanel["inc"].Content = (int)game.GHero.Skills.SHealth.GetInc();
            damagePanel["lvl"].Content = game.GHero.Skills.SDamage.Level;
            damagePanel["value"].Content = (int)game.GHero.Skills.SDamage.Value;
            damagePanel["cost"].Content = (int)game.GHero.Skills.SDamage.Cost;
            damagePanel["inc"].Content = (int)game.GHero.Skills.SDamage.GetInc();
            protectionPanel["lvl"].Content = game.GHero.Skills.SProtection.Level;
            protectionPanel["value"].Content = game.GHero.Skills.SProtection.Value;
            protectionPanel["cost"].Content = (int)game.GHero.Skills.SProtection.Cost;
            protectionPanel["inc"].Content = game.GHero.Skills.SProtection.GetInc();
            petPanel["lvl"].Content = game.GHero.Skills.SPet.Level;
            petPanel["value"].Content = (int)game.GHero.Skills.SPet.Value;
            petPanel["cost"].Content = (int)game.GHero.Skills.SPet.Cost;
            petPanel["inc"].Content = (int)game.GHero.Skills.SPet.GetInc();

            HeroBalanceUpdate();

            //Игра
            StageLabel.Content = game.CurrentStage;
            RewardBtn.IsEnabled = false;

            //Монстр
            MonsterHealthPB.Value = game.GMonster.HealthIndicator;
            MonsterHealthPB.Maximum = game.GMonster.Health;
            MonsterNameLabel.Content = game.GMonster.Name;
            MonsterImg.Source = new BitmapImage(game.GMonster.Appearance);
            MonsterImg.Width = 150 + game.CurrentStage / 10;

            monsterAttackTimer.Start();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!noHero && (MessageBox.Show("Are you sure you want to quit the game?", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Cancel)
                == MessageBoxResult.No))
                e.Cancel = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            BackMusic.Stop();
            attackMusic.Stop();
            monsterAttackTimer.Stop();
            petAttackTimer.Stop();
            heroAttackTimer.Stop();
            petAttackFireTimer.Stop();
            infoDeathTimer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MonsterImg.Margin = new Thickness(0, 0, 0, 0);
            heroAttackTimer.Stop();
        }

        private void PetAttackFireTimer_Tick(object sender, EventArgs e)
        {
            petAttackFireTimer.Stop();
            PetFireImg.Visibility = Visibility.Hidden;
        }

        private void PetAttackTimer_Tick(object sender, EventArgs e)
        {
            PetFireImg.Visibility = Visibility.Visible;
            petAttackFireTimer.Start();

            game.PetAttack();

            if (game.GMonster.Status == Statuses.Inactive)
                StageUpdate();

            HealthUpdate();
        }

        private void MonsterAttackTimer_Tick(object sender, EventArgs e)
        {
            game.MonsterAttack();

            if (game.GHero.Status == Statuses.Inactive)
                StageUpdate();

            HealthUpdate();
        }

        private void InfoDeathTimer_Tick(object sender, EventArgs e)
        {
            InfoDeath.Visibility = Visibility.Hidden;
            infoDeathTimer.Stop();
        }

        private void NextLvlBtn_Click(object sender, RoutedEventArgs e)
        {
            if (game.prevStage)
                return;

            game.CurrentStage++;

            StageUpdate();
        }

        private void RestartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to go to stage 1?", "Attention!", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel)
                == MessageBoxResult.OK)
            {
                game.GoToTheFirstStage();
                StageUpdate();
            }
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            monsterAttackTimer.Stop();
            petAttackTimer.Stop();

            SettingWindow setting = new SettingWindow(game.GetInformation());

            if (setting.ShowDialog() == true)
            {
                if (setting.exit)
                    Close();

                if (setting.createNewGame)
                    Window_Loaded(null, null);

                monsterAttackTimer.Start();
                if (game.GPet != null)
                    petAttackTimer.Start();
            }
        }

        private void RewardBtn_Click(object sender, RoutedEventArgs e)
        {
            game.GHero.GetReward();
            HeroBalanceUpdate();
            RewardBtn.IsEnabled = game.GHero.OpportunityToGetAnAward > 0 ? true : false;
        }

        private void HealthBtn_Click(object sender, RoutedEventArgs e) => UpdateSkill(game.GHero.Skills.SHealth);

        private void DamageBtn_Click(object sender, RoutedEventArgs e) => UpdateSkill(game.GHero.Skills.SDamage);

        private void ProtectionBtn_Click(object sender, RoutedEventArgs e) => UpdateSkill(game.GHero.Skills.SProtection);

        private void PetBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateSkill(game.GHero.Skills.SPet);
            game.GPet.Damage = game.GHero.Skills.SPet.Value;
        }

        private void MonsterField_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            attackMusic.Stop();
            attackMusic.Play();

            game.HeroAttack();

            if (game.GMonster.Status == Statuses.Inactive)
                StageUpdate();

            HealthUpdate();

            MonsterImg.Margin = new Thickness(0, 0, 0, 2);
            heroAttackTimer.Start();
        }

        private void BuyPetBtn_Click(object sender, RoutedEventArgs e)
        {
            monsterAttackTimer.Stop();

            PetWindow petWindow = new PetWindow();

            if (petWindow.ShowDialog() == true)
            {
                game.CreatePet(petWindow.petName);
                HeroBalanceUpdate();
                PetNameLabel.Content = petWindow.petName;
                BuyPetBtn.Visibility = Visibility.Hidden;
                PetBtn.IsEnabled = true;
                PetGrid.Opacity = 1;
                petAttackTimer.Start();
            }

            monsterAttackTimer.Start();
        }

        private void HealthUpdate()
        {
            HeroHealthPB.Value = game.GHero.HealthIndicator;
            MonsterHealthPB.Value = game.GMonster.HealthIndicator;
        }

        private void StageUpdate()
        {
            monsterAttackTimer.Stop();

            //Надпись
            InfoDeath.Visibility = Visibility.Visible;
            if (game.GHero.Status == Statuses.Inactive)
                InfoDeath.Content = "Вас убили!";
            else
                InfoDeath.Content = "Вы убили монстра!";
            infoDeathTimer.Start();

            //Герой
            HeroBalanceUpdate();

            //Игра
            game.UpdateStage();
            StageLabel.Content = game.CurrentStage;

            //Монстр
            MonsterHealthPB.Maximum = game.GMonster.Health;
            MonsterNameLabel.Content = game.GMonster.Name;
            MonsterImg.Source = new BitmapImage(game.GMonster.Appearance);
            MonsterImg.Width = 150 + game.CurrentStage / 10;

            HealthUpdate();

            monsterAttackTimer.Start();
        }

        /// <summary>
        /// Updates values for skill in WPF.
        /// </summary>
        /// <param name="panel">Upgradeable skill.</param>
        private void UpdateSkill(Skill skill)
        {
            game.GHero.LevelUp(skill);
            HeroBalanceUpdate();

            Dictionary<string, Label> panel = skill == game.GHero.Skills.SHealth ? healthPanel
                : skill == game.GHero.Skills.SDamage ? damagePanel
                : skill == game.GHero.Skills.SProtection ? protectionPanel
                : skill == game.GHero.Skills.SPet ? petPanel
                : null;

            if (skill == null)
                throw new Exception("Не удалость распознать навык!");

            if (skill == game.GHero.Skills.SHealth)
                HeroHealthPB.Maximum = (int)game.GHero.Skills.SHealth.Value;

            panel["lvl"].Content = skill.Level;
            panel["value"].Content = panel == protectionPanel ? Math.Round(skill.Value, 4) : (int)skill.Value;
            panel["cost"].Content = (int)skill.Cost;
            panel["inc"].Content = panel == protectionPanel ? skill.GetInc() : (int)skill.GetInc();

            HeroLvlLabel.Content = game.GHero.Level;
            RewardBtn.IsEnabled = game.GHero.OpportunityToGetAnAward > 0 ? true : false;
        }

        private void HeroBalanceUpdate()
        {
            HeroBalanceLabel.Content = game.GHero.Balance;

            int balance = int.Parse(HeroBalanceLabel.Content.ToString());
            HealthBtn.IsEnabled = balance >= int.Parse(CostHealthLabel.Content.ToString()) ? true : false;
            DamageBtn.IsEnabled = balance >= int.Parse(CostDamageLabel.Content.ToString()) ? true : false;
            ProtectionBtn.IsEnabled = balance >= int.Parse(CostProtectionLabel.Content.ToString()) ? true : false;

            if (PetBtn.Visibility == Visibility.Visible)
            {
                BuyPetBtn.IsEnabled = balance >= int.Parse(PetCostLabel.Text) ? true : false;
                PetBtn.IsEnabled = balance >= int.Parse(CostPetLabel.Content.ToString()) ? true : false;
            }
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            BackMusic.Position = TimeSpan.Zero;
            BackMusic.Play();
        }
    }
}
