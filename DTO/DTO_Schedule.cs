using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Schedule
    {
        public string ID { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string SMS_info { get; set; }
        public string AccountID { get; set; }
        public bool Active { get; set; }

    }
}
