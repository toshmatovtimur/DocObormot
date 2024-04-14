using Aspose.Words;
using DocumentoOborotWpfApp.Models;
using DocumentoOborotWpfApp.Windows;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
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

namespace DocumentoOborotWpfApp.Pages
{
    public partial class Directories : System.Windows.Controls.Page
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

        #region View
        // 1 папка
        private void ViewDoc1(object sender, RoutedEventArgs e)
        {
            string input = listviewVS.SelectedValue.ToString();
            string pattern = @"DocId = (\d+)";
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

            UserWindow.answerEvent = Convert.ToInt32(temp);
        }

        // 2 папка
        private void ViewDoc2(object sender, RoutedEventArgs e)
        {
            string input = listviewSZP.SelectedValue.ToString();
            string pattern = @"DocId = (\d+)";
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

            UserWindow.answerEvent = Convert.ToInt32(temp);
        }

        // 3 папка
        private void ViewDoc3(object sender, RoutedEventArgs e)
        {
            string input = listviewEM.SelectedValue.ToString();
            string pattern = @"DocId = (\d+)";
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

            UserWindow.answerEvent = Convert.ToInt32(temp);
        }

        // 4 папка
        private void ViewDoc4(object sender, RoutedEventArgs e)
        {
            string input = listviewENO.SelectedValue.ToString();
            string pattern = @"DocId = (\d+)";
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

            UserWindow.answerEvent = Convert.ToInt32(temp);
        }

        // 5 папка
        private void ViewDoc5(object sender, RoutedEventArgs e)
        {
            string input = listviewTSO.SelectedValue.ToString();
            string pattern = @"DocId = (\d+)";
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

            UserWindow.answerEvent = Convert.ToInt32(temp);

        }
        #endregion
        #region Add
        // 1 папка
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveDocumentInDir(1);
        }

        // 2 папка
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveDocumentInDir(2);
        }

        // 3 папка
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SaveDocumentInDir(3);
        }

        // 4 папка
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SaveDocumentInDir(4);
        }

        // 5 папка
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SaveDocumentInDir(5);
        }

        private void SaveDocumentInDir(int id)
        {
            using apContext db = new();
            Models.Document document = new();

            OpenFileDialog open = new();
            open.Filter = "Word File (.docx ,.doc)|*.docx;*.doc";

            if (open.ShowDialog() == true)
            {
                // Load the document from disk.
                Aspose.Words.Document doc = new(open.FileName);

                // Create a new memory stream.
                MemoryStream outStream = new();

                // Save the document to stream.
                doc.Save(outStream, SaveFormat.Docx);

                // Convert the document to byte form.
                byte[] docBytes = outStream.ToArray();
                document.DocName = System.IO.Path.GetFileName(open.FileName); 
                document.DocByte = docBytes;
                db.Documents.Add(document);
                db.SaveChanges();
            }

            var lastIdDoc = db.Documents.OrderBy(u => u.Id).LastOrDefault();

            if (lastIdDoc != null)
            {
                Dirdoc dirdoc = new();
                dirdoc.FkDir = id;
                dirdoc.FkDoc = lastIdDoc.Id;
                db.Dirdocs.Add(dirdoc);
                db.SaveChanges();
                StartDirectory();
            }
        }
        #endregion
        #region Delete

        // док дел 1
        private void DelDoc1(object sender, RoutedEventArgs e)
        {
            string input = listviewVS.SelectedValue.ToString();
            DeleteDocInDir(input);
        }

        // док дел 2
        private void DelDoc2(object sender, RoutedEventArgs e)
        {
            string input = listviewSZP.SelectedValue.ToString();
            DeleteDocInDir(input);
        }

        // док дел 3
        private void DelDoc3(object sender, RoutedEventArgs e)
        {
            string input = listviewEM.SelectedValue.ToString();
            DeleteDocInDir(input);
        }

        // док дел 4
        private void DelDoc4(object sender, RoutedEventArgs e)
        {
            string input = listviewENO.SelectedValue.ToString();
            DeleteDocInDir(input);
        }

        // док дел 5
        private void DocDel5(object sender, RoutedEventArgs e)
        {
            string? input = listviewTSO.SelectedValue.ToString();
            DeleteDocInDir(input);
        }

        // Метод удаления
        private void DeleteDocInDir(string str)
        {
            string pattern = @"DocId = (\d+)";
            string temp = "";
            foreach (Match match in Regex.Matches(str, pattern).Cast<Match>())
            {
                for (int i = 0; i < match.Value.Length; i++)
                {
                    if (char.IsDigit(match.Value[i]))
                    {
                        temp += match.Value[i];
                    }
                }
            }

            var Result = MessageBox.Show("Вы уверены что хотите удалить файл??", "Требуется подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                using apContext db = new();

                var getDeleteFile = db.Documents.FirstOrDefault(u => u.Id == Convert.ToInt32(temp));
                var getDirFile = db.Dirdocs.FirstOrDefault(u => u.FkDoc == Convert.ToInt32(temp));

                if (getDeleteFile != null && getDirFile != null)
                {
                    try
                    {
                        db.Dirdocs.Remove(getDirFile);
                        db.Documents.Remove(getDeleteFile);
                        db.SaveChanges();

                        StartDirectory();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }
            else if (Result == MessageBoxResult.No)
            {
                return;
            }
        }

        #endregion
    }
}
