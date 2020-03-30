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
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace jotfi.Jot.Database.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        public Guid Id { get; }
        public DbConnection Connection { get; }
        public DbTransaction Transaction { get; private set; }

        public UnitOfWork(DbConnection connection)
        {
            Id = Guid.NewGuid();
            Connection = connection;
            Transaction = null;
        }

        public void Begin()
        {
            Transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            Transaction.Rollback();
            Dispose();
        }

        public async Task CommitAsync()
        {
            await Transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await Transaction.RollbackAsync();
        }

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
            }
            Transaction = null;
        }
    }
}
