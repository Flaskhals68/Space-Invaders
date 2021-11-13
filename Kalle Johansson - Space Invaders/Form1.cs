using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media;

namespace Kalle_Johansson___Space_Invaders
{

    public partial class Form1 : Form
    {
        //En samling av alla spelarens fält
        static class Player
        {
            public static int height = 50;
            public static int width = 50;
            public static int x = 1920 / 2 - width / 2;
            public static int y = 900;
            public static int speed = 15;

            //En metod som ritar ut figurerna som representerar spelarens liv
            public static void drawLives(Form p, List<PictureBox> lives)
            {
                int lifeSpace = 20;
                for (int i = 0; lives.Count < 3; i++)
                {
                    PictureBox life = new PictureBox();
                    life.Location = new Point(20 + ((40 + lifeSpace) * i), 900 + height);
                    life.Size = new Size(width, height);
                    life.BackgroundImage = Properties.Resources.player;
                    life.BackgroundImageLayout = ImageLayout.Stretch;
                    lives.Add(life);
                    p.Controls.Add(life);
                }
            }
        }

        //En samling för alla aliens fält
        static class Alien
        {
            public static int width = 50;
            public static int height = 50;
            public static int rows = 5;
            public static int columns = 10;
            public static int space = 10;
            public static int x = 1920 / 2 - width - (width * columns / 2) - (space * columns / 2) + (space / 2 + width) - 5;
            public static int y = 100;
            public static int speed = 6;

            //En metod som skapar och placerar alla aliens
            public static void createImage(Form p)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        //Skapar en alien
                        PictureBox alien = new PictureBox();
                        alien.Size = new Size(width, height);
                        alien.BackgroundImageLayout = ImageLayout.Stretch;
                        alien.Location = new Point(x, y);
                        //Väljer en bild beroende på vilken rad den är på
                        if (i == 3 || i == 4)
                        {
                            alien.BackgroundImage = Properties.Resources.invader11;
                            alien.Name = "Alien1";
                        }

                        else if (i == 1 || i == 2)
                        {
                            alien.BackgroundImage = Properties.Resources.invader22;
                            alien.Name = "Alien2";
                        }

                        else if (i == 0)
                        {
                            alien.BackgroundImage = Properties.Resources.invader33;
                            alien.Name = "Alien3";
                        }
                        p.Controls.Add(alien);
                        x += width + space;
                    }
                    //Hoppar tillbaka till början av raden samt ner en rad
                    x = 1920 / 2 - width - (width * columns / 2) - (space * columns / 2) + (space / 2 + width) - 5;
                    y += height + space;
                }
                //Återställer y-värdet till nästa användning av functionen
                y = 100;
            }
        }

        //En separat statisk klass för ufot
        static class Ufo
        {
            public static int width = 75;
            public static int height = 40;
            public static int x = 1920 + width;
            public static int y = 50;
            public static int score = 100;
            public static int speed = -10;
        }

        //En statisk klass för spelarens skott
        static class Bullet
        {
            static int width = 5;
            static int height = 20;
            public static int speed = 40;

            //En mall för spelarens skott
            public static PictureBox setFields(PictureBox _playerImage, PictureBox _bullet)
            {
                _bullet.Location = new Point(_playerImage.Location.X + _playerImage.Width / 2, _playerImage.Location.Y - _playerImage.Height / 2);
                _bullet.Size = new Size(width, height);
                _bullet.BackgroundImage = Properties.Resources.bullet;
                _bullet.BackgroundImageLayout = ImageLayout.Stretch;
                _bullet.Name = "Bullet";
                return _bullet;
            }
        }

        //En statisk klass för fiendernas lasrar
        static class Laser
        {
            public static int width = 5;
            public static int height = 20;
            public static int speed = 20;

            //En mall för fiendens laser
            public static PictureBox createLaser(PictureBox _alien, PictureBox _laser)
            {
                _laser.Location = new Point(_alien.Location.X + width / 2, _alien.Location.Y + height);
                _laser.Size = new Size(width, height);
                _laser.Image = Properties.Resources.bullet;
                _laser.Name = "Laser";
                return _laser;
            }
        }

        //En statisk klass för de två utseenderna score labeln har
        static class Score
        {
            static int width;
            static int height;
            static int x;
            static int y;
            
            //En mall för hur score labeln har under spelets gång
            public static Label gameRunning(Label _scoreLabel)
            {
                width = 500;
                height = 50;
                x = (1920 - width) / 2;
                y = 900 + height    ;
                _scoreLabel.BringToFront();
                _scoreLabel.Size = new Size(width, height);
                _scoreLabel.Location = new Point(x, y);
                _scoreLabel.Font = new Font("Arial", 32, FontStyle.Bold);
                _scoreLabel.ForeColor = Color.White;
                _scoreLabel.BackColor = Color.Black;
                _scoreLabel.TextAlign = ContentAlignment.MiddleCenter;
                _scoreLabel.Text = "0";
                return _scoreLabel;
            }

            //En mall för score labeln när spelet är avslutat
            public static Label gameEnded(Label _scoreLabel)
            {
                width = 600;
                height = 75;
                x = (1920 - width) / 2;
                y = 320;
                _scoreLabel.Size = new Size(width, height);
                _scoreLabel.BringToFront();
                _scoreLabel.Location = new Point(x, y);
                _scoreLabel.Font = new Font("Arial", 40, FontStyle.Bold);
                _scoreLabel.BackColor = Color.FromArgb(58, 88, 175);
                _scoreLabel.TextAlign = ContentAlignment.MiddleCenter;
                _scoreLabel.Text = "SCORE: " + _scoreLabel.Text;
                return _scoreLabel;
            }
        }

        //Skapar de object och datatyper som kommer användas senare
        Random random = new Random();
        PictureBox player = new PictureBox();
        PictureBox bullet = new PictureBox();
        PictureBox ufo = new PictureBox();
        Label scoreLabel = new Label();
        Label muteStatusLabel = new Label();
        Button startButton = new Button();
        Button exitButton = new Button();
        List<PictureBox> lives = new List<PictureBox>();
        List<PictureBox> aliens = new List<PictureBox>();
        List<PictureBox> lasers = new List<PictureBox>();
        SoundPlayer sound = new SoundPlayer();
        SoundPlayer otherSound = new SoundPlayer();

        bool moveLeft = false;
        bool moveRight = false;
        bool fired = false;
        bool changeDirection = false;
        bool stageBeaten = false;
        bool gameOver = false;
        bool mute = false;
        bool godMode = false;
        
        int stage = 1;
        int chosedAlien;
        int chanseToCreate;

        public Form1()
        {
            InitializeComponent();

            //Skapar en startknapp
            startButton.Size = new Size(250, 75);
            startButton.Location = new Point((1920 - startButton.Width) / 2, 500);
            startButton.Font = new Font("Sans Serif ms", 40);
            startButton.Text = "Play";
            startButton.BackColor = Color.White;
            startButton.ForeColor = Color.FromArgb(64, 64, 64);
            startButton.TextAlign = ContentAlignment.MiddleCenter;
            startButton.Click += new System.EventHandler(startButtonClick);
            Controls.Add(startButton);
            startButton.BringToFront();

            //Skapar knapp för att stänga programmet
            exitButton.Size = new Size(250, 75);
            exitButton.Location = new Point((1920 - exitButton.Width) / 2, 600);
            exitButton.Font = new Font("Sans Serif ms", 40);
            exitButton.Text = "Exit";
            exitButton.BackColor = Color.White;
            exitButton.ForeColor = Color.FromArgb(64, 64, 64);
            exitButton.TextAlign = ContentAlignment.MiddleCenter;
            exitButton.Click += new System.EventHandler(exitButtonClick);
            Controls.Add(exitButton);
            exitButton.BringToFront();

            //Skapar label som visar om spelet är mutat
            muteStatusLabel.Size = new Size(64, 64);
            muteStatusLabel.Location = new Point(1700, + 955);
            muteStatusLabel.BackColor = Color.Black;
            muteStatusLabel.Image = Properties.Resources.soundon3;

            //Skapar en control för spelaren som vi lägger till senare
            player.Size = new Size(Player.width, Player.height);
            player.Location = new Point(Player.x, Player.y);
            player.BackgroundImage = Properties.Resources.player;
            player.BackgroundImageLayout = ImageLayout.Stretch;
            player.Name = "Player";

            //Skapar en control ufot som vi lägger till senare
            ufo.Size = new Size(Ufo.width, Ufo.height);
            ufo.BackgroundImage = Properties.Resources.ufo;
            ufo.BackgroundImageLayout = ImageLayout.Stretch;
            ufo.Name = "Ufo";
        }

        //Stänger programmet
        private void exitButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        //Tar bort intromenyn och lägger till alla pictureboxes samt startar en "startStageDelay" timern
        private void startButtonClick(object sender, EventArgs e)
        {
            Controls.Remove(menuBackround);
            Controls.Remove(textLabel);
            Controls.Remove(startButton);
            Controls.Remove(exitButton);
            muteTextLabel.Dispose();
            Controls.Add(player);
            Controls.Add(muteStatusLabel);
            Player.drawLives(this, lives);
            Alien.createImage(this);
            addAliens();
            scoreLabel = Score.gameRunning(scoreLabel);
            Controls.Add(scoreLabel);
            
            startStageDelay.Start();
        }

        //En taimer som skapar en delay innan spelet börjar
        private void startStageDelayTick(object sender, EventArgs e)
        {
            movement.Start();
            alienMove.Start();
            createLaser.Start();
            createUfo.Start();
            laserMove.Start();
            startStageDelay.Stop();
        }

        //Lägger till alla aliens i en lista
        private void addAliens()
        {
            //Söker igenom alla controls i formen
            foreach (Control c in Controls)
            {
                //Om controlen är en alien läggs den till i listan
                if (isAlien(c))
                {
                    PictureBox alien = (PictureBox)c;
                    aliens.Add(alien);
                }
            }
        }

        //Avgör om en kontroll är en alien baserat på dess namn
        private bool isAlien(Control c)
        {
            bool isAlien = false;
            if (c.Name.Contains("Alien"))
            {
                isAlien = true;
            }
            return isAlien;
        }

        //En int som avgör hur mycket poängen ska öka med beroende på "aliens" namn
        private int Points(PictureBox p)
        {
            int pointIncrease = 0;
            switch (p.Name)
            {
                case "Alien1":
                    pointIncrease = 10;
                    break;
                case "Alien2":
                    pointIncrease = 20;
                    break;
                case "Alien3":
                    pointIncrease = 30;
                    break;
                case "Ufo":
                    pointIncrease = 150;
                    break;
            }
            return pointIncrease;
        }

        //Ett event som sker när en tangent trycks ner
        private void keyPressed(object sender, KeyEventArgs e)
        {
            //Använder tangenternas KeyCode för att avgöra om spelaren förflyttas
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }
            //Använder tangenternas KeyCode för att avgöra om spelaren avfyrar och em spelet är igång samt om spelaren redan har avfyrat
            else if (e.KeyCode == Keys.Space && !gameOver && !fired && !startStageDelay.Enabled)
            {
                //Sätter "bullets" fält till samma som pictureboxen i klassen "Bullet"
                bullet = Bullet.setFields(player, bullet);
                //Lägger till "bullet" i controls så den syns
                Controls.Add(bullet);
                //Startar timern som förflyttar "bullet"
                bulletMove.Start();
                //Spelar ljudeffekten för när man avfyrar ett skott
                playSound(bullet);
                fired = true;

            }
            //Använder KeyCode för att aktivera "godmode" där spelaren är odödlig
            else if (e.KeyCode == Keys.G)
            {
                if (!godMode)
                {
                    godMode = true;
                }
                else
                {
                    godMode = false;
                }
            }
            //Använder KeyCode för att muta eller un-muta spelet
            else if (e.KeyCode == Keys.M)
            {
                if (!mute)
                {
                    mute = true;
                    muteStatusLabel.Image = Properties.Resources.muted2;
                }
                else
                {
                    mute = false;
                    muteStatusLabel.Image = Properties.Resources.soundon3;
                }
            }
        }

        //Ett event som sker när en tangent släpps upp
        private void keyReleased(object sender, KeyEventArgs e)
        {
            //Sätter boolarna som avgör om spelaren försöker röra sig till "false"
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
        }

        //En timer som flyttar spelaren om "A", "D" eller någon av piltangenterna är nedtryckta i respektive riktning
        private void playerMoveTick(object sender, EventArgs e)
        {
            if (moveLeft && player.Left > 0)
            {
                player.Left -= Player.speed;
            }
            else if (moveRight && player.Left < 1920 - player.Width)
            {
                player.Left += Player.speed;
            }
        }

        //En function som spelar en ljudeffekt beroende på vilken picturebox som utlöste ljudeffekten.
        private void playSound(PictureBox p)
        {
            //Om inte spelet är mutat så spelas ljudeffekten
            if (!mute)
            {
                //Om en alien utlöste ljudeffekten så har det en 33% chans att spela ludeffekten
                if (isAlien(p))
                {
                    sound.SoundLocation = @"../../Sounds/roblox-death-sound-effect.wav";
                    sound.Play();
                }
                //Annars spelas ljudeffekten garanterat
                else
                {
                    switch (p.Name)
                    {
                        case "Ufo":
                            otherSound.SoundLocation = @"../../Sounds/ufodeath.wav";
                            otherSound.Play();
                            break;
                        case "Bullet":
                            sound.SoundLocation = @"../../Sounds/shoot.wav";
                            sound.Play();
                            break;
                        case "Player":
                            sound.SoundLocation = @"../../Sounds/playerdeath.wav";
                            sound.Play();
                            break;
                    }
                    
                }
            }
        }

        //En timer som körs varje 30 ms
        private void alienMoveTick(object sender, EventArgs e)
        {
            //Flyttar alla aliens
            foreach (PictureBox alien in aliens)
            {
                alien.Left += Alien.speed;
                if (Controls.Contains(alien))
                {
                    //När en alien träffar en kant så blir "changedDirection" true
                    if (alien.Location.X > 1920 - alien.Width || alien.Location.X < 0)
                    {

                        changeDirection = true;

                    }
                    //Kolliderar spelaren och en alien så förlorar spelaren
                    else if (alien.Bounds.IntersectsWith(player.Bounds))
                    {
                        gameOver = true;
                        gameEnded();
                    }
                }
            }

            //Om en alien har träffat en kant så flyttas varje alien ner en rad och rör sig åt det motsatta hållet
            if (changeDirection)
            {
                foreach (PictureBox alien in aliens)
                {
                    //Endast de aliens som är "levande" och finns i controls flyttas
                    if (Controls.Contains(alien))
                    {
                        int newAlienY = alien.Location.Y + alien.Width;
                        alien.Location = new Point(alien.Location.X, newAlienY);
                    }
                }
                //Aliens byter riktning
                Alien.speed *= -1;
                changeDirection = false;
            }
        }

        //En timer som körs varje 30 ms som flyttar spelarens skott
        public void bulletMoveTick(object sender, EventArgs e)
        {
            //Flyttar skottet uppåt
            bullet.Top -= Bullet.speed;
            //Tar bort skottet då det lämnar skärmen och låter spelaren skjuta igen
            if (bullet.Top <= 0)
            {
                Controls.Remove(bullet);
                fired = false;
            }
            //Kntrollerar om kulan kolliderar med en alien
            foreach (PictureBox alien in aliens)
            {
                if (bullet.Bounds.IntersectsWith(alien.Bounds) && Controls.Contains(alien))
                {
                    //Tar bort både skottet och alien samt låter spelren skjuta igen
                    fired = false;
                    alien.Dispose();
                    Controls.Remove(bullet);
                    //Ökar poängen beroende på vilken typ av alien som är träffad
                    scoreLabel.Text = (int.Parse(scoreLabel.Text) + Points(alien)).ToString();
                    bulletMove.Stop();
                    //Om alla aliens är "döda" startas nästa omgång
                    if (isStageBeaten(this))
                    {
                        nextStage(this);
                    }
                    playSound(alien);
                    break;
                }
            }
            //Kontrollerar om skottet kolliderar med ufot
            if (bullet.Bounds.IntersectsWith(ufo.Bounds))
            {
                //Tar bort både skottet och ufot samt ökar poängen
                fired = false;
                Controls.Remove(bullet);
                Controls.Remove(ufo);
                bulletMove.Stop();
                scoreLabel.Text = (int.Parse(scoreLabel.Text) + Points(ufo)).ToString();
                playSound(ufo);
            }
            //Kontrollerar om skottet kolliderar med någon utav aliens lasrar
            foreach (PictureBox laser in lasers)
            {
                if (bullet.Bounds.IntersectsWith(laser.Bounds))
                {
                    fired = false;
                    Controls.Remove(bullet);
                    laser.Dispose(); ;
                    lasers.Remove(laser);
                    bulletMove.Stop();
                    break;
                }
            }
            
        }

        //Kontrollerar om det finns någon "levande" alien som då är tillagd i Controls
        private bool isStageBeaten(Form p)
        {
            stageBeaten = true;
            foreach (PictureBox alien in aliens)
            {
                if (p.Controls.Contains(alien))
                {
                    stageBeaten = false;
                }
            }
            return stageBeaten;
        }

        //Startar nästa omgång
        private void nextStage(Form p)
        {
            //Stannar timers och tar bort spelarens skott samt ufot
            movement.Stop();
            alienMove.Stop();
            bulletMove.Stop();
            createLaser.Stop();
            laserMove.Stop();
            Controls.Remove(bullet);
            Controls.Remove(ufo);
            //Tar bort alla lasrar från cotrols och clearar listan "lasers"
            foreach (PictureBox laser in lasers)
            {
                laser.Dispose();
            }
            lasers.Clear();
            //Kontrollerar vilken omgång spelet är på
            //Är omgång tre avklarat så avslutas spelet
            if (stage == 3)
            {
                gameEnded();
            }
            //Annars så återställs spelplanen och intervallen för aliens frekvensen de skjuter är minskad
            //Aliens och lasrarnas hastighet ökas också
            else
            {
                Laser.speed += 10;
                createLaser.Interval -= 25;
                alienMove.Interval -= 12;
                player.Location = new Point(Player.x, Player.y);
                Alien.createImage(p);
                addAliens();
                startStageDelay.Start();
                stage += 1;
            }
        }
        
        //En timer som körs med 250 ms mellanrum som skapar aliens lasrar
        private void createLaserTick( object sender, EventArgs e)
        {
            //Skapar en laser baserat på en utvald aliens position
            PictureBox laser = new PictureBox();
            laser = Laser.createLaser(aliens[chooseAlien(aliens)], laser);
            Controls.Add(laser);
            lasers.Add(laser);
        }

        //En metod som väljer vart lasern ska skapas
        private int chooseAlien(List<PictureBox> _aliens)
        {
            chosedAlien= random.Next(_aliens.Count);
            //Om inte den framslumpade alien finns i Controls så väljs en ny
            while (!Controls.Contains(_aliens[chosedAlien]))
            {
                chosedAlien = random.Next(aliens.Count);
            }
            //Kontrollerar om det finns en alien på raden nedanför som finns i Controls
            while (chosedAlien < aliens.Count - Alien.columns)
            {
                //Hoppar ner en rad
                if (Controls.Contains(_aliens[chosedAlien + Alien.columns]))
                {
                    chosedAlien += Alien.columns;
                }
                else
                {
                    break;
                }
            }
            return chosedAlien;
        }

        //En timer som körs med 30 ms mellanrum som flyttar alla aliens lasrar
        private void laserMoveTick(object sender, EventArgs e)
        {
            //Flyttar alla lasrar
            foreach (PictureBox laser in lasers)
            {
                laser.Top += Laser.speed;
                //Tar bort lasern från Controls och listan med alla lasrar då den lämnar skärmen
                if (laser.Top > 1080)
                {
                    laser.Dispose();
                    lasers.Remove(laser);
                    break;
                }
                //Om en laser kolliderar med spelaren så förlorar spelaren ett liv och lasern tas bort
                else if (player.Bounds.IntersectsWith(laser.Bounds) && !godMode)
                {
                    laser.Dispose();
                    lasers.Remove(laser);
                    looseLife(this);
                    break;
                }
            }
        }

        //En timer som körs varje sekund som har en chans att spawna ett ufo
        private void createUfoTick(object sender, EventArgs e)
        {
            chanseToCreate = random.Next(1, 11);
            if (!Controls.Contains(ufo) && chanseToCreate == 1)
            {
                ufo.Location = new Point(Ufo.x, Ufo.y);
                Controls.Add(ufo);
                ufoMove.Start();
            }
        }

        //Flyttar ufot i sidled
        private void ufoMoveTick(object sender, EventArgs e)
        {
            ufo.Left += Ufo.speed;
            if (ufo.Location.X < -ufo.Width)
            {
                Controls.Remove(ufo);
                ufoMove.Stop();
            }
        }

        //En metod som tar bort ett liv från spelaren
        private void looseLife(Form p)
        {
            playSound(player);
            //Om spelaren har några liv kvar så tas det bort
            if (lives.Any())
            {
                //Tar bort sista objectet i listan "lives"
                lives[lives.Count - 1].Dispose();
                lives.RemoveAt(lives.Count - 1);
            }
            //Finns det inga liv kvar dör spelaren
            if (!lives.Any())
            {
                gameOver = true;
                gameEnded();
            }
        }

        //En metod som avslutar spelet
        private void gameEnded()
        {
            //Stoppar alla timerar
            movement.Stop();
            alienMove.Stop();
            createUfo.Stop();
            ufoMove.Stop();
            bulletMove.Stop();
            createLaser.Stop();
            laserMove.Stop();
            //Tar fram menyn igen
            Controls.Add(menuBackround);
            menuBackround.BringToFront();
            //Byter positition och utseende på scorelabeln
            scoreLabel = Score.gameEnded(scoreLabel);

            if (gameOver)
            {
                textLabel.Text = "GAME OVER";
            }
            else
            {
                textLabel.Text = "VICTORY";
            }
            //Lägger till textLabeln i Controls
            Controls.Add(textLabel);
            textLabel.Location = new Point(660, 200);
            textLabel.BringToFront();
            
            //Lägger till Startknappen i Controls och byter clickevent så den kan användas igen
            Controls.Add(startButton);
            startButton.Text = "Play Again";
            startButton.Size = new Size(300, 75);
            startButton.Location = new Point(810, 500);
            startButton.Click += new EventHandler(replay);
            startButton.BringToFront();

            //Lägger till exitknappen i Controls
            Controls.Add(exitButton);
            exitButton.BringToFront();
        }

        //En metod som låter spelet köras igen
        private void replay(object s, EventArgs e)
        {
            //Tar bort alla lasrar från Controls och clearar listan "lasers"
            foreach (PictureBox laser in lasers)
            {
                laser.Dispose();
            }
            lasers.Clear();
            //Tar bort alla aliens från Controls och clearar listan "aliens"
            foreach (PictureBox alien in aliens)
            {
                alien.Dispose();
            }
            aliens.Clear();
            //Tar bort enskillda pictureboxes samt startmenyn
            Controls.Remove(bullet);
            Controls.Remove(ufo);
            Controls.Remove(menuBackround);
            Controls.Remove(textLabel);
            Controls.Remove(startButton);
            Controls.Remove(exitButton);
            //Byter positition och utseende på scorelabeln
            scoreLabel = Score.gameRunning(scoreLabel);
            //Återställer spelarens positition
            player.Location = new Point(Player.x, Player.y);
            //Återställer spelarens liv
            Player.drawLives(this, lives);
            //Skapar nya aliens och lägger till dem i listan "aliens"
            Alien.createImage(this);
            addAliens();
            //Återställer hastigheter och intervaller på timers
            Alien.speed = 6;
            alienMove.Interval = 30;
            bulletMove.Stop();
            createLaser.Interval = 250;
            Laser.speed = 20;
            //Sätter rundan till runda 1
            stage = 1;
            gameOver = false;
            //Startar en delay innan rundan startar
            startStageDelay.Start();
        }
    }
}
