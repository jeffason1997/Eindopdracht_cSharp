using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eindopdracht_cSharp.Multiplayer;
using Timer = System.Windows.Forms.Timer;

namespace Eindopdracht_cSharp.Game
{
    public partial class GameField : Form
    {
        public Client Me { get; set; }
        public int Rows { get; }
        public int Columns { get; }
        public Label label { get; set; }
        private int Diameter;
        public List<Coin> Coins = new List<Coin>();
        public Coin[,] Board { get; }
        public Region AreaBoard;
        private Color LastColor { get; set; }
        private Timer _sessionTimer;
        private Thread win;
        private Graphics g;
        public bool canClick { get; set; } = false;
        public string text;

        public GameField(int columns, Client me)
        {
            InitializeComponent();

            this.Text = "Connect Four";
            this.Size = new Size(1200, 800);


            g = this.CreateGraphics();
            LastColor = Color.White;
            Columns = columns + 1;
            Me = me;
            Rows = Columns;
            Board = new Coin[Columns, Rows];
            Diameter = 1;
            AreaBoard = new Region(new Rectangle(0, 0, 100, 100));

            this.Click += delegate { MouseEvent(); };

            _sessionTimer = new Timer
            {
                Interval = 1000
            };
            _sessionTimer.Tick += (it, works) => updateIt();
            _sessionTimer.Start();

            WinChecker w = new WinChecker(this, Columns, Rows);
            win = new Thread(w.run);



            label = new Label();
            label.AutoSize = true;
            label.Text = $"Waiting for player 2.\nServer IP: {Me.IP}";
            label.Location = new Point(0, 13);
            this.Controls.Add(label);
            this.DoubleBuffered = true;


        }

        public void setText(string text)
        {
            this.text = text;
        }


        public void ChangeText()
        {
            if (this.label.InvokeRequired)
            {
                this.label.BeginInvoke((MethodInvoker)delegate () { this.label.Text = text; ; });
            }
            else
            {
                this.label.Text = text; ;
            }
        }

        public void ChangeText(string t)
        {
            if (this.label.InvokeRequired)
            {
                this.label.BeginInvoke((MethodInvoker)delegate () { this.label.Text = text+t; ; });
            }
            else
            {
                this.label.Text = text+t; ;
            }
        }

        public void MouseEvent()
        {
            PaintComponent();
            Point position = this.PointToClient(Cursor.Position);
            if (Me.IsMyTurn() && Me.myColor != LastColor && canClick)
            {
                foreach (Coin coin in Coins)
                {

                    Point tempPoint = coin.getTempMidPoint();
                    double first = Math.Sqrt((Math.Abs(((position.X - tempPoint.X) * (position.X - tempPoint.X)) - ((position.Y - tempPoint.Y) * (position.Y - tempPoint.Y)))));
                    if (first < (coin.radius / 2) && (Math.Abs(position.X - tempPoint.X)) <= (coin.radius / 2) && (Math.Abs(position.Y - tempPoint.Y)) <= (coin.radius / 2))
                    {

                        for (int i = Rows; i > 0; i--)
                        {
                            Color col = Me.myColor;
                            if (Board[coin.column, i - 1].color == Color.White && col != LastColor)
                            {
                                Board[coin.column, i - 1].color = col;

                                Console.WriteLine($"Coin:{Board[coin.column, i - 1].column}");
                                Me.MyTurn(Board[coin.column, i - 1].column);
                                Me.setMyTurn(false);
                                ChangeText("\nIt is not your turn.");
                                break;
                            }
                        }
                    }
                }
            }
        }



        public void updateIt()
        {
            PaintComponent();
        }

        public void makeBoard(int width)
        {
            int height, startX, startY, space, count, xClip, yClip;
            height = (width / Columns) * (Rows);
            startX = (this.Width / 2) - (width / 2);
            startY = (this.Height / 2) - (height / 2);
            space = (int)(Diameter / (Columns + 1));
            count = (int)(Diameter + space);
            xClip = startX + (space / 2);
            yClip = startY = (space / 2);
            AreaBoard = new Region(new Rectangle(startX, startY, width, height));

            for (int w = 0; w < Columns; w++)
            {
                for (int h = 0; h < Rows; h++)
                {
                    Board[w, h] = new Coin(Diameter, new Point(xClip, yClip), Color.White, w);
                    Coins.Add(Board[w, h]);
                    GraphicsPath path = new GraphicsPath();
                    path.AddEllipse(xClip, yClip, Diameter, Diameter);
                    AreaBoard.Exclude(path);
                    yClip += count;
                }
                yClip = startY + (space / 2);
                xClip += count;
            }

            if (!win.IsAlive)
            {
                win.Start();
            }
        }

        public void Winner()
        {
            win.Abort();
        }

        public void PaintComponent()
        {
            this.BackColor = Color.FromArgb(103, 250, 166);
            SolidBrush brush = new SolidBrush(Color.Blue);

            int width = ((this.Height) / (Columns + 1)) * Columns;
            if (Diameter != (width / (Columns + 1)))
            {
                Diameter = width / (Columns + 1);
                makeBoard(width);
            }
            foreach (Coin coin in Board)
            {
                if (coin != null)
                {
                    coin.draw(g);
                }
            }


            g.FillRegion(brush, AreaBoard);

            //this is for debugging
            /*foreach (Coin coin in Board)
            {
                if (coin != null)
                {
                    counter++;
                    coin.draw2(g,counter);
                }
            }*/
        }

        private void GameField_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(Environment.ExitCode);
        }
    }

}
