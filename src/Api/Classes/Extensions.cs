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

using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace jotfi.Jot.Api.Classes
{
    public static class Extensions
    {
        /// <summary>
        /// https://stackoverflow.com/questions/55307370/how-to-take-odata-queryable-web-api-endpoint-filter-and-map-it-from-dto-object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetFilter<T>(this ODataQueryOptions<T> options)
        {
            // The same trick as in the linked post
            IQueryable query = Enumerable.Empty<T>().AsQueryable();
            query = options.Filter?.ApplyTo(query, new ODataQuerySettings());
            // Extract the predicate from `Queryable.Where` call
            if (query?.Expression is MethodCallExpression call && 
                call.Method.Name == nameof(Queryable.Where) && 
                call.Method.DeclaringType == typeof(Queryable))
            {
                var predicate = ((UnaryExpression)call.Arguments[1]).Operand;
                return (Expression<Func<T, bool>>)predicate;
            }
            return null;
        }
    }
}
