using Phnx.Contracts;
using Phnx.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Infrastructure.Services
{

    public class LanguageService(LanguageManager languageManager, ICurrentUserService currentUserService) : ILanguageService
    {
        public string GetMessage(string key)
        {
            return languageManager.GetValue(currentUserService.Language, key);
        }
    }
}