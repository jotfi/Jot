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
using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Database.Classes;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.Services.System
{
    public partial class UserService
    {
        public async Task<long> CreateUserAsync(User user)
        {
            try
            {
                long userId = 0;
                using var context = GetContext();
                var unitOfWork = context.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    PasswordUtils.CreatePasswordHash(user.CreatePassword, out byte[] hash, out byte[] salt);
                    user.PasswordHash = hash;
                    user.PasswordSalt = salt;
                    user.Person.ContactDetailsId = (long)await user.Person.ContactDetails.InsertAsync(unitOfWork);
                    user.Person.AddressId = (long)await user.Person.Address.InsertAsync(unitOfWork);
                    user.PersonId = (long)await user.Person.InsertAsync(unitOfWork);
                    userId = (long)await user.InsertAsync(unitOfWork);
                    user.Id = userId;
                    await unitOfWork.CommitAsync();
                    return userId;
                }
                catch
                {
                    await unitOfWork.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
                return 0;
            }
        }

    }
}
