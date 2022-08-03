﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAnalysis.WPF.FireBird;
using Microsoft.EntityFrameworkCore;

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
            List<T> IEssenceMethods.GetAll<T>() where T : class
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
                            .Select(x => x as T).ToList();
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
                            await db.Database.MigrateAsync();
                            await db.ReportsCollectionDbSet.AddAsync(obj as DatabaseAnalysis.WPF.FireBird.Reports);
                            await db.SaveChangesAsync();
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
                        await db.Database.MigrateAsync();
                        return await db.ReportsCollectionDbSet.Where(x => x.Id == ID)
                            .Include(x => x.Master_DB)
                            .FirstOrDefaultAsync() as T;
                    }
                }
                return null;
            }
            async Task<List<T>> IEssenceMethods.GetAllAsync<T>() where T : class
            {
                if (CheckType(typeof(T)))
                {
                    if (StaticConfiguration.TpmDb.Equals("OPER"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBOperPath))
                        {
                            await db.Database.MigrateAsync();
                            var tmp = await db.ReportsCollectionDbSet
                                .Include(x => x.Master_DB).ThenInclude(x => x.Rows10)
                                .Include(x => x.Report_Collection)
                                .Select(x => x as T).ToListAsync();
                            return tmp;
                        }
                    }
                    if (StaticConfiguration.TpmDb.Equals("YEAR"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBPath))
                        {
                            await db.Database.MigrateAsync();
                            var tmp = await db.ReportsCollectionDbSet
                                .Include(x => x.Master_DB).ThenInclude(x => x.Rows20)
                                .Include(x => x.Report_Collection)
                                .Select(x => x as T).ToListAsync();
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
                        await db.Database.MigrateAsync();
                        db.ReportsCollectionDbSet.Update(obj as DatabaseAnalysis.WPF.FireBird.Reports);
                        await db.SaveChangesAsync();
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
                        await db.Database.MigrateAsync();
                        var reps = await db.ReportsCollectionDbSet.Where(x => x.Id == ID).FirstOrDefaultAsync();
                        if (reps != null)
                        {
                            db.ReportsCollectionDbSet.Remove(reps);
                            await db.SaveChangesAsync();
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
