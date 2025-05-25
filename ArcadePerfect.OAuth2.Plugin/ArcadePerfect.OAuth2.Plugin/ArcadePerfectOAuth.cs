using System.Collections.Specialized;

namespace ArcadePerfect.OAuth2.Plugin;
/// <summary>
/// Represents the OAuth2 authentication result for ArcadePerfect.<br/>
/// </summary>
public class ArcadePerfectOAuth
{
	/// <summary>
	/// Contains only the authorization code returned from the OAuth provider.<br/>
	/// Necessary to exchange for an access token.
	/// </summary>
	public string? Authorization_Code { get; init; } = default;

	/// <summary>
	/// Collection Containing the query string parameters returned from the OAuth provider.
	/// </summary>
	public NameValueCollection? QueryString { get; init; } = default;

	/// <summary>
	/// Contains the error log if any error occurs during the OAuth flow.<br/>
	/// </summary>
	public string? ErrorLog { get; set; }

	/// <summary>
	/// Creates an instance of the ArcadePerfectOAuth class.<br/>
	/// </summary>
	/// <param name="authorization_Code"></param>
	/// <param name="queryString"></param>
	public ArcadePerfectOAuth(string? authorization_Code, NameValueCollection? queryString)
	{
		Authorization_Code = authorization_Code;
		QueryString = queryString;
	}

	/// <summary>
	/// Creates an instance of the ArcadePerfectOAuth class.<br/>
	/// </summary>
	/// <param name="errorLog"></param>
	public ArcadePerfectOAuth(string? errorLog)
	{
		ErrorLog = errorLog;
	}
}
