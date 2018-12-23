using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UAParser;


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
                    IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
                    var ip = heserver.AddressList[2].ToString();
                    HistoryBaseModel model = (HistoryBaseModel)entity.Entity;
                    HistoryViewModel vm = new HistoryViewModel()
                    {
                        AgentIp = ip,
                        AgentOs = "",
                        DateTime = DateTime.Now,
                        State = entity.State.ToString()

                    };
                    //List<List<HistoryViewModel>> ls = new List<List<HistoryViewModel>>();
                    List<HistoryViewModel> data = new List<HistoryViewModel>();
                    if (!string.IsNullOrWhiteSpace(model.Hs_Change))
                    {
                        data = JsonConvert.DeserializeObject<List<HistoryViewModel>>(model.Hs_Change);


                    }
                    data.Add(vm);

                    var JSON = JsonConvert.SerializeObject(data);
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



        public static int SaveChangesWithHistory(this DbContext db, HttpContext httpContext)
        {
            var entries = db.ChangeTracker.Entries().ToArray();
            string userAgent = httpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
           
            string ip = httpContext.Connection.RemoteIpAddress.ToString();
            if(ip=="::1" || ip== "127.0.0.1")
            {
                ip = Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList
                    .FirstOrDefault(C => C.AddressFamily == 
                    System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            }
          

            foreach (var entity in entries)
            {
                try
                {
                    
                    HistoryBaseModel model = (HistoryBaseModel)entity.Entity;
                    HistoryViewModel vm = new HistoryViewModel()
                    {
                        AgentIp = ip,
                        AgentOs = c.OS.ToString(),
                        AgentBrowser = c.UserAgent.ToString(),
                        DateTime = DateTime.Now,
                        State = entity.State.ToString()

                    };
                    //List<List<HistoryViewModel>> ls = new List<List<HistoryViewModel>>();
                    List<HistoryViewModel> data = new List<HistoryViewModel>();
                    if (!string.IsNullOrWhiteSpace(model.Hs_Change))
                    {
                        data = JsonConvert.DeserializeObject<List<HistoryViewModel>>(model.Hs_Change);


                    }
                    data.Add(vm);

                    var JSON = JsonConvert.SerializeObject(data);
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
