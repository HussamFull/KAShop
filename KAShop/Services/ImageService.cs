namespace KAShop.Services
{
    public class ImageService
    {

        private  string _imageFolderPath ;
        public ImageService() {

            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images");
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }
        }
        public string UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                image.CopyTo(stream);
            }
            return fileName;
        }
        public bool DeleteImage(string fileName)
        {
            //if (string.IsNullOrEmpty(fileName))
            //    return;
            var fullPath = Path.Combine(_imageFolderPath, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }
    }
}
