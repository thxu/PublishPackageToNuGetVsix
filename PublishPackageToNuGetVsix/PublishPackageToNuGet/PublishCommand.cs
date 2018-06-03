using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PublishPackageToNuGet.Service;
using PublishPackageToNuGet.Setting;
using Task = System.Threading.Tasks.Task;

namespace PublishPackageToNuGet
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class PublishCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("fd6a0c34-31b3-4de3-bc9f-51eb0c9a2fc2");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private PublishCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static PublishCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Verify the current thread is the UI thread - the call to AddCommand in PublishCommand's constructor requires
            // the UI thread.
            ThreadHelper.ThrowIfNotOnUIThread();

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new PublishCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var projInfo = GetSelectedProjInfo();
            if (projInfo == null)
            {
                throw new Exception("您还未选中项目");
            }

            var projModel = projInfo.AnalysisProject();
            if (projModel == null)
            {
                throw new Exception("您当前选中的项目输出类型不是DLL文件");
            }

            OptionPageGrid settingInfo = NuGetService.GetSettingPage();
            if (string.IsNullOrWhiteSpace(settingInfo?.DefaultPackageSource))
            {
                throw new Exception("请先完善包设置信息");
            }

            var tmp = NuGetService.GetAllPackageSources();

            projModel.PackageInfo = projModel.LibName.GetPackageData(settingInfo.DefaultPackageSource);
            projModel.Author = settingInfo.Authour;
            projModel.Owners = projModel.PackageInfo?.Owners ?? new List<string>{ settingInfo.Authour };
            projModel.Desc = projModel.PackageInfo?.Description ?? string.Empty;
            projModel.Version = (projModel.PackageInfo?.Version?.OriginalVersion).AddVersion();

            var form = new PublishInfoForm();
            form.Ini(projModel);
            form.Show();

            PublishInfoForm.PublishEvent = model =>
            {
                model.BuildPackage().PushToNugetSer(settingInfo.PublishKey, settingInfo.DefaultPackageSource);
                MessageBox.Show("推送完成");
                form.Close();
            };
        }

        private Project GetSelectedProjInfo()
        {
            if (!((ServiceProvider.GetServiceAsync(typeof(DTE)).Result) is DTE2 dte))
            {
                return null;
            }
            var projInfo = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;
            foreach (UIHierarchyItem selItem in projInfo)
            {
                if (selItem.Object is Project item)
                {
                    return item;
                }
            }
            return null;
        }

    }
}
