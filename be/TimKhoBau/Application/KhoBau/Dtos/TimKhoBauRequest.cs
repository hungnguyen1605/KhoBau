using TimKhoBau.Model.KhoBau;

namespace TimKhoBau.Application.KhoBau.Dtos
{
    public class TimKhoBauRequest
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int P { get; set; }
        public List<List<int>> Matrix { get; set; } // Flattened matrix
        public KhobauEntity ToEntity(KhobauEntity khobauEntity=null)
        {
            var entity = new KhobauEntity()
            {
                Rows = Rows,
                Columns = Columns,
                P = P,
                CreatedAt = DateTime.Now,
                // Matrix = Matrix,
            };
            return entity;
        }

    }
}
