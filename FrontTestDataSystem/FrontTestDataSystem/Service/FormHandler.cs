using System.Windows;

namespace FrontTestDataSystem.Service
{
    class FormHandler
    {
        public static void ActionSuccess(string text)
        {
            MessageBox.Show(text, "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ActionError(string text)
        {
            MessageBox.Show(text, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static MessageBoxResult ActionDelete(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result;
        }
    }
}
