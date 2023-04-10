using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblDesignationType
    {
        public TblDesignationType()
        {
            TblDesignations = new HashSet<TblDesignation>();
        }

        public int DesigTypeId { get; set; }
        public string DesigName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<TblDesignation> TblDesignations { get; set; }
    }
}
