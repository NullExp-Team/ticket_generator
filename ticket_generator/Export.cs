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
                foreach (var task in ticket.tasks)
                {
                    foreach (var par in task.text)
                    {
                        if (par.ParentContainer != ContainerType.Cell) {
                            try
                            { 
                                doc.InsertParagraph(par);
                            } 
                            catch
                            {

                            }
                           
                        } 

                        // Добавление таблиц
                        // TODO: Попробовать пофиксить слетающий стиль у таблицы кайфа
                        if (par.FollowingTables?.Count > 0)
                        {
                            var tables = par.FollowingTables;
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
