using System.Linq;
using ZXing;

namespace BarCodeScanner {
    public class PhotoCameraLuminanceSource : BaseLuminanceSource {
        public byte[] PreviewBufferY {
            get {
                return luminances;
            }
            set {
                this.luminances = value;
            }
        }

        public PhotoCameraLuminanceSource(int width, int height)
            : base(width, height) {
            luminances = new byte[width * height];
        }

        internal PhotoCameraLuminanceSource(int width, int height, byte[] newLuminances)
            : base(width, height) {
            luminances = newLuminances;
        }

        protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height) {
            return new PhotoCameraLuminanceSource(width, height, newLuminances);
        }

        public override LuminanceSource crop(int left, int top, int width, int height) {
            return base.crop(left, top, width, height);
        }

        public void Cutout(int x, int y, int w, int h) {

            var routedRaw = new byte[this.luminances.Length];
            //1,	2,	3,	4
            //5,	6,	7,	8
            //9,	10,	11,	12
            //13,	14,	15,	16
            //17	18	19	20
            //21	22	23	24

            //rh = 6 , rw = 4
            //21,17,13,9,5,1  22,18,14,10,6,2  23,19,15,11,7,3  24,20,16,12,8,4

            //RW*(RH-0) - RW + 1 	4*(6-0)-4+1 = 21
            //RW*(RH-1) - RW + 1	4*(6-1)-4+1=17
            //RW*(RH-2) - RW + 1	4*(6-2)-4+1=13
            //RW*(RH-3) - RW + 1	4*(6-3)-4+1=9
            //RW*(RH-4) - RW + 1	4*(6-4)-4+1=5
            //RW*(RH-5) - RW + 1	4*(6-5)-4+1=1

            for (var i = 0; i < routedRaw.Length; i++) {
                //var add = i / rh + 1;
                //var idx = rw * (rh - i % rh) - rw + add;//下标从1开始
                routedRaw[i] = this.luminances[this.Width * (this.Height - i % this.Height) - this.Width + i / this.Height];
            }

            var t = routedRaw.Skip(y * this.Width).Take(h * this.Width).ToArray();

            var datas = routedRaw
                .Skip(y * this.Height) //y是目标所在的起始列
                .Take(h * this.Height)
                .Where((r, i) => {
                    var row = i % this.Height;
                    return x <= row && (x + w) > row;
                }).ToArray();

            this.luminances = datas;
            this.Height = h;
            this.Width = w;
        }
    }
}
