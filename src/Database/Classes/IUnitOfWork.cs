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

using System;
using System.Data.Common;

namespace jotfi.Jot.Database.Classes
{
    /// <summary>
    /// https://github.com/timschreiber/DapperUnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Guid Id { get; }
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
}