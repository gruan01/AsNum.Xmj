using System.Data.Entity;

namespace AsNum.Xmj.EFLogger {
    public class Configuration : DbConfiguration {

        public Configuration() {
            this.SetDatabaseLogFormatter((db, act) => new Formatter(db, act));
            this.AddInterceptor(new CommandInterceptor());
        }

    }
}
