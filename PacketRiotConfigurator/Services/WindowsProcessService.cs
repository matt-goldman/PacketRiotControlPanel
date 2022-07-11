using System.Diagnostics;

namespace PacketRiotConfigurator.Services;

public class WindowsProcessService : IProcessService
{
    Process pktRiotProcess;

    public async Task StartProcess()
    {
        string ExeFilePath = Preferences.Get(nameof(ExeFilePath), "C:\\Program Files\\Packetriot\\pktriot.exe");

        if (File.Exists(ExeFilePath))
        {
            var procInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                //UseShellExecute = true,
                FileName = ExeFilePath,
                Arguments = "start"
            };

            pktRiotProcess = Process.Start(procInfo);
            MessagingCenter.Send<object, bool>(this, IProcessService.ProcessStateChangeMessage, true);
        }
        else
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    {DevicePlatform.WinUI, new[] {".exe"} }
                });

            PickOptions options = new PickOptions
            {
                PickerTitle = "Locate your PacketRiot executable",
                FileTypes = customFileType
            };

            var result = await FilePicker.Default.PickAsync(options);

            pktRiotProcess = Process.Start(result.FullPath);

            Preferences.Set(nameof(ExeFilePath), result.FullPath);

            MessagingCenter.Send<object, bool>(this, IProcessService.ProcessStateChangeMessage, true);
        }
    }

    public async Task StopProcess()
    {
        if (pktRiotProcess is null)
        {
            var processes = System.Diagnostics.Process.GetProcessesByName(IProcessService.ProcessName);

            if (processes.Any())
            {
                pktRiotProcess = processes.FirstOrDefault();
            }
        }

        if (pktRiotProcess is not null)
        {
            try
            {
                pktRiotProcess.Kill();
                await pktRiotProcess.WaitForExitAsync();
                pktRiotProcess.CloseMainWindow();
                pktRiotProcess.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
