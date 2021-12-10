using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Agenda.Domain.Core;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure
{
    public abstract class PaginationFilterParamsBase<T> : IPaginationFilterParams<T> where T : Record
    {

        private Expression<Func<T, bool>> _predicate = PredicateBuilder.New<T>(true);
        private Func<IQueryable<T>, IQueryable<T>> _preQuery;
        public int? Take { get; set; }
        public int? Skip { get; set; }

        public IQueryable<T> ApplyFilter(IQueryable<T> query)
        {
            Filter();
            query = query.AsNoTracking();

            if (_preQuery != null)
                query = _preQuery(query);

            return query.AsExpandableEFCore().Where(_predicate);
        }

        protected abstract void Filter();

        protected void And(Expression<Func<T, bool>> expression)
        {
            _predicate = _predicate.And(expression);
        }

        protected void Or(Expression<Func<T, bool>> expression)
        {
            _predicate = _predicate.Or(expression);
        }

        protected void PreQuery(Func<IQueryable<T>, IQueryable<T>> func)
        {
            _preQuery = func;
        }

    }
}
