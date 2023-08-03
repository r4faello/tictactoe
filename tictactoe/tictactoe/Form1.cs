using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe
{
    public partial class Form1 : Form
    {
        public static Control.ControlCollection controls;
        List<Button> buttons = new List<Button>();


        public void InitializeFormSize()
        {
            var rec = ClientRectangle;
            this.Height = 500 + this.Height-rec.Height;
            this.Width = 1000 + this.Width - rec.Width;
        }


        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeFormSize();

            Loginpage loginpage = new Loginpage();
            this.Controls.Add(loginpage);
            controls = this.Controls;
        }

    }
}
