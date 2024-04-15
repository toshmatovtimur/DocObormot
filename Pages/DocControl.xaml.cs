using Aspose.Words;
using DocumentoOborotWpfApp.Models;
using DocumentoOborotWpfApp.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;

namespace DocumentoOborotWpfApp.Pages
{

    public partial class DocControl : Page
    {
        int ansWer = 0;
        int idSend = 0;
        public DocControl(int idUser)
        {
            InitializeComponent();
            StartSendsTable(4);
            ansWer = idUser;
        }

        private void StartSendsTable(int t)
        {
            using apContext db = new();

            var getmymind = from send in db.Sendings.ToList()
                            join user1 in db.Users.ToList() on send.FkSendUser equals user1.Id
                            join user2 in db.Users.ToList() on send.FkRecUser equals user2.Id
                            join doc in db.Documents.ToList() on send.FkDoc equals doc.Id
                            join status in db.Sendstatuses.ToList() on send.FkStatus equals status.Id
                            join rol in db.Roles.ToList() on user1.FkRole equals rol.Id
                            where user2.Id == 4
                            select new
                            {
                                docId = doc.Id,
                                send.Id,
                                otpravka = $"{user1.Firstname} {user1.Middlename[1].ToString().ToUpper()}.{user1.Lastname[1].ToString().ToUpper()}. - {rol.RoleName}",
                                document = doc.DocName,
                                doc.DocByte,
                                stat = status.SendstatusName
                            };

            listviewSends.ItemsSource = getmymind.OrderByDescending(u => u.stat).ToList();
        }

        // Обновить таблицу
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartSendsTable(ansWer);
        }

        // Просмотр документа
        private void ViewDocumentAnswer(object sender, RoutedEventArgs e)
        {

            int t = ReturnId(listviewSends.SelectedValue.ToString());

            ControlUser.docdirEvent = t;

            string input = listviewSends.SelectedValue.ToString();
            string pattern = @", Id = (\d+)";
            string temp = "";
            foreach (Match match in Regex.Matches(input, pattern).Cast<Match>())
            {
                for (int i = 0; i < match.Value.Length; i++)
                {
                    if (char.IsDigit(match.Value[i]))
                    {
                        temp += match.Value[i];
                    }
                }
            }

            using apContext db = new();

            // Получить Id отправки
            var getSend = db.Sendings.FirstOrDefault(u => u.Id == Convert.ToInt32(temp));

            if (getSend != null)
            {
                idSend = getSend.Id;

                // Получить наименование документа
                var detDoc = db.Documents.FirstOrDefault(u => u.Id == getSend.FkDoc);
                docX.Text = detDoc.DocName;

                // Изменить статус
                getSend.FkStatus = 2;
                db.SaveChanges();

                StartSendsTable(4);
            }















            // Возвращает Id
            int ReturnId(string str)
            {
                var temp = "";
                for (int i = 0; i < str.Length; i++)
                {
                    if (char.IsDigit(str[i]))
                    {
                        temp += str[i];
                    }
                    if (str[i] == ',')
                    {
                        break;
                    }
                }
                return Convert.ToInt32(temp);
            }
        }

        // Утвердить документ
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Вы уверены?", "Требуется подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                using apContext db = new();

                var updateSend = db.Sendings.FirstOrDefault(u => u.Id == idSend);
                if(updateSend!= null)
                {
                    updateSend.FkStatus = 3;
                    db.SaveChanges();
                    StartSendsTable(4);
                    MessageBox.Show("Документ утвержден");
                }
            }
            else if (Result == MessageBoxResult.No)
            {
                return;
            }
        }
    }
}
