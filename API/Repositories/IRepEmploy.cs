using API.AppliDbContext;
using Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public interface IRepEmploy
    {
        Task<IEnumerable<Employee>> GetAll();
        Task Create(Employee employee);
        Task Update(Employee employee);
        Task Delete(int id);
        Task<Employee> GetById(int id);
        Task<string> umploadImage(IFormFile file);
    }
    public class RepEmploy : IRepEmploy
    {
        private readonly EmployDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string UploadFolder = "uploadss";
        private readonly string[] _AllowExtension = { ".jpg", ".png", ".gif", ".jpeg" };
        public RepEmploy(EmployDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var Result = await _db.employees.ToListAsync();
            return Result;
        }

        public async Task Create(Employee employee)
        {
            _db.employees.Add(employee);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var getId = await _db.employees.FindAsync(id);
            if (getId != null) 
            {
                _db.employees.Remove(getId);
                await _db.SaveChangesAsync();
            }
        }

   

        public async Task<Employee> GetById(int id)
        {
            var getId = await _db.employees.FindAsync(id);
            return getId;
        }

        public async Task<string> umploadImage(IFormFile file)
        {
            var Extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_AllowExtension.Contains(Extension))
            {
                throw new ArgumentException("Error");
            }
            var Filename = Guid.NewGuid().ToString() + Extension;
            var uploadFile = Path.Combine(_webHostEnvironment.WebRootPath, UploadFolder);
            Directory.CreateDirectory(uploadFile);
            var PathFile = Path.Combine(uploadFile, Filename);
            using (var Stream = new FileStream(PathFile, FileMode.Create))
            {
                await file.CopyToAsync(Stream);
            }
            return $"/{UploadFolder}/{Filename}";
        }

        public async Task Update(Employee employee)
        {
            _db.employees.Update(employee);
            await _db.SaveChangesAsync();
        }
    }
}
