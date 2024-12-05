using ContractApi.HttpConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using TimKhoBau.Application.KhoBau.Dtos;
using TimKhoBau.Data;
using TimKhoBau.Model.KhoBau;

namespace TimKhoBau.Application.KhoBau
{
    public class TimKhoBauSevice : Service, ITimKhoBauSevice
    {
        private readonly AppDbContext _dbContext;

        public TimKhoBauSevice(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Hàm để tìm kho báu
        public async Task<ServiceResponse> KhoBau(TimKhoBauRequest request)
        {
            try
                {
                var chestPositions = new Dictionary<int, List<(int, int)>>();

                for (int i = 0; i < request.Rows; i++)
                {
                    for (int j = 0; j < request.Columns; j++)
                    {
                        int chestValue = request.Matrix[i][j];
                        if (!chestPositions.ContainsKey(chestValue))
                        {
                            chestPositions[chestValue] = new List<(int, int)>();
                        }
                        chestPositions[chestValue].Add((i, j));
                    }
                }

                // Bắt đầu từ rương giá trị 1
                double totalFuel = 0;
             
                List<listPosition> listPositions = new List<listPosition>();
                var listPosition= new listPosition()
                {
                    chestPositions= (0,0),
                    distance= 0,
                };
                listPositions.Add(listPosition);
                // Duyệt qua các giá trị rương từ 1 đến maxChest
                for (int chestValue = 1; chestValue <= request.P; chestValue++)
                {
                    if (!chestPositions.ContainsKey(chestValue))
                    {
                        throw new InvalidOperationException($"Không tìm thấy rương giá trị {chestValue}.");
                    }

                    List<listPosition> nextChest = FindNearestChest(listPositions, chestPositions[chestValue]);

                    listPositions = nextChest;
                     // Di chuyển đến vị trí rương tiếp theo
                }
                totalFuel = listPositions.OrderBy(x => x.distance).FirstOrDefault().distance;

                //
                var entity = request.ToEntity();
                //await _repository.AddAsync(entity);
                var entities = await _dbContext.khobauEntities.ToListAsync();
                var entityData=entities.Where(x=>x.P==request.P && x.Columns==request.Columns &&x.Rows==request.Rows && x.Matrix == JsonConvert.SerializeObject(request.Matrix)).ToList();
                if(entityData.Count==0) {
                    var addData = await AddNewData(request, totalFuel);
                }

                return Ok(totalFuel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private List<listPosition> FindNearestChest(List<listPosition> listPositions, List<(int, int)> chestPositions)
        {
            // Tạo danh sách các vị trí rương và khoảng cách
            List<listPosition> nextChest = new List<listPosition>();
            foreach (var position in chestPositions)
            {
                foreach(var curent in listPositions) {
                    double distance = CalculateFuel(curent.chestPositions, position);

                    // Tạo đối tượng vị trí rương với khoảng cách
                    var positionObj = new listPosition();

                    positionObj.chestPositions = position;
                    positionObj.distance = curent.distance+ distance;


                    // Thêm vào danh sách
                    nextChest.Add(positionObj);
                }
              
            }

            // Trả về danh sách các vị trí rương
            return nextChest;
        }

        public static double CalculateFuel((int x1, int y1) pos1, (int x2, int y2) pos2)
        {
            int dx = pos1.x1 - pos2.x2;
            int dy = pos1.y1 - pos2.y2;
            return Math.Sqrt(dx * dx + dy * dy);  // Tính khoảng cách Euclid
        }
        public class listPosition
        {
            public (int, int) chestPositions { get; set; } 
            public double distance { get; set; }
        }
        public async Task<ServiceResponse> AddNewData(TimKhoBauRequest request,double totalFuel)
        {
            try
            {
                var entity = request.ToEntity();
                entity.Matrix = JsonConvert.SerializeObject(request.Matrix);
                entity.TotalFuel = totalFuel;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return Ok(entity);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        public async Task<ServiceResponse> GetAllData()
        {
            try
            {
                var entities = await _dbContext.khobauEntities.ToListAsync();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ServiceResponse>DeletedAsync(Guid id)
        {
            try
            {
                var entity=await _dbContext.khobauEntities.FindAsync(id);
                if (entity != null)
                {
                     _dbContext.khobauEntities.Remove(entity);
                    _dbContext.SaveChanges();
                    return Ok("xóa data thành công");

                }
                else
                {
                    return Ok("xóa data thất bại");
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}



