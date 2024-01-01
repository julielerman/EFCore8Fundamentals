using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace PublisherData
{
    internal class MyCustomDbCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            if (command.CommandText.ToUpper().Contains("AUTHORS"))
                //DBA  recommended ROBUST PLAN query hint for any access to AUTHORS table 
                command.CommandText += " OPTION (ROBUST PLAN)";

            return result; 
        }

        //TODO: Duplicate logic for ReaderExecutingAsync method

    }
} 
