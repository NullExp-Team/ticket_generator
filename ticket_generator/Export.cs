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

        private static void ExportExamTest(ExamTest test, string path)
        {
            var doc = DocX.Create(path);

            // Применение контитулов из шаблона
            doc.ApplyTemplate("../../template.docx");

            var template = DocX.Load ("../../template.docx");

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
                        if (par.ParentContainer != ContainerType.Cell) doc.InsertParagraph(par);

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

        public static void ExportDialog(ExamTest test)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Word file(*.docx)| *.docx";
            dlg.DefaultExt = ".docx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExportExamTest(test, dlg.FileName);
            }
        }

    }
}
