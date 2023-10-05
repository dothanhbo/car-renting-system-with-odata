using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Models.Base;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace DataAcessObjects.DAO.Base
{
    public class BaseDAO<T> where T : BaseEntity
    {
        public BaseDAO()
        {
        }

        public async Task AddAsync(T entity)
        {
            using (var context = new FUCarRentingSystemContext())
            {
                var result = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null)
        {
            using (var context = new FUCarRentingSystemContext())
            {
                return expression == null ? await context.Set<T>().CountAsync() : await context.Set<T>().CountAsync(expression);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            using (var context = new FUCarRentingSystemContext())
            {
                entity = await context.Set<T>().FindAsync(entity) ?? throw new Exception("Not found");
                context.Entry(entity).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(object id)
        {
            using (var context = new FUCarRentingSystemContext())
            {
                var entity = await context.Set<T>().FindAsync(id) ?? throw new Exception("Not found");
                context.Entry(entity).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<PaginationResult<T>> GetAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isDisableTracking = true, bool isTakeAll = false, int pageSize = 0, int pageIndex = 0)
        {
            using (var context = new FUCarRentingSystemContext())
            {
                IQueryable<T> query = context.Set<T>();
                var paginationResult = new PaginationResult<T>();
                paginationResult.TotalCount = await CountAsync(expression);
                if (expression != null)
                    query = query.Where(expression);
                if (isDisableTracking is true)
                    query = query.AsNoTracking();
                if (isTakeAll is true)
                {
                    if (orderBy != null)
                        paginationResult.Result = await orderBy(query).ToListAsync();
                    else
                        paginationResult.Result = await query.ToListAsync();
                }
                else
                {
                    paginationResult.PageIndex = pageIndex;
                    if (orderBy == null)
                        paginationResult.Result = await query.Skip(pageSize * pageIndex).Take(pageSize).ToListAsync();
                    else
                        paginationResult.Result = await orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
                }
                return paginationResult;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            using (var context = new FUCarRentingSystemContext())
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                   context.Entry(entity).State = EntityState.Modified;
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
