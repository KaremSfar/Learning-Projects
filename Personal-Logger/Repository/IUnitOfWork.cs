using PersonalLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLogger.Repository
{
    public interface IUnitOfWork
    {
        IMyLogsRepository MyLogs { get; }
        ILogCategoryRepository LogCategories { get; }
        IRepository<FieldType> FieldTypes { get; }

        void Commit();
    }
}
