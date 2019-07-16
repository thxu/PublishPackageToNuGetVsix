using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PublishPackageToNuGet2017.Form
{
    public partial class PackageDependenciesForm : System.Windows.Forms.Form
    {
        protected List<PackageDependencyGroup> _dependencyGroups;

        public PackageDependenciesForm()
        {
            InitializeComponent();
        }

        public void Ini(List<PackageDependencyGroup> groups)
        {
            _dependencyGroups = groups;
            listView_GroupList.View = View.List;

            string firstGroupName = "";

            if (_dependencyGroups != null && _dependencyGroups.Any())
            {
                foreach (PackageDependencyGroup dependencyGroup in _dependencyGroups)
                {
                    var tmp = dependencyGroup.TargetFramework.GetShortFolderName();
                    if (string.IsNullOrWhiteSpace(firstGroupName))
                    {
                        firstGroupName = tmp;
                    }
                    listView_GroupList.Items.Add(new ListViewItem { Text = tmp });
                }
            }

            ShowPkgListByGroupName(firstGroupName);
        }

        private void ShowPkgListByGroupName(string groupName)
        {
            var pkgGroup = _dependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == groupName);
            if (pkgGroup != null && pkgGroup.Packages.Any())
            {
                this.dg_PkgList.Rows.Clear();
                txtTargetFramework.Text = groupName;
                foreach (PackageDependency package in pkgGroup.Packages)
                {
                    int index = this.dg_PkgList.Rows.Add();
                    DataGridViewTextBoxCell id = new DataGridViewTextBoxCell() { Value = package.Id };
                    DataGridViewTextBoxCell version = new DataGridViewTextBoxCell() { Value = package.VersionRange.OriginalString };
                    DataGridViewLinkCell op = new DataGridViewLinkCell() { Value = "Delete" };
                    this.dg_PkgList.Rows[index].Cells[0] = id;
                    this.dg_PkgList.Rows[index].Cells[1] = version;
                    this.dg_PkgList.Rows[index].Cells[2] = op;
                }
            }
        }

        private void dg_PkgList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dg_PkgList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                this.dg_PkgList.Rows.RemoveAt(e.RowIndex);
            }
        }

        #region GroupList
        private void btn_AddGroup_Click(object sender, System.EventArgs e)
        {
            var targetFrameWork = NuGetFramework.Parse(txtTargetFramework.Text);
            if (targetFrameWork == null || targetFrameWork.IsUnsupported)
            {
                MessageBox.Show("NuGetFramework版本名称错误，示例：netstandard2.0 或 net451");
                return;
            }

            var isExist = _dependencyGroups.Exists(n => n.TargetFramework.GetShortFolderName() == targetFrameWork.GetShortFolderName());
            if (isExist)
            {
                MessageBox.Show("该TargetFramework已存在");
                return;
            }

            listView_GroupList.Items.Add(new ListViewItem { Text = targetFrameWork.GetShortFolderName() });
            _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, null));
            this.dg_PkgList.Rows.Clear();
        }

        private void btn_DelGroup_Click(object sender, System.EventArgs e)
        {
            if (listView_GroupList.SelectedIndices.Count > 0)
            {
                foreach (int index in listView_GroupList.SelectedIndices)
                {
                    var targetFrameWorkName = listView_GroupList.Items[index].Text;
                    DeleteDelpendencyGroup(targetFrameWorkName);
                }
            }
            this.dg_PkgList.Rows.Clear();

            // 删除依赖组后，默认显示第一个依赖组信息
            if (listView_GroupList.Items.Count > 0)
            {
                var targetFrameWorkName = listView_GroupList.Items[0].Text;
                ShowPkgListByGroupName(targetFrameWorkName);
            }
        }

        private void DeleteDelpendencyGroup(string targetFrameWorkName)
        {
            var pkgGroup = _dependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == targetFrameWorkName);
            if (pkgGroup != null)
            {
                _dependencyGroups.Remove(pkgGroup);
            }
        }

        private void listView_GroupList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listView_GroupList.SelectedIndices.Count > 0)
            {
                foreach (int index in listView_GroupList.SelectedIndices)
                {
                    var targetFrameWorkName = listView_GroupList.Items[index].Text;
                    ShowPkgListByGroupName(targetFrameWorkName);
                }
            }
        }
        #endregion

        #region PkgList
        private void btn_AddPkg_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPkgId.Text))
            {
                MessageBox.Show("NuGet包Id不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPkgVersion.Text))
            {
                MessageBox.Show("NuGet包版本不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTargetFramework.Text))
            {
                MessageBox.Show("TargetFramework不能为空");
                return;
            }

            var targetFrameWork = NuGetFramework.Parse(txtTargetFramework.Text);
            if (targetFrameWork == null || targetFrameWork.IsUnsupported)
            {
                MessageBox.Show("NuGetFramework版本名称错误，示例：netstandard2.0 或 net451");
                return;
            }

            var pkgGroup = _dependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == txtTargetFramework.Text);
            List<PackageDependency> pkgList = new List<PackageDependency>();
            if (pkgGroup == null)
            {
                pkgList.Add(new PackageDependency(txtPkgId.Text, VersionRange.Parse(txtPkgVersion.Text)));
                _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, pkgList));
            }
            else
            {
                if (pkgGroup.Packages != null)
                {
                    pkgList = pkgGroup.Packages.ToList();
                }
                pkgList.Add(new PackageDependency(txtPkgId.Text, VersionRange.Parse(txtPkgVersion.Text)));
                DeleteDelpendencyGroup(txtTargetFramework.Text);
                _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, pkgList));
            }

            int index = this.dg_PkgList.Rows.Add();
            DataGridViewTextBoxCell id = new DataGridViewTextBoxCell() { Value = txtPkgId.Text };
            DataGridViewTextBoxCell version = new DataGridViewTextBoxCell() { Value = txtPkgVersion.Text };
            DataGridViewLinkCell op = new DataGridViewLinkCell() { Value = "Delete" };
            this.dg_PkgList.Rows[index].Cells[0] = id;
            this.dg_PkgList.Rows[index].Cells[1] = version;
            this.dg_PkgList.Rows[index].Cells[2] = op;
        }



        #endregion

        private void btn_OpenOnLinePkgListForm_Click(object sender, System.EventArgs e)
        {
            OnLinePkgListForm form = new OnLinePkgListForm();
            form.Ini();
            form.ShowDialog();

            OnLinePkgListForm.AddPkgEvent += view =>
            {
                txtPkgId.Text = view.Id;
                txtPkgVersion.Text = view.Version;
            };
        }
    }
}
