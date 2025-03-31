using System;

namespace DecoratorPatternExample
{
    public abstract class Message
    {
        public abstract string Content { get; }
    }

    public class TextMessage : Message
    {
        private string content;
        public TextMessage(string content)
        {
            this.content = content;
        }

        public override string Content => content;
    }

    public abstract class MessageDecorator : Message
    {
        protected Message message;

        protected MessageDecorator(Message message)
        {
            this.message = message;
        }
    }

    public class EncryptionDecorator : MessageDecorator
    {
        public EncryptionDecorator(Message message) : base(message) { }

        public override string Content => Encrypt(message.Content);

        private string Encrypt(string content)
        {
            char[] charArray = content.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    public class SigningDecorator : MessageDecorator
    {
        private string signature;

        public SigningDecorator(Message message, string signature) : base(message)
        {
            this.signature = signature;
        }

        public override string Content => $"{message.Content} [Подпись: {signature}]";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите текст сообщения: ");
            string messageContent = Console.ReadLine();
            Message message = new TextMessage(messageContent);

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Добавить шифрование");
                Console.WriteLine("2 - Добавить подпись");
                Console.WriteLine("3 - Показать сообщение");
                Console.WriteLine("4 - Выход");

                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    message = new EncryptionDecorator(message);
                    Console.WriteLine("Шифрование добавлено");
                }
                else if (choice == "2")
                {
                    Console.Write("Введите подпись: ");
                    string signature = Console.ReadLine();
                    message = new SigningDecorator(message, signature);
                    Console.WriteLine("Подпись добавлена");
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Текущее сообщение: " + message.Content);
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный выбор");
                }
            }
        }
    }
}
