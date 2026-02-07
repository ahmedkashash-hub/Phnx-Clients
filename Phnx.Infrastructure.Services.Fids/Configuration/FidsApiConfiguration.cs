using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Infrastructure.Services.Fids.Configuration
{

    public class FidsApiConfiguration
    {
        public string BaseUrl { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string DeparturesPath { get; init; } = "api/FIDS/currentflights/departures";
        public string ArrivalsPath { get; init; } = "api/FIDS/currentflights/arrivals";
    }
}