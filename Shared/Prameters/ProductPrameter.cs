using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Prameters
{
  public  class ProductPrameter
    {
        private const int DafualtPageSize = 5;

        private const int MaxPageIndex = 10;
        private int pageSize = DafualtPageSize;
        public int? BrandId { get; set; }
        public int ?TypeId { get; set; }
        public ProductSortingOptions? ProductSortingOptions { get; set; }
        public string? Search { get; set; }

        public int PageSize { get => pageSize; set => pageSize = value > 0 && value < MaxPageIndex ? value : DafualtPageSize; }
        public int PageIndex { get; set; }
    }
}
