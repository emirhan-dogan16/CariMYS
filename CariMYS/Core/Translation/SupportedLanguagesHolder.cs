using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Translation
{
    public static class SupportedLanguagesHolder
    {
        public static HashSet<string>? SupportedLanguages { get; private set; } = new HashSet<string>();

        public static void Initialize(IEnumerable<string>? languages)
        {
            SupportedLanguages = new HashSet<string>(languages ?? null, StringComparer.OrdinalIgnoreCase);
        }
    }
}
