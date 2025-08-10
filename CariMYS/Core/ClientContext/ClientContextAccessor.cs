using System;
using System.Globalization;
using System.Linq;
using Core.Translation;
using Microsoft.AspNetCore.Http;

namespace Core.Application.ClientContext
{
    public class ClientContextAccessor : IClientContextAccessor
    {
        public virtual ClientContext Context { get; protected set; }

        public ClientContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            Context = BuildContext(httpContext);
        }

        protected virtual ClientContext BuildContext(HttpContext httpContext)
        {
            var ipAddress = GetIpAddress(httpContext);
            var language = GetLanguage(httpContext);
            var utcOffset = GetUtcOffset(httpContext);
            var userId = GetUserId(httpContext);
            SetCulture(language);

            return new ClientContext(userId, language, utcOffset, ipAddress);
        }

        protected virtual string GetIpAddress(HttpContext httpContext)
        {
            return httpContext?.Connection?.RemoteIpAddress?.ToString() ?? String.Empty;
        }

        protected virtual Language GetLanguage(HttpContext httpContext)
        {
            var language = httpContext?.Request?.Headers["X-Language"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(language))
                return Language.Tr;

            var supported = SupportedLanguagesHolder.SupportedLanguages;
            var langCode = language.ToLowerInvariant();

            if (supported != null && !supported.Any(l => l.Equals(langCode, StringComparison.OrdinalIgnoreCase)))
                throw new NotSupportedException($"Dil '{language}' desteklenmiyor.");

            if (Enum.TryParse<Language>(language, true, out var culture))
                return culture;

            return Language.Tr;
        }


        protected void SetCulture(Language language)
        {
            var cultureName = language.ToString();
            var cultureInfo = new CultureInfo(cultureName);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }


        protected virtual TimeSpan GetUtcOffset(HttpContext httpContext)
        {
            var utcOffsetHeader = httpContext?.Request?.Headers["X-Timezone-Offset"].FirstOrDefault();
            return ParseUtcOffset(utcOffsetHeader);
        }

        protected virtual string GetUserId(HttpContext httpContext)
        {
            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                return httpContext.User.FindFirst("sub")?.Value
                       ?? httpContext.User.Identity.Name
                       ?? string.Empty;
            }
            return httpContext.Request.Headers["X-User-Id"].FirstOrDefault() ?? string.Empty;
        }


        protected virtual TimeSpan ParseUtcOffset(string offset)
        {
            if (int.TryParse(offset, out var minutes))
                return TimeSpan.FromMinutes(minutes);

            return TimeSpan.Zero;
        }
    }

    public enum IpAddressStrategy
    {
        None,
        XForwardedFor,
        XRealIp,
        CustomHeader
    }
}
