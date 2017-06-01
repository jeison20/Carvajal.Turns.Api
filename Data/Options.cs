using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Options")]
    public partial class Options
    {
        [Key]
        public decimal Option_Id { get; set; }

        [Required]
        public decimal? Option_father { get; set; }

        [Required]
        public decimal Option_Level { get; set; }

        [StringLength(50)]
        [Required]
        public string Description { get; set; }

        [StringLength(100)]
        [Required]
        public string Display_Name { get; set; }
  
        [Required]
        public bool Is_Visible { get; set; }

        [Required]
        public bool Requires_Authorization { get; set; }

        [StringLength(200)]
        [Required]
        public string Url { get; set; }

        [StringLength(200)]
        [Required]
        public string Icon { get; set; }

        [StringLength(200)]
        [Required]
        public string Parameter { get; set; }

        

    }
}
