using DocumentoOborotWpfApp.Models;
using DocumentoOborotWpfApp.Windows;
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
    public partial class Directories : Page
    {

        public Directories()
        {
            InitializeComponent();
            StartDirectory();
        }

        void StartDirectory()
        {
            using apContext db = new();

            var docs = from dd in db.Dirdocs
                        join dir in db.Directories on dd.FkDir equals dir.Id
                        join doc in db.Documents on dd.FkDoc equals doc.Id
                        select new
                        {
                            Dd =  dd.Id,
                            DocId = doc.Id,
                            DirId = dir.Id,
                            NameDoc = doc.DocName,
                            NameDir = dir.DirName
                        };

            // Внутриплощадочные сети  ЭС4
            listviewVS.ItemsSource = docs.Where(u => u.DirId == 1).ToList();

            // Средства защиты проезда  СЗП
            listviewSZP.ItemsSource = docs.Where(u => u.DirId == 2).ToList();

            // Электрооборудование силовое  ЭМ
            listviewEM.ItemsSource = docs.Where(u => u.DirId == 3).ToList();

            // Охранное освещение  ЭНО
            listviewENO.ItemsSource = docs.Where(u => u.DirId == 4).ToList();

            // Технические средства охраны  ТСО
            listviewTSO.ItemsSource = docs.Where(u => u.DirId == 5).ToList();



        }

        // Робит Выборка данных выбранной записи в ListView
        private void check(object sender, MouseButtonEventArgs e)
        {
            var str = listviewVS.SelectedItem.ToString();
            MessageBox.Show(str);
        }

        // 1 папка
        private void ViewDoc1(object sender, RoutedEventArgs e)
        {
            int t = ReturnId(listviewVS.SelectedValue.ToString());

            UserWindow.answerEvent = t;

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

        // 2 папка
        private void ViewDoc2(object sender, RoutedEventArgs e)
        {
            int t = ReturnId(listviewSZP.SelectedValue.ToString());

            UserWindow.answerEvent = t;

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

        // 3 папка
        private void ViewDoc3(object sender, RoutedEventArgs e)
        {
            int t = ReturnId(listviewEM.SelectedValue.ToString());

            UserWindow.answerEvent = t;

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

        // 4 папка
        private void ViewDoc4(object sender, RoutedEventArgs e)
        {
            int t = ReturnId(listviewENO.SelectedValue.ToString());

            UserWindow.answerEvent = t;

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

        // 5 папка
        private void ViewDoc5(object sender, RoutedEventArgs e)
        {
            int t = ReturnId(listviewTSO.SelectedValue.ToString());

            UserWindow.answerEvent = t;

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
    }
}
