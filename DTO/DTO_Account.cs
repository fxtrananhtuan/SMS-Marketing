using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Account
    {
        public string ID { get; set; }
        public string email { get; set; }
        public DateTime create_date { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
    }
}
