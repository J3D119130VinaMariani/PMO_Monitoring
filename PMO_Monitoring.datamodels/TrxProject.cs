using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PMO_Monitoring.datamodels
{
    [Table("trx_project")]
    public partial class TrxProject
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("projectname")]
        [StringLength(250)]
        [Unicode(false)]
        public string? Projectname { get; set; }
        [Column("divisiid")]
        public int? Divisiid { get; set; }
        [Column("keterangan")]
        [StringLength(500)]
        [Unicode(false)]
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

       
    }
}
