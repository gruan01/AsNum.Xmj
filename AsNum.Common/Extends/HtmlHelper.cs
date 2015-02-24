using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.Common.Extends {
    public static class HtmlHelper {

        //public static Dictionary<string, string> CollectFormItems(this string ctx, string formNameID) {

        //    var subCtxReg = string.Format(@"<form[\s\S]*?(name|id)=['""]{0}[\s\S]*?>(?<subCtx>[\s\S]*?)</form>", formNameID);
        //    if(Regex.IsMatch(ctx, subCtxReg)) {
        //        var subCtx = Regex.Match(ctx, subCtxReg).Groups["subCtx"].Value;

        //        Regex rx = new Regex(@"<input type=(['""""]?)hidden\1 name=([""""']?)(?<name>[^\2]*?)\2\s\S*?value=(['""]?)(?<value>[^\3]*?)\3");
        //        var mas = rx.Matches(subCtx);

        //        var dic = new Dictionary<string, string>();
        //        foreach(Match ma in mas) {
        //            dic.Set(ma.Groups["name"].Value, ma.Groups["value"].Value);
        //        }
        //        //var kvs = rx.Matches(subCtx).Cast<Match>().Select(ma => new { K = ma.Groups["name"].Value, V = ma.Groups["value"].Value });
        //        return dic;
        //    }
        //    return null;
        //}

        public static Dictionary<string, string> CollectFormItems(this string ctx, string formNameID) {
            var doc = new HtmlDocument();
            doc.LoadHtml(ctx);
            var nodes = doc.DocumentNode.QuerySelectorAll("input[type='hidden']").ToList();
            var dic = new Dictionary<string, string>();
            nodes.ForEach(n => {
                dic.Set(n.Attributes["name"].Value, n.Attributes["value"].Value);
            });
            return dic;
        }
    }
}
