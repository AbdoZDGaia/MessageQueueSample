using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueCSharp
{
    public struct Payment
    {
        public string Payer, Payee;
        public decimal Amount;
        public string DueDate;
    }
}
