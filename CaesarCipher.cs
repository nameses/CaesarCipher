using System.Text;

namespace СaesarCipher
{
    public static class CaesarCipher
    {
        private static readonly string _ukrainianAlphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";
        private static readonly string _englishAlphabet = "abcdefghijklmnopqrstuvwxyz";

        public static string Encrypt(string input, int shift)
        {
            if (!validateShift(shift))
            {
                return string.Empty;
            }
            return processText(input, shift);
        }

        public static string Decrypt(string input, int shift)
        {
            if (!validateShift(shift))
            {
                return string.Empty;
            }
            return processText(input, -shift);
        }

        private static bool validateShift(int shift)
        {
            return shift > 0 && shift != _ukrainianAlphabet.Length && shift != _englishAlphabet.Length;
        }

        private static string processText(string input, int shift)
        {
            StringBuilder result = new StringBuilder(input.Length);

            foreach (char currentChar in input)
            {
                //символи, які не є буквами
                if (!char.IsLetter(currentChar))
                {
                    result.Append(currentChar);
                    continue;
                }

                string alphabet = getAlphabet(currentChar);
                //не знайома абетка
                if (string.IsNullOrEmpty(alphabet))
                {
                    result.Append(currentChar);
                    continue;
                }

                int alphabetSize = alphabet.Length;
                bool isUpper = char.IsUpper(currentChar);
                char lowerCurrentChar = char.ToLower(currentChar);

                int originalPosition = alphabet.IndexOf(lowerCurrentChar);
                // символи не в алфавіті залишаємо без змін
                if (originalPosition == -1)
                {
                    result.Append(currentChar);
                    continue;
                }

                // циклічний зсув для шифрування/розшифрування
                int newPosition = (originalPosition + shift) % alphabetSize;
                if (newPosition < 0) newPosition += alphabetSize;

                char newChar = alphabet[newPosition];

                // записуємо оброблену літеру з урахуванням регістру
                result.Append(isUpper ? char.ToUpper(newChar) : newChar);
            }

            return result.ToString();
        }

        // Метод для визначення алфавіту залежно від символу
        private static string getAlphabet(char c)
        {
            if (_ukrainianAlphabet.Contains(char.ToLower(c))) return _ukrainianAlphabet;
            if (_englishAlphabet.Contains(char.ToLower(c))) return _englishAlphabet;

            return string.Empty;
        }
    }

}
