using PR5.Models;
using PR5.Services;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PR5.Services
{
    public partial class Registration : Page
    {
        private readonly Validate _validator = new Validate();

        public Registration()
        {
            InitializeComponent();
        }

        private void tbBirthday_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbBirthday.Text == "дд.мм.гггг")
            {
                tbBirthday.Text = "";
                tbBirthday.Foreground = SystemColors.WindowTextBrush;
            }
        }

        private void tbBirthday_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbBirthday.Text))
            {
                tbBirthday.Text = "дд.мм.гггг";
                tbBirthday.Foreground = Brushes.Gray;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime birthday;
                if (!DateTime.TryParseExact(tbBirthday.Text, "dd.MM.yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out birthday))
                {
                    MessageBox.Show("Введите дату в формате дд.мм.гггг");
                    return;
                }
                var selectedPost = (ComboBoxItem)cbPost.SelectedItem;
                int postId = int.Parse(selectedPost.Tag.ToString());
                var newUser = new User
                {
                    Surname = tbSurname.Text,
                    Name = tbName.Text,
                    Patronymic = tbPatronymic.Text,
                    Birthday = birthday,
                    Login = tbLogin.Text,
                    Password = pbPassword.Password,
                    Phone = tbPhone.Text,
                    Mail = tbMail.Text,
                    ID_Post = postId
                };
                string validationError = _validator.ValidateUser(newUser);
                if (!string.IsNullOrEmpty(validationError))
                {
                    MessageBox.Show(validationError);
                    return;
                }

                using (var db = new SchoolEntities())
                {
                    if (db.User.Any(u => u.Login == newUser.Login))
                    {
                        MessageBox.Show("Этот логин уже занят!");
                        return;
                    }

                    db.User.Add(newUser);
                    db.SaveChanges();

                    MessageBox.Show($"Регистрация успешна! ID: {newUser.ID_User}");
                    NavigationService?.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}\nПодробности: {ex.InnerException?.Message}");
            }
        }
    }
}