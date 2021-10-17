using Microsoft.Ajax.Utilities;
using PersonalLogger.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PersonalLogger.Repository
{
    public class MyLogsRepository : Repository<MyLog>, IMyLogsRepository
    {
        public MyLogsRepository(ApplicationDbContext context)
            :base(context)
        {
        }

        public MyLog GetByIDWithFields(int id)
        {
            return dbSet.Include(ml=>ml.Fields.Select(f=>f.CategoryField)).SingleOrDefault(ml=>ml.Id == id);
        }

        public IEnumerable<MyLog> GetWithFields(Expression<Func<MyLog, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<MyLog> query = dbSet;
            query = query.Include(ml => ml.Fields.Select(f => f.CategoryField));

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