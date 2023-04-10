using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DAL.Models
{
    public partial class TblGalleryPhoto
    {
        public int ImageId { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public string? Flag { get; set; }
        public bool IsDelete { get; set; }
        public string Details { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [NotMapped]
        public IFormFile PhotoUpload { get; set; }
    }
}
