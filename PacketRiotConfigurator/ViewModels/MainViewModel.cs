using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace PacketRiotConfigurator.ViewModels;

[INotifyPropertyChanged]
public partial class MainViewModel
{
	private readonly IProcessService _processService;
	private readonly IConfigService _configService;

	[ObservableProperty]
	bool processIsRunning;

	[ObservableProperty]
	bool isRunning;

	[ObservableProperty]
	Config config;

	[ObservableProperty]
	string buttonGlyph;

	public ObservableCollection<Tunnel> Tunnels { get; set; } = new();

	public MainViewModel(IProcessService processService, IConfigService configService)
	{
		_processService = processService;
		_configService = configService;
	}

	public async Task Initialise()
	{
		IsRunning = true;

		ProcessIsRunning = _processService.GetProcessIsRunning();

		await LoadConfig();

        SetButtonGlyph();

		IsRunning = false;
    }

	[ICommand]
	async Task ToggleProcess()
	{
		if (ProcessIsRunning)
		{
			try
			{
				_processService.StopProcess();
				ProcessIsRunning = _processService.GetProcessIsRunning();
				if (ProcessIsRunning)
				{
					await ShowToggleError("stop");
				}
			}
			catch(Exception ex)
			{
				// log ex
				await ShowToggleError("stop");
			}
			finally
			{
				SetButtonGlyph();
			}
		}
		else
		{
			try
			{
				await _processService.StartProcess();
				ProcessIsRunning = _processService.GetProcessIsRunning();
				if (!ProcessIsRunning)
				{
					await ShowToggleError("start");
				}
			}
			catch (Exception ex)
			{
				// log ex
				await ShowToggleError("start");
			}
            finally
            {
                SetButtonGlyph();
            }
        }
	}

	private void SetButtonGlyph()
	{
        ButtonGlyph = ProcessIsRunning ? IconFont.Stop : IconFont.Play;
    }

	private async Task ShowToggleError(string state)
	{
        await App.Current.MainPage.DisplayAlert("Something went wrong", $"Couldn't {state} the PacketRiot service. Please check the logs for information", "OK");
    }

	[ICommand]
	async Task LoadConfig()
	{
		Config = await _configService.LoadConfig();

		Tunnels.Clear();

		if (Config.https is not null)
		{
			foreach (var tunnel in Config.https)
			{
				Tunnels.Add(tunnel);
			} 
		}
	}

	[ICommand]
	async Task SaveConfig()
	{
		var saveCfg = new Config
		{
			hostname = Config.hostname,
			key = Config.key,
			serverHost = config.serverHost,
			serverKey = config.serverKey,
			version = config.version
		};

		saveCfg.https = new();

		foreach (var tunnel in Tunnels)
		{
			saveCfg.https.Add(tunnel);
		}

		await _configService.SaveConfig(saveCfg);
	}

	[ICommand]
	async Task CopyUrl(string url)
	{
		await Clipboard.Default.SetTextAsync(url);

		var snackbar = Snackbar.Make("Copied!");
		await snackbar.Show();
	}
}
