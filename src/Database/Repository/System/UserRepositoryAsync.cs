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
using System.Linq.Expressions;
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

        public override Task<IEnumerable<User>> SelectAsync(Expression<Func<User, bool>> predicate, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<User>(uow).SelectAsync<User, Person, User>(predicate);
            }
            using var context = GetContext();
            return new DbProxy<User>(context.UnitOfWork).SelectAsync<User, Person, User>(predicate);
        }

        public override Task<object> InsertAsync(User user, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return InsertUserAsync(user, uow);
            }
            using var context = GetContext();
            var unitOfWork = context.UnitOfWork;
            unitOfWork.Begin();
            try
            {
                return InsertUserAsync(user, unitOfWork, true);
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        async Task<object> InsertUserAsync(User user, UnitOfWork uow, bool commit = false)
        {
            user.AddPasswordHash();
            user.PersonId = (long)await Persons.InsertAsync(user.Person, uow);
            user.Id = (long)await base.InsertAsync(user, uow);
            if (commit)
            {
                uow.Commit();
            }
            return user.Id;
        }
    }
}
