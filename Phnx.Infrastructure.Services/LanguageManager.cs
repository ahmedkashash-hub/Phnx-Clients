using Phnx.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;

namespace Phnx.Infrastructure.Services
{

    public class LanguageManager(ResourceManager resourceManager)
    {
        public string GetValue(Language language, string key)
        {
            return resourceManager.GetString(key, new CultureInfo(language.ToString()))
                ?? throw new KeyNotFoundException($"language:{language}\nResource Key:{key}");
        }
    }
}