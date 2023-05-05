using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DAL.Models
{
    public partial class TblCompany
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Phone3 { get; set; }
        public string? Image { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? CompanyDetails { get; set; }
        public bool IsActive { get; set; }
        public bool IsAprroved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        [NotMapped]
        public IFormFile PhotoUpload { get; set; }
        [NotMapped]
        public string Password { get; set; }
    }
}
