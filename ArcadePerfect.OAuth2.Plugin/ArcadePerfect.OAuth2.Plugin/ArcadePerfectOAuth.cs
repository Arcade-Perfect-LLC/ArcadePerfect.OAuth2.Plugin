using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadePerfect.OAuth2.Plugin;
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

	public string? ErrorLog { get; set; }

	public ArcadePerfectOAuth(string? authorization_Code, NameValueCollection? queryString)
	{
		Authorization_Code = authorization_Code;
		QueryString = queryString;
	}

	public ArcadePerfectOAuth(string? errorLog)
	{
		ErrorLog = errorLog;
	}
}
