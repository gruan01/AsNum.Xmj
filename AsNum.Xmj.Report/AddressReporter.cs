using AsNum.Xmj.Entity;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ZXing;

namespace AsNum.Xmj.Report {
    public class AddressReporter {
        public void ExportDeliveryInfo(List<Order> receivers, string savePath) {
            using (var output = new FileStream(savePath, FileMode.Create)) {
                var dic = new Dictionary<string, List<Order>>();
                dic.Add("发货地址", receivers);
                this.ExportDeliveryInfo(dic, output);
            }
        }

        public void ExportDeliveryInfo(Dictionary<string, List<Order>> receiversByAccounts, string savePath) {
            using (var output = new FileStream(savePath, FileMode.Create)) {
                this.ExportDeliveryInfo(receiversByAccounts, output);
            }
        }

        private void ExportOrderList(List<string> orders, ISheet sheet) {
            for (var i = 0; i < orders.Count; i++) {
                var row = sheet.GetRow(i);
                if (row == null)
                    row = sheet.CreateRow(i);
                var cell = row.GetCell(0, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                cell.SetCellValue(orders[i]);
            }
        }

        private void ExportDeliveryInfo(IDictionary<string, List<Order>> receiversByAccounts, Stream stm) {

            HSSFWorkbook book = new HSSFWorkbook();
            var topBorderStyle = book.CreateCellStyle();
            topBorderStyle.BorderTop = BorderStyle.DASH_DOT;

            var noteBorderStyle = book.CreateCellStyle();
            noteBorderStyle.BorderTop = BorderStyle.DASH_DOT;
            noteBorderStyle.TopBorderColor = HSSFColor.GREY_25_PERCENT.index;
            noteBorderStyle.BorderBottom = BorderStyle.DASH_DOT;


            foreach (var ra in receiversByAccounts) {
                var sheet = book.CreateSheet(ra.Key);
                sheet.SetColumnWidth(0, 10 * 256);
                sheet.SetColumnWidth(1, 100 * 256);

                var row = 0;
                foreach (var o in ra.Value) {
                    var r = o.Receiver;
                    {
                        var bytes = this.GetBarcodeBytes(r.OrderNO);
                        int pictureIdx = book.AddPicture(bytes, NPOI.SS.UserModel.PictureType.PNG);

                        var patriarch = sheet.CreateDrawingPatriarch();
                        HSSFClientAnchor anchor = new HSSFClientAnchor(250, 0, 500, 100, 1, row + 3, 1, row + 5);
                        var pict = patriarch.CreatePicture(anchor, pictureIdx);
                        //pict.Resize();
                    }

                    var tmpNo = string.Format("{0} {1}      {2} {3}", o.AccountOf.QuickCode, Regex.Replace(r.OrderNO, @"(.{4})", "$1 "), r.Country.ZhName, r.CountryCode);

                    this.WriteField(sheet, ref row, "NO.", tmpNo, topBorderStyle);
                    this.WriteField(sheet, ref row, "Name", r.Name);
                    //var addr = string.Join(", ", (new string[] { r.Address, r.City, r.Province, r.Country }).Where(s => !string.IsNullOrEmpty(s)));
                    //var addr = string.Join(" ", r.FullAddress, r.Country.ZhName);
                    this.WriteField(sheet, ref row, "Address", r.FullAddress);
                    this.WriteField(sheet, ref row, "Postcode", r.ZipCode);
                    this.WriteField(sheet, ref row, "Phone", r.Mobi);
                    this.WriteField(sheet, ref row, "Tel", r.Phone);

                    this.WriteField(sheet, ref row, "Note", string.Format("[{0}]  {1}", r.OrderNO, o.Note != null ? o.Note.Note : ""), noteBorderStyle);

                    row += 2;
                }
            }

            this.ExportOrderList(
                receiversByAccounts.Values
                .SelectMany(o => o)
                .Select(o => o.OrderNO).ToList(), book.CreateSheet("本次导出的订单号码列表"));

            book.Write(stm);
        }

        private void WriteField(ISheet sheet, ref int rowIdx, string title, string ctx, ICellStyle style = null) {
            var row = sheet.GetRow(rowIdx);
            if (row == null)
                row = sheet.CreateRow(rowIdx);

            int cell1 = 0, cell2 = 1;

            var cell = row.GetCell(cell1, MissingCellPolicy.CREATE_NULL_AS_BLANK);
            cell.SetCellValue(title);
            if (style != null)
                cell.CellStyle = style;

            cell = row.GetCell(cell2, MissingCellPolicy.CREATE_NULL_AS_BLANK);
            cell.SetCellValue(ctx);
            if (style != null)
                cell.CellStyle = style;

            rowIdx++;
        }

        private Bitmap GetBarcode(string orderNo) {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.CODE_128;
            writer.Options.Width = 120;
            writer.Options.Height = 50;
            return writer.Write(orderNo);
        }

        private byte[] GetBarcodeBytes(string orderNo) {
            using (var img = this.GetBarcode(orderNo)) {
                var ms = new MemoryStream();
                img.Save(ms, ImageFormat.Png);
                return ms.GetBuffer();
            }
        }
    }
}
