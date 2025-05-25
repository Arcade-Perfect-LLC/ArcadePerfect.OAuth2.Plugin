using System.Collections.Specialized;

namespace ArcadePerfect.OAuth2.Plugin;
/// <summary>
/// Provides methods to initiate and manage OAuth2 authentication flows for OAuthProvider.
/// </summary>
public interface IArcadePerfectOAuth2Service
{
	const string DEFAULT_HTML = "<html><head><title>OAuth 2.0 Authentication Code Received</title></head><body> Authorized!. You may now close this window.</body></html>";
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
	Task<ArcadePerfectOAuth> LaunchOAuthFlowForced(string OAuthURL, string html = DEFAULT_HTML, int port = 41794);

	/// <summary>
	/// Initiates the OAuth2 authentication flow. It starts a local HTTP listener on the specified port 
	/// to receive the callback from the OAuth provider after the user authenticates in their default browser.<br/>
	/// Redirection URL is as follow: http://localhost:{port}/authorized. -> make sure to add this URL to the OAuth provider's redirect URL list.<br/>
	/// Example Default RedirectUrl: http://localhost:41794/authorized
	/// </summary>
	/// <returns>The task result contains object [<see cref="ArcadePerfectOAuth"/>] with authorization code and query strings containing others url parameters if successful; otherwise, null.</returns>
	Task<ArcadePerfectOAuth> LaunchOAuthFlow();
}
