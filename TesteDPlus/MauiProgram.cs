using Microsoft.Extensions.Logging;
using TesteDPlus.DataAccess;
using TesteDPlus.ViewModels;
using TesteDPlus.Views;
namespace TesteDPlus;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		var dbContext = new ClienteDbContext();
		dbContext.Database.EnsureCreated();
		dbContext.Dispose();

		builder.Services.AddDbContext<ClienteDbContext>();

		builder.Services.AddTransient<ClientePage>();
        builder.Services.AddTransient<ClienteViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainViewModel>();

		Routing.RegisterRoute(nameof(ClientePage), typeof(ClientePage));



#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

