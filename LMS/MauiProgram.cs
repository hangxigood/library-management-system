using Microsoft.Extensions.Logging;

namespace LMS;

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
			});

		builder.Services.AddMauiBlazorWebView();

		string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "LibraryManagement.db");
		Console.WriteLine($"{dbPath}");
        builder.Services.AddSingleton<IBookService>(s => new BookService(dbPath));
        builder.Services.AddSingleton<IMemberService>(s => new MemberService(dbPath));
        builder.Services.AddSingleton<IAdminService>(s => new AdminService(dbPath));

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
