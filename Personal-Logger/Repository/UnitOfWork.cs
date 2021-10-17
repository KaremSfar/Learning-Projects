using PersonalLogger.Models;


namespace PersonalLogger.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;

        private MyLogsRepository _myLogs;
        private LogCategoryRepository _logCategories;
        private Repository<FieldType> _fieldTypes;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMyLogsRepository MyLogs
        {
            get
            {
                return _myLogs ?? (_myLogs = new MyLogsRepository(_dbContext));
            }
        }

        public ILogCategoryRepository LogCategories
        {
            get
            {
                return _logCategories ?? (_logCategories = new LogCategoryRepository(_dbContext));
            }
        }

        public IRepository<FieldType> FieldTypes
        {
            get
            {
                return _fieldTypes ?? (_fieldTypes = new Repository<FieldType>(_dbContext));
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

    }
}