﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DAL.Models
{
    public partial class TblMissionVission
    {
        public int MissionVissionId { get; set; }
        public string? MissionImage { get; set; }
        public string? VissionImage { get; set; }
        public string? MissionDetails { get; set; }
        public string? VissionDetails { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [NotMapped]
        public IFormFile? PhotoUpload1 { get; set; }
        [NotMapped]
        public IFormFile? PhotoUpload2 { get; set; }
    }
}
