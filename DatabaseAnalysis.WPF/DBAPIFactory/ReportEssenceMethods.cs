using DatabaseAnalysis.WPF.FireBird;
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
                        using (var db = new DBModel(StaticConfiguration.DBYearPath))
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
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
                    {
                        db.Database.Migrate();
                        return db.ReportCollectionDbSet.Where(x => x.Id == ID).OrderBy(x => x.Order)
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
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
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
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
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
                        using (var db = new DBModel(StaticConfiguration.DBYearPath))
                        {
                            await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                            await db.ReportCollectionDbSet.AddAsync(obj as Report, ReportsStorge.cancellationToken);
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
                    if (StaticConfiguration.TpmDb.Equals("OPER"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBOperPath))
                        {
                            T tmp = new object() as T;
                            try
                            {
                                await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                                IQueryable<Report> dbQ = db.ReportCollectionDbSet;
                                tmp = await dbQ.Where(x => x.Id == ID)
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
                                    .FirstOrDefaultAsync(ReportsStorge.cancellationToken) as T;
                            }
                            catch { }
                            return tmp;
                        }
                    }
                    if (StaticConfiguration.TpmDb.Equals("YEAR"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBYearPath))
                        {
                            T tmp = new object() as T;
                            try
                            {
                                await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                                IQueryable<Report> dbQ = db.ReportCollectionDbSet;
                                tmp = await dbQ.Where(x => x.Id == ID)
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
                                    .FirstOrDefaultAsync(ReportsStorge.cancellationToken) as T;
                            }
                            catch { }
                            return tmp;
                        }
                    }
                }
                return null;
            }
            async Task<List<T?>> IEssenceMethods.GetAllAsync<T>(string param) where T : class
            {
                if (CheckType(typeof(T)))
                {
                    if (StaticConfiguration.TpmDb.Equals("OPER"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBOperPath))
                        {
                            List<T?> tmp = new();
                            try
                            {
                                await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                                switch (param)
                                {
                                    #region 1
                                    case "1":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.Rows10.Count != 0 || x.Rows10.Count != null)
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows11).Include(x => x.Rows12).Include(x => x.Rows13)
                                            .Include(x => x.Rows14).Include(x => x.Rows15).Include(x => x.Rows16)
                                            .Include(x => x.Rows17).Include(x => x.Rows18).Include(x => x.Rows19)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 1.1
                                    case "1.1":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows11)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 1.2
                                    case "1.2":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows12)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 1.3
                                    case "1.3":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows13)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 1.4
                                    case "1.4":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows14)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 1.5
                                    case "1.5":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows15)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 1.6
                                    case "1.6":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows16)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 1.7
                                    case "1.7":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows17)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 1.8
                                    case "1.8":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows18)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 1.9
                                    case "1.9":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows10)
                                            .Include(x => x.Rows19)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    default:
                                        break;
                                }
                            }
                            catch { }
                            return tmp;
                        }
                    }
                    if (StaticConfiguration.TpmDb.Equals("YEAR"))
                    {
                        using (var db = new DBModel(StaticConfiguration.DBYearPath))
                        {
                            List<T?> tmp = new();
                            try
                            {
                                await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                                switch (param)
                                {
                                    #region 2
                                    case "2":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.Rows20.Count != 0 || x.Rows20.Count != null)
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows21).Include(x => x.Rows22).Include(x => x.Rows23)
                                            .Include(x => x.Rows24).Include(x => x.Rows25).Include(x => x.Rows26)
                                            .Include(x => x.Rows27).Include(x => x.Rows28).Include(x => x.Rows29)
                                            .Include(x => x.Rows210).Include(x => x.Rows211).Include(x => x.Rows212)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.1
                                    case "2.1":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows21)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.2
                                    case "2.2":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows22)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.3
                                    case "2.3":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows23)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.4
                                    case "2.4":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows24)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.5
                                    case "2.5":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows25)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.6
                                    case "2.6":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows26)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.7
                                    case "2.7":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows27)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.8
                                    case "2.8":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows28)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.9
                                    case "2.9":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows29)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.10
                                    case "2.10":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows210)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.11
                                    case "2.11":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows211)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    #region 2.12
                                    case "2.12":
                                        tmp = await ((IQueryable<Report>)db.ReportCollectionDbSet)
                                            .Where(x => x.FormNum_DB.Equals(param))
                                            .Include(x => x.Rows20)
                                            .Include(x => x.Rows212)
                                            .Select(x => x as T)
                                            .ToListAsync(ReportsStorge.cancellationToken);
                                        break;
                                    #endregion
                                    default:
                                        break;
                                }
                            }
                            catch { }
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
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
                    {
                        await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                        Report _rep = obj as Report;
                        db.ReportCollectionDbSet.Update(_rep);
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
                    using (var db = new DBModel(StaticConfiguration.DBYearPath))
                    {
                        await db.Database.MigrateAsync(ReportsStorge.cancellationToken);
                        var rep = await db.ReportCollectionDbSet.Where(x => x.Id == ID).FirstOrDefaultAsync(ReportsStorge.cancellationToken);
                        if (rep != null)
                        {
                            db.ReportCollectionDbSet.Remove(rep);
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
