using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AsNum.Common.Extends {
    public static class StreamHelper {

        public static byte[] GetBytes(this Stream stm, int perCount = 1024) {
            if(stm == null)
                throw new ArgumentNullException("stm");
            if(perCount <= 0)
                throw new ArgumentOutOfRangeException("perCount", "perCount 必须大于等于0");

            if(stm.CanSeek)
                stm.Position = 0;

            byte[] bytes = new byte[stm.Length];
            var offset = 0;
            var count = 0;
            while(0 != (count = stm.Read(bytes, offset, stm.Length - stm.Position > perCount ? perCount : (int)(stm.Length - stm.Position)))) {
                offset += count;
            }

            return bytes;
        }


        private static int Chunk = 256;

        public static void AsyncGetBytes(this Stream stm, Action<byte[]> readCallBack, Action complete) {
            byte[] buffer = new byte[Chunk];
            stm.BeginRead(buffer, 0, Chunk, new AsyncCallback(ReadAsyncCallback), new { Stream = stm, Buffer = buffer, Action = readCallBack, Complete = complete });
        }

        private static void ReadAsyncCallback(IAsyncResult ar) {
            var obj = ar.AsyncState as dynamic;
            var stream = obj.Stream as Stream;
            var buffer = obj.Buffer as byte[];
            var act = obj.Action as Action<byte[]>;
            var complete = obj.Complete as Action;

            var count = stream.EndRead(ar);
            if(count > 0) {
                act(buffer.Take(count).ToArray());
                stream.BeginRead(buffer, 0, Chunk, ReadAsyncCallback, new { Stream = stream, Buffer = buffer, Action = act, Complete = complete });
            } else {
                complete();
            }
        }

        public static string ToBase64Url(this Stream stm) {
            return Convert.ToBase64String(stm.GetBytes());
        }

        public static Bitmap GetBitmapFromBase64Url(this string str) {
            var bytes = Convert.FromBase64String(str);
            var msm = new MemoryStream(bytes);
            var img = new Bitmap(msm);
            return img;
        }
    }
}
