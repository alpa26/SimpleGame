using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class Form1 : Form
    {
        public Enemy enemy;
        public Player blueplayer;
        public Player redplayer;

        public Bonus bonuses = new Bonus();

        public Bullet redBullet;
        public Bullet blueBullet;

        public Button Continue;
        public Level currentLevel;
        public PictureBox winMenu;
        public Label Score;
        public List<PictureBox> boxes;
        public List<Level> Levels = new List<Level>();
        public List<Button> LvlsButton = new List<Button>();

        public Bitmap bit1Right = Properties.Resources.BlueCovboy;
        public Bitmap bit1Left = Properties.Resources.BlueCovboy1;
        public Bitmap bit2Right = Properties.Resources.RedCovboy;
        public Bitmap bit2Left = Properties.Resources.RedCovboy2;

        public Form1(IEnumerable<Level> levels)
        {
            this.TopMost = true;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            Cursor.Hide();
            BackgroundImage = Properties.Resources.Pustina;
            Levels = levels.ToList();
            MakeButtons(levels);
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            ActivatePlayer(ref blueplayer, ref blueBullet, bit1Right);
            ActivatePlayer(ref redplayer, ref redBullet, bit2Left);

            Score = new Label { Size = new Size(126,40), Text = "Rezult : " + blueplayer.score.ToString(), Location = new Point(676, 20) };
            Score.Font = new Font("Microsoft Sans Serif", 18, Score.Font.Style);
            Controls.Add(Score);
            Score.BackColor = Color.Transparent;
            Score.Visible = false;

            enemy = new Enemy(new Point(-1, -1));
            Controls.Add(enemy.box);
            enemy.box.Visible = false;

            Controls.Add(bonuses.slower);
            bonuses.slower.BackColor = Color.Transparent;
            Controls.Add(bonuses.speed);
            bonuses.speed.BackColor = Color.Transparent;
            bonuses.speed.Visible = false;

            winMenu = new PictureBox() { Location = new Point(150, 100), Size = new Size(500, 119) };
            Controls.Add(winMenu);
            ActivateMenu(Properties.Resources.Menu);

            Controls.Add(Continue);
            Continue.Visible = false;


            timer1.Interval = 5;
            timer1.Tick += new EventHandler(update);
            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);
            
            foreach (var level in LvlsButton)
            {
                level.Click += (sender, args) =>
                {
                    ChangeLevel(Levels.Find(x=>x.Name == level.Text));
                };
                level.Parent = this;
                Controls.Add(level);
            }
            KeyPreview = true;
        }

        public void ChangeLevel(Level level)
        {
            currentLevel = level;
            ActivateLevel();
            timer1.Start();
        }

        public void ActivateLevel()
        {
            blueplayer.myPhysics.box.Location = currentLevel.player.myPhysics.box.Location;
            blueplayer.myPhysics.box.Visible = true;

            redplayer.myPhysics.box.Location = currentLevel.player2.myPhysics.box.Location;
            redplayer.myPhysics.box.Visible = true;

            winMenu.Visible = false;
            BackgroundImage = currentLevel.image;

            redBullet.box.Location = new Point(-100, -100);
            blueBullet.box.Location = new Point(-100, -100);
            if (currentLevel == Levels.Find(x => x.Name == "SinglePlay"))
            {
                blueplayer.speed = 9;
                enemy.box.Location = new Point(600, 60);
                enemy.box.Visible = true;
                Score.Visible = true;
                blueplayer.score = 0;
                Score.Text = "Score : " + blueplayer.score.ToString();
                bonuses.speed.Location = new Point(-100, -100);
                bonuses.slower.Location = new Point(-100, -100);
            }
            else
            {
                blueplayer.speed = 6;
                enemy.box.Location = new Point(-100, -100);
                enemy.box.Visible = false;
                bonuses.speed.Visible = true;
                bonuses.speed.Location = new Point(200, 20);
                bonuses.slower.Location = new Point(-100, -100);
                Score.Visible = false;
                Score.BackColor = Color.Transparent;
            }
            // Удаляет старые платформы 
            if (boxes != null)
                foreach (var box in boxes)
                    box.Visible = false;

            boxes = currentLevel.panels;
            // Добавляет новые платформы 
            foreach (var box in boxes)
            {
                Controls.Add(box);
                box.Visible = true;
            }

            if (LvlsButton != null)
                foreach (var levels in LvlsButton)
                {
                    levels.Visible = false;
                    levels.Enabled = false;
                }
        }
        public void MovePlayer(ref Player player, Bitmap image, int dir)
        {
            player.myPhysics.box.Image = image;
            player.dirX = dir *player.speed;
        }
        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            blueplayer.dirX = 0;
            blueplayer.isMoving = false;
            redplayer.dirX = 0;
            redplayer.isMoving = false;
        }

        public void OnPress(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (winMenu.Visible == true)
                        this.Close();
                    else ActivatePauseMenu();
                    break;
                case Keys.D:
                    MovePlayer(ref blueplayer, bit1Right, 1);
                    break;
                case Keys.A:
                    MovePlayer(ref blueplayer, bit1Left, -1);
                    break;
                case Keys.Space:
                    blueplayer.myPhysics.Jumping(boxes);
                    break;
                case Keys.F:
                    if(blueBullet.box.Visible == false)
                    {
                        if (blueplayer.myPhysics.box.Image == bit1Left) blueBullet.dirX = -10;
                        else blueBullet.dirX = 10;
                        blueBullet.box.Location = blueplayer.myPhysics.box.Location;
                        blueBullet.box.Top += 40;
                        blueBullet.box.Visible = true;
                    }
                    break;

                case Keys.NumPad4:
                    MovePlayer(ref redplayer, bit2Left, -1);
                    break;
                case Keys.NumPad6:
                    MovePlayer(ref redplayer, bit2Right, 1);
                    break;
                case Keys.NumPad0:
                    redplayer.myPhysics.Jumping(boxes);
                    break;
                case Keys.NumPad5:
                    if (redBullet.box.Visible == false)
                    {
                        if (redplayer.myPhysics.box.Image == bit2Left) redBullet.dirX = -10;
                        else redBullet.dirX = 10;
                        redBullet.box.Location = redplayer.myPhysics.box.Location;
                        redBullet.box.Top += 40;
                        redBullet.box.Visible = true;
                    }
                    break;
            }
            blueplayer.isMoving = true;
            redplayer.isMoving = true;
        }

        
        private void update(object sender, EventArgs e)
        {
            UpdatePlayer(ref redBullet, ref blueplayer, ref redplayer, Properties.Resources.RedWin);
            UpdatePlayer(ref blueBullet, ref redplayer, ref blueplayer, Properties.Resources.BlueWin);
            UpdateBullet(ref blueBullet, ref redplayer);
            UpdateBullet(ref redBullet, ref blueplayer);

            if (blueplayer.Interaction(enemy.box))
            {
                ActivateMenu(Properties.Resources.GameOver);
                ActivateLevelMenu();
                timer1.Stop();
                blueplayer.score = 0;
            }
            if (currentLevel == Levels.Find(x => x.Name == "SinglePlay"))
            {

                enemy.box.Visible = true;
                enemy.DoMove(blueplayer, boxes);
                enemy.box.BackColor = Color.Transparent;
                if (enemy.Interaction(blueBullet.box))
                {
                    blueBullet.box.Location = new Point(-10, -10);
                    blueBullet.box.Visible = false;
                    blueplayer.score++;
                    Score.Text = "Score : " + blueplayer.score.ToString();
                    enemy.MakeNewLocation(blueplayer.myPhysics.box.Location);
                }
            }
            Invalidate();
        }

        public void UpdatePlayer(ref Bullet bullet, ref Player player, ref Player enemy, Bitmap image)
        {
            if (player.Interaction(bonuses.speed))
            {
                new Action(player.fasterSpeed).BeginInvoke(null, null);
                bonuses.speed.Location = new Point(-100, -100);
                bonuses.slower.Location = new Point(new Random().Next(20,700), new Random().Next(20, 400));
            }
            if (player.Interaction(bonuses.slower))
            {
                new Action(enemy.slowerSpeed).BeginInvoke(null, null);
                bonuses.slower.Location = new Point(-100, -100);
                bonuses.speed.Location = new Point(new Random().Next(20, 700), new Random().Next(20, 400));
            }

            player.myPhysics.box.BackColor = Color.Transparent;
            if (player.isMoving)
                player.MovePlayer();
            if (player.Interaction(bullet.box))
            {
                ActivateMenu(image);
                bullet.box.Visible = false;
                bullet.box.Location = new Point(0, 0);
                ActivateLevelMenu();
                timer1.Stop();
            }
            player.myPhysics.ApplyPhysics(boxes);
        }

        public void UpdateBullet(ref Bullet bullet, ref Player player)
        {
            if (bullet.box.Visible == true)
                bullet.MoveBullet();
            if (!bullet.InMapBoundaries())
            {
                bullet.box.Visible = false;
                bullet.box.Location = new Point(-30, -30);
            }
        }

        public void ActivateLevelMenu()
        {
            foreach (var levels in LvlsButton)
            {
                levels.Visible = true;
                levels.Enabled = true;
                levels.BringToFront();
            }
        }

        public void ActivatePauseMenu()
        {
            ActivateMenu(Properties.Resources.Pause);
            Continue.Visible = true;
            Continue.Enabled = true;
            timer1.Stop();
            Continue.Click += (sender, args) =>
            {
                winMenu.Visible = false;
                Continue.Visible = false;
                Continue.Enabled = false;
                timer1.Start();
            };
        }

        public void ActivateMenu(Bitmap image)
        {
            winMenu.Image = image;
            winMenu.BackColor = Color.Transparent;
            winMenu.Visible = true;
            winMenu.BringToFront();
        }

        public void ActivatePlayer(ref Player player, ref Bullet bullet, Bitmap image)
        {
            player = new Player(new Point(10, 200));
            player.myPhysics.box.Image = image;
            Controls.Add(player.myPhysics.box);
            player.myPhysics.box.Visible = false;
            bullet = new Bullet();
            Controls.Add(bullet.box);
            bullet.box.Visible = false;
        }

        public void MakeButtons(IEnumerable<Level> levels)
        {
            var top = 250;
            foreach (var level in levels)
            {
                if (currentLevel == null) currentLevel = level;
                LvlsButton.Add(new Button
                {
                    Size = new Size(200, 30),
                    Text = level.Name,
                    Left = 300,
                    Top = top,
                    BackColor = Color.Transparent
                });
                top += LvlsButton.Find(x => x.Text == level.Name).Height + 10;
            }

            Continue = new Button
            {
                Size = new Size(200, 30),
                Text = "Continue",
                Left = 300,
                Top = 280,
                BackColor = Color.Transparent
            };
            KeyPreview = true;
        }
    }
}
