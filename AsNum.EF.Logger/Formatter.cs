using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;

namespace AsNum.Xmj.EFLogger {
    public class Formatter : DatabaseLogFormatter {

        public Formatter(DbContext ctx, Action<string> action)
            : base(ctx, action) {

        }

        public override void Executed<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext) {
            this.Write(command);
        }

        public override void Executing<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext) {
            this.Write(command);
        }

        public override void LogCommand<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext) {
            this.Write(command);
        }

        public override void LogParameter<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext, System.Data.Common.DbParameter parameter) {
            this.Write(command);
        }

        public override void LogResult<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext) {
            this.Write(command);
        }

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext) {
            this.Write(command);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext) {
            this.Write(command);
        }

        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext) {
            this.Write(command);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext) {
            this.Write(command);
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext) {
            this.Write(command);
        }

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext) {
            this.Write(command);
        }

        private void Write(DbCommand cmd) {
            this.Write(cmd.CommandText);
        }

    }
}
