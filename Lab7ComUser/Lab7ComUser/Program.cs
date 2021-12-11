using System;
using System.Net.Sockets;
using System.Text;
namespace Lab7ComUser
{
    class Program
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.Write("Введiть iм'я:");
            string userName = Console.ReadLine();
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    Console.Write(userName + ": ");
                    // Введення повідомлення
                    string message = Console.ReadLine();
                    message = String.Format("{0}: {1}", userName, message);
                    // Перетворення повідомлення в масив
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    // Відправка повідомлення
                    stream.Write(data, 0, data.Length);

                    // Отримання відповіді
                    data = new byte[64]; // буфер для отриманих даних
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();
                    Console.WriteLine("Сервер: {0}", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
