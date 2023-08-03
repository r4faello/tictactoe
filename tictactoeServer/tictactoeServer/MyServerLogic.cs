using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;

namespace tictactoeServer
{
    public enum NotifyType
    {
        Userexists,
        UserAlreadyExists,
        AccountSuccessfullyCreated,
        UserDoNotExist,
        SuccessfullyLoggedIn,
        SearchingForPlayers,
        GameStarted,
        XOPlaced,
        XOShow,
        ShowWinningMsg,
        ShowLossMsg,
        HighlightButtons
    }


    public class MyServerLogic : WebSocketBehavior
    {

        public bool checkIfUserExists(string userName, ref Users users)
        {
            users = JsonConvert.DeserializeObject<Users>(System.IO.File.ReadAllText(dataFilePath));
            foreach (var user in users.users)
            {
                if (user.userName == userName)
                {
                    return true;
                }
            }
            return false;
        }

        public void matchLookingUsers()
        {
            if (Form1.lookingUsers.Count == 2)
            {
                GameData gData = new GameData {
                    users = new List<User> { Form1.lookingUsers[0], Form1.lookingUsers[1] },
                    xPlayer = Form1.lookingUsers[0].userName,
                    oPlayer = Form1.lookingUsers[1].userName,
                    tableValues = new string[,] { { "", "", "" }, { "", "", "" }, { "", "", "" } },
                    turn = Form1.lookingUsers[0].userName
                };
                Form1.allGames.Add(gData);

                Form1.lookingUsers[0].socket.Send($"{NotifyType.GameStarted}|{Form1.lookingUsers[1].userName}&{gData.turn}&{gData.xPlayer == Form1.lookingUsers[0].userName}");
                Form1.lookingUsers[1].socket.Send($"{NotifyType.GameStarted}|{Form1.lookingUsers[0].userName}&{gData.turn}&{gData.xPlayer == Form1.lookingUsers[1].userName}");

                Form1.lookingUsers.Clear();

            }




        }


        string dataFilePath = @"C:\Users\Admin\Desktop\testas.json";

        protected override void OnClose(CloseEventArgs e)
        {
            Debug.WriteLine(e.Reason);
            base.OnClose(e);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Debug.WriteLine(e.Message);
            base.OnError(e);
        }

        protected override void OnMessage(MessageEventArgs e)
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

            if(msgType == NotifyType.SearchingForPlayers)
            {

                foreach (var item in Form1.onlineUsers)
                {
                    Debug.WriteLine(item.userName);
                }
                
                User useris = new User();
                foreach (var user in Form1.onlineUsers)
                {
                    if (user.socket == Context.WebSocket)
                    {
                        useris = user;
                    }
                }
                
                
                Form1.lookingUsers.Add(useris);

                matchLookingUsers();
            }
            else if(msgType == NotifyType.XOPlaced)
            {
                string xo = additionalMessage.Split("&")[0];
                int pos = Int32.Parse(additionalMessage.Split("&")[1]);


                foreach (GameData gData in Form1.allGames.ToList())
                {
                    foreach (User usr in gData.users)
                    {
                        if(usr.socket == Context.WebSocket)
                        {
                            if(usr.userName != gData.turn)
                            {
                                return;
                            }

                            gData.tableValues[pos / 3, pos % 3] = xo;

                            Debug.WriteLine(gData.users[0].userName);
                            Debug.WriteLine(gData.users[1].userName);
                            gData.users[0].socket.Send($"{NotifyType.XOShow}|{xo}&{pos}");
                            gData.users[1].socket.Send($"{NotifyType.XOShow}|{xo}&{pos}");

                            if(usr.userName == gData.users[0].userName)
                            {
                                gData.turn = gData.users[1].userName;
                            }
                            else
                            {
                                gData.turn = gData.users[0].userName;
                            }


                            checkWinLoss(gData);



                        }
                    }
                }




            }



        }

        private void checkWinLoss(GameData gameData)
        {
            string[,] tblVal = gameData.tableValues;
            User u1 = gameData.users[0];
            User u2 = gameData.users[1];
            string xP = gameData.xPlayer;

            User xPlayer;
            User oPlayer;
            if(u1.userName == xP)
            {
                xPlayer = u1;
                oPlayer = u2;
            }
            else
            {
                xPlayer = u2;
                oPlayer = u1;
            }

            string xo = "X";

            int[] matchPos = new int[] { };
            // Horizontal
            if (!tblVal[0, 0].IsNullOrEmpty() && tblVal[0, 0] == tblVal[0, 1] && tblVal[0, 1] == tblVal[0, 2])
            {
                matchPos = new int[] { 0, 1, 2};
                xo = tblVal[0, 0];
            }
            else if(!tblVal[1, 0].IsNullOrEmpty() && tblVal[1, 0] == tblVal[1, 1] && tblVal[1, 1] == tblVal[1, 2])
            {
                matchPos = new int[] { 3, 4, 5 };
                xo = tblVal[1, 0];
            }
            else if(!tblVal[2, 0].IsNullOrEmpty() && tblVal[2, 0] == tblVal[2, 1] && tblVal[2, 1] == tblVal[2, 2])
            {
                matchPos = new int[] { 6, 7, 8 };
                xo = tblVal[2, 0];
            }
            // Vertical
            else if(!tblVal[0, 0].IsNullOrEmpty() && tblVal[0, 0] == tblVal[1, 0] && tblVal[1, 0] == tblVal[2, 0])
            {
                matchPos = new int[] { 0, 3, 6 };
                xo = tblVal[0, 0];
            }
            else if (!tblVal[0, 1].IsNullOrEmpty() && tblVal[0, 1] == tblVal[1, 1] && tblVal[1, 1] == tblVal[2, 1])
            {
                matchPos = new int[] { 1, 4, 7 };
                xo = tblVal[0, 1];
            }
            else if (!tblVal[0, 2].IsNullOrEmpty() && tblVal[0, 2] == tblVal[1, 2] && tblVal[1, 2] == tblVal[2, 2])
            {
                matchPos = new int[] { 2, 5, 8 };
                xo = tblVal[0, 2];
            }
            // Diagonal
            else if(!tblVal[0, 0].IsNullOrEmpty() && tblVal[0, 0] == tblVal[1, 1] && tblVal[1, 1] == tblVal[2, 2])
            {
                matchPos = new int[] { 0, 4, 8 };
                xo = tblVal[0, 0];
            }
            else if(!tblVal[2, 0].IsNullOrEmpty() && tblVal[2, 0] == tblVal[1, 1] && tblVal[1, 1] == tblVal[0, 2])
            {
                matchPos = new int[] { 6, 5, 2 };
                xo = tblVal[2, 0];
            }


            if (matchPos.Length != 0)
            {
                if(xo == "X")
                {
                    xPlayer.socket.Send($"{NotifyType.HighlightButtons}|{matchPos[0]}&{matchPos[1]}&{matchPos[2]}&green");
                    oPlayer.socket.Send($"{NotifyType.HighlightButtons}|{matchPos[0]}&{matchPos[1]}&{matchPos[2]}&red");

                    xPlayer.socket.Send(NotifyType.ShowWinningMsg.ToString());
                    oPlayer.socket.Send(NotifyType.ShowLossMsg.ToString());
                }
                else if(xo == "O")
                {
                    oPlayer.socket.Send($"{NotifyType.HighlightButtons}|{matchPos[0]}&{matchPos[1]}&{matchPos[2]}&green");
                    xPlayer.socket.Send($"{NotifyType.HighlightButtons}|{matchPos[0]}&{matchPos[1]}&{matchPos[2]}&red");

                    oPlayer.socket.Send(NotifyType.ShowWinningMsg.ToString());
                    xPlayer.socket.Send(NotifyType.ShowLossMsg.ToString());
                }
                Form1.allGames.RemoveAll(game => game == gameData);
            }
            Debug.WriteLine(Form1.allGames.Count);
        }

        protected override void OnOpen()
        {
            Debug.WriteLine("connected");

            string userName = Context.QueryString["name"];
            string userPass = Context.QueryString["pass"];
            string conType = Context.QueryString["conType"];



            if (conType == "login")
            {
                // Tikrinam ar toks useris egzistuoja
                Users users = new Users();
                if (!checkIfUserExists(userName, ref users))
                {
                    Send(NotifyType.UserDoNotExist.ToString());
                    return;
                }

                Form1.onlineUsers.Add(new User{ userName = userName, userPass = userPass, socket = Context.WebSocket});

                Send($"{NotifyType.SuccessfullyLoggedIn}|{Form1.onlineUsers}");
                




            }
            else if(conType == "register")
            {
                Users users = new Users();
                if (checkIfUserExists(userName, ref users))
                {
                    Send(NotifyType.UserAlreadyExists.ToString());
                    return;
                }

                User user = new User { userName = userName, userPass = userPass };
                users.users.Add(user);

                string strUsers = JsonConvert.SerializeObject(users, Formatting.Indented);
                System.IO.File.WriteAllText(dataFilePath, strUsers);
                
                Send(NotifyType.AccountSuccessfullyCreated.ToString());

                
            }





            //Debug.WriteLine(userList[0].userName);


            //Debug.WriteLine(userName);
            //Debug.WriteLine(userPass);
        }
    }
}
