namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Users")]
    public partial class Users
    {

        public Users()
        {

        }

        [Key]
        [StringLength(35)]
        public string PkIdentifier { get; set; }

        public bool ChangePasswordNextTime { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(175)]
        public string Name { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LastAccess { get; set; }

        [Required]
        [StringLength(2)]
        public string FkRole_Identifier { get; set; }

        [Required]
        [StringLength(512)]
        public string Email { get; set; }

        public bool Status { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LastChangeDate { get; set; }

        [Required]
        [StringLength(35)]
        public string FkCompanies_Identifier { get; set; }

        [Required]
        public long FkCountries_Identifier { get; set; }

        [StringLength(200)]
        public string OldPassword { get; set; }

        [StringLength(70)]
        public string Address { get; set; }
    }
}
