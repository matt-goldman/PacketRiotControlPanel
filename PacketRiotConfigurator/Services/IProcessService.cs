namespace PacketRiotConfigurator.Services;

public interface IProcessService
{
    public bool GetProcessIsRunning()
    {
        var processes = System.Diagnostics.Process.GetProcessesByName(ProcessName);

        if (processes.Any())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Task StartProcess();

    Task StopProcess();

    const string ProcessName = "pktriot";

    const string ProcessStateChangeMessage = "ProcessStateChanged";
}
