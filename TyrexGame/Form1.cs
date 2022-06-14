namespace TyrexGame
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        // untuk kecepatan lompatan
        int jumpSpeed = 10;
        // untuk tinggi lompatan
        int force = 15;
        int score = 0;
        bool isGameOver = false;
        int pipeSpeedObstacle = 6;
        int pipeSpeedEagle = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void GameTimer(object sender, EventArgs e)
        {
            tyrex.Top += jumpSpeed;
            obstacle1.Left -= pipeSpeedObstacle;
            obstacle2.Left -= pipeSpeedObstacle;
            eagle.Left -= pipeSpeedEagle;

            labelScore.Text = "Score : " + score;
            
            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            { 
                jumpSpeed = -10;
                force -= 1;
            } else
            {
                jumpSpeed = 10;
            }

            if (tyrex.Top > 298 && jumping == false)
            {
                force = 15;
                tyrex.Top = 299;
                jumpSpeed = 0;
            }

            if (obstacle1.Left < -30)
            {
                score++;
                obstacle1.Left = 810;
            }

            if (obstacle2.Left < -50)
            {
                score++;
                obstacle2.Left = 850;
            }

            if (eagle.Left < -500)
            {
                score += 2;
                eagle.Left = 1300;
            }

            if (score > 50)
            {
                pipeSpeedObstacle = 8;
                pipeSpeedEagle = 10;
            } 
            else if (score > 100)
            {
                pipeSpeedObstacle = 10;
                pipeSpeedEagle = 12;
            }
            else if (score > 200)
            {
                pipeSpeedObstacle = 12;
                pipeSpeedEagle = 14;
            }

            if (tyrex.Bounds.IntersectsWith(obstacle1.Bounds) || tyrex.Bounds.IntersectsWith(obstacle2.Bounds)
                || tyrex.Bounds.IntersectsWith(eagle.Bounds))
            {
                gameTimer.Stop();
                tyrex.Image = Properties.Resources.dead;
                labelScore.Text += " Press R to Restart Game";
                isGameOver = true;
            }
        }

        private void Location()
        {
            tyrex.Location = new Point(80, 299);
            obstacle1.Location = new Point(477, 290);
            obstacle2.Location = new Point(619, 300);
            eagle.Location = new Point(1231, 218);
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameReset();
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && jumping == false)
            {
                jumping = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                tyrex.Top = 290;
            }

            switch (e.KeyCode)
            {
                case Keys.Right:
                    if ((tyrex.Left + 20) < (this.Width - tyrex.Width))
                    {
                        tyrex.Left += 15;
                    }
                    break;
                case Keys.Left:
                    if ((tyrex.Left - 10) > 0)
                    {
                        tyrex.Left -= 15;
                    }
                    break;
            }
        }

        private void GameReset()
        {
            Location();
            jumpSpeed = 10;
            force = 15;
            score = 0;
            isGameOver = false;
            labelScore.Text = "Score : " + score;
            tyrex.Image = Properties.Resources.running;
            tyrex.Top = 299;
            gameTimer.Start();
        }
    }
}