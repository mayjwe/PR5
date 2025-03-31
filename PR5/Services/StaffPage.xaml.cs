using PR5.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PR5.Services
{
    public partial class StaffPage : Page
    {
        public StaffPage(User user)
        {
            InitializeComponent();
            LoadStaffListWithPosts();
        }

        private void LoadStaffListWithPosts()
        {
            try
            {
                using (var db = new SchoolEntities())
                {
                    // Получаем все должности
                    var postsDict = db.Post.ToDictionary(p => p.ID_Post, p => p.Post1);

                    // Создаем список с объединенными данными
                    var staffList = db.User.ToList().Select(user => new
                    {
                        User = new
                        {
                            user.ID_User,
                            user.Surname,
                            user.Name,
                            user.Patronymic,
                            user.Birthday,
                            user.Phone,
                            user.Mail,
                            FullName = $"{user.Surname} {user.Name} {user.Patronymic}"
                        },
                        PostTitle = postsDict.TryGetValue(user.ID_Post, out var postName)
                                  ? postName
                                  : "Должность не указана"
                    }).ToList();

                    StaffListView.ItemsSource = staffList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPage());
        }
    }
}