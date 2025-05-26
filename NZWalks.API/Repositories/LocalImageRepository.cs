using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly NZWalksDbContext nZWalksDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public LocalImageRepository(IHttpContextAccessor httpContext,NZWalksDbContext nZWalksDbContext,IWebHostEnvironment webHostEnvironment)
        {
            this.httpContext = httpContext;
            this.nZWalksDbContext = nZWalksDbContext;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create,FileAccess.ReadWrite);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}{httpContext.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath= urlFilePath;

            await nZWalksDbContext.Images.AddAsync(image);
            await nZWalksDbContext.SaveChangesAsync();
            return image;
        }
    }
}
