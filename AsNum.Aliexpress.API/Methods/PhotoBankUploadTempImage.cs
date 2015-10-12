using AsNum.Common.Extends;
using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {

    public class PhotoBankUploadTempImage : MethodBase<PhotoBankUploadTempImageResult> {
        protected override string APIName {
            get { return "api.uploadTempImage"; }
        }

        private string filePath = "";
        public string FilePath {
            get {
                return this.filePath;
            }
            set {
                this.filePath = value;
                if (File.Exists(value)) {
                    //两线程同时读同一文件,会报 IOExcepton 的错
                    //this.UploadData = File.ReadAllBytes(value);
                    this.UploadData = value.ReadAllBytes();
                    this.FileName = Path.GetFileName(value);
                }
            }
        }

        [Param("srcFileName", Required = true)]
        public string FileName { get; set; }

        public byte[] UploadData { get; set; }

        [NeedAuth]
        public async override Task<string> GetResult(Auth auth) {
            var url = auth.GetApiUrl(this.APIName, new Dictionary<string, string>() { 
                {"srcFileName",this.FileName}
            });

            using (var client = new WebClient()) {
                try {
                    //var result = client.UploadData(url, this.UploadData);
                    var result = await client.UploadDataTaskAsync(url, this.UploadData);
                    return Encoding.UTF8.GetString(result);
                } catch (WebException ex) {
                    return Encoding.UTF8.GetString(ex.Response.GetResponseStream().GetBytes());
                }
            }
        }
    }
}
