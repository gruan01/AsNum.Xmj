using System.Collections.Generic;

namespace AsNum.Xmj.IBiz {
    public interface IBaseBiz {

        Dictionary<string, string> Errors { get; set; }

        bool HasError { get; }
    }
}
