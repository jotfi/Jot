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

using jotfi.Jot.Base.Utils;
using jotfi.Jot.Database.Classes;
using jotfi.Jot.Database.Repository.Base;
using jotfi.Jot.Model.System;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Database.Repository.System
{
    public partial class UserRepository : BaseRepository<UserRepository, User>
    {
        private readonly PersonRepository Persons;

        public UserRepository(IServiceProvider services) : base(services)
        {
            Persons = services.GetRequiredService<PersonRepository>();
        }

        public override User Get(object id, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<User>(uow).Get<User, Person, User>(id);
            }
            using var context = GetContext();
            return new DbProxy<User>(context.UnitOfWork).Get<User, Person, User>(id);
        }

        public override long Insert(User user, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return InsertUser(user, uow);
            }
            using var context = GetContext();
            var unitOfWork = context.UnitOfWork;
            unitOfWork.Begin();
            try
            {
                InsertUser(user, unitOfWork, true);
                return user.Id;
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }

        long InsertUser(User user, UnitOfWork uow, bool commit = false)
        {
            user.AddPasswordHash();
            user.PersonId = Persons.Insert(user.Person, uow);
            user.Id = base.Insert(user, uow);
            if (commit)
            {
                uow.Commit();
            }
            return user.Id;
        }
    }
}
