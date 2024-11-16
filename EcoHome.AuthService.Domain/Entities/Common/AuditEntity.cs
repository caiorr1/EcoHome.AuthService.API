using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Entities.Common
{
    public abstract class AuditEntity : BaseEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
