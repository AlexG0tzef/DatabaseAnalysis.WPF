using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DatabaseAnalysis.WPF.FireBird
{
    public class DataContext : DbContext
    {
        public string _path { get; set; }
        public DbSet<DBObservable> DbobservableDbSets { get; set; }
        public DbSet<Form10> Form10s { get; set; }
        public DbSet<Form11> Form11s { get; set; }
        public DbSet<Form12> Form12s { get; set; }
        public DbSet<Form13> Form13s { get; set; }
        public DbSet<Form14> Form14s { get; set; }
        public DbSet<Form15> Form15s { get; set; }
        public DbSet<Form16> Form16s { get; set; }
        public DbSet<Form17> Form17s { get; set; }
        public DbSet<Form18> Form18s { get; set; }
        public DbSet<Form19> Form19s { get; set; }
        public DbSet<Form20> Form20s { get; set; }
        public DbSet<Form21> Form21s { get; set; }
        public DbSet<Form210> Form210s { get; set; }
        public DbSet<Form211> Form211s { get; set; }
        public DbSet<Form212> Form212s { get; set; }
        public DbSet<Form22> Form22s { get; set; }
        public DbSet<Form23> Form23s { get; set; }
        public DbSet<Form24> Form24s { get; set; }
        public DbSet<Form25> Form25s { get; set; }
        public DbSet<Form26> Form26s { get; set; }
        public DbSet<Form27> Form27s { get; set; }
        public DbSet<Form28> Form28s { get; set; }
        public DbSet<Form29> Form29s { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Reports> ReportsCollectionDbSet { get; set; }
        public DbSet<Report> ReportCollectionDbSet { get; set; }

        public DataContext(string Path = "")
        {
            _path = Path;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DBObservable>()
                .ToTable("DBObservable_DbSet");

            modelBuilder.Entity<Reports>()
                .ToTable("ReportsCollection_DbSet");
            modelBuilder.Entity<Report>()
                .ToTable("ReportCollection_DbSet");
            modelBuilder.Entity<Note>()
                .ToTable("notes");

            modelBuilder.Entity<Form10>()
                .ToTable("form_10");
            modelBuilder.Entity<Form11>()
                .ToTable("form_11");
            modelBuilder.Entity<Form12>()
                .ToTable("form_12");
            modelBuilder.Entity<Form13>()
                .ToTable("form_13");
            modelBuilder.Entity<Form14>()
                .ToTable("form_14");
            modelBuilder.Entity<Form15>()
                .ToTable("form_15");
            modelBuilder.Entity<Form16>()
                .ToTable("form_16");
            modelBuilder.Entity<Form17>()
                .ToTable("form_17");
            modelBuilder.Entity<Form18>()
                .ToTable("form_18");
            modelBuilder.Entity<Form19>()
                .ToTable("form_19");

            modelBuilder.Entity<Form20>()
                .ToTable("form_20");
            modelBuilder.Entity<Form21>()
                .ToTable("form_21");
            modelBuilder.Entity<Form22>()
                .ToTable("form_22");
            modelBuilder.Entity<Form23>()
                .ToTable("form_23");
            modelBuilder.Entity<Form24>()
                .ToTable("form_24");
            modelBuilder.Entity<Form25>()
                .ToTable("form_25");
            modelBuilder.Entity<Form26>()
                .ToTable("form_26");
            modelBuilder.Entity<Form27>()
                .ToTable("form_27");
            modelBuilder.Entity<Form28>()
                .ToTable("form_28");
            modelBuilder.Entity<Form29>()
                .ToTable("form_29");
            modelBuilder.Entity<Form210>()
                .ToTable("form_210");
            modelBuilder.Entity<Form211>()
                .ToTable("form_211");
            modelBuilder.Entity<Form212>()
                .ToTable("form_212");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            string pth = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\")), "data"), "REDDB"), "win-x64"), "fbclient.dll");
                }
                if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\")), "data"), "REDDB"), "win-x32"), "fbclient.dll");
                }
            }
            else
            {
                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\")), "data"), "REDDB"), "linux-x64"), "lib"), "libfbclient.so");
                }
                if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\")), "data"), "REDDB"), "linux-x32"), "lib"), "libfbclient.so");
                }
            }
#else
            string pth = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(AppContext.BaseDirectory), "data"), "REDDB"), "win-x64"), "fbclient.dll");
                }
                if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(AppContext.BaseDirectory), "data"), "REDDB"), "win-x32"), "fbclient.dll");
                }
            }
            else
            {
                if (RuntimeInformation.OSArchitecture == Architecture.X64)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(AppContext.BaseDirectory), "data"), "REDDB"), "linux-x64"), "lib"), "libfbclient.so");
                }
                if (RuntimeInformation.OSArchitecture == Architecture.X86)
                {
                    pth = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.GetFullPath(AppContext.BaseDirectory), "data"), "REDDB"), "linux-x32"), "lib"), "libfbclient.so");
                }
            }
#endif
            //var clLib = @"D:\RAO\RAO_Project\data\REDDB\win-x64\fbclient.dll";
            var clLib = pth;
            optionsBuilder.UseFirebird($"$User=SYSDBA;Password=masterkey;Database={_path};Role=ADMIN;Connection lifetime=15;Pooling=false;ServerType=1;ClientLibrary={clLib};");
        }

        public async Task LoadTablesAsync()
        {
            await Notes.LoadAsync();
            await Form10s.LoadAsync();
            await Form11s.LoadAsync();
            await Form12s.LoadAsync();
            await Form13s.LoadAsync();
            await Form14s.LoadAsync();
            await Form15s.LoadAsync();
            await Form16s.LoadAsync();
            await Form17s.LoadAsync();
            await Form18s.LoadAsync();
            await Form19s.LoadAsync();
            await Form20s.LoadAsync();
            await Form21s.LoadAsync();
            await Form22s.LoadAsync();
            await Form23s.LoadAsync();
            await Form24s.LoadAsync();
            await Form25s.LoadAsync();
            await Form26s.LoadAsync();
            await Form27s.LoadAsync();
            await Form28s.LoadAsync();
            await Form29s.LoadAsync();
            await Form210s.LoadAsync();
            await Form211s.LoadAsync();
            await Form212s.LoadAsync();
            await ReportCollectionDbSet.LoadAsync();
            await ReportsCollectionDbSet.LoadAsync();
            await DbobservableDbSets.LoadAsync();
        }
    }
}
