namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Centres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Centres()
        {
  
        }

        [Required]
        [StringLength(35)]
        public string FkUsers_Merchant_Identifier { get; set; }

        [StringLength(35)]
        public string FkUsers_Responsable_Identifier { get; set; }

        [Key]
        [StringLength(35)]
        public string PkIdentifier { get; set; }

        public int? WeeklyCapacity { get; set; }

        public int? CurrentWeekCapacity { get; set; }

        [StringLength(1)]
        public string FirstDay { get; set; }

        [StringLength(7)]
        public string ListOfWorkingDays { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? StartTime { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? EndTime { get; set; }

        [StringLength(175)]
        public string Name { get; set; }

        public short? NumberOfDocks { get; set; }

        public int? TimeBetweenSuppliers { get; set; }

        public bool Status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LastChangeDate { get; set; }
        public long FkTimezones_Identifier { get; set; }
        [StringLength(70)]
        public string AddressStreet { get; set; }
        [StringLength(70)]
        public string AddressNumber { get; set; }
        [StringLength(9)]
        public string PostCode { get; set; }
        [StringLength(35)]
        public string Town { get; set; }
        [StringLength(9)]
        public string Region { get; set; }
       
    }
}
