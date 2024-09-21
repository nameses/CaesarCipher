using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace СaesarCipher
{
    public sealed partial class MainWindow : Window
    {
        private string OriginalTextValue { get; set; } = string.Empty;
        private string ProcessedTextValue { get; set; } = string.Empty;

        public MainWindow()
        {
            this.InitializeComponent();

            OriginalTextBox.DataContext = OriginalTextValue;
            OriginalTextBox.SetBinding(TextBox.TextProperty, new Binding { Source = OriginalTextValue });

            ProcessedTextBox.DataContext = ProcessedTextValue;
            ProcessedTextBox.SetBinding(TextBox.TextProperty, new Binding { Source = ProcessedTextValue });
        }

        private void ClearOriginalText_Click(object sender, RoutedEventArgs e)
        {
            OriginalTextBox.ClearValue(TextBox.TextProperty);
        }

        private void ClearProcessedText_Click(object sender, RoutedEventArgs e)
        {
            ProcessedTextBox.ClearValue(TextBox.TextProperty);
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var stringFileContent = await readFileAsync();

            if (string.IsNullOrEmpty(stringFileContent))
            {
                return;
            }

            OriginalTextBox.SetValue(TextBox.TextProperty, stringFileContent);
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            var originalText = OriginalTextBox.GetValue(TextBox.TextProperty).ToString();
            var processedText = CaesarCipher.Encrypt(originalText, 15);
            ProcessedTextBox.SetValue(TextBox.TextProperty, processedText);
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            var originalText = OriginalTextBox.GetValue(TextBox.TextProperty).ToString();
            var processedText = CaesarCipher.Decrypt(originalText, 15);
            ProcessedTextBox.SetValue(TextBox.TextProperty, processedText);
        }

        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            await saveFileAsync(ProcessedTextBox.GetValue(TextBox.TextProperty).ToString());
        }

        private async Task saveFileAsync(string fileContent)
        {
            // Створення діалогу для вибору файлу
            var savePicker = new FileSavePicker()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                FileTypeChoices = { { "Текстовий файл", new List<string>() { ".txt" } } },
                SuggestedFileName = "Нове_ім'я"
            };

            var handledWindow = WindowNative.GetWindowHandle(App.Window);
            InitializeWithWindow.Initialize(savePicker, handledWindow);

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Збереження вмісту файлу
                await FileIO.WriteTextAsync(file, fileContent);
            }
        }

        /// <summary>
        /// Read file from FilePicker and return its content in string
        /// </summary>
        private async Task<string> readFileAsync()
        {
            var openPicker = new FileOpenPicker()
            {
                SuggestedStartLocation = PickerLocationId.Downloads,
                FileTypeFilter = { ".txt" }
            };

            var handledWindow = WindowNative.GetWindowHandle(App.Window);
            InitializeWithWindow.Initialize(openPicker, handledWindow);

            var file = await openPicker.PickSingleFileAsync();

            if (file == null)
            {
                return string.Empty;
            }

            return await FileIO.ReadTextAsync(file);
        }
    }
}
