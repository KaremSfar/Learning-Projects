using PersonalLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLogger.Repository
{
    public interface ILogCategoryRepository : IRepository<LogCategory>
    {
        IEnumerable<LogCategory> GetWithCategoryFields(Expression<Func<LogCategory, bool>> filter = null, string includeProperties = "");

        LogCategory GetByIDWithCategoryFields(int id);
    }
}
