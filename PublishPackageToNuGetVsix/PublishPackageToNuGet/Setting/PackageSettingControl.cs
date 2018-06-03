using System;
using System.Linq;
using System.Windows.Forms;
using PublishPackageToNuGet.Service;

namespace PublishPackageToNuGet.Setting
{
    public partial class PackageSettingControl : UserControl
    {
        internal OptionPageGrid OptionPage;
        public PackageSettingControl()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            try
            {
                var sources = NuGetService.GetAllPackageSources();
                OptionPage.AllPackageSource = sources;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            txtAuthour.Text = OptionPage.Authour;
            txtPublishKey.Text = OptionPage.PublishKey;

            if (OptionPage.AllPackageSource != null && OptionPage.AllPackageSource.Any())
            {
                foreach (var sc in OptionPage.AllPackageSource)
                {
                    cbPackageSource.Items.Add(sc);
                }

                if (string.IsNullOrWhiteSpace(OptionPage.DefaultPackageSource) || !OptionPage.AllPackageSource.Contains(OptionPage.DefaultPackageSource))
                {
                    cbPackageSource.SelectedIndex = 0;
                }
                else
                {
                    cbPackageSource.SelectedItem = OptionPage.DefaultPackageSource;
                }
            }
        }

        public void SavePackageSource(object sender, EventArgs e)
        {
            OptionPage.DefaultPackageSource = cbPackageSource.SelectedItem.ToString();
        }

        public void SaveAuthour(object sender, EventArgs e)
        {
            OptionPage.Authour = txtAuthour.Text;
        }

        public void SavePublishKey(object sender, EventArgs e)
        {
            OptionPage.PublishKey = txtPublishKey.Text;
        }
    }
}
