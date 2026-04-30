using Hook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class MarketplaceProductImage : Auditable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public virtual MarketplaceProduct Product { get; set; } = null!;

        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMainImage { get; set; }
    }
}
