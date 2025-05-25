namespace ArcadePerfect.OAuth2.Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new MainPage()) { Title = "ArcadePerfect.OAuth2.Sample", MaximumWidth = 800, MaximumHeight=600 };
	}
}
