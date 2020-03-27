using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UAParser;


namespace AutoHistoryCore
{
    public static class DbContextExtention
    {
        /// <summary>
        /// UndeltedRecord
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
        /// Save Chaange with SoftDelete Pattern(Logical Delete)
        /// Save Agent info -- OS,Broswer and IpAddres
        /// </summary>
        /// <param name="db"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static int SaveChangesWithHistory(this DbContext db, HttpContext httpContext)
        {
            var entries = db.ChangeTracker.Entries().ToArray();
            string ip = "";
            string os = "";
            string Browser = "";
            string Device = "";
            try
            {
                string userAgent = httpContext.Request.Headers["User-Agent"];
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(userAgent);
                Device = c.Device.ToString();
                os = c.String;
                Browser = c.String;
                if (c.OS.ToString() != "Other")
                {
                    os = c.OS.ToString();
                }
                if (c.UserAgent.ToString() != "Other")
                {
                    Browser = c.UserAgent.ToString();

                }
                ip = httpContext.Connection.RemoteIpAddress.ToString();
                if (ip == "::1" || ip == "127.0.0.1")
                {
                    ip = Dns.GetHostEntry(Dns.GetHostName())
                        .AddressList
                        .FirstOrDefault(C => C.AddressFamily ==
                        System.Net.Sockets.AddressFamily.InterNetwork).ToString();
                }
            }
            catch
            {

                ;
            }
            foreach (var entity in entries)
            {
                try
                {

                    HistoryBaseModel model = (HistoryBaseModel)entity.Entity;
                    HistoryViewModel vm = new HistoryViewModel()
                    {
                        AgentIp = ip,
                        AgentOs = os,
                        Device = Device,
                        AgentBrowser = Browser,
                        DateTime = DateTime.Now,
                        State = entity.State.ToString()

                    };
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


        /// <summary>
        /// Save Chaange with SoftDelete Pattern(Logical Delete)
        /// Save Agent info -- OS,Broswer and IpAddres
        /// </summary>
        /// <param name="db"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static async Task<int> SaveChangesWithHistoryAsync(this DbContext db, HttpContext httpContext)
        {
            var entries = db.ChangeTracker.Entries().ToArray();
            string ip = "";
            string os = "";
            string Browser = "";
            string Device = "";
            try
            {
                string userAgent = httpContext.Request.Headers["User-Agent"];
                var uaParser = Parser.GetDefault();
                ClientInfo c = uaParser.Parse(userAgent);
                Device = c.Device.ToString();
                os = c.String;
                Browser = c.String;
                if (c.OS.ToString() != "Other")
                {
                    os = c.OS.ToString();
                }
                if (c.UserAgent.ToString() != "Other")
                {
                    Browser = c.UserAgent.ToString();

                }
                ip = httpContext.Connection.RemoteIpAddress.ToString();
                if (ip == "::1" || ip == "127.0.0.1")
                {
                    ip = Dns.GetHostEntry(Dns.GetHostName())
                        .AddressList
                        .FirstOrDefault(C => C.AddressFamily ==
                        System.Net.Sockets.AddressFamily.InterNetwork).ToString();
                }
            }
            catch
            {

                ;
            }
            foreach (var entity in entries)
            {
                try
                {

                    HistoryBaseModel model = (HistoryBaseModel)entity.Entity;
                    HistoryViewModel vm = new HistoryViewModel()
                    {
                        AgentIp = ip,
                        AgentOs = os,
                        Device = Device,
                        AgentBrowser = Browser,
                        DateTime = DateTime.Now,
                        State = entity.State.ToString()

                    };
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

            return await db.SaveChangesAsync();
        }


    }
}
