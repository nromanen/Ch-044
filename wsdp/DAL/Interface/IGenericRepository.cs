namespace DAL.Interface
{
    public interface IGenericRepository<TEntity> : IBaseRepository<TEntity>
     where TEntity : class
    {
        void Delete(TEntity entityToDelete);

        TEntity GetByID(object id);

        void Delete(object id);
    }
}