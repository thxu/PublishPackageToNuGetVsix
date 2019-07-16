using PublishPackageToNuGet2017.Model;
using PublishPackageToNuGet2017.Service;
using PublishPackageToNuGet2017.Setting;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PublishPackageToNuGet2017.Form
{
    public partial class OnLinePkgListForm : System.Windows.Forms.Form
    {
        public static Action<SimplePkgView> AddPkgEvent;


        public OnLinePkgListForm()
        {
            InitializeComponent();
        }

        public void Ini()
        {
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
            }
        }

        private void search(int skip)
        {
            var currSource = cbPackageSource.SelectedItem.ToString();
            var dataList = currSource.GetPkgList(txt_PkgId.Text, skip);
            this.dgv_PkgList.Rows.Clear();
            if (dataList != null && dataList.Any())
            {
                foreach (SimplePkgView simplePkgView in dataList)
                {
                    int index = this.dgv_PkgList.Rows.Add();
                    DataGridViewTextBoxCell id = new DataGridViewTextBoxCell() { Value = simplePkgView.Id };
                    DataGridViewTextBoxCell version = new DataGridViewTextBoxCell() { Value = simplePkgView.Version };
                    DataGridViewTextBoxCell author = new DataGridViewTextBoxCell() { Value = simplePkgView.Author };
                    DataGridViewTextBoxCell desc = new DataGridViewTextBoxCell() { Value = simplePkgView.Desc };
                    this.dgv_PkgList.Rows[index].Cells[0] = id;
                    this.dgv_PkgList.Rows[index].Cells[1] = version;
                    this.dgv_PkgList.Rows[index].Cells[2] = author;
                    this.dgv_PkgList.Rows[index].Cells[3] = desc;
                }

                skip += dataList.Count;
                lb_CurrPage.Text = skip.ToString();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search(0);
        }

        private void btn_MoveToFirst_Click(object sender, EventArgs e)
        {
            search(0);
        }

        private void btn_MoveNext_Click(object sender, EventArgs e)
        {
            var skip = Convert.ToInt32(lb_CurrPage.Text);
            if (skip % 10 != 0)
            {
                return;
            }
            search(skip);
        }

        private void btn_MovePre_Click(object sender, EventArgs e)
        {
            var skip = Convert.ToInt32(lb_CurrPage.Text);
            skip -= 10;
            search(skip);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (this.dgv_PkgList.SelectedRows.Count <= 0)
            {
                MessageBox.Show("您还未选择任何项");
                return;
            }
            var id = this.dgv_PkgList.SelectedRows[0].Cells[0].Value.ToString();
            var version = this.dgv_PkgList.SelectedRows[0].Cells[1].Value.ToString();
            var author = this.dgv_PkgList.SelectedRows[0].Cells[2].Value.ToString();
            var desc = this.dgv_PkgList.SelectedRows[0].Cells[3].Value.ToString();
            AddPkgEvent?.Invoke(new SimplePkgView()
            {
                Author = author,
                Desc = desc,
                Id = id,
                Version = version,
            });
            this.Close();
        }
    }
}
