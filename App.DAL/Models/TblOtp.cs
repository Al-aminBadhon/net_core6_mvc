using System;
using System.Collections.Generic;

namespace App.DAL.Models
{
    public partial class TblOtp
    {
        public int OtpId { get; set; }
        public string Otp { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
