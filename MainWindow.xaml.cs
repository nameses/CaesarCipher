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
        private string EncryptedTextValue { get; set; } = string.Empty;

        public MainWindow()
        {
            this.InitializeComponent();

            OriginalTextBox.DataContext = OriginalTextValue;
            OriginalTextBox.SetBinding(TextBox.TextProperty, new Binding { Source = OriginalTextValue });

            EncryptedTextBox.DataContext = EncryptedTextValue;
            EncryptedTextBox.SetBinding(TextBox.TextProperty, new Binding { Source = EncryptedTextValue });
        }

        private void ClearOriginalText_Click(object sender, RoutedEventArgs e)
        {
            OriginalTextBox.ClearValue(TextBox.TextProperty);
        }

        private void ClearEncryptedText_Click(object sender, RoutedEventArgs e)
        {
            EncryptedTextBox.ClearValue(TextBox.TextProperty);
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

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
