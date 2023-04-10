using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblEducationalBackground
    {
        public int EduBackId { get; set; }
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public string DegreeName { get; set; } = null!;
        public string InstitutionName { get; set; } = null!;
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
