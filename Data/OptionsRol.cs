
namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OptionsRol")]
    public partial class OptionsRol
    {
        [Key]
        public long PkIdOptionRol { get; set; }

        [StringLength(2)]
        [Required]
        public string FkRol_Identifier { get; set; }

        [Required]
        public decimal FkOption_Identifier { get; set; }
    }
}
