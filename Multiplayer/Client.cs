using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Eindopdracht_cSharp.Game;

namespace Eindopdracht_cSharp.Multiplayer
{
    public class Client
    {
        private int player;
        private GameField game;
        public String IP { get; set; }
        private NetworkStream ServerStream;
        public Color myColor { get; set; }
        Color otherColor;
        private bool myTurn = false;
        private bool waiting = true;
        private string color;

        public Client(string ip)
        {
            IP = ip;
            connectToServer(ip);
            game = new GameField(7, this);
            game.Show();
            game.PaintComponent();
            new Thread(this.run).Start();
        }

        private void connectToServer(string ip)
        {
            TcpClient player = new TcpClient(ip, 8080);
            ServerStream = player.GetStream();
        }

        public bool IsMyTurn()
        {
            return myTurn;
        }

        public void setMyTurn(bool turn)
        {
            myTurn = turn;
        }

        public void run()
        {

            byte[] buffer = new byte[4];
            player = ServerStream.Read(buffer, 0, 4);
            var sendInt = BitConverter.ToInt32(buffer, 0);

            if (sendInt == (int)GameConstants.Player1)
            {
                Console.WriteLine("player 1");
                myColor = Color.Red;
                color = "RED";
                otherColor = Color.Yellow;
                myTurn = true;
            }
            else if (sendInt == (int)GameConstants.Player2)
            {
                Console.WriteLine("player 2");
                myColor = Color.Yellow;
                otherColor = Color.Red;
                color = "YELLOW";
                updateIt();
            }



            while (true)
            {

                if (sendInt == (int)GameConstants.Player1)
                {
                    WaitForPlayerAction();
                    RecieveInfoFromServer();
                }
                else if (sendInt == (int)GameConstants.Player2)
                {
                    RecieveInfoFromServer();
                    WaitForPlayerAction();
                }

            }
        }

        public void updateIt()
        {
            game.canClick = true;
            string text = $"You are player {color}";
            Console.WriteLine(text);
            game.setText(text);
            if (color == "RED")
            {
                game.ChangeText("\nIt is your turn.");
            } else
            {
                game.ChangeText("\nIt is not your turn.");
            }
        }

        public void WaitForPlayerAction()
        {
            while (waiting)
            {
                ServerStream.ReadTimeout = 100;
            }
            waiting = true;
        }

        private void SendMove(int i)
        {
            var buf = BitConverter.GetBytes(i);
            ServerStream.Write(buf, 0, buf.Length);
        }

        public void RecieveInfoFromServer()
        {
            byte[] buffer = new byte[4];
            while (true)
            {
                if (ServerStream.DataAvailable)
                {
                    int colum = ServerStream.Read(buffer, 0, 4);
                    break;
                }
            }
            var column = BitConverter.ToInt32(buffer, 0);

            myTurn = true;
            game.ChangeText("\nIt is your turn.");
            Coin[,] board = game.Board;

            for (int i = game.Rows; i > 0; i--)
            {
                Coin tempCoin = board[column, i - 1];
                if (tempCoin.color == Color.White)
                {
                    tempCoin.color = otherColor;
                    break;

                }
            }
        }

        public void MyTurn(int column)
        {
            waiting = false;
            SendMove(column);
        }

        public void newGame()
        {
            if (myColor == Color.Red)
            {
                myTurn = true;
                game.ChangeText("\nIt is your turn.");
                var buf = BitConverter.GetBytes(-1);
                ServerStream.Write(buf, 0, buf.Length);
            }
            else if (myColor == Color.Yellow)
            {
                myTurn = false;
                game.ChangeText("\nIt is not your turn.");
                var buf = BitConverter.GetBytes(-1);
                ServerStream.Write(buf, 0, buf.Length);
            }
        }

    }
}
