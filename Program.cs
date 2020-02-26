using Telegram.Bot;

namespace Pizdyk
{
    class Program
    {
        static TelegramBotClient client;
        static void Main(string[] args)
        {
            PizdykBot bot= new PizdykBot();
            bot.Start();
        }
    }
}
