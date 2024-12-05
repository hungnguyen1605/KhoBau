using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Be.Domain.Common
{
   public abstract class AuditedEntity : AuditedEntity<Guid>
   {
   }

   public abstract class AuditedEntity<TKey> : Entity<TKey>, IAuditedEntity where TKey : IEquatable<TKey>
   {
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
 
   }
}
