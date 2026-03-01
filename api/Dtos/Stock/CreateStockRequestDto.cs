using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using System.Threading.Tasks;

namespace api.Dtos

{

    public class CreateStockRequestDto

    { 
         [Required]
        [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters.")]
        public string CompanyName { get; set; } = string.Empty;
       [Required]
       [Range(1,100000)]
        public decimal Purchase { get; set; }

        public double LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

    }

}