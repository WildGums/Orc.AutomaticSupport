[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.6", FrameworkDisplayName=".NET Framework 4.6")]


public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.AutomaticSupport
{
    
    public class AutomaticSupportService : Orc.AutomaticSupport.IAutomaticSupportService
    {
        public AutomaticSupportService(Catel.Services.IProcessService processService, Catel.Services.IDispatcherService dispatcherService) { }
        public string CommandLineParameters { get; set; }
        public string SupportUrl { get; set; }
        public event System.EventHandler<System.EventArgs> DownloadCompleted;
        public event System.EventHandler<Orc.AutomaticSupport.ProgressChangedEventArgs> DownloadProgressChanged;
        public event System.EventHandler<System.EventArgs> SupportAppClosed;
        public System.Threading.Tasks.Task DownloadAndRunAsync() { }
    }
    public interface IAutomaticSupportService
    {
        string CommandLineParameters { get; set; }
        string SupportUrl { get; set; }
        public event System.EventHandler<System.EventArgs> DownloadCompleted;
        public event System.EventHandler<Orc.AutomaticSupport.ProgressChangedEventArgs> DownloadProgressChanged;
        public event System.EventHandler<System.EventArgs> SupportAppClosed;
        System.Threading.Tasks.Task DownloadAndRunAsync();
    }
    public class ProgressChangedEventArgs : System.EventArgs
    {
        public ProgressChangedEventArgs(int progress, System.TimeSpan remainingTime) { }
        public int Progress { get; set; }
        public System.TimeSpan RemainingTime { get; set; }
    }
}
namespace Orc.AutomaticSupport.ViewModels
{
    
    public class RequestSupportViewModel : Catel.MVVM.ViewModelBase
    {
        public static readonly Catel.Data.PropertyData ProgressProperty;
        public static readonly Catel.Data.PropertyData RemainingTimeProperty;
        public RequestSupportViewModel(Orc.AutomaticSupport.IAutomaticSupportService automaticSupportService, Catel.Services.ILanguageService languageService) { }
        public int Progress { get; }
        public string RemainingTime { get; }
        protected override System.Threading.Tasks.Task CloseAsync() { }
        protected override System.Threading.Tasks.Task InitializeAsync() { }
    }
}
namespace Orc.AutomaticSupport.Views
{
    
    public class RequestSupportView : Catel.Windows.Window, System.Windows.Markup.IComponentConnector
    {
        public RequestSupportView() { }
        public void InitializeComponent() { }
    }
}