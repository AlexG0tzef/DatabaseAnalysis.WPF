using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.InnerLogger;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.DBAPIFactory
{
    public static partial class EssanceMethods
    {
        protected class ReportsEssenceMethods : IEssenceMethods<Reports>
        {
            protected static Type InnerType { get; } = typeof(Reports);
            public static ReportsEssenceMethods GetMethods()
            {
                return new ReportsEssenceMethods();
            }
            public ReportsEssenceMethods() { }

            #region CheckType
            private bool CheckType<T>(T obj)
            {
                if (obj.GetType() == InnerType)
                {
                    return true;
                }
                return false;
            }
            private bool CheckType(Type Type)
            {
                if (Type == InnerType)
                {
                    return true;
                }
                return false;
            }
            #endregion

            #region MethodsRealizationNotAsync
            #region Post
            T IEssenceMethods.Post<T>(T obj) where T : class
            {
                if (CheckType(obj))
                {
                    if ((obj as Reports).Id == 0)
                    {
                        using (var db = new DBModel(StaticConfiguration.DBYearPath))
                        {
                            db.Database.Migrate();
                            db.ReportsCollectionDbSet.Add(obj as Reports);
                            db.SaveChanges();
                            return obj;
                        }
                    }
                }
                return null;
            }
            #endregion

            #region Get
            T? IEssenceMethods.Get<T>(int ID) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
                    {
                        db.Database.Migrate();
                        return db.ReportsCollectionDbSet.Where(x => x.Id == ID)
                            .Include(x => x.Master_DB)
                            .FirstOrDefault() as T;
                    }
                }
                return null;
            }
            #endregion

            #region GetAll
            List<T?> IEssenceMethods.GetAll<T>() where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
                    {
                        db.Database.Migrate();
                        IQueryable<Reports> dbQ = db.ReportsCollectionDbSet;
                        return dbQ
                            .Include(x => x.Master_DB).ThenInclude(x => x.Rows10)
                            .Include(x => x.Master_DB).ThenInclude(x => x.Rows20)
                            .Include(x => x.Report_Collection)
                            .Select(x => x as T).ToList();
                    }
                }
                return null;
            }
            #endregion

            #region Update
            bool IEssenceMethods.Update<T>(T obj) where T : class
            {
                if (CheckType(obj))
                {
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
                    {
                        db.Database.Migrate();
                        Reports _rep = obj as Reports;
                        db.ReportsCollectionDbSet.Update(_rep);
                        db.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            #endregion

            #region Delete
            bool IEssenceMethods.Delete<T>(int ID) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
                    {
                        db.Database.Migrate();
                        var rep = db.ReportsCollectionDbSet.Where(x => x.Id == ID).FirstOrDefault();
                        db.ReportsCollectionDbSet.Remove(rep);
                        db.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            #endregion
            #endregion

            #region MethodsRealizationAsync
            #region PostAsync
            async Task<T> IEssenceMethods.PostAsync<T>(T obj) where T : class
            {
                if (CheckType(obj))
                {
                    if ((obj as Reports).Id == 0)
                    {
                        using (var db = new DBModel(StaticConfiguration.DBYearPath))
                        {
                            await db.Database.MigrateAsync(ReportsStorage.cancellationToken);
                            await db.ReportsCollectionDbSet.AddAsync(obj as Reports, ReportsStorage.cancellationToken);
                            await db.SaveChangesAsync(ReportsStorage.cancellationToken);
                            return obj;
                        }
                    }
                }
                return null;
            }
            #endregion

            #region GetAsync
            async Task<T?> IEssenceMethods.GetAsync<T>(int ID) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    if (StaticConfiguration.TpmDb.Equals("OPER"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBOperPath))
                        {
                            try
                            {
                                await db.Database.MigrateAsync(ReportsStorage.cancellationToken);
                                return await db.ReportsCollectionDbSet.Where(x => x.Id == ID)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows10)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows11)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows12)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows13)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows14)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows15)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows16)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows17)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows18)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows19)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows20)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows21)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows22)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows23)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows24)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows25)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows26)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows27)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows28)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows29)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows210)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows211)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows212)
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Notes)
                                    .FirstOrDefaultAsync(ReportsStorage.cancellationToken) as T;
                            }
                            catch (Exception ex)
                            {
                                ServiceExtension.LoggerManager.Error(ex.Message);
                            }
                        }
                    }
                    if (StaticConfiguration.TpmDb.Equals("YEAR"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBYearPath))
                        {
                            try
                            {
                                await db.Database.MigrateAsync(ReportsStorage.cancellationToken);
                                return await db.ReportsCollectionDbSet.Where(x => x.Id == ID)
                                    .Include(x => x.Master_DB)
                                    .FirstOrDefaultAsync(ReportsStorage.cancellationToken) as T;
                            }
                            catch (Exception ex)
                            {
                                ServiceExtension.LoggerManager.Error(ex.Message);
                            }
                        }
                    }
                }
                return null;
            }
            #endregion

            #region GetAllAsync
            async Task<List<T>> IEssenceMethods.GetAllAsync<T>(string param) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    if (StaticConfiguration.TpmDb.Equals("OPER"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBOperPath))
                        {
                            try
                            {
                                await db.Database.MigrateAsync(ReportsStorage.cancellationToken);
                                IQueryable<Reports> dbQ = db.ReportsCollectionDbSet;
                                var tmp = await dbQ
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows10)
                                    .Include(x => x.Report_Collection)
                                    .Select(x => x as T).ToListAsync(ReportsStorage.cancellationToken);
                                return tmp;
                            }
                            catch (Exception ex)
                            {
                                ServiceExtension.LoggerManager.Error(ex.Message);
                            }
                        }
                    }
                    if (StaticConfiguration.TpmDb.Equals("YEAR"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBYearPath))
                        {
                            try
                            {
                                await db.Database.MigrateAsync(ReportsStorage.cancellationToken);
                                IQueryable<Reports> dbQ = db.ReportsCollectionDbSet;
                                var tmp = await dbQ
                                    .Include(x => x.Master_DB).ThenInclude(x => x.Rows20)
                                    .Include(x => x.Report_Collection)
                                    .Select(x => x as T).ToListAsync(ReportsStorage.cancellationToken);
                                return tmp;
                            }
                            catch (Exception ex)
                            {
                                ServiceExtension.LoggerManager.Error(ex.Message);
                            }
                        }
                    }
                }
                return null;
            }
            #endregion

            #region UpdateAsync
            async Task<bool> IEssenceMethods.UpdateAsync<T>(T obj) where T : class
            {
                if (CheckType(obj))
                {
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
                    {
                        await db.Database.MigrateAsync(ReportsStorage.cancellationToken);
                        db.ReportsCollectionDbSet.Update(obj as Reports);
                        await db.SaveChangesAsync(ReportsStorage.cancellationToken);
                    }
                    return true;
                }
                return false;
            }
            #endregion

            #region DeleteAsync
            async Task<bool> IEssenceMethods.DeleteAsync<T>(int ID) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
                    {
                        await db.Database.MigrateAsync(ReportsStorage.cancellationToken);
                        var reps = await db.ReportsCollectionDbSet.Where(x => x.Id == ID).FirstOrDefaultAsync(ReportsStorage.cancellationToken);
                        if (reps != null)
                        {
                            db.ReportsCollectionDbSet.Remove(reps);
                            await db.SaveChangesAsync(ReportsStorage.cancellationToken);
                        }
                    }
                    return true;
                }
                return false;
            } 
            #endregion
            #endregion
        }
    }
}