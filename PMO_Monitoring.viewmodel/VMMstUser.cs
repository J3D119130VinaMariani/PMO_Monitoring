using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMO_viewmodel
{
    public class VMMstUser
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        [StringLength(100)]

        public string Username { get; set; } = null!;
        [Column("password")]
        [StringLength(255)]
      
        public string Password { get; set; } = null!;
        [Column("nama")]
        [StringLength(200)]
        
        public string Nama { get; set; } = null!;
        [Column("divisi")]
        public int Divisi { get; set; }
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

        public string Password2 { get; set; }
    }
}
