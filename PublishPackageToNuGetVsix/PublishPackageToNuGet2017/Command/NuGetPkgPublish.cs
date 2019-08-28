using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;
using PublishPackageToNuGet2017.Form;
using PublishPackageToNuGet2017.Service;
using PublishPackageToNuGet2017.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace PublishPackageToNuGet2017.Command
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class NuGetPkgPublish
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("31996537-9480-4b34-b98e-89dee99343de");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="NuGetPkgPublish"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private NuGetPkgPublish(AsyncPackage package, OleMenuCommandService commandService)
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
        public static NuGetPkgPublish Instance
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
            // Switch to the main thread - the call to AddCommand in NuGetPkgPublish's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new NuGetPkgPublish(package, commandService);
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
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                var projInfo = ThreadHelper.JoinableTaskFactory.Run(GetSelectedProjInfoAsync);
                if (projInfo == null)
                {
                    throw new Exception("您还未选中项目");
                }

                var projModel = projInfo.AnalysisProject();
                if (projModel == null)
                {
                    throw new Exception("您当前选中的项目输出类型不是DLL文件");
                }

                OptionPageGrid settingInfo = NuGetPkgPublishService.GetSettingPage();
                if (string.IsNullOrWhiteSpace(settingInfo?.DefaultPackageSource))
                {
                    throw new Exception("请先完善包设置信息");
                }

                projModel.PackageInfo = projModel.LibName.GetPackageData(settingInfo.DefaultPackageSource) ?? new ManifestMetadata
                {
                    Authors = new List<string> { projModel.Author },
                    ContentFiles = new List<ManifestContentFiles>(),
                    Copyright = $"CopyRight © {projModel.Author} {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                    DependencyGroups = new List<PackageDependencyGroup>(),
                    Description = projModel.Desc,
                    DevelopmentDependency = false,
                    FrameworkReferences = new List<FrameworkAssemblyReference>(),
                    Id = projModel.LibName,
                    Language = null,
                    MinClientVersionString = "1.0.0.0",
                    Owners = new List<string> { projModel.Author },
                    PackageAssemblyReferences = new List<PackageReferenceSet>(),
                    PackageTypes = new List<PackageType>(),
                    ReleaseNotes = null,
                    Repository = null,
                    RequireLicenseAcceptance = false,
                    Serviceable = false,
                    Summary = null,
                    Tags = string.Empty,
                    Title = projModel.LibName,
                    Version = NuGetVersion.Parse("1.0.0.0"),
                };
                projModel.Author = settingInfo.Authour;
                projModel.Owners = projModel.PackageInfo?.Owners ?? new List<string> { settingInfo.Authour };
                projModel.Desc = projModel.PackageInfo?.Description ?? string.Empty;
                projModel.Version = (projModel.PackageInfo?.Version?.OriginalVersion).AddVersion();

                // 判断包是否有依赖项组，若没有则根据当前项目情况自动添加
                List<PackageDependencyGroup> groupsTmp = projModel.PackageInfo.DependencyGroups.ToList();
                foreach (string targetVersion in projModel.NetFrameworkVersionList)
                {
                    var targetFrameworkDep = projModel.PackageInfo.DependencyGroups.FirstOrDefault(n => n.TargetFramework.GetShortFolderName() == targetVersion);
                    if (targetFrameworkDep == null)
                    {
                        groupsTmp.Add(new PackageDependencyGroup(NuGetFramework.Parse(targetVersion), new List<PackageDependency>()));
                    }
                }
                projModel.PackageInfo.DependencyGroups = groupsTmp;

                var form = new PublishInfoForm();
                form.Ini(projModel);
                form.Show();

                PublishInfoForm.PublishEvent = model =>
                {
                    try
                    {
                        var isSuccess = model.BuildPackage().PushToNugetSer(settingInfo.PublishKey, settingInfo.DefaultPackageSource);
                        MessageBox.Show(isSuccess ? "推送完成" : "推送失败");
                        form.Close();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                };

                int a = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }


        /// <summary>
        /// 获取当前选中的项目
        /// </summary>
        /// <returns>项目信息</returns>
        private async Task<Project> GetSelectedProjInfoAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            if (ServiceProvider != null)
            {
                var dte = await ServiceProvider.GetServiceAsync(typeof(DTE)) as DTE2;
                if (dte == null)
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
            return null;
        }
    }
}
