using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using mshtml;
using SHDocVw;

namespace AsNum.BHO {
    [ComVisible(true) ,
    Guid("33A56655-5179-02FF-042E-7BF4884E0DD1") ,
    ClassInterface(ClassInterfaceType.None),
    ]
    public class DragDropEnhancement : IObjectWithSite {

        private static readonly string regKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Browser Helper Objects";

        private WebBrowser Browser;
        private HTMLDocument Document;

        private Point MousePoint = new Point();

        public int SetSite(object site) {

            if(site != null) {
                this.Browser = (WebBrowser)site;
                //this.Browser.DocumentComplete += Browser_DocumentComplete;
                //this.Browser.NavigateComplete2 += Browser_NavigateComplete2;
                this.Browser.DownloadComplete += Browser_DownloadComplete;
            } else {
                //this.Browser.DocumentComplete -= Browser_DocumentComplete;
                //this.Browser.NavigateComplete2 -= Browser_NavigateComplete2;
                this.Browser.DownloadComplete -= Browser_DownloadComplete;
                this.Document = null;
                this.Browser = null;
            }

            return 0;
        }

        private void Browser_DownloadComplete() {
            //Debugger.Launch();
            this.Document = this.Browser.Document;
            if(this.Document.documentElement == null)
                return;
            var docEvent = (HTMLElementEvents_Event)this.Document.documentElement;
              //docEvent.ondrag += docEvent_ondrag;
            docEvent.onmousedown += docEvent_onmousedown;
        }

        void docEvent_onmousedown() {
            var e = this.Document.parentWindow.@event as IHTMLEventObj2;
            this.MousePoint.X = e.clientX;
            this.MousePoint.Y = e.clientY;
        }

        public int GetSite(ref Guid guid , out IntPtr site) {
            IntPtr punk = Marshal.GetIUnknownForObject(this.Browser);
            int hr = Marshal.QueryInterface(punk , ref guid , out site);
            Marshal.Release(punk);

            return hr;
        }

        [ComRegisterFunction]
        public static void RegistBHO(Type type) {
            Console.WriteLine(type.Name);
            RegistryKey key = Registry.LocalMachine.OpenSubKey(regKey , true);
            if(key == null)
                key = Registry.LocalMachine.CreateSubKey(regKey);

            var guid = type.GUID.ToString("B");
            Console.Write(guid);
            RegistryKey bhoKey = key.OpenSubKey(guid , true);
            if(bhoKey == null)
                bhoKey = key.CreateSubKey(guid);

            bhoKey.SetValue("Alright" , "1");
            bhoKey.Close();
            key.Close();
        }

        [ComUnregisterFunction]
        public static void UnregisterBHO(Type t) {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Browser Helper Objects" , true);
            rk.DeleteSubKey("{" + t.GUID.ToString() + "}");

        }
    }
}