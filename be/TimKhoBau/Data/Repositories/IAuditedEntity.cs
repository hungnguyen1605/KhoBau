using System;

namespace Be.Domain.Common
{
   public interface IAuditedEntity
   {
      DateTime CreatedAt { get; set; }


   }
}
