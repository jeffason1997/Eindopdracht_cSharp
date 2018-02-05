using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eindopdracht_cSharp.Game;
using Eindopdracht_cSharp.Multiplayer;

namespace Eindopdracht_cSharp
{
    public partial class HomeGUI : Form
    {
        public HomeGUI()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(102, 250, 166);

            IPLabel.Text = GetLocalIPAddress();

        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            //label1.ForeColor=Color.Blue;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void StartGameLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Client c = new Client(GetLocalIPAddress());
            new ServerForm(c).Show();
            
           
           
        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (IPfield.Text == "")
            {
                Client c = new Client(GetLocalIPAddress());
            } else
            {
                Client c = new Client(IPfield.Text);
            }
        }

        private void StartGameLabel_MouseEnter(object sender, EventArgs e)
        {
            StartGameLabel.ForeColor = Color.DarkRed;
        }

        private void StartGameLabel_MouseLeave(object sender, EventArgs e)
        {
            StartGameLabel.ForeColor = Color.Black;
        }

        private void JoinButton_MouseEnter(object sender, EventArgs e)
        {
            JoinButton.ForeColor = Color.DarkRed;
        }

        private void JoinButton_MouseLeave(object sender, EventArgs e)
        {
            JoinButton.ForeColor = Color.Black;
        }
    }
}
