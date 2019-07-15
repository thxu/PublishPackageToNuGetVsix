using NuGet.Packaging;
using NuGet.Packaging.Core;
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

            if (_dependencyGroups != null && _dependencyGroups.Any())
            {
                foreach (PackageDependencyGroup dependencyGroup in _dependencyGroups)
                {
                    listView_GroupList.Items.Add(new ListViewItem { Text = dependencyGroup.TargetFramework.GetShortFolderName() });
                    if (dependencyGroup.Packages != null && dependencyGroup.Packages.Any())
                    {
                        foreach (PackageDependency package in dependencyGroup.Packages)
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

            }
        }
    }
}
