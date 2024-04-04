using DocumentoOborotWpfApp.Models;
using DocumentoOborotWpfApp.Windows;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DocumentoOborotWpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AdminWindow admin = new();
            admin.Show();
            Close();
            Start();
        }

        //Войти
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //При нажатии на кнопку Войти
            //Вход
            //Проверка на пустые поля
            //Проверка входных значений
            if (string.IsNullOrWhiteSpace(login_text.Text)
            && string.IsNullOrWhiteSpace(password_text.Password)
            || string.IsNullOrWhiteSpace(login_text.Text)
            || string.IsNullOrWhiteSpace(password_text.Password))
            {
                MessageBox.Show("Не заполнено одно или несколько полей", "Пропущено поле", MessageBoxButton.OK, MessageBoxImage.Stop);
                if (string.IsNullOrWhiteSpace(login_text.Text))
                    login_text.BorderBrush = Brushes.Red;


                if (string.IsNullOrWhiteSpace(password_text.Password))
                    password_text.BorderBrush = Brushes.Red;

            }
            else
            {
                //Если поля не пустые
                // Инженер - пользователь
                int temp = 0;

                using apContext db = new();

                var GetUserLogPass = await db.Users.Where(u => u.Login == login_text.Text
                                                            && u.Password == MD5Hash(password_text.Password)
                                                            && u.FkRole == 1).FirstOrDefaultAsync();


                if (GetUserLogPass != null)
                {
                    //UserWindow table = new(GetUserLogPass.Id);
                    UserWindow table = new(6);
                    table.Show();
                    temp = 1;
                    Close();
                }


                // Инженер - контроль
                if (temp == 0)
                {
                    var GetAdminLogPass = await db.Users.Where(u => u.Login == login_text.Text 
                                                                 && u.Password == MD5Hash(password_text.Password)
                                                                 && u.FkRole == 2).FirstOrDefaultAsync();

                    if(GetAdminLogPass != null)
                    {
                         ControlUser control = new();
                         control.Show();
                         temp = 1;
                         Close();
                    }
                }


                // Администратор
                if (temp == 0)
                {
                    var GetAdminLogPass = await db.Users.Where(u => u.Login == login_text.Text
                                                                 && u.Password == MD5Hash(password_text.Password)
                                                                 && u.FkRole == 3).FirstOrDefaultAsync();

                    if (GetAdminLogPass != null)
                    {
                        AdminWindow admin = new();
                        admin.Show();
                        temp = 1;
                        Close();
                    }
                }


                if (temp == 0)
                {
                    MessageBox.Show("Повторите попытку", "Неправильный логин или пароль", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        //Метод хэширования вводимого пароля
        private static string MD5Hash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        //Метод хорошего старта
        private void Start()
        {
            using (apContext db = new())
            {
                var start = db.Users.ToList();
                foreach (var item in start) { }
            }
            login_text.Focus();
        }
        //Методы подсвечивают рамки красным при неправильном вводе
        private void Pa(object sender, MouseEventArgs e)
        {
            password_text.BorderBrush = Brushes.Black;
        }
        private void Bo(object sender, MouseEventArgs e)
        {
            login_text.BorderBrush = Brushes.Black;
        }
    }
}