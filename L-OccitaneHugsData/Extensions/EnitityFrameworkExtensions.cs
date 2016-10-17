using System.Data.Entity;
using System.Text;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace L_OccitaneHugsData.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static IQueryable<TEntity> Inclui<TEntity>(this DbQuery<TEntity> dbQuery, params Expression<Func<TEntity, object>>[] includeExpressions) where TEntity : class
        {
            IQueryable<TEntity> query = dbQuery;
            if (includeExpressions != null && includeExpressions.Any())
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
            return query;
        }

        public static IQueryable<TEntity> Inclui<TEntity>(this DbSet<TEntity> dbQuery, params Expression<Func<TEntity, object>>[] includeExpressions) where TEntity : class
        {
            IQueryable<TEntity> query = dbQuery;
            if (includeExpressions != null && includeExpressions.Any())
                query = includeExpressions.Aggregate(query, (current, expression) => current.Include(expression));
            return query;
        }
        public static void Clear<TEntity>(this DbContext dbContext) where TEntity : class
        {
            var tableName = typeof(TEntity).Name;
            dbContext.Database.ExecuteSqlCommand($"DELETE FROM [{tableName}]");
            dbContext.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('{tableName}',RESEED, 0)");
        }
        public static void Clear(this DbContext dbContext)
        {
            //nocheckConstraint
            string nocheckConstraint = "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT all'";

            //clearTablesCommand
            string whereand = "AND o.id != OBJECT_ID(\"__MigrationHistory\")";
            string clearTablesCommand = $"EXEC sp_MSForEachTable @command1='DELETE FROM ?', @whereand='{whereand}'";

            //checkConstraint
            string checkConstraint = "EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'";

            //resetIdentityCommand
            StringBuilder whereandSb = new StringBuilder();
            whereandSb.Append(" AND ");
            whereandSb.Append("(SELECT top 1 last_value");
            whereandSb.Append("	FROM sys.identity_columns ");
            whereandSb.Append("	WHERE object_id=o.id and last_value IS NOT NULL) IS NOT NULL"); //ref: http://goo.gl/dpxURS
            string resetIdentityCommand = $"EXEC sp_MSForEachTable @command1='DBCC CHECKIDENT ([?], reseed, 0)', @whereand='{whereandSb}'";
            dbContext.Database.ExecuteSqlCommand($"{nocheckConstraint}; {clearTablesCommand}; {checkConstraint}; {resetIdentityCommand};");
        }
    }
    //public static class IQueryableExtensions
    //{
    //    public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sort)
    //    {
    //        if (source == null)
    //        {
    //            throw new ArgumentNullException("source");
    //        }

    //        if (sort == null)
    //        {
    //            return source;
    //        }

    //        // split the sort string
    //        var lstSort = sort.Split(',');

    //        // run through the sorting options and create a sort expression string from them

    //        string completeSortExpression = "";
    //        foreach (var sortOption in lstSort)
    //        {
    //            // if the sort option starts with "-", we order
    //            // descending, otherwise ascending

    //            if (sortOption.StartsWith("-"))
    //            {
    //                completeSortExpression = completeSortExpression + sortOption.Remove(0, 1) + " descending,";
    //            }
    //            else
    //            {
    //                completeSortExpression = completeSortExpression + sortOption + ",";
    //            }

    //        }

    //        if (!string.IsNullOrWhiteSpace(completeSortExpression))
    //        {
    //            source = source.OrderBy(completeSortExpression.Remove(completeSortExpression.Count() - 1));
    //        }

    //        return source;
    //    }

    //    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
    //    {
    //        if (includes != null)
    //        {
    //            query = includes.Aggregate(query,
    //                      (current, include) => current.Include(include));
    //        }

    //        return query;
    //    }

    //}
}
