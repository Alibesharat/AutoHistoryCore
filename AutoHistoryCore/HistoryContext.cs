using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoHistoryCore
{
    public static class DbContextExtention
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
                    HistoryViewModel vm = new HistoryViewModel()
                    {
                        AgentIp = "",
                        AgentOs = "",
                        DateTime = DateTime.Now,
                        State = entity.State

                    };
                    List<List<HistoryViewModel>> ls = new List<List<HistoryViewModel>>();
                    List<HistoryViewModel> data = new List<HistoryViewModel>();
                    data.Add(vm);
                    if (!string.IsNullOrWhiteSpace(model.Hs_Change))
                    {
                        ls = JsonConvert.DeserializeObject<List<List<HistoryViewModel>>>(model.Hs_Change);


                    }
                    ls.Add(data);
                    var JSON = JsonConvert.SerializeObject(ls);
                    switch (entity.State)
                    {

                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            model.IsDeleted = true;
                            entity.State = EntityState.Modified;
                            break;
                        case EntityState.Modified:
                            break;
                        case EntityState.Added:
                            model.IsDeleted = false;
                            break;
                        default:
                            break;
                    }
                    model.Hs_Change = JSON;
                }
                catch 
                {

                    ;
                }

            }

            return db.SaveChanges();
        }
    }
}
