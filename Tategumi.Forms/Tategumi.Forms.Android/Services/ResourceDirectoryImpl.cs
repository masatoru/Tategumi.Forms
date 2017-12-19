using System.IO;
using Tategumi.Droid.Services;
using Tategumi.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ResourceDirectoryImpl))]
namespace Tategumi.Droid.Services
{
    public class ResourceDirectoryImpl : IResourceDirectory
    {
        public string ReadText(string fileName)
        {
            var stream = Android.App.Application.Context.Assets.Open(fileName);
            var text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
        public FileStream OpenFontFile(string fileName)
        {
            var stream = Android.App.Application.Context.Assets.Open(fileName);

            var copyPath = System.IO.Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);

//            var fontPath = Path.Combine(CacheDir.AbsolutePath, fileName);
            using (var asset = Android.App.Application.Context.Assets.Open(fileName))
            using (var dest = File.Open(copyPath, FileMode.Create))
            {
                asset.CopyTo(dest);
            }
            return File.OpenRead(copyPath);

        }
    }
}


