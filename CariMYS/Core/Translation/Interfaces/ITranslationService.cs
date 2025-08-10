using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Translation.Interfaces
{
    public interface ITranslationService
    {
        string Translate(string text, Language sourceLanguage, Language targetLanguage);
        TranslateJson TranslateAll(string text, Language sourceLanguage);
    }
}
