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
using jotfi.Jot.Model.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace jotfi.Jot.Database.Repository.Base
{
    public partial class BaseRepository<S, T> : IBaseRepository<T>, IRepository where T : Transaction
    {
        protected readonly ILogger Log;
        protected readonly AppSettings Settings;

        public BaseRepository(IServiceProvider services)
        {
            Log = services.GetRequiredService<ILogger<S>>();
            Settings = services.GetRequiredService<IOptions<AppSettings>>().Value;
        }

        protected DbContext GetContext()
        {
            return new DbContext(Settings.Database);
        }

        public virtual bool Exists(UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).Exists();
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).Exists();
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).Any(predicate);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).Any(predicate);
        }

        public virtual long Count(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).Count(predicate);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).Count(predicate);
        }

        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null, bool buffered = true)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).Select(predicate, buffered);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).Select(predicate, buffered);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).FirstOrDefault(predicate);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).FirstOrDefault(predicate);
        }

        public virtual IEnumerable<T> GetAll(UnitOfWork? uow = null, bool buffered = true)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).GetAll(buffered);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).GetAll(buffered);
        }

        public virtual T Get(object id, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).Get(id);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).Get(id);
        }

        public virtual long Insert(T obj, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return Convert.ToInt64(new DbProxy<T>(uow).Insert(obj));
            }
            using var context = GetContext();
            return Convert.ToInt64(new DbProxy<T>(context.UnitOfWork).Insert(obj));
        }

        public virtual bool Update(T obj, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).Update(obj);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).Update(obj);
        }

        public virtual bool Delete(T obj, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).Delete(obj);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).Delete(obj);
        }
    }
}
