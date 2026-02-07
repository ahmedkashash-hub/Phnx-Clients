using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Contracts
{
    public interface ILanguageService
    {
        string GetMessage(string key);

    }
}