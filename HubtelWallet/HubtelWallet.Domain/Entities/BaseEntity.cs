using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubtelWallet.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }

        protected BaseEntity()
        {
            CreatedAt = DateTimeOffset.Now;
            UpdatedAt = DateTimeOffset.Now;
        }
    }
}
