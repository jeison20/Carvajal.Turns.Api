
namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [Key]
        public long Id { get; set; }

        [StringLength(50)]
        [Required]
        public string User { get; set; }

        [StringLength(4000)]
        [Required]
        public string Token { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime RefreshTokenLifeTime { get; set; }

        [StringLength(4000)]
        [Required]
        public string AllowedOrigin { get; set; }
        

    }

}
