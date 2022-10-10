namespace Orc.AutomaticSupport.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Catel.MVVM;
    using Catel.Services;

    public class RequestSupportViewModel : ViewModelBase
    {
        private readonly IAutomaticSupportService _automaticSupportService;
        private readonly ILanguageService _languageService;

        public RequestSupportViewModel(IAutomaticSupportService automaticSupportService, ILanguageService languageService)
        {
            ArgumentNullException.ThrowIfNull(automaticSupportService);
            ArgumentNullException.ThrowIfNull(languageService);

            _automaticSupportService = automaticSupportService;
            _languageService = languageService;

            RemainingTime = string.Empty;
            Title = languageService.GetRequiredString("AutomaticSupport_AutomaticSupport");
        }

        public int Progress { get; private set; }

        public string RemainingTime { get; private set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _automaticSupportService.DownloadProgressChanged += OnAutomaticSupportServiceDownloadProgressChanged;
            _automaticSupportService.SupportAppClosed += OnAutomaticSupportClosed;

            // Note: don't await, let it run by itself
#pragma warning disable 4014
            _automaticSupportService.DownloadAndRunAsync();
#pragma warning restore 4014
        }

        protected override async Task CloseAsync()
        {
            _automaticSupportService.DownloadProgressChanged -= OnAutomaticSupportServiceDownloadProgressChanged;
            _automaticSupportService.SupportAppClosed -= OnAutomaticSupportClosed;

            await base.CloseAsync();
        }

        private void OnAutomaticSupportServiceDownloadProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            Progress = e.Progress;
            RemainingTime = string.Format(_languageService.GetRequiredString("AutomaticSupport_RemainingTime"), e.RemainingTime);
        }

        private void OnAutomaticSupportClosed(object? sender, EventArgs e)
        {
#pragma warning disable 4014
            CloseViewModelAsync(true);
#pragma warning restore 4014
        }
    }
}
