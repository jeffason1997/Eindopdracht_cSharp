using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eindopdracht_cSharp.Multiplayer
{
    public partial class ServerForm : Form
    {
        Client client;
        public ServerForm(Client c)
        {
            client = c;
            InitializeComponent();
            this.Size = new Size(400,400);
            this.Text = "Multiplayer server";
            new Thread(new ThreadStart(this.run)).Start();
        }

        public void writeInDialog(string message)
        {
            serverDialog.Invoke((MethodInvoker) delegate { serverDialog.AppendText(message + "\n"); });
        }

        public void run()
        {
            TcpListener serverSocket = new TcpListener(8080);
            TcpClient player1 = default(TcpClient);
            TcpClient player2 = default(TcpClient);

            int sessionCounter = 1;

            serverSocket.Start();

            while (true)
            {
                writeInDialog($"{DateTime.Now} : Wait for Players to join session {sessionCounter}");

                player1 = serverSocket.AcceptTcpClient();
                NetworkStream networkStream = player1.GetStream();
                networkStream.WriteByte((Byte)GameConstants.Player1);
                writeInDialog($"{DateTime.Now} : Player 1 joined session {sessionCounter}\n");
               

                player2 = serverSocket.AcceptTcpClient();
                NetworkStream networkStream2 = player2.GetStream();
                networkStream2.WriteByte((Byte)GameConstants.Player2);
                writeInDialog($"{DateTime.Now} : Player 2 joined session {sessionCounter}\n");

                writeInDialog($"{DateTime.Now} : start a thread for session {sessionCounter}\n");

                HandleASession task = new HandleASession(player1,player2);
                Thread taskThread = new Thread(new ThreadStart(task.run));
                taskThread.Start();
                client.updateIt();
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
