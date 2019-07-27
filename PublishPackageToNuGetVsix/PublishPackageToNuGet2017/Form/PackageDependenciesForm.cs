using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;
using PublishPackageToNuGet2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PublishPackageToNuGet2017.Form
{
    public partial class PackageDependenciesForm : System.Windows.Forms.Form
    {
        protected List<PackageDependencyGroup> _dependencyGroups;
        protected string _currPkgId;

        public static Action<List<PackageDependencyGroup>> SaveDependencyEvent;

        public PackageDependenciesForm()
        {
            InitializeComponent();
        }

        public void Ini(List<PackageDependencyGroup> groups, string currPkgId)
        {
            _dependencyGroups = groups;
            _currPkgId = currPkgId;

            listView_GroupList.View = View.List;

            string firstGroupName = "";

            if (_dependencyGroups != null && _dependencyGroups.Any())
            {
                foreach (PackageDependencyGroup dependencyGroup in _dependencyGroups)
                {
                    var tmp = dependencyGroup.TargetFramework.GetShortFolderName();
                    ListViewItem listViewItem = new ListViewItem { Text = tmp };
                    if (string.IsNullOrWhiteSpace(firstGroupName))
                    {
                        firstGroupName = tmp;
                        listViewItem.Selected = true;
                    }
                    listView_GroupList.Items.Add(listViewItem);
                }
            }

            ShowPkgListByGroupName(firstGroupName);
        }

        private void ShowPkgListByGroupName(string groupName)
        {
            this.dg_PkgList.Rows.Clear();
            var pkgGroup = _dependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == groupName);
            if (pkgGroup != null && pkgGroup.Packages.Any())
            {
                txtTargetFramework.Text = groupName;
                foreach (PackageDependency package in pkgGroup.Packages)
                {
                    int index = this.dg_PkgList.Rows.Add();
                    DataGridViewTextBoxCell id = new DataGridViewTextBoxCell() { Value = package.Id };
                    DataGridViewTextBoxCell version = new DataGridViewTextBoxCell() { Value = package.VersionRange.OriginalString };
                    DataGridViewLinkCell op = new DataGridViewLinkCell() { Value = "Delete", Tag = groupName };
                    DataGridViewLinkCell op1 = new DataGridViewLinkCell() { Value = "Update", Tag = groupName };
                    this.dg_PkgList.Rows[index].Cells[0] = id;
                    this.dg_PkgList.Rows[index].Cells[1] = version;
                    this.dg_PkgList.Rows[index].Cells[2] = op1;
                    this.dg_PkgList.Rows[index].Cells[3] = op;
                }
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

            listView_GroupList.Items.Add(new ListViewItem { Text = targetFrameWork.GetShortFolderName(), Selected = true });
            _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, new List<PackageDependency>()));
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
                    this.listView_GroupList.Items.RemoveAt(index);
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
            var targetFrameWorkName = GetCurrSelectedGroup();
            if (!string.IsNullOrWhiteSpace(targetFrameWorkName))
            {
                ShowPkgListByGroupName(targetFrameWorkName);
            }
        }

        private string GetCurrSelectedGroup()
        {
            if (listView_GroupList.SelectedIndices.Count > 0)
            {
                foreach (int index in listView_GroupList.SelectedIndices)
                {
                    var targetFrameWorkName = listView_GroupList.Items[index].Text;
                    return targetFrameWorkName;
                }
            }

            return string.Empty;
        }

        private void listView_GroupList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                if (listView_GroupList.SelectedItems.Count <= 0)//点击空白区  
                {
                    if (this.listView_GroupList.FocusedItem != null)
                    {
                        ListViewItem item = this.listView_GroupList.GetItemAt(e.X, e.Y);
                        if (item == null)
                        {
                            this.listView_GroupList.FocusedItem.Selected = true;
                        }
                    }
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

            var targetFrameWorkName = GetCurrSelectedGroup();
            if (string.IsNullOrWhiteSpace(targetFrameWorkName))
            {
                // 没有选中框架，取用txtTargetFramework的值，若该值未创建组则自动创建一个
                targetFrameWorkName = txtTargetFramework.Text;
            }

            var targetFrameWork = NuGetFramework.Parse(targetFrameWorkName);
            if (targetFrameWork == null || targetFrameWork.IsUnsupported)
            {
                MessageBox.Show("NuGetFramework版本转换失败");
                return;
            }

            if (txtPkgId.Text == _currPkgId)
            {
                MessageBox.Show("不能依赖自身");
                return;
            }

            var pkgGroup = _dependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == targetFrameWorkName);
            List<PackageDependency> pkgList = new List<PackageDependency>();
            if (pkgGroup == null)
            {
                pkgList.Add(new PackageDependency(txtPkgId.Text, VersionRange.Parse(txtPkgVersion.Text)));
                _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, pkgList));
                this.listView_GroupList.Items.Add(new ListViewItem()
                { Text = targetFrameWork.GetShortFolderName(), Selected = true });
            }
            else
            {
                if (pkgGroup.Packages != null)
                {
                    if (pkgGroup.Packages.Any(n => n.Id == txtPkgId.Text))
                    {
                        MessageBox.Show("当前NuGet包已存在");
                        return;
                    }
                    pkgList = pkgGroup.Packages.ToList();
                }
                pkgList.Add(new PackageDependency(txtPkgId.Text, VersionRange.Parse(txtPkgVersion.Text)));
                DeleteDelpendencyGroup(txtTargetFramework.Text);
                _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, pkgList));
            }

            ShowPkgListByGroupName(targetFrameWorkName);


            txtPkgId.Text = "";
            txtPkgVersion.Text = "";
        }

        private void dg_PkgList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var currCel = this.dg_PkgList.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (currCel.Value.ToString() == "Delete")
            {
                var targetFrameWorkName = currCel.Tag.ToString();
                var targetFrameWork = NuGetFramework.Parse(targetFrameWorkName);
                if (targetFrameWork == null || targetFrameWork.IsUnsupported)
                {
                    MessageBox.Show("NuGetFramework版本转换失败");
                    return;
                }
                var pkgGroup = _dependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == targetFrameWorkName);
                List<PackageDependency> pkgList = new List<PackageDependency>();
                if (pkgGroup == null)
                {
                    return;
                }
                if (pkgGroup.Packages != null)
                {
                    var currId = this.dg_PkgList.Rows[e.RowIndex].Cells[0].Value.ToString();
                    pkgList = pkgGroup.Packages.Where(n => n.Id != currId).ToList();
                }
                DeleteDelpendencyGroup(txtTargetFramework.Text);
                _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, pkgList));

                this.dg_PkgList.Rows.RemoveAt(e.RowIndex);
            }
            else if (currCel.Value.ToString() == "Update")
            {
                var currId = this.dg_PkgList.Rows[e.RowIndex].Cells[0].Value.ToString();

                PackageDetailsForm form = new PackageDetailsForm();
                form.Ini(currId);
                PackageDetailsForm.AddPkgEvent = view =>
                {
                    this.dg_PkgList.Rows[e.RowIndex].Cells[1].Value = view.Version;
                    var targetFrameWorkName = currCel.Tag.ToString();
                    var targetFrameWork = NuGetFramework.Parse(targetFrameWorkName);
                    if (targetFrameWork == null || targetFrameWork.IsUnsupported)
                    {
                        MessageBox.Show("NuGetFramework版本转换失败");
                        return;
                    }
                    var pkgGroup = _dependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == targetFrameWorkName);
                    List<PackageDependency> pkgList = new List<PackageDependency>();
                    if (pkgGroup == null)
                    {
                        return;
                    }
                    if (pkgGroup.Packages != null)
                    {
                        pkgList = pkgGroup.Packages.Where(n => n.Id != currId).ToList();
                        pkgList.Add(new PackageDependency(currId, VersionRange.Parse(view.Version)));
                    }
                    DeleteDelpendencyGroup(txtTargetFramework.Text);
                    _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, pkgList));

                    form.Close();
                };
                form.ShowDialog();
            }
        }

        #endregion

        private void btn_OpenOnLinePkgListForm_Click(object sender, System.EventArgs e)
        {
            OnLinePkgListForm form = new OnLinePkgListForm();
            form.Ini();

            OnLinePkgListForm.AddPkgEvent = list =>
            {
                if (AddPkgList(list))
                {
                    form.Close();
                }
            };
            form.ShowDialog();
        }

        private bool AddPkgList(List<SimplePkgView> simplePkgList)
        {
            try
            {
                if (simplePkgList == null || !simplePkgList.Any())
                {
                    return false;
                }

                var targetFrameWorkName = GetCurrSelectedGroup();
                if (string.IsNullOrWhiteSpace(targetFrameWorkName))
                {
                    // 没有选中框架，取用txtTargetFramework的值，若该值未创建组则自动创建一个
                    targetFrameWorkName = txtTargetFramework.Text;
                }
                var targetFrameWork = NuGetFramework.Parse(targetFrameWorkName);
                if (targetFrameWork == null || targetFrameWork.IsUnsupported)
                {
                    MessageBox.Show("NuGetFramework版本转换失败");
                    return false;
                }

                targetFrameWorkName = targetFrameWork.GetShortFolderName();

                if (simplePkgList.Any(n => n.Id == _currPkgId))
                {
                    MessageBox.Show("不能依赖自身");
                    return false;
                }

                var pkgGroup = _dependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == targetFrameWorkName);
                List<PackageDependency> pkgList = new List<PackageDependency>();
                if (pkgGroup == null)
                {
                    foreach (SimplePkgView pkgView in simplePkgList)
                    {
                        pkgList.Add(new PackageDependency(pkgView.Id, VersionRange.Parse(pkgView.Version)));
                    }

                    _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, pkgList));
                    this.listView_GroupList.Items.Add(new ListViewItem() { Text = targetFrameWork.GetShortFolderName(), Selected = true });
                }
                else
                {
                    if (pkgGroup.Packages != null)
                    {
                        foreach (SimplePkgView pkgView in simplePkgList)
                        {
                            if (pkgGroup.Packages.Any(n => n.Id == pkgView.Id))
                            {
                                MessageBox.Show("当前NuGet包已添加");
                                return false;
                            }
                        }
                        pkgList = pkgGroup.Packages.ToList();
                    }
                    foreach (SimplePkgView pkgView in simplePkgList)
                    {
                        pkgList.Add(new PackageDependency(pkgView.Id, VersionRange.Parse(pkgView.Version)));
                    }

                    DeleteDelpendencyGroup(targetFrameWorkName);
                    _dependencyGroups.Add(new PackageDependencyGroup(targetFrameWork, pkgList));
                }

                ShowPkgListByGroupName(targetFrameWorkName);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private void btn_Cancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btn_ok_Click(object sender, System.EventArgs e)
        {
            SaveDependencyEvent?.Invoke(_dependencyGroups);
            this.Close();
        }
    }
}
