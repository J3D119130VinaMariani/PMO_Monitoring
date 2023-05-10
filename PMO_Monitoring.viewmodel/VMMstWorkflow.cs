using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMO_Monitoring.viewmodel
{
    public class VMMstWorkflow
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

        public string? NamaMenu { get; set; }
        public string? NamaJabatan { get; set; }

    }
}
