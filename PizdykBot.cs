using System;
using System.Collections;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Threading;

namespace Pizdyk
{
    class PizdykBot
    {
        TelegramBotClient bot;
        Hashtable counters = new Hashtable();

        List<string> nicks = new List<string>()
        {
            "Подгнивший Лось",
            "Вкусно пукнул",
            "Гомик из деревни",
            "Белый нигер",
            "Пиздючка",
            "Пиздюк",
            "Злобный бульбулятор",
            "Виртуальная сопля",
            "Бегущий по граблям",
            "Элефант",
            "Утка в тапках",
            "Чилийский перчик",
            "хранительница червя",
            "без трусиков",
            "собака сутулая",
            "потерпевшая дичь",
            "старый пердун",
            "ебака",
            "калач копчёный",
            "щенок",
            "педовка",
            "ебало говяжье",
            "волосатая подмышка",
            "генетический эксперимент",
            "чугунный лоб",
            "расист ебаный",
            "мандавошка",
            "любитель спиздануть всякую херню",
            "властелин шоколадного глаза",
            "слабый задок",
            "подсерало",
            "принцесска",
            "обосранец",
            "мокрощёлка",
            "сказочный долбаёб",
            "дедов хер",
            "извращённый пидарас",
            "почти не гей",
            "лизбуха",
            "героиновый наркоман",
            "любитель пердануть в муку",
            "тупая овца",
            "зловонная туша"
        };

        List<string> intro = new List<string>()
        {
            "ты знаешь, что ты",
            "ебать ты",
            " - ",
            "все в курсе, что ты",
            "мой кот говорит, что ты",
            "в подъезде написано, что ты",
            "мне очень жаль, что ты",
            "мне кажется, логичнее было бы называть тебя",
            "твоё второе имя",
            "да ты же"
        };

        List<string> da = new List<string>()
        {
            "песда",
            "пизда",
            "хуй на"
        };

        List<string> _300 = new List<string>()
        {
            "отсоси у программиста",
            "отсоси у тракториста",
            "Сколько-сколько?"
        };

        List<string> notReduplicatable = new List<string>()
        {
            "🖕",
            "опять несёшь всякую херню? 🔇",
            "что за херня? 👎",
            "да всем насрать! 💩",
            "и что? 🙊"
        };

        public void Start()
        {
            bot = new TelegramBotClient("662652277:AAFFhwVcUNZ-HN9m7Y7TBpVmxkm2LGQ3NwI");
            bot.OnMessage += Bot_OnMessage;

            bot.OnMessageEdited += Bot_OnMessage;
            bot.StartReceiving();

            Console.WriteLine("Started...");

            var input = Console.ReadLine();
            while (input != "q")
            {
                if (input == "s")
                {
                    int i = 1;
                    foreach (DictionaryEntry entry in counters)
                    {
                        Console.WriteLine(i + ") " + entry.Key + " : " + (entry.Value as Counter).name);
                        i++;
                    }
                }
                input = Console.ReadLine();
            }
            bot.StopReceiving();
        }

        void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            {
                var cid = e.Message.Chat.Id;
                if (!counters.Contains(cid))
                    counters.Add(cid, new Counter(e.Message.Chat.Title));
                
                if (e.Message.Text == null)
                    return;

                if ((Reduplicator.getLastWord(e.Message.Text).ToLower() == " нет") || (Reduplicator.getLastWord(e.Message.Text).ToLower() == "нет"))
                    Reply(e.Message.Chat.Id, e.Message.From.FirstName + ", пидора ответ!");
                else if ((Reduplicator.getLastWord(e.Message.Text).ToLower() == " да") || (Reduplicator.getLastWord(e.Message.Text).ToLower() == "да"))
                    Reply(e.Message.Chat.Id, e.Message.From.FirstName +  ", " + da[new Random().Next(0, da.Count)] + "!");
                else if ((Reduplicator.getLastWord(e.Message.Text).ToLower() == " 300") || (Reduplicator.getLastWord(e.Message.Text).ToLower() == "300"))
                    Reply(e.Message.Chat.Id, e.Message.From.FirstName + ", " + _300[new Random().Next(0, _300.Count)] + "!");

                else if (((Counter)counters[cid]).reduplicationIsNeeded())
                {
                    var reduplicated = Reduplicator.Reduplicate(e.Message.Text);

                    if (string.IsNullOrEmpty(reduplicated))
                        reduplicated =  e.Message.From.FirstName + ", " + notReduplicatable[new Random().Next(0, notReduplicatable.Count)];
                    else
                        reduplicated = e.Message.From.FirstName + ", " + reduplicated;
                    Reply(e.Message.Chat.Id, reduplicated);
                }

                if (((Counter)counters[cid]).NickIsNeeded())
                {
                    var startTimeSpan = TimeSpan.FromSeconds(10);
                    var periodTimeSpan = TimeSpan.Zero;
                    new Timer((o) =>
                    {
                        Reply(e.Message.Chat.Id, e.Message.From.FirstName + ", " + intro[new Random().Next(0, intro.Count)] + " " + nicks[new Random().Next(0, nicks.Count)]);
                    }, null, startTimeSpan, periodTimeSpan);
                }
            }
        }

        void Reply(long chadId, string msg)
        {
            Console.WriteLine(chadId + " : " + getChannelNameById(chadId) + " | " + msg);
            bot.SendTextMessageAsync(chadId, msg);
        }

        string getChannelNameById(long id)
        {
            return (counters[id] as Counter).name;
        }
    }
}
