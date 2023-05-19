using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblWhyBdcrew
    {
        public int WhyBdcrewId { get; set; }
        public string? Image { get; set; }
        public string? Details { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
