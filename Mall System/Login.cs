using MallSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mall_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        
        private void btn_log_Click(object sender, EventArgs e)
        {
            User u = new User(txt_mail.Text, txt_pass.Text);
            if(u.Login())
            {

                int id2 = u.Renter_id;
                Registration r = new Registration(u.jo,id2);
                this.Hide();
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Welcome Mr/ "+u.name, ToolTipIcon.Info);
                r.ShowDialog();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(2000, "Welcome", "Inccorect Information", ToolTipIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(2000, "Welcome", "Welcome To Our System", ToolTipIcon.Info);
        }

        private void txt_mail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 'ا' && e.KeyChar <= 'ى' || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
