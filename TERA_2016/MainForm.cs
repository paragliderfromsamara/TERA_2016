using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TERA_2016
{
    public partial class mainForm : Form
    {
        public bool isTestApp = Properties.Settings.Default.isTestApp;
        public string user_type = "undefined";
        public string user_id = "undefined";
        private dbUsersForm dbUsersForm = null;
        public mainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataMigration dm = new dataMigration();
            Signin signForm = new Signin();
            DialogResult result = signForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel) this.Close();
            else if (result == System.Windows.Forms.DialogResult.OK)
            {
                sesUserName.Text = String.Format("{0} {1}.{2}.", signForm.userLastName, signForm.userFirstName[0], signForm.userThirdName[0]);
                sesTabNum.Text = "Таб.номер: " + signForm.userTabNum;
                sesRole.Text = "Должность: " + signForm.userRole;
                user_type = signForm.userRole;
                user_id = signForm.userId;
                signForm.Dispose();
                checkTabsPermission();
            }
        }
        private void checkTabsPermission()
        {
            UserGrants grants = new UserGrants(user_type);

        }
        public void switchMenuStripItems(bool v)
        {
            
        }

        private void dbUsersMenuItem_Click(object sender, EventArgs e)
        {
            dbUsersForm = new dbUsersForm(this);
            dbUsersForm.MdiParent = this;
            dbUsersForm.Show();
        }
    }
}
