﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatabaseAnalysis.WPF.FireBird;

namespace DatabaseAnalysis.WPF.DBAPIFactory
{
    public static partial class EssanceMethods
    {
        protected class ReportEssenceMethods : IEssenceMethods<DatabaseAnalysis.WPF.FireBird.Report>
        {
            protected static Type InnerType { get; } = typeof(DatabaseAnalysis.WPF.FireBird.Report);
            public static ReportEssenceMethods GetMethods()
            {
                return new ReportEssenceMethods();
            }
            public ReportEssenceMethods()
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
                    if ((obj as DatabaseAnalysis.WPF.FireBird.Report).Id == 0)
                    {
                        using (var db = new DBModel(StaticConfiguration.DBPath))
                        {
                            db.Database.Migrate();
                            db.ReportCollectionDbSet.Add(obj as Report);
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
                        return db.ReportCollectionDbSet.Where(x => x.Id == ID).OrderBy(x=>x.Order)
                            .Include(x => x.Rows10).Include(x => x.Rows11.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows12.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows13.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows14.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows15.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows16.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows17.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows18.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows19.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows20.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows21.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows22.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows23.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows24.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows25.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows26.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows27.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows28.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows29.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows210.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows211.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows212.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Notes.OrderBy(x => x.Order))
                            .FirstOrDefault() as T;
                    }
                }
                return null;
            }
            List<T> IEssenceMethods.GetAll<T>() where T : class
            {
                if (CheckType(typeof(T)))
                {
                    //DoSomething
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
                        Report _rep = obj as DatabaseAnalysis.WPF.FireBird.Report;
                        db.ReportCollectionDbSet.Update(_rep);
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
                        var rep = db.ReportCollectionDbSet.Where(x => x.Id == ID).FirstOrDefault();
                        db.ReportCollectionDbSet.Remove(rep);
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
                    if ((obj as Report).Id == 0)
                    {
                        using (var db = new DBModel(StaticConfiguration.DBPath))
                        {
                            await db.Database.MigrateAsync();
                            await db.ReportCollectionDbSet.AddAsync(obj as DatabaseAnalysis.WPF.FireBird.Report);
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
                        var tmp= await db.ReportCollectionDbSet.Where(x => x.Id == ID)
                            .Include(x => x.Rows10).Include(x => x.Rows11.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows12.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows13.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows14.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows15.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows16.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows17.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows18.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows19.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows20.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows21.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows22.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows23.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows24.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows25.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows26.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows27.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows28.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows29.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows210.OrderBy(x => x.NumberInOrder_DB)).Include(x => x.Rows211.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Rows212.OrderBy(x => x.NumberInOrder_DB))
                            .Include(x => x.Notes.OrderBy(x => x.Order))
                            .FirstOrDefaultAsync() as T;
                        return tmp;
                    }
                }
                return null;
            }
            async Task<List<T>> IEssenceMethods.GetAllAsync<T>() where T : class
            {
                if (CheckType(typeof(T)))
                {
                    using (var db = new DBModel(StaticConfiguration.DBPath))
                    {
                        await db.Database.MigrateAsync();
                        return await db.ReportCollectionDbSet.OrderBy(x=>Convert.ToInt64(x.NumberInOrder_DB)).Select(x => x as T).ToListAsync();
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
                        Report _rep = obj as DatabaseAnalysis.WPF.FireBird.Report;
                        db.ReportCollectionDbSet.Update(_rep);
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
                        var rep = await db.ReportCollectionDbSet.Where(x => x.Id == ID).FirstOrDefaultAsync();
                        if (rep != null)
                        {
                            db.ReportCollectionDbSet.Remove(rep);
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
