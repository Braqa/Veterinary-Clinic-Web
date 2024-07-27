using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlinikaVeterinareTI2.Models
{
    public class BaseAudit
    {
        public string InsertBy { get; set; }

        public DateTime InsertDate { get; set; }

        public string LUB { get; set; }

        public DateTime LUD { get; set; }

        public int LUN { get; set; }

        public bool IsDeleted { get; set; }
    }
}
