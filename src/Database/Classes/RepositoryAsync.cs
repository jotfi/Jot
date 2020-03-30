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
using Dommel;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jotfi.Jot.Database.Classes
{
    public partial class Repository<T> where T : Transaction
    {
        public virtual async Task<bool> ExistsAsync()
        {
            var count = await CountAsync(p => p.Id > 0);
            return count > 0;
        }
        public virtual Task<long> CountAsync(Expression<Func<T, bool>> predicate) 
            => UnitOfWork.Connection.CountAsync(predicate);
        public virtual Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> predicate) 
            => UnitOfWork.Connection.SelectAsync(predicate);
        public virtual Task<IEnumerable<T>> GetAllAsync(bool buffered = true) 
            => UnitOfWork.Connection.GetAllAsync<T>();
        public virtual Task<T> GetAsync(object id) 
            => UnitOfWork.Connection.GetAsync<T>(id);
        public virtual Task<bool> DeleteAsync(T obj) 
            => UnitOfWork.Connection.DeleteAsync(obj, UnitOfWork.Transaction);

        public virtual Task<object> InsertAsync(T obj)
        {
            obj.Init();
            return UnitOfWork.Connection.InsertAsync(obj, UnitOfWork.Transaction);
        }

        public virtual Task<bool> UpdateAsync(T obj)
        {
            obj.Init();
            return UnitOfWork.Connection.UpdateAsync(obj, UnitOfWork.Transaction);
        }
    }
}
