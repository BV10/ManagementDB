using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementShopDB.Model
{
    class Accessory
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "No name";
        public string Color { get; set; } = "No name";
        public string TypeOfLink { get; set; } = "No name";
        public int ProducerId { get; set; } = 0;
        public int AccessoryTypeId { get; set; } = 0;
    }
}
