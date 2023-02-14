using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PMO_Monitoring.datamodels
{
    public partial class PMO_MonitoringContext : DbContext
    {
        public PMO_MonitoringContext()
        {
        }

        public PMO_MonitoringContext(DbContextOptions<PMO_MonitoringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MstUser> MstUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.15.2.162;Initial Catalog=PMO_Monitoring;user id=vina_pmo;Password=P@ssw0rd@2023");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
