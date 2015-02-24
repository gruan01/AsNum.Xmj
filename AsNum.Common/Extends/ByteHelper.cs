using System.Text;

namespace AsNum.Common.Extends {
    public static class ByteHelper {

        public static string Bin2Hex(this byte[] binary) {
            StringBuilder builder = new StringBuilder();
            foreach(byte num in binary) {
                if(num > 15) {
                    builder.AppendFormat("{0:X}", num);
                } else {
                    builder.AppendFormat("0{0:X}", num);/////// 大于 15 就多加个 0
                }
            }
            return builder.ToString();
        }

    }
}
