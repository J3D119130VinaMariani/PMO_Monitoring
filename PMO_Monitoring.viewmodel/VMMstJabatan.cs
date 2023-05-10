using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMO_Monitoring.viewmodel
{
    public class VMMstJabatan
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nama")]
        [StringLength(100)]        
        public string Nama { get; set; } = null!;
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
