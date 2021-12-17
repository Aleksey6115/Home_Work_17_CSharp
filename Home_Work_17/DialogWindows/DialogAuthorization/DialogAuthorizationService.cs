using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Home_Work_17.DialogWindows.DialogAuthorization
{
    /// <summary>
    /// Логика окна авторизации
    /// </summary>
    public class DialogAuthorizationService
    {
        string password = "1234"; // Пароль для входа

        /// <summary>
        /// Открыть окно ввода пароля
        /// </summary>
        /// <returns></returns>
        public bool OpenAuthorizationDialog()
        {
            DialogAuthorizationWindow DAW = new DialogAuthorizationWindow();

            if (DAW.ShowDialog() == true)
            {
                if (DAW.passwordBox.Text == password)
                {
                    MessageBox.Show("Пароль введён верно!", "Статус авторизации", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }

                else
                {
                    MessageBox.Show("Пароль введён не верно!", "Статус авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return false;
        }
    }
}
