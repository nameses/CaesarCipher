using System.Text;

namespace СaesarCipher
{
    public class CaesarCipher
    {
        private const string _ukrainianAlphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";
        private const string _englishAlphabet = "abcdefghijklmnopqrstuvwxyz";

        public string Encrypt(string input, int shift)
        {
            return ProcessText(input, shift);
        }

        public string Decrypt(string input, int shift)
        {
            return ProcessText(input, -shift);
        }

        private string ProcessText(string input, int shift)
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

                string alphabet = GetAlphabet(currentChar);
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
                if (originalPosition != -1)
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
        private string GetAlphabet(char c)
        {
            if (_ukrainianAlphabet.Contains(char.ToLower(c))) return _ukrainianAlphabet;
            if (_englishAlphabet.Contains(char.ToLower(c))) return _englishAlphabet;

            return string.Empty;
        }
    }

}
