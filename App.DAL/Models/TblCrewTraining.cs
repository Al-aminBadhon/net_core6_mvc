using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblCrewTraining
    {
        public int CrewTrainingId { get; set; }
        public string Title { get; set; } = null!;
        public string? Image { get; set; }
        public string? Details { get; set; }
        public string? Url { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
