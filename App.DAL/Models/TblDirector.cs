using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DAL.Models
{
    public partial class TblDirector
    {
        public int DirectorId { get; set; }
        public string DirectorName { get; set; } = null!;
        public string? Designation { get; set; }
        public string? CompanyPost { get; set; }
        public string? Image { get; set; }
        public string Details { get; set; } = null!;
        public string? FacebookLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? LinkedInLink { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [NotMapped]
        public IFormFile? PhotoUpload { get; set; }
    }
}
