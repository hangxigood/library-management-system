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

        builder.Services.AddSingleton(new DatabaseConfig("LibraryManagement.db"));

        builder.Services.AddSingleton<IBookService>(s => new BookService(s.GetRequiredService<DatabaseConfig>().dbPath));
        builder.Services.AddSingleton<IMemberService>(s => new MemberService(s.GetRequiredService<DatabaseConfig>().dbPath));
        builder.Services.AddSingleton<ILoanService>(s => new LoanService(s.GetRequiredService<DatabaseConfig>().dbPath));

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
