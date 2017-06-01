namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Roles
    {
    
        [Key]
        [StringLength(2)]
        public string PkIdentifier { get; set; }

        [Required]
        [StringLength(35)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string InitialUrl { get; set; }


    }
}
