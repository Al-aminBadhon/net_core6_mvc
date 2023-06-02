using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DAL.Models
{
    public partial class TblExecutive
    {
        public int ExecutiveId { get; set; }
        public int? UserRoleId { get; set; }
        public int? UserId { get; set; }
        public string ExFirstName { get; set; } = null!;
        public string? ExLastName { get; set; }
        public string? Designation { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        [NotMapped]
        public IFormFile? PhotoUpload { get; set; }
        [NotMapped]
        public string? Password { get; set; }
    }
}
