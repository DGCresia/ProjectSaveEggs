using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectSaveEggs
{
    public partial class Main : Form
    {
        private SqlConnection sqlcon = new SqlConnection();
        private string constr = "SERVER=127.0.0.1,1433; DATABASE=projectSaveEggs" + "UID=qwer; PASSWORD=1234";

        bool goLeft, goRight;
        int speed = 8, score = 0, miss  = 0;

        Random randX = new Random(), randY = new Random();
        PictureBox egg_broken = new PictureBox();

        public Main()
        {
            InitializeComponent();
            LogIn();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            scoreText.Text = "점수 : " + score;
            missText.Text = "놓친 달걀 : " + miss;

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= 18;
                player.Image = Properties.Resources.left;
            }

            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += 18;
                player.Image = Properties.Resources.right;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "eggs")
                {
                    x.Top += speed;

                    if (x.Top + x.Height > this.ClientSize.Height)
                    {
                        egg_broken.Image = Properties.Resources.egg_broken;
                        egg_broken.Location = x.Location;
                        egg_broken.Height = 60;
                        egg_broken.Width = 60;
                        egg_broken.BackColor = Color.Transparent;
                        egg_broken.SizeMode = PictureBoxSizeMode.StretchImage;

                        this.Controls.Add(egg_broken);

                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        miss++;
                        player.Image = Properties.Resources.hurt_right;
                    }

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        score++;
                    }
                }
            }

            if (score > 10)
            {
                speed = 12;
            }

            if (miss > 5)
            {
                gameTimer.Stop();

                var gameOver = MessageBox.Show(
                    "게임오버!"+Environment.NewLine+"재도전은 다시시도, 끝내려면 취소",
                    "게임오버",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Exclamation);

                if (gameOver == DialogResult.Retry)
                {
                    RestartGame();
                }
                else if (gameOver == DialogResult.Cancel)
                {
                    FinishGame();
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }

        private void RestartGame()
        {
            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "eggs")
                {
                    x.Top = randY.Next(80, 300) * -1;
                    x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                }
            }
            player.Left = this.ClientSize.Width / 2;
            player.Image = Properties.Resources.right;

            score = 0;
            miss = 0;
            speed = 8;
            goLeft = false;
            goRight = false;

            gameTimer.Start();
        }

        private void LogIn()
        {
            Form2 logIn = new Form2();
            logIn.ShowDialog();
            RestartGame();
        }

        private void FinishGame()
        {
            Close();
        }
    }
}
