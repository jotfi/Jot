// Copyright 2020 John Cottrell
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

using Dommel;
using jotfi.Jot.Base.System;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace jotfi.Jot.Database.Classes
{
    public partial class Repository<T> where T : Transaction
    {
        protected readonly IUnitOfWork UnitOfWork = null;

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual bool Exists() => Count(p => p.Id > 0) > 0;
        public virtual long Count(Expression<Func<T, bool>> predicate) => UnitOfWork.Connection.Count(predicate);
        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> predicate,
            bool buffered = true) => UnitOfWork.Connection.Select(predicate, buffered);
        public virtual IEnumerable<T> GetAll(bool buffered = true) => UnitOfWork.Connection.GetAll<T>(buffered);
        public virtual T Get(object id) => UnitOfWork.Connection.Get<T>(id);
        public virtual bool Delete(T obj) => UnitOfWork.Connection.Delete(obj, UnitOfWork.Transaction);

        public virtual object Insert(T obj)
        {
            obj.Init();
            return UnitOfWork.Connection.Insert(obj, UnitOfWork.Transaction);
        }

        public virtual bool Update(T obj)
        {
            obj.Init();
            return UnitOfWork.Connection.Update(obj, UnitOfWork.Transaction);
        }
    }
}
