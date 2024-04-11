using Aspose.Words.Bibliography;
using DocumentoOborotWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SkiaSharp;
using System.Security.Cryptography;

namespace DocumentoOborotWpfApp.Windows
{
    public partial class AdminWindow : Window
    {
        public static List<Role>? RoleCombobox { get; set; }
        public static List<User>? MyListUsers { get; set; }
        public AdminWindow()
        {
            InitializeComponent();
            StartTable();
        }

        // Заполнение таблицы
        private void StartTable()
        {
            using apContext ok = new();

            MyListUsers = ok.Users.OrderBy(u => u.Id).ToList();

            dataGrid.ItemsSource = MyListUsers;


            //Заполнение Comboboxes
            RoleCombobox = ok.Roles.ToList();
        }

        // Обновить данные пользователя
        private void CellUsers(object sender, DataGridCellEditEndingEventArgs e)
        {
            User? a = e.Row.Item as User;

            using apContext db = new();

            if (a.Id != 0)
            {
                int result = 0;
                if(a.Password == null || string.IsNullOrWhiteSpace(a.Password))
                {
                    result = db.Database.ExecuteSqlInterpolated($"update users SET firstname={a.Firstname}, middlename={a.Middlename}, lastname={a.Lastname}, birthday={a.Birthday}, email={a.Email}, login={a.Login}, fk_role={a.FkRole}, status={a.Status} WHERE id={a.Id};");
                    if (result == 0)
                        MessageBox.Show("Произошла ошибка при обновлении таблицы(Заявитель)\nПовторите попытку");
                }
                else
                {
                    result = db.Database.ExecuteSqlInterpolated($"update users SET firstname={a.Firstname}, middlename={a.Middlename}, lastname={a.Lastname}, birthday={a.Birthday}, email={a.Email}, login={a.Login}, password={MD5Hash(a.Password)}, fk_role={a.FkRole}, status={a.Status} WHERE id={a.Id};");
                    if (result == 0)
                        MessageBox.Show("Произошла ошибка при обновлении таблицы(Заявитель)\nПовторите попытку");
                }
                StartTable();
            }
        }

        // Добавить пользователя(Решено)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using apContext db = new();

            dataGrid.CanUserAddRows = true;

            var AddPer =  db.Database.ExecuteSqlInterpolated($"INSERT INTO users(firstname, middlename, lastname, birthday, sex, email, login, password, fk_role, status) VALUES({null}, {null}, {null}, {null}, {null}, {null}, {null}, {null}, {null}, {null});");
            
            if (AddPer == 0)
                MessageBox.Show("Произошла ошибка при обновлении таблицы(Заявитель)\nПовторите попытку");
            else
            {
                StartTable();
                dataGrid.CanUserAddRows = false;
            }
        }

        // Удалить пользователя(Решено)
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            using apContext db = new();
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                int numberOfRowDeleted = db.Database.ExecuteSqlRaw("DELETE FROM users WHERE id = {0}", (dataGrid.SelectedItem as User)?.Id);
                if (numberOfRowDeleted == 1)
                {
                    StartTable();
                    MessageBox.Show("Запись удалена");
                }
                    

                else
                    MessageBox.Show("Произошла ошибка при удалении записи\n Повторите попытку");

            }
        }

        //Метод хэширования вводимого пароля
        private static string MD5Hash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        // Поиск пользователя
        private async void SearchUser(object sender, KeyEventArgs e)
        {
            if (sUserX.Text == "")
                StartTable();

            else
            {
                using apContext db = new();

                var sesese = await db.Users.ToListAsync();


                if(sesese != null)
                {
                    dataGrid.ItemsSource = sesese.Where(a => $"{a.Firstname}{a.Middlename}{a.Lastname}{a.Login}".ToLower().Contains(sUserX.Text.ToLower())).ToList();
                }
                
            }

        }

        // Обновление должности (Роли)
        private async void UpdateComboBox(object sender, EventArgs e)
        {
            if ((dataGrid.SelectedItem as User)?.Id == 0 || (dataGrid.SelectedItem as User)?.Id == null)
                return;
            else
            {
                try
                {
                    //Меняем подразделение Заявителю
                    using apContext ok = new();

                    var GetId = await ok.Roles.AsNoTracking().Where(u => u.RoleName == (sender as ComboBox).Text).FirstOrDefaultAsync();

                    if (GetId != null)
                        await ok.Database.ExecuteSqlRawAsync("UPDATE users SET fk_role = {0} WHERE Id = {1}", GetId.Id, (dataGrid.SelectedItem as User)?.Id);
                    else
                        MessageBox.Show("Произошла ошибка при обновлении данных");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка, повторите попытку", ex.Message);
                }
            }
        }

    }
}
