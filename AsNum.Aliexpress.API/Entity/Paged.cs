using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    public class Paged<T> where T : class {

        public int Total {
            get;
            set;
        }

        public List<T> Results {
            get;
            set;
        }

        public int CurrPage {
            get;
            set;
        }
    }
}
