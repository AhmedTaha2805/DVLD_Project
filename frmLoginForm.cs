using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsersBuisnessLayer;
using CurrentUserInformation;
using System.IO;

namespace DVLD_project
{
    public partial class frmLoginForm : Form
    {
        public frmLoginForm()
        {
            InitializeComponent();
            string LoginInfo = File.ReadAllText(CurrentUser.LoginFilePath);
            if (!string.IsNullOrEmpty(LoginInfo))
            {
                chkRemember.Checked = true;
                string[] lines = File.ReadAllLines(CurrentUser.LoginFilePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(txtUserName.Text))
                    {
                        txtUserName.Text = line;
                    }
                    else
                    {
                        txtPassword.Text = line;
                    }
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool Remember = false;
            clsUsers user = clsUsers.FindUser(txtUserName.Text,txtPassword.Text);
            if (user != null)
            {
                File.WriteAllText(CurrentUser.LoginFilePath, string.Empty);
                if (chkRemember.Checked)
                {
                    Remember = true;
                    string[] lines = {txtUserName.Text,txtPassword.Text};
                    File.WriteAllLines(CurrentUser.LoginFilePath, lines);
                }
                CurrentUser.user = user;
                FrmMain frm = new FrmMain();
                this.Hide();
                frm.ShowDialog();
                this.Show();
                if (!Remember)
                {
                    txtUserName.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    chkRemember.Checked = false;
                }
                

            }
            else
            {
                MessageBox.Show("Username/Password are not correct","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
