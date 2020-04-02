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
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jotfi.Jot.Database.Repository.Base
{
    public partial class BaseRepository<S, T> where T : Transaction
    {
        public virtual Task<bool> ExistsAsync(UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).ExistsAsync();
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).ExistsAsync();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).AnyAsync(predicate);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).AnyAsync(predicate);
        }

        public virtual Task<long> CountAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).CountAsync(predicate);
        }

        public virtual Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).SelectAsync(predicate);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).SelectAsync(predicate);
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).FirstOrDefaultAsync(predicate);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).FirstOrDefaultAsync(predicate);
        }

        public virtual Task<IEnumerable<T>> GetAllAsync(UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).GetAllAsync();
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).GetAllAsync();
        }

        public virtual Task<T> GetAsync(object id, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).GetAsync(id);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).GetAsync(id);
        }

        public virtual Task<object> InsertAsync(T obj, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).InsertAsync(obj);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).InsertAsync(obj);
        }

        public virtual Task<bool> UpdateAsync(T obj, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).UpdateAsync(obj);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).UpdateAsync(obj);
        }

        public virtual Task<bool> DeleteAsync(T obj, UnitOfWork? uow = null)
        {
            if (uow != null)
            {
                return new DbProxy<T>(uow).DeleteAsync(obj);
            }
            using var context = GetContext();
            return new DbProxy<T>(context.UnitOfWork).DeleteAsync(obj);
        }
    }
}
