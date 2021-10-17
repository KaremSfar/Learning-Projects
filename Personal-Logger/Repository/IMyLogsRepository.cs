using PersonalLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLogger.Repository
{
    public interface IMyLogsRepository : IRepository<MyLog>
    {
        IEnumerable<MyLog> GetWithFields(Expression<Func<MyLog, bool>> filter = null, string includeProperties = "");

        MyLog GetByIDWithFields(int id);
    }
}
