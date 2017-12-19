using System.IO;
using Foundation;
using Tategumi.iOS.Services;
using Tategumi.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ResourceDirectoryImpl))]
namespace Tategumi.iOS.Services
{
    public class ResourceDirectoryImpl : IResourceDirectory
    {
        public string ReadText(string fileName)
        {
            var path = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            var text = "";
            using (var reader = new System.IO.StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
        public FileStream OpenFontFile(string fileName)
        {
            var path = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            return File.OpenRead(path);
        }
    }
}


