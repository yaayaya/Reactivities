using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    // DbContext EntityFrameworkCore 連接DB建立ORM實例 產生Table
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // 設置DB  將會創建Activities的資料表
        public DbSet<Activity> Activities { get; set; }
    }
}