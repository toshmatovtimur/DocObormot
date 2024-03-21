using Aspose.Words;
using Aspose.Words.Replacing;
using DocumentoOborotWpfApp.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Controls : Page
    {
        readonly int idInzhener = 0;
        


        public Controls(int idUser)
        {
            InitializeComponent();
            idInzhener = idUser;
            StartControl();

        }

        private void StartControl()
        {
            using apContext db = new();

            var getI = db.Users.Where(u => u.Id == idInzhener).FirstOrDefault();
            inzhenerX.Text = $"{getI.Firstname} {getI.Middlename.Remove(1, getI.Middlename.Length - 1)}. {getI.Lastname.Remove(1, getI.Lastname.Length - 1)}.";

            controlsX.ItemsSource = db.Users.Where(u => u.FkRole == 2).ToList();
            controlsX.SelectedIndex = 0;
        }

        // Отправить документ на проверку
        private void Otpravka_Click(object sender, RoutedEventArgs e)
        {
            ValidateAndCheckNullAndSend(); 
        }

        private void ValidateAndCheckNullAndSend()
        {
            #region Есть просто путь
            var path0 = System.IO.Directory.GetCurrentDirectory();
            var upPat1 = System.IO.Directory.GetParent(path0).FullName;
            var upPath2 = System.IO.Directory.GetParent(upPat1).FullName;
            var upPath3 = System.IO.Directory.GetParent(upPath2).FullName;
            #endregion

            //Сначала проверяю на пустоту
            var mass = new List<string>()
            {
                aktX.Text, vidIzdeliyaX.Text, dataX.Text,
                osmotrX.Text, nameIzX.Text, nomerProektX.Text,
                privyazkaX.Text, pasportX.Text, razmerX.Text
            };

            int temp = 0;
            foreach (var item in mass)
            {
                if (string.IsNullOrWhiteSpace(item) || string.IsNullOrEmpty(item))
                {
                    temp++;
                }
            }

            if (temp > 0)
            {
                MessageBox.Show("Вы пропустили поле", "Вот балбес ;D");
            }
            else if (temp == 0)
            {
                if (podpis.IsChecked == true)
                {
                    string Path = $"{upPath3}/Resources/act-sample.docx";

                    // Загрузите документ Word docx
                    Aspose.Words.Document doc = new(Path);
                    try
                    {
                        // Поиск и замена текста в документе
                        doc.Range.Replace("Акт", aktX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("Дата0", dataX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("Вид изделия", vidIzdeliyaX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("ИнженерИмя", inzhenerX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));

                        using apContext db = new();
                        var getControl = db.Users.Where(u => u.Id == (int)controlsX.SelectedValue).FirstOrDefault();
                        doc.Range.Replace("КонтрольИмя", $"{getControl.Firstname} {getControl.Middlename.Remove(1, getControl.Middlename.Length - 1)}. {getControl.Lastname.Remove(1, getControl.Lastname.Length - 1)}.", new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("осмотр", osmotrX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("Наименование изделия", nameIzX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("Номер проекта", nomerProektX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("Привязка", privyazkaX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("Паспорт", pasportX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("размеры", razmerX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("подписьИнж", inzhenerX.Text, new FindReplaceOptions(FindReplaceDirection.Forward));
                        doc.Range.Replace("Дата1", DateTime.Now.ToString("d"), new FindReplaceOptions(FindReplaceDirection.Forward));

                        // Удалить текст в первом разделе документа
                        doc.Sections[0].Range.Delete();


                        // Сохраните документ Word
                        doc.Save($"{upPath3}/Resources/act-output.docx");
                        MessageBox.Show("Проверь");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Нужно подписать документ");
                }
            }




            

        }

    }
}
