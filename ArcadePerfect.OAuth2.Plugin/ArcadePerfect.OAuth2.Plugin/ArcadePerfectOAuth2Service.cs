
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ArcadePerfect.OAuth2.Plugin;

/// <summary>
/// 
/// </summary>
public class ArcadePerfectOAuth2Service : IArcadePerfectOAuth2Service
{
	private const string DEFAULT_HTML = "<html><head><title>OAuth 2.0 Authentication Code Received</title></head><body> Authorized!. You may now close this window.</body></html>";
	private readonly string OAuthURL;
	private string? html { get; set; }
	private readonly int port;

	public ArcadePerfectOAuth2Service(string OAuthURL, string html = DEFAULT_HTML, int port = 41794)
	{
		Debug.WriteLine($"oauthurl: {OAuthURL}");
		this.OAuthURL = OAuthURL;
		if (string.IsNullOrWhiteSpace(html))
			this.html = DEFAULT_HTML;
		this.port = port;
	}

	public async Task<ArcadePerfectOAuth> LaunchOAuthFlow()
	{
		try
		{
			using var httpListener = new HttpListener();
			httpListener.Prefixes.Add(GetRedirectUrl(port));
			httpListener.Start();

			Process.Start(new ProcessStartInfo("cmd", $"/c start \"\" \"{OAuthURL}\"") { CreateNoWindow = true });

			var context = await httpListener.GetContextAsync();

			
			
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
