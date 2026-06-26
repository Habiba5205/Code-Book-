using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBook.Business.App.DTOs
{
    internal class ModerationRequest
    {
        public int PostId { get; set; }
        public int ReportId { get; set; }
        public string Reason { get; set; }
    }
}
