using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblDesignation
    {
        public int DesigId { get; set; }
        public string DesigName { get; set; } = null!;
        public int DesigTypeId { get; set; }
        public bool? IsActive { get; set; }
        public string Details { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual TblDesignationType DesigType { get; set; } = null!;
    }
}
