namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LinkedCentres")]
    public partial class LinkedCentres
    {
        [Key]
        public long PkIdentifier { get; set; }

        [StringLength(35)]
        public string FkUsers_Identifier { get; set; }

        [StringLength(35)]
        public string FkCentres_Identifier { get; set; }

    }
}
