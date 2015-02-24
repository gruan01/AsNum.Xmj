using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using AsNum.Aliexpress.API.Entity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AsNum.Aliexpress.API.Handlers {
    internal class LoadAuthDataHandler : ICallHandler {

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext) {
            var user = (string)input.Inputs["user"];
            if(user != null) {
                var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AsNum.Aliexpress", user);
                if(File.Exists(file)) {
                    var bf = new BinaryFormatter();
                    using(var fs = new FileStream(file, FileMode.Open)) {
                        var token = (Token)bf.Deserialize(fs);
                    }
                }
            }
            return getNext()(input, getNext);
        }

        public int Order {
            get;
            set;
        }
    }
}
