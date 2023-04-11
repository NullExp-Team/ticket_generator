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
    public static class Import
    {
        /// <summary>
        /// Иногда нумерация сбивается и она вписывается в строку. Например "26. Даны несколько.." Эта функция убирает это
        /// </summary>
        private static int ClearStartOfParagraphs(DocX document, int i)
        {
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int startIndex = 0;
            try
            {
                if (numbers.Contains(document.Paragraphs[i].Text[startIndex]))
                {
                    startIndex++;
                    while (numbers.Contains(document.Paragraphs[i].Text[startIndex]))
                    {
                        startIndex++;
                    }
                    if (document.Paragraphs[i].Text[startIndex] == '.' && !numbers.Contains(document.Paragraphs[i].Text[startIndex + 1]))
                    {
                        startIndex++;
                        if (document.Paragraphs[i].Text[startIndex] == ' ')
                        {
                            startIndex++;
                        }
                        return startIndex;
                    }
                }
            } catch
            {
                return 0;
            }
            
            return 0;
        }
        public static List<GeneratorsTask> ImportTasks()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.doc; *.docx)|*.doc; *.docx";

            string path = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
            }

            var document = DocX.Load(path);

           List<GeneratorsTask> answer = new List<GeneratorsTask>();

            var taskType = TaskType.Practice;
            
            for (int i = 0, task = -1; i < document.Paragraphs.Count; i++)
            {
                
                //перед началом списка с теоретическими или практическими вопросами должен идти жирный заголовок
                if (document.Paragraphs[i].MagicText.Count >= 1 && document.Paragraphs[i].MagicText[0].formatting.Bold == true)
                {
                    taskType = taskType == TaskType.Practice ? TaskType.Theory : TaskType.Practice;
                } else
                {
                    if ((document.Paragraphs[i].Text.Length == 0 || document.Paragraphs[i].Text[0] == ' ') && document.Paragraphs[i].Pictures.Count == 0 && document.Paragraphs[i].FollowingTables == null)
                    {
                        continue;
                    }
                    int startIndex = ClearStartOfParagraphs(document, i);
                    // чтобы определить, что начался новый вопрос, достаточно найти знак ! вначале, после которого следует сложность
                    if (document.Paragraphs[i].Text.Length > 0 && document.Paragraphs[i].Text[startIndex] == '!')
                    {
                        int difficulty = document.Paragraphs[i].Text[startIndex + 1] - 48;
                        document.Paragraphs[i].RemoveText(0, startIndex+3);
                        task++;
                        answer.Add(new GeneratorsTask(task, difficulty, taskType, new List<Paragraph>()));
                        answer[task].text.Add(document.Paragraphs[i]);
                    } else
                    {
                        answer[task].text.Add(document.Paragraphs[i]);
                    }
                }
            }
            
            //Итого получается список из двух списков заданий. Каждое задание - это полученная сложность из текста
            //по метке !Х, где  Х 1..5, индекс от 0 и до количества заданий, а также список параграфов.
            //Так как ворд делали мастера своего дела, то картинки и таблицы принадлежат некому параграфу.
            //Так что при экпорте достаточно проверить списки Pictures и FollowingTables, чтобы понять,
            //есть ли здесь картинки или таблицы. Короче, пока что изи выглядит
            return answer;
        }
    }
}
