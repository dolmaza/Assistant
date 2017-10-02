using Core.DB;
using Core.Repository;

namespace Service
{
    public class BaseService
    {
        public DataContext DataContext { get; set; }

        public BaseService()
        {
            DataContext = new DataContext();
        }

        protected Repository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(DataContext);
        }
    }
}