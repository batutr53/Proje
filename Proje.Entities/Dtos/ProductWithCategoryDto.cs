using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Entities.Dtos
{
    public class ProductWithCategoryDto:ProductListDto
    {
        public CategoryDto Category { get; set; }
    }
}
