using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyOnlineStore.HtmlFile
{
    public class Html
    {
        string userCode;

        public Html(string code)
        {
            this.userCode= code;
        }

      
    

        public string ReadFileInPackage(string name)
        {
            string fileContent = string.Empty;

            var assembly = typeof(LocalFilesHelper).GetTypeInfo().Assembly;

            var resourceName = assembly.GetManifestResourceNames().Where(r => r.ToLowerInvariant().EndsWith(name.ToLowerInvariant(), StringComparison.Ordinal)).FirstOrDefault();

            if (resourceName != default(string))
            {
                using (Stream fileStream = assembly.GetManifestResourceStream(resourceName))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    using (var fileReader = new StreamReader(fileStream))
                    {
                        fileContent = fileReader.ReadToEnd();
                    }
                }
            }

            return fileContent;
        }
    }
}
