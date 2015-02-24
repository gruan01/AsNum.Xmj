using AsNum.Xmj.Common;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace AsNum.Xmj.EFLogger {
    public class CommandInterceptor : IDbCommandInterceptor {

        [Import]
        private LogObserverable LogObserverable {
            get;
            set;
        }

        public CommandInterceptor() {
            //MefHelper.ComposeParts(this);
            GlobalData.MefContainer.ComposeParts(this);
        }

        public void NonQueryExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext) {
            this.Log(command);
        }

        public void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext) {
            this.Log(command);
        }

        public void ReaderExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext) {
            this.Log(command);
        }

        public void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext) {
            this.Log(command);
        }

        public void ScalarExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext) {
            this.Log(command);
        }

        public void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext) {
            this.Log(command);

        }

        private void Log(DbCommand cmd) {
            if (EFLogger.IsEnable && this.LogObserverable != null) {
                this.LogObserverable.Notify(cmd);
            }
        }
    }
}
