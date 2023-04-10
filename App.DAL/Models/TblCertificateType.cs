using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblCertificateType
    {
        public int CerTypeId { get; set; }
        public string CerTypeName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
