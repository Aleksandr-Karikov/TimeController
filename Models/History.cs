using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeController.Areas.Identity.Data;

namespace TimeController.Models
{
    public class History
    {
        public TimeControllerUser User { get; set; }
        public string UserID { get; set; }
        public DateTime Date { get; set; }

        public int Time { get; set; }
    }
}
