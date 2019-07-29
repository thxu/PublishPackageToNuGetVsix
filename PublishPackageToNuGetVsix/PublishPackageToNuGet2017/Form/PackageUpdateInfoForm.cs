using PublishPackageToNuGet2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PublishPackageToNuGet2017.Form
{
    public partial class PackageUpdateInfoForm : System.Windows.Forms.Form
    {
        protected List<UpdatePkgView> _updatePkgViews;
        public static Action<Dictionary<string, List<UpdatePkgView>>> UpdPkgEvent;
        private bool _IsListChanged = false;
        private bool _IsChkAllChanged = false;

        public PackageUpdateInfoForm()
        {
            InitializeComponent();
        }

        public void Ini(List<UpdatePkgView> views)
        {
            _updatePkgViews = views;

            lv_UpdPkgList.View = View.Details;
            lv_UpdPkgList.CheckBoxes = true;
            if (_updatePkgViews != null && _updatePkgViews.Any())
            {
                foreach (UpdatePkgView view in _updatePkgViews)
                {
                    var versionTmp = string.IsNullOrWhiteSpace(view.OldVersion) ? $"{view.Version}(New)" : $"{view.OldVersion} -> {view.Version}";
                    lv_UpdPkgList.Items.Add(new ListViewItem() { Text = view.Id, SubItems = { versionTmp }, Tag = view, Checked = true });
                }

                lv_UpdPkgList.Columns[0].Width = -1;
                lv_UpdPkgList.Columns[1].Width = -1;
                chk_CheckAll.Checked = true;
                this.chk_CheckAll.ThreeState = true;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (_IsListChanged)
            {
                return;
            }

            _IsChkAllChanged = true;
            foreach (ListViewItem item in lv_UpdPkgList.Items)
            {
                item.Checked = chk_CheckAll.CheckState == CheckState.Checked;
            }
            _IsChkAllChanged = false;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<UpdatePkgView>> data = new Dictionary<string, List<UpdatePkgView>>();
            if (lv_UpdPkgList.CheckedItems.Count > 0)
            {
                foreach (ListViewItem checkedItem in lv_UpdPkgList.CheckedItems)
                {
                    var view = (UpdatePkgView)checkedItem.Tag;
                    if (data.ContainsKey(view.TargetFramework))
                    {
                        data[view.TargetFramework].Add(view);
                    }
                    else
                    {
                        data.Add(view.TargetFramework, new List<UpdatePkgView> { view });
                    }
                }
            }

            UpdPkgEvent?.Invoke(data);
        }

        private void lv_UpdPkgList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_IsChkAllChanged)
            {
                return;
            }
            _IsListChanged = true;
            if (lv_UpdPkgList.CheckedItems.Count <= 0)
            {
                //this.chk_CheckAll.Checked = false;
                this.chk_CheckAll.CheckState = CheckState.Unchecked;
            }
            else if (lv_UpdPkgList.CheckedItems.Count == lv_UpdPkgList.Items.Count)
            {
                //this.chk_CheckAll.Checked = true;
                this.chk_CheckAll.CheckState = CheckState.Checked;
            }
            else
            {
                this.chk_CheckAll.CheckState = CheckState.Indeterminate;
            }
            _IsListChanged = false;
        }
    }
}
