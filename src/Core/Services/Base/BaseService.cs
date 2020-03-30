#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.
//
#endregion
using jotfi.Jot.Base.Settings;
using jotfi.Jot.Base.System;
using jotfi.Jot.Database.Classes;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace jotfi.Jot.Core.Services.Base
{
    public class BaseService
    {
        protected readonly AppSettings Settings;
        protected readonly HttpClient Client;

        public BaseService(IOptions<AppSettings> settings)
        {
            Settings = settings.Value;
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            Client = new HttpClient
            {
                BaseAddress = new Uri(Settings.ServerUrl)
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(mediaType);
            //using var cts = new CancellationTokenSource(new TimeSpan(0, 0, 5));
            //var response = await Client.GetAsync("user", cts.Token).ConfigureAwait(false);
            Client.Timeout = new TimeSpan(0, 0, 10);
        }

        public DbContext GetContext()
        {
            return new DbContext(Settings.Database);
        }
    }
}
