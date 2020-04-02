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

using jotfi.Jot.Database.Classes;
using jotfi.Jot.Model.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jotfi.Jot.Database.Repository.System
{
    public partial class UserRepository
    {
        public override Task<User> GetAsync(object id, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<User>(uow).GetAsync<User, Person, User>(id);
            }
            using var context = GetContext();
            return new DbProxy<User>(context.UnitOfWork).GetAsync<User, Person, User>(id);
        }

        public override Task<IEnumerable<User>> GetAllAsync(UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<User>(uow).GetAllAsync<User, Person, User>();
            }
            using var context = GetContext();
            return new DbProxy<User>(context.UnitOfWork).GetAllAsync<User, Person, User>();
        }

        public override Task<object> InsertAsync(User user, UnitOfWork? uow = null)
        {
            return Task.Run(async () =>
            {
                user.AddPasswordHash();
                if (uow != null)
                {
                    user.PersonId = Convert.ToInt64(user.Person.InsertEntityAsync(uow));
                    user.Id = Convert.ToInt64(user.InsertAsync(uow));
                    return user.Id;
                }
                using var context = GetContext();
                var unitOfWork = context.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    user.PersonId = Convert.ToInt64(user.Person.InsertEntityAsync(unitOfWork));
                    user.Id = Convert.ToInt64(user.InsertAsync(unitOfWork));
                    await unitOfWork.CommitAsync();
                }
                catch
                {
                    await unitOfWork.RollbackAsync();
                    throw;
                }
                return (object)user.Id;
            });
        }
    }
}
