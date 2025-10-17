using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ProdutModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        //1-to-many relationship Between Product and ProductBrand
        public ProductBrand ProductBrand { get; set; }
        public int BrandId { get; set; } // Foreign Key for ProductBrand

        //1-to-many relationship Between Product and ProductType
        public ProductType ProductType { get; set; }
        public int TypeId { get; set; } // Foreign Key for ProductType

    }
}
