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
using WebSocketSharp.Server;
using System.Reflection;
using System.Diagnostics;

namespace tictactoeServer
{
    public class User
    {
        public string userName { get; set; }
        public string userPass { get; set; }
        public WebSocket socket { get; set; }
    }

    public class Users
    {
        public List<User> users { get; set; }
    }

    public class GameData
    {
        public List<User> users { get; set; }
        public string xPlayer { get; set; }
        public string oPlayer { get; set; }
        public string[,] tableValues { get; set; }
        public string turn { get; set; }

    }

    public partial class Form1 : Form
    {
        public static List<User> onlineUsers = new List<User>();
        public static List<User> lookingUsers = new List<User>();
        public static List<GameData> allGames = new List<GameData>();

        WebSocketServer server = new WebSocketServer(666);
        public Form1()
        {
            InitializeComponent();
            server.AddWebSocketService<MyServerLogic>("/chatApp");
            server.Start();
        }
    }
}