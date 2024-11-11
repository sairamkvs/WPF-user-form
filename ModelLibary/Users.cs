using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibary
{
    public class Users
    {
        public int User_Id { get; set; }
        public string? User_Name { get; set; }
        public string? Group { get; set; }

        public string? Host { get; set; }

        public long IP_Address { get; set; }
    }
}
