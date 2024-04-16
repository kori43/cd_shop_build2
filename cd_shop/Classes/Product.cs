using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cd_shop.Classes
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int PublisherId { get; set; }
        public int GenreId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
