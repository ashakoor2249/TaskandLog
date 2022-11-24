using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskandLog.SharedComponents
{
    public class Methods
    {
        public string Prefix {  get; set; }
        public string SetPrefix(string LogType)
        {
            if (LogType.Equals("Incident"))
            {
                Prefix = "I-";
            }
            else if (LogType.Equals("ESR"))
            {
                Prefix = "E-";
            }

            else if (LogType.Equals("Work Order"))
            {
                Prefix = "WO-";
            }

            else if (LogType.Equals("MAF"))
            {
                Prefix = "M-";
            }

            else
            {
                Prefix = "NA";
            }
            return Prefix;
        }
    }

  
}
