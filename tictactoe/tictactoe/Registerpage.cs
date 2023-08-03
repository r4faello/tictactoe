using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace tictactoe
{
    public partial class Registerpage : UserControl
    {
        WebSocket client;

        public Registerpage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Loginpage loginpage = new Loginpage();

            Form1 myParent = (Form1)this.Parent;
            myParent.Controls.Clear();
            myParent.Controls.Add(loginpage);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.IsNullOrEmpty() || txtPassword.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please specify your username and password.");
                return;
            }
            else if(txtPassword.Text != txtPasswordConfirm.Text)
            {
                MessageBox.Show("Passwords does not match.");
                client.Close();
                return;
            }


            client = new WebSocket($"ws://localhost:666/chatApp?name={txtUsername.Text}&pass={txtPassword.Text}&conType=register");
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

            if (msgType == NotifyType.UserAlreadyExists)
            {
                MessageBox.Show("Username is already taken.");
                client.Close();
                return;
            }
            else if(msgType == NotifyType.AccountSuccessfullyCreated)
            {
                MessageBox.Show("Account has been successfully created. Click Login button to login and start playing with others!");
                client.Close();
                return;
            }
        }

        private void Client_OnOpen(object sender, EventArgs e)
        {
            
        }
    }
}
