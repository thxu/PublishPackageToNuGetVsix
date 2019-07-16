using System;
using PublishPackageToNuGet2017.Model;
using PublishPackageToNuGet2017.Service;
using PublishPackageToNuGet2017.Setting;

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

            var list = settingInfo.DefaultPackageSource.GetPkgList("", 0);
        }
    }
}
