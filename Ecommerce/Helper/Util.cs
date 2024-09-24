using System.Text;

namespace Ecommerce.Helper
{
    public class Util
    {
        public static string UpLoadHinh(IFormFile hinh, string folder)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Hinh", folder,hinh.FileName);
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    hinh.CopyTo(myfile);
                }
                return hinh.FileName;
            }
            catch (Exception ex) {
                return string.Empty;
            }
        }
        public static string GenerateRamdomKey(int length = 5)
        {
            var parttern = @"iuerjhsainduucnakeudndajisnUEHJSISNWJ!";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(parttern[rd.Next(0, parttern.Length)]);
            }
            return sb.ToString();
        }
    }
}
