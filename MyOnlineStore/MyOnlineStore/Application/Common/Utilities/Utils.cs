using MyOnlineStore.Entities.Models.Users;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyOnlineStore.Application.Common.Utilities
{
    public static class Utils
    {
        public static void ExtractSaveResource(string filename, string location)
        {
            var a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream != null)
                {
                    lock (resFilestream)
                    {
                        var full = Path.Combine(location, filename);
                        using (var stream = File.Create(full))
                        {
                            resFilestream.CopyTo(stream);
                        }
                    }
                }
            }
        }
        public static void GetByteArrayFromPath(out byte[] source, string path)
        {
            ExtractSaveResource(path, FileSystem.CacheDirectory);
            Stream str = new StreamReader(Path.Combine(FileSystem.CacheDirectory, path)).BaseStream;
            MemoryStream ms = new MemoryStream();
            str.CopyTo(ms);
            source = ms.ToArray();
        }

        public static async Task<byte[]> PickPhoto()
        {
            MediaFile mediaFile;
            MemoryStream memoryStream;
            byte[] productImageSource = null!;
            string directory = "MyStores";

            //bool init = await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions 
                { 
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 50
                });


                ////Get Root Documents Directory And Add Variable Directory
                //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //folderPath = Path.Combine(folderPath, directory);

                ////If Variable Directory Does Not Exist Create It
                //if (!File.Exists(folderPath))
                //    Directory.CreateDirectory(folderPath);

                ////Create New File Path
                //var newPath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(mediaFile.Path) +
                //    Path.GetExtension(mediaFile.Path));

                ////If New File Path Exists Delete It  
                //if (File.Exists(newPath))
                //    File.Delete(newPath);

                ////Copy File to New File Path
                //File.Copy(mediaFile.Path, newPath);


                if (mediaFile != null)
                {
                    memoryStream = new MemoryStream();
                    mediaFile.GetStream().CopyTo(memoryStream);
                    productImageSource = memoryStream.ToArray();
                    mediaFile.Dispose();
                }
               
            }
            else
            {
                //--------------------------------------
                //
                // TODO: Inject PopupView and display 
                //       message of permission.
                //
                //--------------------------------------
            }

            return productImageSource;
        }

        public static Type CreateType(string discriminator)
        {
            var types = new List<Type>(typeof(User).Assembly.GetExportedTypes());
            Type type = types.Where(t => t.Name == discriminator).FirstOrDefault();

            return type;
        }
    }
}
