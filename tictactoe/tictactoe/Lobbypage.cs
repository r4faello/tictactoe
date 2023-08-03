using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace tictactoe
{
    public partial class Lobbypage : UserControl
    {
        
        WebSocket client;
        Form1 parent;
        public Lobbypage(WebSocket _client, bool load, Form1 _parent)
        {
            InitializeComponent();

            client = _client;
            parent = _parent;
            client.Send(NotifyType.Userexists.ToString());
            client.OnMessage += Client_OnMessage;

            if (load)
            {
                btnPlay.Visible = false;

                PictureBox loading = new PictureBox
                {
                    Name = "loading",
                    Size = new Size(225, 150),
                    Location = new Point((ClientRectangle.Width / 2) - (225 / 2), 300),
                    Image = tictactoe.Properties.Resources.loading1
                };
                this.Controls.Add(loading);
                lblLookingForPlayers.Visible = true;


                client.Send($"{NotifyType.SearchingForPlayers}");
            }

        }

        private void Client_OnMessage(object sender, MessageEventArgs e)
        {
            NotifyType msgType;
            string additionalMessage = "";
            if (e.Data.Contains("|"))
            {
                var msg = e.Data.Split("|");
                msgType = (NotifyType)Enum.Parse(typeof(NotifyType), msg[0]);
                additionalMessage = msg[1];
            }
            else
            {
                msgType = (NotifyType)Enum.Parse(typeof(NotifyType), e.Data);
            }


            if(msgType == NotifyType.GameStarted)
            {
                string enemy = additionalMessage.Split("&")[0];
                string turn = additionalMessage.Split("&")[1];
                string xo = "";
                if (bool.Parse(additionalMessage.Split("&")[2]))
                {
                    xo = "X";
                }
                else
                {
                    xo = "O";
                }
                Debug.WriteLine(enemy);
                Debug.WriteLine(turn);
                Debug.WriteLine(xo);

                Game gamepage = new Game(client, enemy, turn, xo);
                parent.Invoke((MethodInvoker)delegate
                {
                    parent.Controls.Clear();
                    parent.Controls.Add(gamepage);
                });
                
                return;
            }
        }


        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Visible = false;

            PictureBox loading = new PictureBox
            {
                Name = "loading",
                Size = new Size(225, 150),
                Location = new Point((ClientRectangle.Width/2)-(225/2), 300),
                Image = tictactoe.Properties.Resources.loading1
            };
            this.Controls.Add(loading);
            lblLookingForPlayers.Visible = true;

            
            client.Send($"{NotifyType.SearchingForPlayers}");



        }
    }
}
