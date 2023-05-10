using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMO_Monitoring.viewmodel
{
    public class VMTrxProject
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("projectname")]
        [StringLength(250)]
        
        public string? Projectname { get; set; }
        [Column("divisiid")]
        public int? Divisiid { get; set; }
        [Column("keterangan")]
        [StringLength(500)]
       
        public string? Keterangan { get; set; }
        [Column("createdby")]
        public int? Createdby { get; set; }
        [Column("createddate", TypeName = "datetime")]
        public DateTime? Createddate { get; set; }
        [Column("lastupdatedby")]
        public int? Lastupdatedby { get; set; }
        [Column("lastupdateddate", TypeName = "datetime")]
        public DateTime? Lastupdateddate { get; set; }
        [Column("deleted")]
        public bool? Deleted { get; set; }

        public string? NamaDivisi { get; set; }

        public decimal? Progress { get; set; }

        public decimal? ActualProgress { get; set; }
        
    }
}
