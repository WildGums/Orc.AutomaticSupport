// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestSupportViewModel.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2014 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.AutomaticSupport.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Catel.Threading;

    public class RequestSupportViewModel : ViewModelBase
    {
        private readonly IAutomaticSupportService _automaticSupportService;

        public RequestSupportViewModel(IAutomaticSupportService automaticSupportService)
        {
            Argument.IsNotNull(() => automaticSupportService);

            _automaticSupportService = automaticSupportService;

            Title = "Automatic support";
        }

        public int Progress { get; set; }

        public TimeSpan RemainingTime { get; set; }

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

        private void OnAutomaticSupportServiceDownloadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.Progress;
            RemainingTime = e.RemainingTime;
        }

        private async void OnAutomaticSupportClosed(object sender, EventArgs e)
        {
            await CloseViewModelAsync(true);
        }
    }
}