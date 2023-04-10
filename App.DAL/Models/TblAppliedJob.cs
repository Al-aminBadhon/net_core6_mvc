using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblAppliedJob
    {
        public int AppliedJobId { get; set; }
        public int JobId { get; set; }
        public int CrewId { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
