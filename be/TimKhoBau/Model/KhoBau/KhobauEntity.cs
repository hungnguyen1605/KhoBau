using Be.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimKhoBau.Model.KhoBau
{
    [Table("kho_bau_entity")]
    public class KhobauEntity: AuditedEntity
    {
 

        [Column("rows")]
        public int Rows { get; set; }

        [Column("Columns")]
        public int Columns { get; set; }

        [Column("p")]
        public int P { get; set; }

        [Column("matrix")]
        public string Matrix { get; set; }        
        [Column("total_fuel")]
        public double TotalFuel { get; set; }
    }
}
