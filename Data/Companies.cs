namespace Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Companies")]
    public partial class Companies
    {
        [Key]
        [StringLength(35)]
        public string PkIdentifier { get; set; }

        public bool ChangePasswordNextTime { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(175)]
        public string Name { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LastAccess { get; set; }

        [StringLength(2)]
        public string FkRole_Identifier { get; set; }

        [StringLength(512)]
        public string Email { get; set; }

        public bool Status { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LastChangeDate { get; set; }

        [StringLength(35)]
        public string Companies_Identifier { get; set; }

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

        public long FkCountries_Identifier { get; set; }
    }
}