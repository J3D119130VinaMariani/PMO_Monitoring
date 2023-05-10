using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMO_Monitoring.viewmodel
{
    public class VMTrxDaftarAktifitas
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
       
        public string Aktifitas { get; set; } = null!;
        [Column("keterangan")]
        [StringLength(3000)]
        
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
        [Column("progress", TypeName = "decimal(18, 2)")]
        public decimal? Progress { get; set; }
        [Column("actual_progress", TypeName = "decimal(18, 2)")]
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

        public string? Approvalstatus { get; set; }
        [Column("komentar")]
 
        public string? Komentar { get; set; }
      
        public string? NamaDivisi { get; set; }
        public string? NamaStatus { get; set; }
        public string? CodeStatus { get; set; }
        public string? Projectname { get; set; }
        public double lamaProgress { get; set; }
        public double lamaActualProgres { get; set; }
    }
}
