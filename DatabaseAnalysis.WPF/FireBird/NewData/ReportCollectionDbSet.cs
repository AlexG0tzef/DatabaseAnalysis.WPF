using System;
using System.Collections.Generic;

namespace Reports.Domain
{
    public partial class ReportCollectionDbSet
    {
        public ReportCollectionDbSet()
        {
            Form10s = new HashSet<Form10>();
            Form11s = new HashSet<Form11>();
            Form12s = new HashSet<Form12>();
            Form13s = new HashSet<Form13>();
            Form14s = new HashSet<Form14>();
            Form15s = new HashSet<Form15>();
            Form16s = new HashSet<Form16>();
            Form17s = new HashSet<Form17>();
            Form18s = new HashSet<Form18>();
            Form19s = new HashSet<Form19>();
            Form20s = new HashSet<Form20>();
            Form210s = new HashSet<Form210>();
            Form211s = new HashSet<Form211>();
            Form212s = new HashSet<Form212>();
            Form21s = new HashSet<Form21>();
            Form22s = new HashSet<Form22>();
            Form23s = new HashSet<Form23>();
            Form24s = new HashSet<Form24>();
            Form25s = new HashSet<Form25>();
            Form26s = new HashSet<Form26>();
            Form27s = new HashSet<Form27>();
            Form28s = new HashSet<Form28>();
            Form29s = new HashSet<Form29>();
            Notes = new HashSet<Note>();
            ReportsCollectionDbSets = new HashSet<ReportsCollectionDbSet>();
        }

        public int Id { get; set; }
        public string? FormNumDb { get; set; }
        public bool IsCorrectionDb { get; set; }
        public short CorrectionNumberDb { get; set; }
        public string? FioexecutorDb { get; set; }
        public string? GradeExecutorDb { get; set; }
        public string? ExecPhoneDb { get; set; }
        public string? ExecEmailDb { get; set; }
        public string? NumberInOrderDb { get; set; }
        public string? CommentsDb { get; set; }
        public string? PermissionNumber28Db { get; set; }
        public string? PermissionIssueDate28Db { get; set; }
        public string? PermissionDocumentName28Db { get; set; }
        public string? ValidBegin28Db { get; set; }
        public string? ValidThru28Db { get; set; }
        public string? PermissionNumber128Db { get; set; }
        public string? PermissionIssueDate128Db { get; set; }
        public string? PermissionDocumentName128Db { get; set; }
        public string? ValidBegin128Db { get; set; }
        public string? ValidThru128Db { get; set; }
        public string? ContractNumber28Db { get; set; }
        public string? ContractIssueDate228Db { get; set; }
        public string? OrganisationReciever28Db { get; set; }
        public string? ValidBegin228Db { get; set; }
        public string? ValidThru228Db { get; set; }
        public string? PermissionNumber27Db { get; set; }
        public string? PermissionIssueDate27Db { get; set; }
        public string? PermissionDocumentName27Db { get; set; }
        public string? ValidBegin27Db { get; set; }
        public string? ValidThru27Db { get; set; }
        public int? SourcesQuantity26Db { get; set; }
        public int? YearDb { get; set; }
        public string? StartPeriodDb { get; set; }
        public string? EndPeriodDb { get; set; }
        public string? ExportDateDb { get; set; }
        public int? ReportsId { get; set; }

        public virtual ReportsCollectionDbSet? Reports { get; set; }
        public virtual ICollection<Form10> Form10s { get; set; }
        public virtual ICollection<Form11> Form11s { get; set; }
        public virtual ICollection<Form12> Form12s { get; set; }
        public virtual ICollection<Form13> Form13s { get; set; }
        public virtual ICollection<Form14> Form14s { get; set; }
        public virtual ICollection<Form15> Form15s { get; set; }
        public virtual ICollection<Form16> Form16s { get; set; }
        public virtual ICollection<Form17> Form17s { get; set; }
        public virtual ICollection<Form18> Form18s { get; set; }
        public virtual ICollection<Form19> Form19s { get; set; }
        public virtual ICollection<Form20> Form20s { get; set; }
        public virtual ICollection<Form210> Form210s { get; set; }
        public virtual ICollection<Form211> Form211s { get; set; }
        public virtual ICollection<Form212> Form212s { get; set; }
        public virtual ICollection<Form21> Form21s { get; set; }
        public virtual ICollection<Form22> Form22s { get; set; }
        public virtual ICollection<Form23> Form23s { get; set; }
        public virtual ICollection<Form24> Form24s { get; set; }
        public virtual ICollection<Form25> Form25s { get; set; }
        public virtual ICollection<Form26> Form26s { get; set; }
        public virtual ICollection<Form27> Form27s { get; set; }
        public virtual ICollection<Form28> Form28s { get; set; }
        public virtual ICollection<Form29> Form29s { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<ReportsCollectionDbSet> ReportsCollectionDbSets { get; set; }
    }
}
