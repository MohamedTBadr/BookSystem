using BookReview.Services.Interfaces;

namespace BookReview.Services
{
    public class AttachmentService : IAttachmentService
    {
        public List<string> allowedExtensions = new()
        {
            ".jpg",".png",".pdf"
        };

        public const int maxAllowed = 2_000_000;
        public async Task<string?> Upload(IFormFile file, string folderName)
        {
            //1.Get File extension
             var extension= Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension)) return null;
            //2. Get file size
            if (file.Length > maxAllowed) return null;

            //3. get located folder path
            var folderPath= Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Attachments",folderName);


            //4. SET UNIQUE FILE NAME   
            var fileName = $"{Guid.NewGuid()}{extension}";


            //5. Get file path
            var filePath= Path.Combine(folderPath,fileName);


            //6. save file as stream
            using var fileStream = new FileStream(filePath, FileMode.Create);

            //7. copy file path to stream
           await file.CopyToAsync(fileStream);


            //8.return file name

            return fileName;

        }
   
        public async Task<bool> Delete(string filePath) {
            //validate if file exists
            if (File.Exists(filePath))
            {

                File.Delete(filePath);
                return true;
            }
            return false;
        }
    
    }
}
