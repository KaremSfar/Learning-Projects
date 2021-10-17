using PersonalLogger.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace PersonalLogger.Repository
{
    public class LogCategoryRepository : Repository<LogCategory>, ILogCategoryRepository
    {
        public LogCategoryRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public LogCategory GetByIDWithCategoryFields(int id)
        {
            return dbSet.Include(lc => lc.CategoryFields.Select(cf => cf.FieldType)).SingleOrDefault(lc => lc.Id == id);
        }

        public IEnumerable<LogCategory> GetWithCategoryFields(Expression<Func<LogCategory, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<LogCategory> query = dbSet;
            query = query.Include(lc=>lc.CategoryFields.Select(cf=>cf.FieldType));

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.ToList();
        }
    }

}