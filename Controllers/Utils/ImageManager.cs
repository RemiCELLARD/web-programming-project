using Microsoft.Extensions.Hosting;

namespace Web_Programming_Project.Controllers.Utils
{
    public class ImageManager
    {
        public async Task<string> UploadImage(string directory, IFormFile fileUploadForm) 
        {
            string imgNameUpload = "";
            string fileName, fileExtension, imgPath;

            if (fileUploadForm != null)
            {
                fileName = Path.GetFileNameWithoutExtension(fileUploadForm.FileName);
                fileExtension = Path.GetExtension(fileUploadForm.FileName);
                imgNameUpload = DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + "_" + (new Random()).Next(5000) + "_" + fileName + fileExtension;
                imgPath = Path.Combine(directory, imgNameUpload);
                using (FileStream fileStream = new FileStream(imgPath, FileMode.Create))
                {
                    await fileUploadForm.CopyToAsync(fileStream);
                }
            }
            else
            {
                imgNameUpload = "default.png";
            }
            return imgNameUpload;
        }

        public async Task<bool> DeleteImage(string directory, string? imgNameUpload) 
        {
            if (imgNameUpload == null)
                return false;

            if (imgNameUpload.Equals("default.png")) // do not delete the default img
                return false;

            string imgPath = Path.Combine(directory, imgNameUpload);
            if (!System.IO.File.Exists(imgPath))
                return false;
            
            System.IO.File.Delete(imgPath);
            return true;
        }

        public async Task<string> ReplaceImage(string directory, string? imgNameUpload, IFormFile fileUploadForm)
        {
            await this.DeleteImage(directory, imgNameUpload);
            return await this.UploadImage(directory, fileUploadForm);
        }
    }
}
