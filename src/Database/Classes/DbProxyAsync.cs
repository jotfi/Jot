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
using System.Linq.Expressions;
using System.Threading.Tasks;

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
namespace jotfi.Jot.Database.Classes
{
    public partial class DbProxy<T> where T : Transaction
    {
        public virtual async Task<bool> ExistsAsync()
            => await AnyAsync(p => p.Id > 0);

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
            => UnitOfWork.Connection.AnyAsync(predicate, UnitOfWork.Transaction);

        public virtual Task<long> CountAsync(Expression<Func<T, bool>> predicate) 
            => UnitOfWork.Connection.CountAsync(predicate, UnitOfWork.Transaction);

        public virtual Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> predicate) 
            => UnitOfWork.Connection.SelectAsync(predicate, UnitOfWork.Transaction);

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
            => UnitOfWork.Connection.FirstOrDefaultAsync(predicate, UnitOfWork.Transaction);

        public virtual Task<IEnumerable<T>> GetAllAsync() 
            => UnitOfWork.Connection.GetAllAsync<T>(UnitOfWork.Transaction);
        public virtual Task<IEnumerable<T>> GetAllAsync<T1, T2, T>(bool buffered = true) where T1 : class, T
            => UnitOfWork.Connection.GetAllAsync<T1, T2, T>(UnitOfWork.Transaction, buffered);
        public virtual Task<IEnumerable<T>> GetAllAsync<T1, T2, T>(Func<T1, T2, T> map, bool buffered = true)
            => UnitOfWork.Connection.GetAllAsync(map, UnitOfWork.Transaction, buffered);
        public virtual Task<IEnumerable<T>> GetAllAsync<T1, T2, T3, T>(Func<T1, T2, T3, T> map, bool buffered = true)
            => UnitOfWork.Connection.GetAllAsync(map, UnitOfWork.Transaction, buffered);
        public virtual Task<IEnumerable<T>> GetAllAsync<T1, T2, T3, T4, T>(Func<T1, T2, T3, T4, T> map, bool buffered = true)
            => UnitOfWork.Connection.GetAllAsync(map, UnitOfWork.Transaction, buffered);
        public virtual Task<IEnumerable<T>> GetAllAsync<T1, T2, T3, T4, T5, T>(Func<T1, T2, T3, T4, T5, T> map, bool buffered = true)
            => UnitOfWork.Connection.GetAllAsync(map, UnitOfWork.Transaction, buffered);
        public virtual Task<IEnumerable<T>> GetAllAsync<T1, T2, T3, T4, T5, T6, T>(Func<T1, T2, T3, T4, T5, T6, T> map, bool buffered = true)
            => UnitOfWork.Connection.GetAllAsync(map, UnitOfWork.Transaction, buffered);
        public virtual Task<IEnumerable<T>> GetAllAsync<T1, T2, T3, T4, T5, T6, T7, T>(Func<T1, T2, T3, T4, T5, T6, T7, T> map, bool buffered = true)
            => UnitOfWork.Connection.GetAllAsync(map, UnitOfWork.Transaction, buffered);

        public virtual Task<T> GetAsync(object id) 
            => UnitOfWork.Connection.GetAsync<T>(id);
        public virtual Task<T> GetAsync<T1, T2, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.GetAsync<T1, T2, T>(id, UnitOfWork.Transaction);
        public virtual Task<T> GetAsync<T1, T2, T3, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.GetAsync<T1, T2, T3, T>(id, UnitOfWork.Transaction);
        public virtual Task<T> GetAsync<T1, T2, T3, T4, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.GetAsync<T1, T2, T3, T4, T>(id, UnitOfWork.Transaction);
        public virtual Task<T> GetAsync<T1, T2, T3, T4, T5, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.GetAsync<T1, T2, T3, T4, T5, T>(id, UnitOfWork.Transaction);
        public virtual Task<T> GetAsync<T1, T2, T3, T4, T5, T6, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.GetAsync<T1, T2, T3, T4, T5, T6, T>(id, UnitOfWork.Transaction);
        public virtual Task<T> GetAsync<T1, T2, T3, T4, T5, T6, T7, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.GetAsync<T1, T2, T3, T4, T5, T6, T7, T>(id, UnitOfWork.Transaction);

        public virtual Task<T> GetAsync<T1, T2, T>(object id, Func<T1, T2, T> map) where T : class
            => UnitOfWork.Connection.GetAsync(id, map);
        public virtual Task<T> GetAsync<T1, T2, T3, T>(object id, Func<T1, T2, T3, T> map) where T : class
            => UnitOfWork.Connection.GetAsync(id, map);
        public virtual Task<T> GetAsync<T1, T2, T3, T4, T>(object id, Func<T1, T2, T3, T4, T> map) where T : class
            => UnitOfWork.Connection.GetAsync(id, map);
        public virtual Task<T> GetAsync<T1, T2, T3, T4, T5, T>(object id, Func<T1, T2, T3, T4, T5, T> map) where T : class
            => UnitOfWork.Connection.GetAsync(id, map);
        public virtual Task<T> GetAsync<T1, T2, T3, T4, T5, T6, T>(object id, Func<T1, T2, T3, T4, T5, T6, T> map) where T : class
            => UnitOfWork.Connection.GetAsync(id, map);
        public virtual Task<T> GetAsync<T1, T2, T3, T4, T5, T6, T7, T>(object id, Func<T1, T2, T3, T4, T5, T6, T7, T> map) where T : class
            => UnitOfWork.Connection.GetAsync(id, map);


        public virtual Task<object> InsertAsync(T obj)
            => UnitOfWork.Connection.InsertAsync(obj.Hash(), UnitOfWork.Transaction);

        public virtual Task<object> InsertEntityAsync<T>(T obj) where T : Entity
        {
            obj.SetCodePrefix();
            var count = Count<T>(p => p.CodePrefix == obj.CodePrefix);
            return UnitOfWork.Connection.InsertAsync(obj.HashEntity(count), UnitOfWork.Transaction);
        }

        public virtual Task<bool> UpdateAsync(T obj)
            => UnitOfWork.Connection.UpdateAsync(obj.Hash(), UnitOfWork.Transaction);
    
        public virtual Task<bool> DeleteAsync(T obj)
            => UnitOfWork.Connection.DeleteAsync(obj, UnitOfWork.Transaction);
    }
}
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
