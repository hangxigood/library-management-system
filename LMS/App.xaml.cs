namespace LMS;

public partial class App : Application
{
	public static DatabaseConfig DatabaseConfig { get; private set; }
	public App()
	{
		InitializeComponent();

		// Initialize the database configuration
        DatabaseConfig = new DatabaseConfig();

		MainPage = new MainPage();
	}
}
