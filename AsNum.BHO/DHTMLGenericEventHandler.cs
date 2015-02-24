using System;
using System.Runtime.InteropServices;
using mshtml;

namespace AsNum.BHO {

    public delegate void DHTMLEvent(IHTMLEventObj e);

    /// <summary>
    /// http://social.msdn.microsoft.com/forums/en-US/ieextensiondevelopment/thread/e69909f0-d883-462d-be1d-792f016ef77e/
    /// </summary>
    [ComVisible(true)]
    public class DHTMLEventHandler {
        public DHTMLEvent NewEventHandlers;
        HTMLDocument Document;
        object OriginalEventHandlers;

        public DHTMLEventHandler(HTMLDocument doc , object existingHandlers) {
            this.Document = doc;
            OriginalEventHandlers = existingHandlers;
        }

        [DispId(0)]
        public void Call() {

            //Execute our event handlers first.
            NewEventHandlers(Document.parentWindow.@event);
            // Then, if any existing event handlers are present, execute them.
            if(OriginalEventHandlers == null || OriginalEventHandlers.GetType() == typeof(DBNull))
                return;

            OriginalEventHandlers.GetType().InvokeMember("[DispID=0]" , System.Reflection.BindingFlags.InvokeMethod , null , OriginalEventHandlers , null);
        }
    }
}
