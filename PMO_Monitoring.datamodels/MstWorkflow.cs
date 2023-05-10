using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PMO_Monitoring.datamodels
{
    [Table("mst_workflow")]
    public partial class MstWorkflow
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("menuid")]
        public int Menuid { get; set; }
        [Column("sequence")]
        public int Sequence { get; set; }
        [Column("jabatanid")]
        public int Jabatanid { get; set; }
        [Column("createdby")]
        public int Createdby { get; set; }
        [Column("createddate", TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("updatedby")]
        public int? Updatedby { get; set; }
        [Column("updateddate", TypeName = "datetime")]
        public DateTime? Updateddate { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
