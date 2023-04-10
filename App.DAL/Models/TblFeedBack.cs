using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblFeedBack
    {
        public int FeedBackId { get; set; }
        public string SenderName { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Opinion { get; set; } = null!;
        public bool IsDelete { get; set; }
        public bool SeenStatus { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
