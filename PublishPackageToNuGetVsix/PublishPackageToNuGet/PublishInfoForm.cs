using System;
using System.Windows.Forms;
using PublishPackageToNuGet.Model;

namespace PublishPackageToNuGet
{
    public partial class PublishInfoForm : Form
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
    }
}
