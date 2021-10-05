
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

            if(cbFindAll.Checked)
            {
                tbOut.Text = FindAll();
            }
            else if(cbUnknown.Checked)
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
        /// <summary>
        ///     Search is called from the Button Click 
        ///     OnSearch() when the Find Anywhere checkbox
        ///     (cbUnknown) is NOT checked.
        /// </summary>
        /// <returns></returns>

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
                    text.Append(GetDisplayString(cert, $"{req.storeLocation.ToString()}.{req.storeName.ToString()}", cert.Verify()));
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
        /// <summary>
        ///     Find is called from the Button Click 
        ///     OnSearch() when the Fin Anywhere checkbox
        ///     (cbUnknown) is checked. It eventually calls 
        ///     Search().
        /// </summary>
        /// <returns></returns>

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

        private string FindAll()
        {
            var retVal = new StringBuilder();

            var certCount = 0;
            var len = new OutputLengths();
            var storeName = ((x509StoreName)cbStoreName.SelectedItem).storeName;
            var location = ((x509StoreLocation)cbStoreLocation.SelectedItem).storeLocation;
            var store = new X509Store(storeName, location);

            store.Open(OpenFlags.ReadOnly);

            foreach(var cert in store.Certificates)
            {
                if(!cbValid.Checked || (cbValid.Checked && cert.Verify()))
                {
                    var subject = StringUtil.ParseValue(cert.Subject, "CN=", ',', 1);

                    len.SubjectLen = Math.Max(len.SubjectLen, subject.Length);
                    len.IssuerLen = Math.Max(len.IssuerLen, cert.Issuer.Length);
                    len.ThumbprintLen = Math.Max(len.ThumbprintLen, cert.Thumbprint.Length);
                    len.ExpDateLen = Math.Max(len.ExpDateLen, cert.NotAfter.ToString().Length);
                }
            }

            retVal.Append($"{"Subject (Parsed)".PadRight(len.SubjectLen)} | " +
                          $"{"Expire Date".PadRight(len.ExpDateLen)} | " +
                          $"{"Thumbprint".PadRight(len.ThumbprintLen)} | " +
                          $"{"Issuer"}{cr}");

            retVal.AppendFormat($"{new string('-', len.Total + 9)}{cr}");

            foreach(var cert in store.Certificates)
            {
                if(!cbValid.Checked || (cbValid.Checked && cert.Verify()))
                {
                    certCount++;
                    retVal.Append(GetCompactDisplayString(cert, $"{location.ToString()}.{storeName.ToString()}", len));
                }
            }

            store.Close();

            lblCertsFound.Text = certCount.ToString();

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

        private void OnFindAllSelection(object sender, EventArgs e)
        {
            cbUnknown.Checked = false;
            cbUnknown.Enabled = !cbFindAll.Checked;
            cbSearchType.Enabled = !cbFindAll.Checked;
            tbSearchValue.Enabled = !cbFindAll.Checked;
        }

        // ------------------------------------------------

        private void OnUnknownSelection(object sender, EventArgs e)
        {
            cbFindAll.Checked = false;
            cbFindAll.Enabled = !cbUnknown.Checked;
            cbStoreName.Enabled = !cbUnknown.Checked;
            cbStoreLocation.Enabled = !cbUnknown.Checked;
        }

        // ------------------------------------------------

        private string GetDisplayString(X509Certificate2 cert, string location, bool isValid)
        {
            var retVal = new StringBuilder();
            var subject = StringUtil.ParseValue(cert.Subject, "CN=", ',', 1);

            retVal.Append($"Location:           {location}{cr}");
            retVal.Append($"Certificate Name:   {subject}{cr}");
            retVal.Append($"Friendly Name:      {cert.FriendlyName}{cr}");
            retVal.Append($"Version:            {cert.Version}{cr}");
            retVal.Append($"Is Valid:           {isValid}{cr}{cr}");

            retVal.Append($"Has Private Key:    {cert.HasPrivateKey}{cr}{cr}");

            retVal.Append($"Not Before:         {cert.NotBefore}{cr}");
            retVal.Append($"Not After:          {cert.NotAfter}{cr}{cr}");

            retVal.Append($"Serial Number:      {cert.SerialNumber}{cr}");
            retVal.Append($"Thumbprint:         {cert.Thumbprint}{cr}{cr}");

            retVal.Append($"Issuer:             {cert.Issuer}{cr}");
            retVal.Append($"Subject (DN):       {cert.Subject}{cr}");

            retVal.AppendFormat("{0}{1}{0}{0}", cr, new string('-', 90));

            return retVal.ToString();
        }

        // ------------------------------------------------

        private string GetCompactDisplayString(X509Certificate2 cert, string location, OutputLengths len)
        {
            var retVal = new StringBuilder();
            var subject = StringUtil.ParseValue(cert.Subject, "CN=", ',', 1);

            retVal.Append($"{subject.PadRight(len.SubjectLen)} | " +
                          $"{cert.NotAfter.ToString().PadRight(len.ExpDateLen)} | " +
                          $"{cert.Thumbprint.PadRight(len.ThumbprintLen)} | " +
                          $"{cert.Issuer}{cr}");

            return retVal.ToString();
        }
    }
}
