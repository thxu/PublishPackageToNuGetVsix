using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace PublishPackageToNuGet.Setting
{
    public class OptionPageGrid : DialogPage
    {
        [Category("NugetPackageSetting")]
        [DisplayName("Authour")]
        [Description("Authour")]
        public string Authour { get; set; } = "";

        [Category("NugetPackageSetting")]
        [DisplayName("PublishKey")]
        [Description("PublishKey Or PAT")]
        public string PublishKey { get; set; } = "";

        [Category("NugetPackageSetting")]
        [DisplayName("PackageSource")]
        [Description("PackageSource, The Publish Url")]
        public string DefaultPackageSource { get; set; } = "https://api.nuget.org/v3/index.json";

        public List<string> AllPackageSource { get; set; }

        protected override IWin32Window Window
        {
            get
            {
                PackageSettingControl page = new PackageSettingControl { OptionPage = this };
                page.Initialize();
                return page;
            }
        }
    }
}
