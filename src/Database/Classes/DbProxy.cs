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
using jotfi.Jot.Base.System;
using jotfi.Jot.Database.Classes;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
namespace jotfi.Jot.Database.Classes
{
    public partial class DbProxy<T> where T : Transaction
    {
        protected readonly IUnitOfWork UnitOfWork;

        public DbProxy(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual bool Exists() => Count(p => p.Id > 0) > 0;
        public virtual long Count(Expression<Func<T, bool>> predicate) 
            => UnitOfWork.Connection.Count(predicate, UnitOfWork.Transaction);

        public virtual bool Any(Expression<Func<T, bool>> predicate)
            => UnitOfWork.Connection.Any(predicate, UnitOfWork.Transaction);

        public virtual long Count<T>(Expression<Func<T, bool>> predicate) where T : Entity 
            => UnitOfWork.Connection.Count(predicate, UnitOfWork.Transaction);

        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> predicate, bool buffered = true) 
            => UnitOfWork.Connection.Select(predicate, UnitOfWork.Transaction, buffered);

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
            => UnitOfWork.Connection.FirstOrDefault<T>(predicate, UnitOfWork.Transaction);

        public virtual IEnumerable<T> GetAll(bool buffered = true) 
            => UnitOfWork.Connection.GetAll<T>(UnitOfWork.Transaction, buffered);

        public virtual T Get(object id) 
            => UnitOfWork.Connection.Get<T>(id, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.Get<T1, T2, T>(id, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.Get<T1, T2, T3, T>(id, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T4, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.Get<T1, T2, T3, T4, T>(id, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T4, T5, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.Get<T1, T2, T3, T4, T5, T>(id, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T4, T5, T6, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.Get<T1, T2, T3, T4, T5, T6, T>(id, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T4, T5, T6, T7, T>(object id) where T1 : class, T
            => UnitOfWork.Connection.Get<T1, T2, T3, T4, T5, T6, T7, T>(id, UnitOfWork.Transaction);

        public virtual T Get<T1, T2, T>(object id, Func<T1, T2, T> map) where T : class
            => UnitOfWork.Connection.Get(id, map, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T>(object id, Func<T1, T2, T3, T> map) where T : class
            => UnitOfWork.Connection.Get(id, map, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T4, T>(object id, Func<T1, T2, T3, T4, T> map) where T : class
            => UnitOfWork.Connection.Get(id, map, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T4, T5, T>(object id, Func<T1, T2, T3, T4, T5, T> map) where T : class
            => UnitOfWork.Connection.Get(id, map, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T4, T5, T6, T>(object id, Func<T1, T2, T3, T4, T5, T6, T> map) where T : class
            => UnitOfWork.Connection.Get(id, map, UnitOfWork.Transaction);
        public virtual T Get<T1, T2, T3, T4, T5, T6, T7, T>(object id, Func<T1, T2, T3, T4, T5, T6, T7, T> map) where T : class
            => UnitOfWork.Connection.Get(id, map, UnitOfWork.Transaction);

        
        public virtual object Insert(T obj)
            => UnitOfWork.Connection.Insert(obj.Hash(), UnitOfWork.Transaction);
        public virtual object InsertEntity<T>(T obj) where T : Entity
        {
            obj.SetCodePrefix();
            var count = Count<T>(p => p.CodePrefix == obj.CodePrefix);
            return UnitOfWork.Connection.Insert(obj.HashEntity(count), UnitOfWork.Transaction);
        }
    
        public virtual bool Update(T obj)
            => UnitOfWork.Connection.Update(obj.Hash(), UnitOfWork.Transaction);

        public virtual bool Delete(T obj)
            => UnitOfWork.Connection.Delete(obj, UnitOfWork.Transaction);
    }
}
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
