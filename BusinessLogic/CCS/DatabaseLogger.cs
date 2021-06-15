using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.CCS
{
    public class DatabaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Veritabanına loglandı");
        }
    }
}
