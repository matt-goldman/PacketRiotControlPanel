using CommunityToolkit.Maui;

namespace PacketRiotConfigurator;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("fa-solid-900.ttf", "FASolid");
			});

		builder.Services.AddSingleton<IConfigService, ConfigService>();

		if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
		{
			builder.Services.AddSingleton<IProcessService, WindowsProcessService>();
		}
		else if (DeviceInfo.Current.Platform == DevicePlatform.macOS)
		{
			builder.Services.AddSingleton<IProcessService, MacProcessService>();
		}

		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<MainPage>();

		return builder.Build();
	}
}
