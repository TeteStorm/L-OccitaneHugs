using L_OccitaneHugsDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace L_OccitaneHugsData
{
   public class Repository<T> where T : BaseEntity
    {
        private readonly EFDbContext context;
        private IDbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(EFDbContext context)
        {
            this.context = context;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public T GetById(int id, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = this.Entities;
            if (includeExpressions != null && includeExpressions.Any())
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
            return query.Where(x => x.Id == id).FirstOrDefault();
        }


        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = this.Entities;
            if (includeExpressions != null && includeExpressions.Any())
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
            return query.AsEnumerable();
        }


        public T Find(Func<T, bool> match, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = this.Entities;
            if (includeExpressions != null && includeExpressions.Any())
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
            return query.FirstOrDefault(match);
        }


        public async Task<T> FindAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = this.Entities;
            if (includeExpressions != null && includeExpressions.Any())
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
            return await query.FirstOrDefaultAsync(match);
        }



        public IEnumerable<T> FindAll(Func<T, bool> filter, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = this.Entities;
            if (includeExpressions != null && includeExpressions.Any())
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
            return query.Where(filter);
        }

        //public async Task<IEnumerable<T>> FindAllAsync(Func<T, bool> filter, params Expression<Func<T, object>>[] includeExpressions)
        //{
        //    IQueryable<T> query = this.Entities;
        //    if (includeExpressions != null && includeExpressions.Any())
        //        query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
        //    return await query.Where(filter).ToListAsync();
        //}

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {               

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);              
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errorMessage, dbEx);                
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);                
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<T>();
                }
                return entities;
            }
        }
    }
}
