using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PMO_Monitoring.datamodels
{
    [Table("trx_daftaraktifitas")]
    public partial class TrxDaftaraktifita
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("divisiid")]
        public int Divisiid { get; set; }
        [Column("projectid")]
        public int Projectid { get; set; }
        [Column("urutan")]
        public int Urutan { get; set; }
        [Column("aktifitas")]
        [StringLength(1000)]
        [Unicode(false)]
        public string Aktifitas { get; set; } = null!;
        [Column("keterangan")]
        [StringLength(3000)]
        [Unicode(false)]
        public string? Keterangan { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("startdate", TypeName = "date")]
        public DateTime Startdate { get; set; }
        [Column("enddate", TypeName = "date")]
        public DateTime Enddate { get; set; }
        [Column("actual_startdate", TypeName = "date")]
        public DateTime? ActualStartdate { get; set; }
        [Column("actual_enddate", TypeName = "date")]
        public DateTime? ActualEnddate { get; set; }
        [Column("progress", TypeName = "numeric(18, 2)")]
        public decimal? Progress { get; set; }
        [Column("actual_progress", TypeName = "numeric(18, 2)")]
        public decimal? ActualProgress { get; set; }
        [Column("createdby")]
        public int Createdby { get; set; }
        [Column("createddate", TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("updatedby")]
        public int? Updatedby { get; set; }
        [Column("updateddate", TypeName = "datetime")]
        public DateTime? Updateddate { get; set; }
        [Column("approvalby")]
        public int? Approvalby { get; set; }
        [Column("approvaldate", TypeName = "datetime")]
        public DateTime? Approvaldate { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
        [Column("approvalstatus")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Approvalstatus { get; set; }
        [Column("komentar")]
        [Unicode(false)]
        public string? Komentar { get; set; }
    }
}
