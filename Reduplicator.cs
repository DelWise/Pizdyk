using System.Text.RegularExpressions;

namespace Pizdyk
{
    class Reduplicator
    {
        public static string Reduplicate(string sWord)
        {
            // Все русские буквы
            string allRu = "а-яё";
            // Все русские гласные
            string vowelsRu = "аеёыиоуэюя";
            
            
            var txtInp = getLastWord(sWord);

            MatchCollection matchesSlogi;
            MatchCollection matchesRussian;
            Regex regexSlova = new Regex("[" + allRu + "]+", RegexOptions.Compiled);
            matchesRussian = regexSlova.Matches(txtInp);
            if (matchesRussian.Count < 1)
                return "";
            Regex regexSlogi = new Regex("[^" + vowelsRu + "]*([" + vowelsRu + "])");
            matchesSlogi = regexSlogi.Matches(txtInp);

            string replaceLetter = "";
            string result = txtInp;
            if (matchesSlogi.Count > 0)
            {
                string firstSlog = matchesSlogi[0].Value;
                string vowel = firstSlog[firstSlog.Length - 1].ToString();

                switch (vowel.ToLower())
                {
                    case "а":
                        replaceLetter = "я";
                        break;
                    case "о":
                        replaceLetter = "ё";
                        break;
                    case "у":
                        replaceLetter = "ю";
                        break;
                    case "э":
                        replaceLetter = "е";
                        break;
                    case "ы":
                        replaceLetter = "и";
                        break;
                    default:
                        replaceLetter = vowel.ToLower();
                        break;
                }

                firstSlog = "ху" + replaceLetter;

                result = firstSlog;
                result = result + txtInp.Substring(matchesSlogi[0].Value.Length, txtInp.Length - matchesSlogi[0].Value.Length);
            }

            return result;
        }

        public static string getLastWord(string sWord)
        {
            string txtInp;
            int startIndexOfLastWord = sWord.LastIndexOf(" ");
            if (startIndexOfLastWord > 0)
                txtInp = sWord.Substring(startIndexOfLastWord).ToLower();
            else
                txtInp = sWord.ToLower();
            return txtInp;
        }
    }
}
