using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Model
{
    class TempVendor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public int companyId { get; set; }
        public string personalPhoneNumber { get; set; }
        public string businessPhoneNumber { get; set; }

    }
}
