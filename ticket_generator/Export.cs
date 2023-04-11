using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace ticket_generator
{
    public class Export
    {

        public static void ExportExamTest(ExamTest test, string outputPath, string templatePath)
        {
            var doc = DocX.Create(outputPath);

            // Применение контитулов из шаблона
            doc.ApplyTemplate(templatePath);

            var template = DocX.Load (templatePath);

            var pars = doc.Paragraphs.Cast<Paragraph>().ToList();

            // Удаление всех параграфов, которые добавились из шаблона
            // Мы будем добавлять позже их на каждой итерации для билета

            foreach (var par in pars)
            {
                doc.RemoveParagraph(par);
            }

            for (int i = 0; i < test.tickets.Count; i++)
            {
                var ticket = test.tickets[i];

                // Добавленям параграфы из шаблона, пока не находим тег.
                int templateParIndex = 0;
                for (; templateParIndex < template.Paragraphs.Count; templateParIndex++)
                {
                    var par = template.Paragraphs[templateParIndex];
                    if (par.FindAll("[[tasks]]").Count > 0) {
                        templateParIndex++;
                        break;
                    }
                    doc.InsertParagraph(par);
                }

                // Добавление текста задач 
                for (int task = 0; task < ticket.tasks.Count; task++)
                {
                    for (int par = 0; par < ticket.tasks[task].text.Count; par++)
                    {
                        if (ticket.tasks[task].text[par].ParentContainer != ContainerType.Cell) {
                            try
                            { 
                                if (par == 0)
                                {
                                    String number = (task + 1).ToString() + ". ";
                                    ticket.tasks[task].text[par].InsertText(0, number);
                                    doc.InsertParagraph(ticket.tasks[task].text[par]);
                                    ticket.tasks[task].text[par].RemoveText(0, number.Length);
                                } else
                                {
                                    doc.InsertParagraph(ticket.tasks[task].text[par]);
                                }
                            } 
                            catch
                            {

                            }
                           
                        } 

                        // Добавление таблиц
                        // TODO: Попробовать пофиксить слетающий стиль у таблицы кайфа
                        if (ticket.tasks[task].text[par].FollowingTables?.Count > 0)
                        {
                            var tables = ticket.tasks[task].text[par].FollowingTables;
                            foreach (var table in tables) doc.InsertTable(table);
                        }
                    }
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
                if (i != test.tickets.Count - 1)
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
