using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHistoryCore
{
    public static class HistoryContext
    {
        /// <summary>
        /// رکورد های حذف نشده
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="set"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> Undelited<TEntity>(this DbSet<TEntity> set)
       where TEntity : HistoryBaseModel
        {
          
            var data = set.AsNoTracking().Where(e => e.IsDeleted == false);
            return data.AsQueryable();
        }

   

        /// <summary>
        /// ثبت تفییرات کانتکس با تاریخچه
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int SaveChangesWithHistory(this DbContext db)
        {
            var entries = db.ChangeTracker.Entries().ToArray();

            foreach (var entity in entries)
            {
                try
                {
                    HistoryBaseModel model = (HistoryBaseModel)entity.Entity;


                    switch (entity.State)
                    {

                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            model.IsDeleted = true;
                            model.DeletedDatTime = DateTime.Now;
                            entity.State = EntityState.Modified;
                            break;

                        case EntityState.Modified:
                            model.IsDeleted = false;
                            model.LastEditedDateTime = DateTime.Now;
                            break;
                        case EntityState.Added:
                            model.IsDeleted = false;
                            model.CrearedDateTime = DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {

                    ;
                }

            }

            return db.SaveChanges();
        }


        /// <summary>
        /// ثبت تفییرات کانتکس با تاریخچه
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public async static Task<int> SaveChangesWithHistoryAsync(this DbContext db)
        {
            var entries = db.ChangeTracker.Entries().ToArray();

            foreach (var entity in entries)
            {
                try
                {
                    HistoryBaseModel model = (HistoryBaseModel)entity.Entity;


                    switch (entity.State)
                    {

                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            model.IsDeleted = true;
                            model.DeletedDatTime = DateTime.Now;
                            entity.State = EntityState.Modified;
                            break;
                        case EntityState.Modified:
                            model.IsDeleted = false;
                            model.LastEditedDateTime = DateTime.Now;
                            break;
                        case EntityState.Added:
                            model.IsDeleted = false;
                            model.CrearedDateTime = DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {

                    ;
                }

            }

            return await db.SaveChangesAsync();
        }
    }
}
