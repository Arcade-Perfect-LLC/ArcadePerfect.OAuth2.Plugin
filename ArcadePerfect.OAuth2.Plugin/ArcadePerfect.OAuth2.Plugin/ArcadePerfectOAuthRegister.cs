using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ArcadePerfect.OAuth2.Plugin;

/// <summary>
/// Extension methods for registering the ArcadePerfectOAuth2Service with the dependency injection container.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ArcadePerfectOAuthRegister
{
	/// <summary>
	/// Registers the ArcadePerfectOAuth2Service with the specified OAuth URL, optional HTML content and optional port.
	/// </summary>
	/// <param name="services"></param>
	/// <param name="OAuthUrl"></param>
	/// <param name="html"></param>
	/// <param name="port"></param>
	/// <returns></returns>
	public static IServiceCollection AddArcadePerfectOAuthServices(this IServiceCollection services, string OAuthUrl, string html = "", int port = 41794)
	{
		services.AddScoped<IArcadePerfectOAuth2Service, ArcadePerfectOAuth2Service>(serviceProvider => new ArcadePerfectOAuth2Service(OAuthUrl, html, port));
		return services;
	}

	/// <summary>
	/// Registers the ArcadePerfectOAuth2Service with the default constructor.<br/>
	/// Please note that this constructor does not set the OAuthURL, so it should be used with caution.<br/>
	/// Use this method if you will be more than one OAuth provider<br/>
	/// </summary>
	/// <param name="services"></param>
	/// <param name="html"></param>
	/// <param name="port"></param>
	/// <returns></returns>
	public static IServiceCollection AddArcadePerfectOAuthServices(this IServiceCollection services, string html = "", int port = 41794)
	{
		services.AddScoped<IArcadePerfectOAuth2Service, ArcadePerfectOAuth2Service>(serviceProvider => new ArcadePerfectOAuth2Service(html, port));
		return services;
	}
}
