﻿namespace Orc.AutomaticSupport;

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Catel.Logging;
using Catel.Services;
using FileSystem;

public class AutomaticSupportService : IAutomaticSupportService
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    private readonly IDispatcherService _dispatcherService;
    private readonly IFileService _fileService;
    private readonly IDirectoryService _directoryService;
    private readonly IProcessService _processService;
    private readonly DateTime _startedTime;

    public AutomaticSupportService(IProcessService processService, IDispatcherService dispatcherService,
        IFileService fileService, IDirectoryService directoryService)
    {
        ArgumentNullException.ThrowIfNull(processService);
        ArgumentNullException.ThrowIfNull(dispatcherService);
        ArgumentNullException.ThrowIfNull(fileService);
        ArgumentNullException.ThrowIfNull(directoryService);

        _processService = processService;
        _dispatcherService = dispatcherService;
        _fileService = fileService;
        _directoryService = directoryService;

        SupportUrl = string.Empty;
        _startedTime = DateTime.Now;
        CommandLineParameters = string.Empty;
    }

    public string SupportUrl { get; set; }

    public string CommandLineParameters { get; set; }

    public event EventHandler<ProgressChangedEventArgs>? DownloadProgressChanged;
    public event EventHandler<EventArgs>? DownloadCompleted;
    public event EventHandler<EventArgs>? SupportAppClosed;

    public async Task DownloadAndRunAsync()
    {
        if (string.IsNullOrWhiteSpace(SupportUrl))
        {
            throw Log.ErrorAndCreateException<InvalidOperationException>("Please initialize the service by setting the SupportUrl property");
        }

        Log.Info("Downloading support app from '{0}'", SupportUrl);

#pragma warning disable SYSLIB0014 // Type or member is obsolete
        using (var webClient = new WebClient())
        {
            webClient.DownloadProgressChanged += OnWebClientOnDownloadProgressChanged;
            webClient.DownloadDataCompleted += OnWebClientOnDownloadDataCompleted;

            var data = await webClient.DownloadDataTaskAsync(SupportUrl);

            Log.Info("Support app is downloaded, storing file in temporary folder");

            var tempDirectory = Path.Combine(Path.GetTempPath(), "Orc_AutomaticSupport", DateTime.Now.ToString("yyyyMMddHHmmss"));
            _directoryService.Create(tempDirectory);

            var tempFile = Path.Combine(tempDirectory, "SupportApp.exe");

            await _fileService.WriteAllBytesAsync(tempFile, data);

            Log.Info("Running support app");

            _processService.StartProcess(tempFile, CommandLineParameters, (context, exitCode) =>
            {
                _dispatcherService.BeginInvoke(() => SupportAppClosed?.Invoke(this, EventArgs.Empty));
            });
        }
#pragma warning restore SYSLIB0014 // Type or member is obsolete
    }

    private void OnWebClientOnDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
    {
        var webClient = (WebClient)sender;

        webClient.DownloadProgressChanged -= OnWebClientOnDownloadProgressChanged;
        webClient.DownloadDataCompleted -= OnWebClientOnDownloadDataCompleted;

        DownloadCompleted?.Invoke(this, EventArgs.Empty);
    }

    private void OnWebClientOnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        var remainingTime = CalculateEta(_startedTime, Convert.ToInt32(e.TotalBytesToReceive), Convert.ToInt32(e.BytesReceived));
        DownloadProgressChanged?.Invoke(this, new ProgressChangedEventArgs(e.ProgressPercentage, remainingTime));
    }

    private TimeSpan CalculateEta(DateTime startedTime, int totalBytesToReceive, int bytesReceived)
    {
        var duration = (int)(DateTime.Now - startedTime).TotalSeconds;
        if (duration == 0)
        {
            return new TimeSpan(0, 0, 0);
        }

        var bytesPerSecond = bytesReceived / duration;
        if (bytesPerSecond == 0)
        {
            return new TimeSpan(0, 0, 0);
        }

        var secondsRemaining = (totalBytesToReceive - bytesReceived) / bytesPerSecond;

        return new TimeSpan(0, 0, secondsRemaining);
    }
}
