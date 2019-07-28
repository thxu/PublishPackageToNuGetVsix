using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;
using PublishPackageToNuGet2017.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

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
                    // 显示依赖组
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

        private Dictionary<string, List<SimplePkgView>> packagesConfigAnync()
        {
            Dictionary<string, List<SimplePkgView>> res = new Dictionary<string, List<SimplePkgView>>();

            var configFilePath = Path.Combine(_projModel.ProjectPath, "packages.config");
            if (File.Exists(configFilePath))
            {
                XElement pkgsElement = XDocument.Load(configFilePath).Element("packages");
                if (pkgsElement == null)
                {
                    return res;
                }
                var allPkgs = pkgsElement.Elements("package");
                foreach (XElement pkgElement in allPkgs)
                {
                    var id = pkgElement.Attribute("id")?.Value ?? string.Empty;
                    var version = pkgElement.Attribute("version")?.Value ?? string.Empty;
                    var targetFramework = pkgElement.Attribute("targetFramework")?.Value ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(version))
                    {
                        SimplePkgView model = new SimplePkgView
                        {
                            Id = id,
                            Version = version,
                            TargetFramework = targetFramework,
                        };
                        if (res.ContainsKey(targetFramework))
                        {
                            res[targetFramework].Add(model);
                        }
                        else
                        {
                            res.Add(targetFramework, new List<SimplePkgView>() { model });
                        }

                    }
                }
            }

            return res;
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
            form.Ini(_projModel.PackageInfo?.DependencyGroups?.ToList() ?? new List<PackageDependencyGroup>(), _projModel.PackageInfo?.Id ?? string.Empty, _projModel.NetFrameworkVersion);
            PackageDependenciesForm.SaveDependencyEvent = list =>
            {
                _projModel.PackageInfo.DependencyGroups = list;
                refreshDepency();
            };
            form.ShowDialog();
        }

        private void PublishInfoForm_Shown(object sender, EventArgs e)
        {
            List<UpdatePkgView> updPkgList = new List<UpdatePkgView>();
            var projPkgs = packagesConfigAnync();
            foreach (var pkgView in projPkgs)
            {
                var targetFrameworkDep = _projModel.PackageInfo.DependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == pkgView.Key);
                if (targetFrameworkDep == null)
                {
                    updPkgList.AddRange(pkgView.Value.Select(n => new UpdatePkgView { Id = n.Id, Version = n.Version, TargetFramework = n.TargetFramework, OldVersion = null }));
                    break;
                }

                foreach (SimplePkgView view in pkgView.Value)
                {
                    var tmp = targetFrameworkDep.Packages.FirstOrDefault(n => n.Id == view.Id);
                    if (tmp == null || tmp.VersionRange.OriginalString != view.Version)
                    {
                        updPkgList.Add(new UpdatePkgView { Id = view.Id, Version = view.Version, TargetFramework = view.TargetFramework, OldVersion = tmp?.VersionRange?.OriginalString });
                    }
                }
            }

            if (updPkgList.Any())
            {
                PackageUpdateInfoForm form = new PackageUpdateInfoForm();
                form.Ini(updPkgList);
                PackageUpdateInfoForm.UpdPkgEvent = list =>
                {
                    if (list != null && list.Any())
                    {
                        List<PackageDependencyGroup> groups = _projModel.PackageInfo.DependencyGroups.ToList();
                        foreach (var item in list)
                        {
                            if (item.Value != null && item.Value.Any())
                            {
                                List<PackageDependency> pkgList = new List<PackageDependency>();
                                var targetFrameworkDep = groups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == item.Key);
                                if (targetFrameworkDep != null)
                                {
                                    var ids = item.Value.Select(n => n.Id);
                                    pkgList = targetFrameworkDep.Packages.Where(n => !ids.Contains(n.Id)).ToList();
                                    groups.Remove(targetFrameworkDep);
                                }

                                foreach (UpdatePkgView updatePkgView in item.Value)
                                {
                                    pkgList.Add(new PackageDependency(updatePkgView.Id, VersionRange.Parse(updatePkgView.Version)));
                                }

                                groups.Add(new PackageDependencyGroup(NuGetFramework.Parse(item.Key), pkgList));
                            }
                        }
                        _projModel.PackageInfo.DependencyGroups = groups;
                        refreshDepency();
                    }
                    form.Close();
                };
                form.ShowDialog();
            }
        }
    }
}
