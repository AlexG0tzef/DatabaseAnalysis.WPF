using DatabaseAnalysis.WPF.FireBird;
using DatabaseAnalysis.WPF.Storages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.DBAPIFactory
{
    public static partial class EssanceMethods
    {
        protected class ReportsEssenceMethods : IEssenceMethods<DatabaseAnalysis.WPF.FireBird.Reports>
        {
            protected static Type InnerType { get; } = typeof(DatabaseAnalysis.WPF.FireBird.Reports);
            public static ReportsEssenceMethods GetMethods()
            {
                return new ReportsEssenceMethods();
            }
            public ReportsEssenceMethods()
            {

            }

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
            T IEssenceMethods.Post<T>(T obj) where T : class
            {
                if (CheckType(obj))
                {
                    if ((obj as DatabaseAnalysis.WPF.FireBird.Reports).Id == 0)
                    {
                        using (var db = new DBModel(StaticConfiguration.DBPath))
                        {
                            db.Database.Migrate();
                            db.ReportsCollectionDbSet.Add(obj as DatabaseAnalysis.WPF.FireBird.Reports);
                            db.SaveChanges();
                            return obj;
                        }
                    }
                }
                return null;
            }
            T IEssenceMethods.Get<T>(int ID) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBPath))
                    {
                        db.Database.Migrate();
                        return db.ReportsCollectionDbSet.Where(x => x.Id == ID)
                            .Include(x => x.Master_DB)
                            .FirstOrDefault() as T;
                    }
                }
                return null;
            }
            IQueryable<T> IEssenceMethods.GetAll<T>() where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBPath))
                    {
                        db.Database.Migrate();
                        return db.ReportsCollectionDbSet
                            .Include(x => x.Master_DB).ThenInclude(x => x.Rows10)
                            .Include(x => x.Master_DB).ThenInclude(x => x.Rows20)
                            .Include(x => x.Report_Collection)
                            .Select(x => x as T) as IQueryable<T>;
                    }
                }
                return null;
            }
            bool IEssenceMethods.Update<T>(T obj) where T : class
            {
                if (CheckType(obj))
                {
                    using (var db = new DBModel(StaticConfiguration.DBPath))
                    {
                        db.Database.Migrate();
                        DatabaseAnalysis.WPF.FireBird.Reports _rep = obj as DatabaseAnalysis.WPF.FireBird.Reports;
                        db.ReportsCollectionDbSet.Update(_rep);
                        db.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            bool IEssenceMethods.Delete<T>(int ID) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBPath))
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

            #region MethodsRealizationAsync
            async Task<T> IEssenceMethods.PostAsync<T>(T obj) where T : class
            {
                if (CheckType(obj))
                {
                    if ((obj as DatabaseAnalysis.WPF.FireBird.Reports).Id == 0)
                    {
                        using (var db = new DBModel(StaticConfiguration.DBPath))
                        {
                            await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                            await db.ReportsCollectionDbSet.AddAsync(obj as FireBird.Reports, ReportsStorge.cancellationToken);
                            await db.SaveChangesAsync(ReportsStorge.cancellationToken);
                            return obj;
                        }
                    }
                }
                return null;
            }
            async Task<T> IEssenceMethods.GetAsync<T>(int ID) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBPath))
                    {
                        await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                        return await db.ReportsCollectionDbSet.Where(x => x.Id == ID)
                            .Include(x => x.Master_DB)
                            .FirstOrDefaultAsync(ReportsStorge.cancellationToken) as T;
                    }
                }
                return null;
            }
            async Task<IQueryable<T>> IEssenceMethods.GetAllAsync<T>() where T : class
            {
                if (CheckType(typeof(T)))
                {
                    if (StaticConfiguration.TpmDb.Equals("OPER"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBOperPath))
                        {
                            await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                            var tmp = await db.ReportsCollectionDbSet
                                .Include(x => x.Master_DB).ThenInclude(x => x.Rows10)
                                .Include(x => x.Report_Collection)
                                .Select(x => x as T).ToListAsync(ReportsStorge.cancellationToken) as IQueryable<T>;
                            return tmp;
                        }
                    }
                    if (StaticConfiguration.TpmDb.Equals("YEAR"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBPath))
                        {
                            await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                            var tmp = await db.ReportsCollectionDbSet
                                .Include(x => x.Master_DB).ThenInclude(x => x.Rows20)
                                .Include(x => x.Report_Collection)
                                .Select(x => x as T).ToListAsync(ReportsStorge.cancellationToken) as IQueryable<T>;
                            return tmp;
                        }
                    }
                }
                return null;
            }
            async Task<bool> IEssenceMethods.UpdateAsync<T>(T obj) where T : class
            {
                if (CheckType(obj))
                {
                    using (var db = new DBModel(StaticConfiguration.DBPath))
                    {
                        await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                        db.ReportsCollectionDbSet.Update(obj as FireBird.Reports);
                        await db.SaveChangesAsync(ReportsStorge.cancellationToken);
                    }
                    return true;
                }
                return false;
            }
            async Task<bool> IEssenceMethods.DeleteAsync<T>(int ID) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBPath))
                    {
                        await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                        var reps = await db.ReportsCollectionDbSet.Where(x => x.Id == ID).FirstOrDefaultAsync(ReportsStorge.cancellationToken);
                        if (reps != null)
                        {
                            db.ReportsCollectionDbSet.Remove(reps);
                            await db.SaveChangesAsync(ReportsStorge.cancellationToken);
                        }
                    }
                    return true;
                }
                return false;
            }
            #endregion
        }
    }
}
