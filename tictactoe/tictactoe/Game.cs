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
    public partial class Game : UserControl
    {
        WebSocket client;
        string enemy;
        string turn;
        string xo;

        List<Button> buttonai = new List<Button>();
        public Game(WebSocket _client, string _enemy, string _turn, string _xo)
        {
            InitializeComponent();
            InitializeButtons();
            client = _client;
            enemy = _enemy;
            turn = _turn;
            xo = _xo;
            
            lblXO.Text += xo;
            lblEnemy.Text += enemy;

            client.OnMessage += Client_OnMessage;
        }

        public void InitializeButtons()
        {
            int initialX = (int)Convert.ToInt32(ClientRectangle.Width / 2 - 75 * 1.5);

            int buttonSize = 75;
            int x = initialX;

            int y = (int)Convert.ToInt32(ClientRectangle.Height / 2 - 75 * 1.5); ;
            for (int i = 0; i < 9; i++)
            {
                Button btn = new Button()
                {
                    Size = new Size(buttonSize, buttonSize),
                    Text = "",
                    Location = new Point(x, y),
                    Tag = i
                };
                buttonai.Add(btn);
                this.Controls.Add(buttonai[i]);
                btn.Click += Btn_Click;
                btn.Font = new Font("Arial", 24, FontStyle.Bold);
                x += buttonSize;

                
                if ((i+1) % 3 == 0 && i != 0)
                {
                    y += buttonSize;
                    x = initialX;
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Debug.WriteLine(btn.Tag);
            
            if(btn.Text == "")
            {
                client.Send($"{NotifyType.XOPlaced}|{xo}&{btn.Tag}");
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

            if(msgType == NotifyType.XOShow)
            {
                string xo = additionalMessage.Split("&")[0];
                int pos = Int32.Parse(additionalMessage.Split("&")[1]);


                buttonai[pos].Invoke((MethodInvoker)delegate
                {
                    buttonai[pos].Text = xo;
                });
            }
            else if (msgType == NotifyType.HighlightButtons)
            {
                int p1 = Int32.Parse(additionalMessage.Split("&")[0]);
                int p2 = Int32.Parse(additionalMessage.Split("&")[1]);
                int p3 = Int32.Parse(additionalMessage.Split("&")[2]);
                Color color = new Color();

                if(additionalMessage.Split("&")[3] == "red")
                {
                    color = Color.Red;
                }
                else if(additionalMessage.Split("&")[3] == "green")
                {
                    color = Color.Green;
                }

                buttonai[p1].Invoke((MethodInvoker)delegate{buttonai[p1].BackColor = color;});
                buttonai[p2].Invoke((MethodInvoker)delegate{buttonai[p2].BackColor = color; });
                buttonai[p3].Invoke((MethodInvoker)delegate{buttonai[p3].BackColor = color; });

                for(int i=0; i<9; i++)
                {
                    buttonai[i].Invoke((MethodInvoker)delegate { buttonai[i].Enabled = false; });
                }
            }
            else if(msgType == NotifyType.ShowWinningMsg)
            {
                Label lbl = new Label
                {
                    AutoSize = false,
                    Text = "You Won!",
                    Font = new Font("Arial", 20),
                    Size = new Size(150, 30),
                    ForeColor = Color.Green,
                    Location = new Point(440, 380)
                };
                this.Invoke((MethodInvoker)delegate { this.Controls.Add(lbl); });

                Button playAgainBtn = new Button
                {
                    Text = "Play Again",
                    Size = new Size(100, 30),
                    Location = new Point(450, 430)
                };
                playAgainBtn.Click += PlayAgainBtn_Click;
                this.Invoke((MethodInvoker)delegate { this.Controls.Add(playAgainBtn); });

                

            }
            else if(msgType == NotifyType.ShowLossMsg)
            {
                Label lbl = new Label
                {
                    AutoSize = false,
                    Text = "You Lost.",
                    Font = new Font("Arial", 20),
                    Size = new Size(150, 30),
                    ForeColor = Color.Red,
                    Location = new Point(440, 380)
                };
                this.Invoke((MethodInvoker)delegate { this.Controls.Add(lbl); });

                Button playAgainBtn = new Button
                {
                    Text = "Play Again",
                    Size = new Size(100, 30),
                    Location = new Point(450, 430)
                };
                playAgainBtn.Click += PlayAgainBtn_Click;
                this.Invoke((MethodInvoker)delegate { this.Controls.Add(playAgainBtn); });
            }
        }

        private void PlayAgainBtn_Click(object sender, EventArgs e)
        {
            Lobbypage lobbypage = new Lobbypage(client, true, (Form1)this.Parent);
            Form1 myP = (Form1)this.Parent;
            myP.Invoke((MethodInvoker)delegate
            {
                myP.Controls.Clear();
                myP.Controls.Add(lobbypage);
            });
        }
    }
}
