using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblJob
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; } = null!;
        public int UserRoleId { get; set; }
        public int CompanyId { get; set; }
        public string? JobLevel { get; set; }
        public string? Salary { get; set; }
        public string? Experience { get; set; }
        public string? JobType { get; set; }
        public DateTime? DeadLine { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; }
        public bool? IsApproved { get; set; }
        public string JobDetails { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
