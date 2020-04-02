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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace jotfi.Jot.Database.Repository.Base
{
    public interface IBaseRepository<T>
    {
        bool Any(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null);
        long Count(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null);
        Task<long> CountAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null);
        IEnumerable<T> Select(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null, bool buffered = true);
        Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, UnitOfWork? uow = null);
        IEnumerable<T> GetAll(UnitOfWork? uow = null, bool buffered = true);
        Task<IEnumerable<T>> GetAllAsync(UnitOfWork? uow = null);
        T Get(object id, UnitOfWork? uow = null);
        Task<T> GetAsync(object id, UnitOfWork? uow = null);
        long Insert(T obj, UnitOfWork? uow = null);
        Task<object> InsertAsync(T obj, UnitOfWork? uow = null);
        bool Update(T obj, UnitOfWork? uow = null);
        Task<bool> UpdateAsync(T obj, UnitOfWork? uow = null);
        bool Delete(T obj, UnitOfWork? uow = null);
        Task<bool> DeleteAsync(T obj, UnitOfWork? uow = null);
    }
}
