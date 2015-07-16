using AsNum.Common.Extends;
using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AsNum.Xmj.API.Methods {
    public class PhotoBankUploadImage : MethodBase<PhotoBankUploadResult> {
        protected override string APIName {
            get { return "api.uploadImage"; }
        }

        private string filePath = "";
        public string FilePath {
            get {
                return this.filePath;
            }
            set {
                this.filePath = value;
                if (File.Exists(value)) {
                    //多线程同时读的一个文件，会报 IOException
                    //this.UploadData = File.ReadAllBytes(value);
                    this.UploadData = FileHelper.ReadAllBytes(value);
                    this.FileName = Path.GetFileName(value);
                }
            }
        }

        [Param("fileName", Required = true)]
        public string FileName { get; set; }

        public byte[] UploadData { get; set; }

        [Param("groupId")]
        public string GroupID { get; set; }

        [NeedAuth]
        public override string GetResult(Auth auth) {
            var url = auth.GetApiUrl(this.APIName, new Dictionary<string, string>() { 
                {"groupId",this.GroupID},
                {"fileName", this.FileName }
            });

            using (var client = new WebClient()) {
                try {
                    var result = client.UploadData(url, this.UploadData);
                    return Encoding.UTF8.GetString(result);
                } catch (WebException ex) {
                    return Encoding.UTF8.GetString(ex.Response.GetResponseStream().GetBytes());
                }
            }
        }
    }
}
