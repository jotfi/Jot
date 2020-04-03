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
using jotfi.Jot.Database.Classes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace jotfi.Jot.Core.Services.Base
{
    public abstract class ClientService<T> : IService
    {
        protected readonly ILogger Log;
        protected readonly AppSettings Settings;
        protected readonly HttpClient Client;

        public ClientService(IServiceProvider services)
        {
            Log = services.GetRequiredService<ILogger<T>>();
            Settings = services.GetRequiredService<IOptions<AppSettings>>().Value;
            Client = new HttpClient
            {
                BaseAddress = new Uri(Settings.ServerUrl),
                Timeout = new TimeSpan(0, 0, 10)
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public DbContext GetContext()
        {
            return new DbContext(Settings.Database);
        }
    }
}
