using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PMO_Monitoring.datamodels
{
    [Table("mst_roleaccess")]
    public partial class MstRoleaccess
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("roleid")]
        public int Roleid { get; set; }
        [Column("menuid")]
        public int Menuid { get; set; }
        [Column("createdby")]
        public int Createdby { get; set; }
        [Column("createddate", TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
