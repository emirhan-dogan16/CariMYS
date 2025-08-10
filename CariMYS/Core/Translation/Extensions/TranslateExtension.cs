using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Translation.Extensions
{
    public static class TranslateExtension
    {
        public static string Translate(this TranslateJson json, Language lang)
        {
            if (json == null)
                return string.Empty;

            var supported = SupportedLanguagesHolder.SupportedLanguages;
            var langCode = lang.ToString().ToLower();

            if (supported != null && !supported.Contains(langCode))
                throw new NotSupportedException($"Dil '{lang}' desteklenmiyor.");


            string? result = lang switch
            {
                Language.Tr => json.Tr,
                Language.En => json.En,
                _ => null
            };

            return result
                ?? json.Tr
                ?? json.En
                ?? string.Empty;
        }

        public static void AddSupportedLanguages(this IServiceCollection services, IConfiguration configuration)
        {
            var supportedLanguages = configuration.GetSection("SupportedLanguages").Get<string[]?>();
            SupportedLanguagesHolder.Initialize(supportedLanguages);
        }
    }
}
