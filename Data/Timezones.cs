namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Timezones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Timezones()
        {
           
        }

        [Key]
        public long PkIdentifier { get; set; }

        public int FkCountries_Identifier { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(4)]
        public string Code { get; set; }

        public int UTCOffset { get; set; }

        public virtual Countries Countries { get; set; }

    }
}
