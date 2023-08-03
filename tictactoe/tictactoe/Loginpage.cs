using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace tictactoe
{
    public partial class Loginpage : UserControl
    {

        WebSocket client;
        public Loginpage()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Registerpage registerpage = new Registerpage();

            Form1 myParent = (Form1)this.Parent;
            myParent.Controls.Clear();
            myParent.Controls.Add(registerpage);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Veikia
            /*
            Game gamepage = new Game();

            Form1 myParent = (Form1)this.Parent;
            myParent.Controls.Clear();
            myParent.Controls.Add(gamepage);
            */

            if (txtUsername.Text.IsNullOrEmpty() || txtPassword.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please specify your username and password.");
                client.Close();
                return;
            }

            client = new WebSocket($"ws://localhost:666/chatApp?name={txtUsername.Text}&pass={txtPassword.Text}&conType=login");
            client.OnOpen += Client_OnOpen;
            client.OnMessage += Client_OnMessage;
            client.Connect();
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

            if (msgType == NotifyType.UserDoNotExist)
            {
                MessageBox.Show("Specified username is not registered.");
                client.Close();
                return;
            }
            else if(msgType == NotifyType.SuccessfullyLoggedIn)
            {
                Debug.WriteLine(additionalMessage);

                Lobbypage lobbypage = new Lobbypage(client, false, (Form1)this.Parent);
                Form1 myParent = (Form1)this.Parent;
                myParent.Invoke((MethodInvoker)delegate
                {
                    myParent.Controls.Clear();
                    myParent.Controls.Add(lobbypage);
                });
                return;
            }
            
        }

        private void Client_OnOpen(object sender, EventArgs e)
        {
            //MessageBox.Show("Connection successful");
            //gbLogin.Enabled = false;
            //gbMain.Enabled = true;
        }
    }
}
