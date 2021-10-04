
using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

using X_509_Lib;
using AboutJoeWare_Lib;
using AflacCommonObjects;

namespace X._509_Tool
{
    public partial class CertSearchForm : Form
    {
        private readonly string cr = $"{Environment.NewLine}";

        public CertSearchForm()
        {
            InitializeComponent();

            SetupStoreName();
            SetupSearchType();
            SetupStoreLocation();
        }

        // ------------------------------------------------

        private void OnSearch(object sender, EventArgs e)
        {
            lblCertsFound.Text = "0";
            Cursor = Cursors.WaitCursor;

            if(cbUnknown.Checked)
            {
                tbOut.Text = Find();
            }
            else
            {
                var req = new CertRequest()
                {
                    validOnly = cbValid.Checked,
                    searchValue = tbSearchValue.Text,
                    storeName = ((x509StoreName)cbStoreName.SelectedItem).storeName,
                    searchType = ((x509SearchType)cbSearchType.SelectedItem).searchType,
                    storeLocation = ((x509StoreLocation)cbStoreLocation.SelectedItem).storeLocation,
                };

                tbOut.Text = Search(req);
            }

            Cursor = Cursors.Default;
        }

        // ------------------------------------------------

        private string Search(CertRequest req)
        {
            var currentCount = 0;
            var retVal = string.Empty;
            var text = new StringBuilder();

            int.TryParse(lblCertsFound.Text, out currentCount);

            try
            {
                var certs = X_509_CertTool.GetCertificates(req);

                currentCount += certs.Count;

                lblCertsFound.Text = currentCount.ToString();

                foreach(var cert in certs)
                {
                    text.Append(GetDisplayString(cert, $"{req.storeLocation.ToString()}.{req.storeName.ToString()}"));
                }

                retVal = text.ToString();
            }
            catch(Exception exp)
            {
                retVal = exp.Message;
            }

            return retVal;
        }

        // ------------------------------------------------

        private string Find()
        {
            var retVal = new StringBuilder();

            foreach(var storeName in cbStoreName.Items)
            {
                foreach(var storeLocation in cbStoreLocation.Items)
                {
                    var req = new CertRequest()
                    {
                        validOnly = cbValid.Checked,
                        searchValue = tbSearchValue.Text,
                        storeName = ((x509StoreName)storeName).storeName,
                        searchType = ((x509SearchType)cbSearchType.SelectedItem).searchType,
                        storeLocation = ((x509StoreLocation)storeLocation).storeLocation,
                    };

                    retVal.Append(Search(req));
                }
            }

            return retVal.ToString();
        }

        // ------------------------------------------------

        private void SetupStoreName()
        {
            var cfg = new ConfigFileReader();
            var list = cfg.ReadObjList<x509StoreName>("StoreNames");

            cbStoreName.Items.AddRange(list.ToArray());
            cbStoreName.SelectedIndex = 0;
        }

        // ------------------------------------------------

        private void SetupSearchType()
        {
            var cfg = new ConfigFileReader();
            var list = cfg.ReadObjList<x509SearchType>("SearchTypes");

            cbSearchType.Items.AddRange(list.ToArray());
            cbSearchType.SelectedIndex = 0;
        }

        // ------------------------------------------------

        private void SetupStoreLocation()
        {
            var cfg = new ConfigFileReader();
            var list = cfg.ReadObjList<x509StoreLocation>("StoreLocations");

            cbStoreLocation.Items.AddRange(list.ToArray());
            cbStoreLocation.SelectedIndex = 0;
        }

        // ------------------------------------------------

        private void OnAbout(object sender, EventArgs e)
        {
            var abt = new AboutJoeWareDlg();
            abt.ShowDialog();
        }

        // ------------------------------------------------

        private void OnUnknownSelection(object sender, EventArgs e)
        {
            cbStoreName.Enabled = !cbUnknown.Checked;
            cbStoreLocation.Enabled = !cbUnknown.Checked;
        }

        // ------------------------------------------------

        private string GetDisplayString(X509Certificate2 cert, string location)
        {
            var retVal = new StringBuilder();
            var subject = StringUtil.ParseValue(cert.Subject, "CN=", ',', 1);

            retVal.Append($"Location:           {location}{cr}");
            retVal.Append($"Certificate Name:   {subject}{cr}{cr}");

            retVal.Append($"Has Private Key:    {cert.HasPrivateKey}{cr}{cr}");
            retVal.Append($"Not Before:         {cert.NotBefore}{cr}");
            retVal.Append($"Not After:          {cert.NotAfter}{cr}{cr}");
            retVal.Append($"Serial Number:      {cert.SerialNumber}{cr}");
            retVal.Append($"Thumbprint:         {cert.Thumbprint}{cr}{cr}");
            retVal.Append($"Issuer:             {cert.Issuer}{cr}");
            retVal.Append($"Subject (DN):       {cert.Subject}{cr}");

            retVal.AppendFormat("{0}{1}{0}{0}", cr, new string('-', 50));

            return retVal.ToString();
        }
    }
}
