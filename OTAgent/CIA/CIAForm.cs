
using System;
using System.Windows.Forms;
using System.Collections.Generic;

using opentoken;
using AboutJoeWare_Lib;
using AflacCommonObjects;
using Security.String.Extensions;

namespace CIA
{
    public partial class CIAForm : Form
    {
        private readonly List<TokenPassword> Passwords = new List<TokenPassword>();

        // ------------------------------------------------

        public CIAForm()
        {
            InitializeComponent();
            LoadPasswords();
        }

        // ------------------------------------------------

        private void OnRead(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            dgvData.DataSource = null;
            tbEnv.Text = string.Empty;
            tbEntityType.Text = string.Empty;

            var success = false;
            var cfg = new ConfigFileReader();

            foreach(var tConfig in cfg.ReadObjList<AgentConfiguration>("Configurations"))
            {
                foreach(var pwd in Passwords)
                {
                    tConfig.SetPassword(pwd.Password.Unwrap(), Token.CipherSuite.AES_128_CBC);

                    OTAgent agent = new OTAgent(tConfig);

                    try
                    {
                        var detail = agent.ReadExpiredToken(tbToken.Text);
                        dgvData.DataSource = GetDataSource(detail);
                        tbEnv.Text = pwd.Environment;
                        tbEntityType.Text = pwd.EntityType;
                        success = true;
                        break;
                    }
                    catch(Exception)
                    {
                        // Try the next password
                    }
                }

                if(success) { break; }
            }

            if(dgvData.DataSource == null)
            {
                MessageBox.Show(tbToken.Text, "Cannot Read Token");
            }

            Cursor = Cursors.Default;
        }

        // ------------------------------------------------

        private void OnTokenClick(object sender, EventArgs e)
        {
            tbToken.SelectAll();
        }

        // ------------------------------------------------

        private void OnAbout(object sender, EventArgs e)
        {
            using(var dlg = new AboutJoeWareDlg())
            {
                dlg.ShowDialog();
            }
        }

        // ------------------------------------------------

        private List<DataItem> GetDataSource(IDictionary<string, string> dict)
        {
            var retVal = new List<DataItem>();

            foreach(var item in (Dictionary<string, string>)dict)
            {
                retVal.Add(new DataItem() { Name = item.Key, Value = item.Value });
            }

            return retVal;
        }

        // ------------------------------------------------

        private void LoadPasswords()
        {
            var cfg = new ConfigFileReader();

            foreach(var tPwd in cfg.ReadObjList<TokenPassword>("CredLocations"))
            {
                tPwd.Password = RegCrypt.ReadRegistry(tPwd.CredLocation, "password");
                Passwords.Add(tPwd);
            }
        }
    }
}
