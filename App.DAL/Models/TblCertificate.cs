using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblCertificate
    {
        public int CertificateId { get; set; }
        public int CerTypeId { get; set; }
        public int UserRoleId { get; set; }
        public int CerNumber { get; set; }
        public string CerName { get; set; } = null!;
        public DateTime? DateOfIssue { get; set; }
        public string? PlaceOfIssue { get; set; }
        public string? Details { get; set; }
        public DateTime? ExpDate { get; set; }
        public bool IsDelete { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
