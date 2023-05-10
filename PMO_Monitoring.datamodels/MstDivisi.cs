﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PMO_Monitoring.datamodels
{
    [Table("mst_divisi")]
    public partial class MstDivisi
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("acronim")]
        [StringLength(10)]
        [Unicode(false)]
        public string Acronim { get; set; } = null!;
        [Column("name")]
        [StringLength(100)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
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