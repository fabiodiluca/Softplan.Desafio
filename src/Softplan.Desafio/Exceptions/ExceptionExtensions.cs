using System;
using System.Collections.Generic;
using System.Text;

namespace Softplan.Desafio.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string ToStringExtensive(this Exception exp)
        {
            try
            {
                string ret = "";
                ret = exp.Message.ToString() + "\r\n" + exp.StackTrace;
                if (exp.InnerException != null)
                    ret += "\r\n Inner Exception:" + exp.InnerException.Message + "\r\n" + exp.InnerException.StackTrace;
                return ret;
            }
            catch { return ""; }
        }
    }
}
