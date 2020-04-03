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
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace jotfi.Jot.Core.Services.Base
{
    public abstract class BaseService<R, S, T> : ClientService<R>, IDbOperations<T> where S : IDbOperations<T>
    {
        protected readonly S Repository;

        public BaseService(IServiceProvider services) : base(services)
        {
            Repository = services.GetRequiredService<S>();
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            //Todo: Check IsClient
            return Repository.Any(predicate, uow);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual  long Count(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> CountAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete(T obj, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteAsync(T obj, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual bool Exists(UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> ExistsAsync(UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            return Repository.FirstOrDefault(predicate, uow);
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(object id, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll(UnitOfWork? uow = null, bool buffered = true)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync(UnitOfWork? uow = null)
        {
            return Repository.GetAllAsync(uow);
        }

        public virtual Task<T> GetAsync(object id, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual long Insert(T obj, UnitOfWork? uow = null)
        {
            return Repository.Insert(obj, uow);
        }

        public virtual Task<object> InsertAsync(T obj, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null, bool buffered = true)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual bool Update(T obj, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateAsync(T obj, UnitOfWork? uow = null)
        {
            throw new NotImplementedException();
        }
    }
}
