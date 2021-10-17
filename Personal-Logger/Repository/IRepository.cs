using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLogger.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity GetByID(object id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");

        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);

        void Delete(TEntity entity);
        void Delete(object id);


    }
}
