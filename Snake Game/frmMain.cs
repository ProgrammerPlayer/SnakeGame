using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class frmMain : Form
    {
        private int direction = 0;
        private int score = 1;
        private Timer gameloop = new Timer();
        private Random rand = new Random();
        private Graphics graphics;
        private Snake snake;
        private Food food; 
        public frmMain()
        {
            InitializeComponent();
            snake = new Snake();
            food = new Food(rand);
            gameloop.Interval = 75;
            gameloop.Tick += Update;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    if(lblMenu.Visible)
                    {
                        lblMenu.Visible = false;
                        gameloop.Start();
                    }
                    break;
                case Keys.Space:
                    if (!lblMenu.Visible)
                        gameloop.Enabled = (gameloop.Enabled) ? false : true;
                    
                        break;
                case Keys.Right:
                    if (direction != 2)
                        direction = 0;
                    break;
                case Keys.Down:
                    if (direction != 3)
                        direction = 1;
                        break;
                case Keys.Left:
                    if (direction != 0)
                        direction = 2;
                        break;
                case Keys.Up:
                    if (direction != 1)
                        direction = 3;
                        break;
            }
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            graphics = this.CreateGraphics();
             snake.Draw(graphics);
            food.Draw(graphics);
        }
        private void Update(object sender,EventArgs e)
        {
            this.Text = string.Format("Snake - Score:{0}", score);
            snake.Move(direction);
            for (int i = 1; i < snake.Body.Length; i++)
                if (snake.Body[0].IntersectsWith(snake.Body[i]))
                    Restart();
            if (snake.Body[0].X < 0 || snake.Body[0].X > 290)
                Restart();
            if (snake.Body[0].Y < 0 || snake.Body[0].Y > 190)
                Restart();
            if (snake.Body[0].IntersectsWith(food.Piece))
            {
                score++;
                snake.Grow();
                food.Generate(rand);
            }
            this.Invalidate();
        }
        private void Restart()
        {
            gameloop.Stop();
            graphics.Clear(SystemColors.Control);
            snake = new Snake();
            food = new Food(rand);
            direction = 0;
            score = 1;
            lblMenu.Visible = true;
        }
    }
}
