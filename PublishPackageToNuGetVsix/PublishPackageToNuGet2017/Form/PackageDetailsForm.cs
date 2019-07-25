using PublishPackageToNuGet2017.Model;
using PublishPackageToNuGet2017.Service;
using PublishPackageToNuGet2017.Setting;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PublishPackageToNuGet2017.Form
{
    public partial class PackageDetailsForm : System.Windows.Forms.Form
    {

        private string _currPkgId;

        public PackageDetailsForm()
        {
            InitializeComponent();
        }

        public void Ini(string currPkgId)
        {
            _currPkgId = currPkgId;
            OptionPageGrid settingInfo = NuGetPkgPublishService.GetSettingPage();
            var sources = NuGetPkgPublishService.GetAllPackageSources();
            if (sources != null && sources.Any())
            {
                cbPackageSource.Items.Clear();
                foreach (var sc in sources)
                {
                    cbPackageSource.Items.Add(sc);
                }

                if (string.IsNullOrWhiteSpace(settingInfo.DefaultPackageSource) || !sources.Contains(settingInfo.DefaultPackageSource))
                {
                    cbPackageSource.SelectedIndex = 0;
                }
                else
                {
                    cbPackageSource.SelectedItem = settingInfo.DefaultPackageSource;
                }

                showPkgDetailList();
            }
        }

        private void showPkgDetailList()
        {
            this.dgv_PkgDetails.Rows.Clear();
            var currSource = cbPackageSource.SelectedItem.ToString();
            var details = currSource.GetPkgDetailsByPkgId(_currPkgId);
            if (details != null && details.Any())
            {
                foreach (SimplePkgView simplePkgView in details)
                {
                    int index = this.dgv_PkgDetails.Rows.Add();
                    DataGridViewTextBoxCell version = new DataGridViewTextBoxCell() { Value = simplePkgView.Version };
                    DataGridViewTextBoxCell author = new DataGridViewTextBoxCell() { Value = simplePkgView.Author };
                    DataGridViewTextBoxCell desc = new DataGridViewTextBoxCell() { Value = simplePkgView.Desc };
                    DataGridViewTextBoxCell publishDate = new DataGridViewTextBoxCell() { Value = simplePkgView.PublishDateTime?.DateTime ?? DateTime.MinValue };
                    this.dgv_PkgDetails.Rows[index].Cells[0] = version;
                    this.dgv_PkgDetails.Rows[index].Cells[1] = desc;
                    this.dgv_PkgDetails.Rows[index].Cells[2] = author;
                    this.dgv_PkgDetails.Rows[index].Cells[3] = publishDate;
                }
            }
        }
    }
}
