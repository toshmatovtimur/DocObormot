using DocumentoOborotWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace DocumentoOborotWpfApp.Pages
{

    public partial class Answer : Page
    {
        public Answer()
        {
            InitializeComponent();
            StartSendsTable();
        }

        private void StartSendsTable()
        {
            using apContext db = new();

            var getmymind = from send in db.Sendings.ToList()
                            join user1 in db.Users.ToList() on send.FkSendUser equals user1.Id
                            join user2 in db.Users.ToList() on send.FkRecUser equals user2.Id
                            join doc in db.Documents.ToList() on send.FkDoc equals doc.Id
                            join status in db.Sendstatuses.ToList() on send.FkStatus equals status.Id
                            join rol in db.Roles.ToList() on user1.FkRole equals rol.Id
                            select new
                            {
                                send.Id,
                                otpravka = $"{user1.Firstname} {user1.Middlename[1].ToString().ToUpper()}.{user1.Lastname[1].ToString().ToUpper()}. - {rol.RoleName}",
                                poluchatel = $"{user2.Firstname} {user2.Middlename[1].ToString().ToUpper()}.{user2.Lastname[1].ToString().ToUpper()}. - {rol.RoleName}",
                                document = doc.DocName,
                                doc.DocByte,
                                stat = status.SendstatusName
                            };

            listviewSends.ItemsSource = getmymind.ToList();
        }

        // Обновить таблицу
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartSendsTable();
        }


    }
}
