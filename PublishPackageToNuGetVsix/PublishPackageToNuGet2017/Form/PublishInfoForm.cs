using NuGet.Packaging;
using PublishPackageToNuGet2017.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PublishPackageToNuGet2017.Form
{
    public partial class PublishInfoForm : System.Windows.Forms.Form
    {
        protected ProjModel _projModel;

        public static Action<ProjModel> PublishEvent;

        public PublishInfoForm()
        {
            InitializeComponent();
        }

        public void Ini(ProjModel projModel)
        {
            _projModel = projModel;

            txtId.Text = _projModel.LibName;
            txtVersion.Text = _projModel.Version;
            txtAuthors.Text = _projModel.Author;
            txtOwners.Text = string.Join(",", _projModel.Owners);
            txtDesc.Text = _projModel.Desc;

            refreshDepency();
        }

        private void refreshDepency()
        {
            int posY = 0;
            panel_PkgDependencyGroup.Controls.Clear();
            if (_projModel.PackageInfo?.DependencyGroups != null && _projModel.PackageInfo.DependencyGroups.Any())
            {
                foreach (PackageDependencyGroup dependencyGroup in _projModel.PackageInfo.DependencyGroups)
                {
                    // 显示依赖组鸣
                    var groupName = dependencyGroup.TargetFramework.DotNetFrameworkName;
                    Label lbGroupName = new Label
                    {
                        Text = groupName,
                        Font = new Font(FontFamily.GenericSerif, 11, FontStyle.Bold),
                        Location = new Point(0, posY),
                        Size = new Size(480, 25),
                    };
                    posY += 25;
                    panel_PkgDependencyGroup.Controls.Add(lbGroupName);

                    if (dependencyGroup.Packages != null && dependencyGroup.Packages.Any())
                    {
                        var pkgList = dependencyGroup.Packages.ToList();
                        for (int i = 0; i < pkgList.Count(); i++)
                        {
                            string txt = $"|__{pkgList[i].Id} {pkgList[i].VersionRange.PrettyPrint()}";
                            Label lbPkg = new Label
                            {
                                Text = txt,
                                Font = new Font(FontFamily.GenericSerif, 9, FontStyle.Regular),
                                Location = new Point(0, posY),
                                Size = new Size(480, 18),
                            };
                            posY += 20;
                            panel_PkgDependencyGroup.Controls.Add(lbPkg);
                        }
                    }
                }
            }
        }

        private void btnPublish_Click(object sender, System.EventArgs e)
        {
            try
            {
                _projModel.Desc = txtDesc.Text;
                if (string.IsNullOrWhiteSpace(_projModel.Desc))
                {
                    MessageBox.Show("请输入描述信息");
                    return;
                }
                PublishEvent?.Invoke(_projModel);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btn_EditDependencies_Click(object sender, EventArgs e)
        {
            var form = new PackageDependenciesForm();
            form.Ini(_projModel.PackageInfo?.DependencyGroups?.ToList() ?? new List<PackageDependencyGroup>(), _projModel.PackageInfo?.Id ?? string.Empty);
            PackageDependenciesForm.SaveDependencyEvent = list =>
            {
                _projModel.PackageInfo.DependencyGroups = list;
                refreshDepency();
            };
            form.ShowDialog();
        }
    }
}
