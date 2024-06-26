﻿using Aspose.Words;
using DocumentoOborotWpfApp.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;

namespace DocumentoOborotWpfApp.Windows
{

    public partial class ControlUser : Window
    {
        int idControl = 0;

        public static int docdirEvent = 0;

        public ControlUser(int idUser)
        {
            InitializeComponent();
            StartTimeNow();
            idControl = idUser;
            MainFrame.Content = new Pages.DocControl(idUser);
            EventsActivate();
        }

        #region События различные из других страниц

        private async void EventsActivate()
        {
            await Task.Run(() =>
            {
                int temp = 0;
                while (true)
                {
                    try
                    {
                        if (docdirEvent != temp)
                        {
                            temp = docdirEvent;
                            using apContext db = new();

                            var getMyWord = db.Documents.Where(u => u.Id == temp).FirstOrDefault();
                            if (getMyWord != null)
                            {
                                byte[]? docBytes = getMyWord.DocByte;
                                MemoryStream inStream = new(docBytes);

                                // Load the stream into a new document object.
                                Aspose.Words.Document loadDoc = new(inStream);

                                // Save the document.
                                var path = System.IO.Directory.GetCurrentDirectory();
                                var upPat1 = System.IO.Directory.GetParent(path).FullName;
                                var upPath2 = System.IO.Directory.GetParent(upPat1).FullName;
                                var upPath3 = System.IO.Directory.GetParent(upPath2).FullName;

                                string endPath = $"{upPath3}/Docx/1234.docx";

                                loadDoc.Save(endPath, SaveFormat.Docx);

                                XpsDocument doc = new(ConvertWordInXps(endPath), FileAccess.Read);
                                Dispatcher.Invoke(() =>
                                {
                                    documentViewer1.Document = doc.GetFixedDocumentSequence();
                                });

                            }

                            // Конвертация из Word в Xps
                            static string ConvertWordInXps(string fileName)
                            {
                                var doc = new Aspose.Words.Document(fileName);

                                var path = System.IO.Directory.GetCurrentDirectory();
                                var upPat1 = System.IO.Directory.GetParent(path).FullName;
                                var upPath2 = System.IO.Directory.GetParent(upPat1).FullName;
                                var upPath3 = System.IO.Directory.GetParent(upPath2).FullName;

                                string endPath = $"{upPath3}/Docx/{GenerationName()}.xps";

                                doc.Save(endPath);

                                return endPath;

                            }

                            // Генерация
                            static string GenerationName()
                            {
                                string str = "";
                                Random random = new();
                                for (int i = 0; i < 10; i++)
                                {
                                    str += random.Next(0, 10).ToString();
                                }

                                return str;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            });
        }

        #endregion
        #region Работа с Word
        /* Задача конвертировать word документ в xps и потом вывести */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new()
            {
                Filter = "Word documents (.docx)|*.docx|(.doc)|*.doc|All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                dlg.DefaultExt = ".xps";
                XpsDocument doc = new(ConvertWordInXps(dlg.FileName), FileAccess.Read);
                documentViewer1.Document = doc.GetFixedDocumentSequence();

            }
        }

        // Конвертация из Word в Xps
        private static string ConvertWordInXps(string fileName)
        {
            var doc = new Aspose.Words.Document(fileName);

            var path = System.IO.Directory.GetCurrentDirectory();
            var upPat1 = System.IO.Directory.GetParent(path).FullName;
            var upPath2 = System.IO.Directory.GetParent(upPat1).FullName;
            var upPath3 = System.IO.Directory.GetParent(upPath2).FullName;

            string endPath = $"{upPath3}/Docx/{GenerationName()}.xps";

            doc.Save(endPath);

            return endPath;


        }
        static string GenerationName()
        {
            string str = "";
            Random random = new();
            for (int i = 0; i < 10; i++)
            {
                str += random.Next(0, 10).ToString();
            }

            return str;
        }

        // Загрузить Word файл в БД
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using apContext db = new();
            Models.Document document = new();

            OpenFileDialog open = new();

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

                document.DocName = open.FileName;
                document.DocByte = docBytes;
                db.Documents.Add(document);
                db.SaveChanges();

            }

            MessageBox.Show("Проверь БД");

        }

        // Обратно из БД в Word файл
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using apContext db = new();
            OpenFileDialog open = new();

            var getMyWord = db.Documents.FirstOrDefault();

            byte[]? docBytes = getMyWord.DocByte;
            MemoryStream inStream = new(docBytes);

            // Load the stream into a new document object.
            Aspose.Words.Document loadDoc = new(inStream);
            // Save the document.
            loadDoc.Save(@"C:\Users\toshm\OneDrive\Рабочий стол\123.docx", SaveFormat.Docx);

            var proc = new Process();
            proc.StartInfo.FileName = @"C:\Users\toshm\OneDrive\Рабочий стол\123.docx";
            proc.StartInfo.UseShellExecute = true;
            proc.Start();

        }
        #endregion
        #region PagesControl
        // Директории
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Pages.Directories();
        }

        // Документы для проверки
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Pages.DocControl(idControl);
        }

        #endregion
        #region Время()
        void StartTimeNow()
        {
            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            Title = "БЕЗ РИСКА. Контроллер. Томское время: " + DateTime.Now.ToLongTimeString();
        }



        #endregion
    }
}
