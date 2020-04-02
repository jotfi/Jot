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
using System;
using System.Collections.Generic;
using System.Text;

namespace jotfi.Jot.Database.Repository.System
{
    public partial class UserRepository : BaseRepository<UserRepository, User>, IBaseRepository<User>, IRepository
    {
        public UserRepository(IServiceProvider services) : base(services)
        {
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
            long userId;
            user.AddPasswordHash();
            if (uow != null)
            {
                user.PersonId = user.Person.InsertEntity(uow);
                userId = user.Insert(uow);
                user.Id = userId;
                return userId;
            }
            using var context = GetContext();
            var unitOfWork = context.UnitOfWork;
            unitOfWork.Begin();
            try
            {
                user.PersonId = user.Person.InsertEntity(unitOfWork);
                userId = base.Insert(user, unitOfWork);
                user.Id = userId;
                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
            return userId;
        }
    }
}
