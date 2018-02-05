using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht_cSharp.Multiplayer
{
    class HandleASession
    {
        private TcpClient playerRed, playerYellow;

        public HandleASession(TcpClient player1, TcpClient player2)
        {
            playerRed = player1;
            playerYellow = player2;
        }

        public void run()
        {
            NetworkStream StreamRed = playerRed.GetStream();
            NetworkStream StreamYellow = playerYellow.GetStream();

            while (true)
            {

                byte[] bufferRed = new byte[4];
                int player = StreamRed.Read(bufferRed, 0, 4);
                var column = BitConverter.ToInt32(bufferRed, 0);
                SendMove(StreamYellow, column);

                byte[] bufferYellow = new byte[4];
                int player1 = StreamYellow.Read(bufferYellow, 0, 4);
                column = BitConverter.ToInt32(bufferYellow, 0);
                SendMove(StreamRed, column);

            }
        }

        void SendMove(NetworkStream stream, int column)
        {
            var buf = BitConverter.GetBytes(column);
            stream.Write(buf, 0, buf.Length);
        }
    }
}
