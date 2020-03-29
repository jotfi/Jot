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

using jotfi.Jot.Base.Classes;
using jotfi.Jot.Base.System;
using jotfi.Jot.Base.Utils;
using jotfi.Jot.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jotfi.Jot.Database.Classes
{
    public static class Extensions
    {
        public static void Init(this Transaction transaction)
        {
            transaction.ModifiedDate = DateTime.Now;
            transaction.Hash = HashUtils.GetSHA256Hash(transaction.ToJson());
        }

        public static long Insert<T>(this T transaction, IUnitOfWork unitOfWork) where T : Transaction
        {
            var repository = new Repository<T>(unitOfWork);
            var id = (long)repository.Insert(transaction);
            id.IsNotZero();
            return id;
        }

        public static Task<object> InsertAsync<T>(this T transaction, IUnitOfWork unitOfWork) where T : Transaction
        {
            var repository = new Repository<T>(unitOfWork);
            return repository.InsertAsync(transaction);
        }

    }
}
