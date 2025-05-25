using System.Diagnostics;
using System.Net;
using System.Text;

namespace ArcadePerfect.OAuth2.Plugin;

/// <summary>
/// 
/// </summary>
public class ArcadePerfectOAuth2Service : IArcadePerfectOAuth2Service
{
	private const string DEFAULT_HTML = "<html><head><title>OAuth 2.0 Authentication Code Received</title></head><body> Authorized!. You may now close this window.</body></html>";
	private readonly string OAuthURL;
	private string Html { get; set; } = DEFAULT_HTML;
	private readonly int port;

	/// <summary>
	/// Creates an instance of the ArcadePerfectOAuth2Service class.
	/// </summary>
	/// <param name="OAuthURL">Set Provider OAuth2 URL</param>
	/// <param name="html">Optionally provide html text for succesfull authenticated message.</param>
	/// <param name="port">Optionally provide a port for listening for the OAuth redirect.</param>
	public ArcadePerfectOAuth2Service(string OAuthURL, string html = DEFAULT_HTML, int port = 41794)
	{
		Debug.WriteLine($"oauthurl: {OAuthURL}");
		this.OAuthURL = OAuthURL;
		if (!string.IsNullOrWhiteSpace(html))
			Html = html;
		this.port = port;
	}

	/// <summary>
	/// Initiates the OAuth2 authentication flow. It starts a local HTTP listener on the specified port when registered the service.
	/// </summary>
	/// <returns>The task result contains object [<see cref="ArcadePerfectOAuth"/>] with authorization code and query strings containing others url parameters if successful; otherwise, null.</returns>
	public async Task<ArcadePerfectOAuth> LaunchOAuthFlow()
	{
		try
		{
			using var httpListener = new HttpListener();
			httpListener.Prefixes.Add(GetRedirectUrl(port));
			httpListener.Start();

			Process.Start(new ProcessStartInfo("cmd", $"/c start \"\" \"{OAuthURL}\"") { CreateNoWindow = true });

			var context = await httpListener.GetContextAsync();



			var bytes = Encoding.UTF8.GetBytes(Html);

			context.Response.ContentLength64 = bytes.Length;
			context.Response.SendChunked = false;
			context.Response.KeepAlive = false;
			var output = context.Response.OutputStream;
			await output.WriteAsync(bytes);

			var result = context.Request.QueryString;
			var code = result["code"];
			output.Close();
			context.Response.Close();
			return new ArcadePerfectOAuth(code, result);
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Error Launching OAuth2 Flow: {ex}");
			return new ArcadePerfectOAuth(ex.Message);
		}
	}

	/// <summary>
	/// **** ONLY USE IF USING WITH DIFERENT OAUTH PROVIDER THAN THE MAIN ONE ****<br/>
	/// Initiates the OAuth2 authentication flow. It starts a local HTTP listener on the specified port 
	/// to receive the callback from the OAuth provider after the user authenticates in their default browser.<br/>
	/// Redirection URL is as follow: http://localhost:{port}/authorized. -> make sure to add this URL to the OAuth provider's redirect URL list.<br/>
	/// Example Default RedirectUrl: http://localhost:41794/authorized
	/// </summary>
	/// <param name="OAuthURL">The authorization URL of the OAuth2 provider.</param>
	/// <param name="html">The HTML content to display after the user authorizes the application. Defaults to a simple success message.</param>
	/// <param name="port">The local port to listen on for the redirect from the OAuth provider. Defaults to 41794.</param>
	/// <returns>The task result contains object [<see cref="ArcadePerfectOAuth"/>] with authorization code and query strings containing others url parameters if successful; otherwise, null.</returns>
	public async Task<ArcadePerfectOAuth> LaunchOAuthFlowForced(string OAuthURL, string html = DEFAULT_HTML, int port = 41794)
	{
		try
		{
			using var httpListener = new HttpListener();
			httpListener.Prefixes.Add(GetRedirectUrl(port));
			httpListener.Start();

			Process.Start(new ProcessStartInfo("cmd", $"/c start \"\" \"{OAuthURL}\"") { CreateNoWindow = true });

			var context = await httpListener.GetContextAsync();

			if (string.IsNullOrWhiteSpace(html))
				html = DEFAULT_HTML;

			var bytes = Encoding.UTF8.GetBytes(html);

			context.Response.ContentLength64 = bytes.Length;
			context.Response.SendChunked = false;
			context.Response.KeepAlive = false;
			var output = context.Response.OutputStream;
			await output.WriteAsync(bytes);

			var result = context.Request.QueryString;
			var code = result["code"];
			output.Close();
			context.Response.Close();
			return new ArcadePerfectOAuth(code, result);
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"Error Launching OAuth2 Flow: {ex}");
			return new ArcadePerfectOAuth(ex.Message);
		}
	}

	private string GetRedirectUrl(int port) => $"http://localhost:{port}/authorized/";
}//class
