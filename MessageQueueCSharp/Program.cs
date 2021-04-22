using System;
using System.Messaging;
using System.Text;

namespace MessageQueueCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var msg = CreateMessage();
            var msgQ = SendMessageOverMQ(msg);
            Console.WriteLine($"\nMessage was sent to MQ");
            StringBuilder sb = ReceiveMessageFromMQ();
            Console.WriteLine(sb.ToString(), "\nMessage Received from MQ!");
            Console.ReadLine();
        }

        private static StringBuilder ReceiveMessageFromMQ()
        {
            MessageQueue msgQ = new MessageQueue(".\\Private$\\billpay");
            Payment myPayment = new Payment();
            var typesArray = new Type[2];
            typesArray[0] = myPayment.GetType();
            typesArray[1] = new Object().GetType();
            msgQ.Formatter = new XmlMessageFormatter(typesArray);
            myPayment = ((Payment)msgQ.Receive().Body);
            StringBuilder sb = new StringBuilder();
            sb.Append("Payment was paid to: " + myPayment.Payee);
            sb.Append("\n");
            sb.Append("Paid by: " + myPayment.Payer);
            sb.Append("\n");
            sb.Append("Amount: $" + myPayment.Amount.ToString());
            sb.Append("\n");
            sb.Append("Due Date: " + myPayment.DueDate);
            return sb;
        }

        private static MessageQueue SendMessageOverMQ(Message message)
        {
            MessageQueue msgQ = new MessageQueue(".\\private$\\billpay");
            msgQ.Send(message);
            return msgQ;
        }

        private static Message CreateMessage()
        {
            var paidBill = DoPayment();
            Message message = new Message();
            message.Body = paidBill;
            return message;
        }

        private static Payment DoPayment()
        {
            Payment payment;
            payment.Payee = "AbdoZ";
            payment.Payer = "Client";
            payment.Amount = new Random().Next(2000, 10000);
            payment.DueDate = "3rd Nov 2021";
            return payment;
        }
    }
}
