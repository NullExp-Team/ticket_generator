using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace ticket_generator
{
    public class Export
    {

        private static Paragraph pasteParagraph(DocX doc, TaskText taskText)
        {
            var images = taskText.pictures;
            var par = taskText.paragraph;

            if (images.Count == 0) return doc.InsertParagraph(par);

            // Дальше копирование параграфа и добавление в него изображений, т.к. в текущем изображения не работают
            var newPar = doc.InsertParagraph();

            newPar.Alignment = par.Alignment;
            newPar.Direction = par.Direction;
            newPar.IndentationAfter = par.IndentationAfter;
            newPar.IndentationBefore = par.IndentationBefore;
            newPar.IndentationFirstLine = par.IndentationFirstLine;
            newPar.IndentationHanging = par.IndentationHanging;

            par.MagicText.ForEach((magic) =>
            {
                newPar.InsertText(magic.text, false, magic.formatting);
            });

            foreach (var pic in images)
            {
                var stream = pic.Stream;
                var image = doc.AddImage(stream);
                var newPic = image.CreatePicture(pic.Height, pic.Width);
                newPar.AppendPicture(newPic);
            }
            return newPar;

        }

        public static void ExportExamTest(ExamTest test, string outputPath, string templatePath, bool onlyNumberMode)
        {
            var doc = DocX.Create(outputPath);

            // Применение контитулов из шаблона
            doc.ApplyTemplate(templatePath);

            var template = DocX.Load(templatePath);


            var pars = doc.Paragraphs.Cast<Paragraph>().ToList();

            // Удаление всех параграфов, которые добавились из шаблона
            // Мы будем добавлять их позже на каждой итерации для билета

            foreach (var par in pars)
            {
                doc.RemoveParagraph(par);
            }

            for (int ticketIndex = 0; ticketIndex < test.tickets.Count; ticketIndex++)
            {
                var ticket = test.tickets[ticketIndex];


                // Добавленям параграфы из шаблона, пока не находим тег.
                int templateParIndex = 0;
                for (; templateParIndex < template.Paragraphs.Count; templateParIndex++)
                {
                    var par = template.Paragraphs[templateParIndex];
                    if (par.FindAll("[[tasks]]").Count > 0)
                    {
                        templateParIndex++;
                        break;
                    }
                    doc.InsertParagraph(par);
                }

                int exsampleTask = 0;
                string practisNumber = "";

                // Добавление текста задач 
                for (int taskIndex = 0; taskIndex < ticket.tasks.Count; taskIndex++)
                {
                    var task = ticket.tasks[taskIndex];
                    if (onlyNumberMode && ticket.tasks[taskIndex].type == TaskType.Practice)
                    {
                        practisNumber += ticket.tasks[taskIndex].id + ", ";
                        exsampleTask = taskIndex;
                    }
                    else
                    {
                        for (int parIndex = 0; parIndex < ticket.tasks[taskIndex].taskTexts.Count; parIndex++)
                        {
                            var taskText = task.taskTexts[parIndex];
                            var par = task.taskTexts[parIndex].paragraph;
                            if (par.ParentContainer != ContainerType.Cell)
                            {

                                if (parIndex == 0)
                                {
                                    string number = (taskIndex + 1).ToString() + ". ";
                                    par.InsertText(0, number);
                                    pasteParagraph(doc, taskText);
                                    par.RemoveText(0, number.Length);
                                }
                                else
                                {
                                    pasteParagraph(doc, taskText);
                                }
                            }

                            // Добавление таблиц
                            if (par.FollowingTables?.Count > 0)
                            {
                                var tables = par.FollowingTables;
                                foreach (var table in tables) doc.InsertTable(table);
                            }
                        }
                    }

                }

                if (onlyNumberMode && practisNumber != "")
                {
                    var par = ticket.tasks[exsampleTask].taskTexts[0].paragraph;
                    int number = par.Text.Length;
                    string newText = "Практические задания номер: " + practisNumber.Substring(0, practisNumber.Length - 2);
                    par.InsertText(0, newText);
                    par.RemoveText(newText.Length, number);
                    doc.InsertParagraph(par);
                }

                // Продолжаем добавлять оставшиеся параграфы из шаблона
                for (; templateParIndex < template.Paragraphs.Count; templateParIndex++)
                {
                    var par = template.Paragraphs[templateParIndex];
                    doc.InsertParagraph(par);
                }


                var replaceOps = new StringReplaceTextOptions();
                replaceOps.SearchValue = "[[number]]";
                replaceOps.NewValue = ticket.number.ToString();

                doc.ReplaceText(replaceOps);

                // Разрыв страницы
                if (ticketIndex != test.tickets.Count - 1)
                {
                    doc.InsertParagraph("").InsertPageBreakAfterSelf();
                }

            }

            doc.Save();
        }

        public static string ExportDialog()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Word file(*.docx)| *.docx";
            dlg.DefaultExt = ".docx";
            string path = null;
            var res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                path = dlg.FileName;
            }

            return path;
        }

    }
}
