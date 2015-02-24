using AsNum.Common.Extends;
using System;
using System.IO;

namespace AsNum.Common.Net {
    public class PostFileItem {

        public string Name { get; set; }

        public string FileName { get; set; }

        public byte[] FileBytes { get; private set; }

        public string ContentType { get; private set; }

        public PostFileItem(string name, string file) {
            if(!File.Exists(file))
                throw new FileNotFoundException(file);

            this.Name = name;
            //this.FileName = Path.GetFileName(file);
            this.FileName = file;
            this.FileBytes = File.ReadAllBytes(file);
            this.ContentType = file.GetMIMEType();
        }

        public PostFileItem(string name, string fileName, byte[] fileBytes, string contentType) {
            if(fileBytes == null || fileBytes.Length == 0)
                throw new ArgumentException("contents");

            this.Name = name;
            this.FileName = fileName;
            this.FileBytes = fileBytes;
            this.ContentType = contentType;
        }

    }
}
