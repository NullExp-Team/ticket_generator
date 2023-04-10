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

            for (int i = 0; i < test.tickets.Count; i++)
            {   
                var ticket = test.tickets[i];

                string title = test.title;
                string ticketNumber = "Билет № " + ticket.number + '\r';

                Formatting titleFormat = new Formatting();
                titleFormat.FontFamily = new Font("Times New Roman");
                titleFormat.Size = 18D;
                titleFormat.Position = 1;
                titleFormat.FontColor = System.Drawing.Color.Black;

                Paragraph paragraphTitle = doc.InsertParagraph(title.Trim('\n', '\r'), false, titleFormat);
                Paragraph paragraphTitle2 = doc.InsertParagraph(ticketNumber, false, titleFormat);

                paragraphTitle.Alignment = Alignment.both;
                paragraphTitle2.Alignment = Alignment.right;

                for (int j = 0; j < ticket.tasks.Count; j++)
                {
                    var task = ticket.tasks[j];
                    //titleFormat.FontFamily = new Font("Times New Roman");
                    //titleFormat.Size = 13D;
                    //titleFormat.Position = 40;
                    //titleFormat.FontColor = System.Drawing.Color.Black;
                    //Formatting textParagraphFormat = new Formatting();
                    //textParagraphFormat.FontFamily = new Font("Arial");
                    //textParagraphFormat.Spacing = 1;
                    //string textParagraph = (j + 1) + ". " + variantList[i].tasks[j].conditionWithNumberedQuestions + '\r';

                    foreach (var par in task.text)
                    {
                        doc.InsertParagraph(par);
                    }
                }

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
