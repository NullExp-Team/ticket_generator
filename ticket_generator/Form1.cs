using Markdig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace ticket_generator
{
    public partial class Form1 : Form
    {

        string questionFilePath;
        string questionPractisFilePath;
        string templateFilePath = "../../template.docx";
        string outputFilePath;

        public Form1()
        {
            InitializeComponent();
            practiceFileLabel.Text = "";
            theoryFileLabel.Text = "";
            outputFileLabel.Text = "";
            templateFileLabel.Text = "Выбран по умолчанию";

            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.DocumentText = Markdown.ToHtml("# Генератор экзаменационных билетов и тестов\r\n\r\n## Краткое описание\r\nНебольшая программа для случайной генерации билетов для экзамена или тестов в формате .docx с учётом указанной и сбалансированной сложности.\r\n\r\n## Инструкция\r\n\r\n### 📂 Файл с вопросами\r\nЭто файл, наполненный теоретическими и практическими вопросами для формирования билетов или тестов.\r\nОн должен удовлетворять нескольким критериям:\r\n- Началом списка для теоретических или практических вопросов является новая строка, в которой первый и более символов выделены жирным шрифтом. Программа не обращает внимания на нумерацию списка\r\n- Каждый новый практический или теоретический вопрос должен начинаться с \"!Х \", где Х - это цифра от 1 до 5, обозначающая сложность вопроса.\r\n- Программа поддерживает формулы, таблицы и изображения.\r\n\r\n### 📂 Файл с шаблоном\r\nЭто файл, который является шаблоном билета. С его помощью можно настроить внешний вид билета или теста.\r\nОн может содержать колонтитулы и текст, в котором должны быть теги.\r\nТеги используются для позиционирования номера билета и списка задач. Они автоматически заменяются при генерации.\r\n- [[number]] - номер билета / теста. Необязательный тег.\r\n- [[tasks]] - сами вопросы и задачи. Обязательный тег, он дожен присутствовать в каждом шаблоне.\r\n\r\n### 📂 Выходной файл\r\nЭто файл, в который запишутся все сгенерировнные билеты в соответствии с шаблоном.\r\nМожет быть выбран существующий файл или создан новый. \r\n\r\n### 🔧 Параметры\r\nСредняя сложность вопроса билета должна быть числом от 1 до 5. Необязательно целым.\r\n\r\nКогда все три файла будут выбраны и заполнены все поля параметров, достаточно нажать на кнопку \"Сгенерировать\".\r\n### ❗ Важно\r\nПри генерации все выбранные файлы должны быть полностью закрыты, чтобы прилжение могло получить к ним доступ.\r\n\r\n<p align=\"center\">\r\n <br>\r\n Developed by NullExp\r\n</p>");
        }

        private void doubleFileMode_CheckedChanged(object sender, EventArgs e)
        {
            if (doubleFileMode.Checked)
            {
                questions.Text = questions.Text.Replace("вопросами", "теорией");
                practisQuestions.Enabled = true;
                practisQuestions.Visible = true;
                practiceFileLabel.Visible = true;
           
            } else
            {
                questions.Text = questions.Text.Replace("теорией", "вопросами");
                practisQuestions.Enabled = false;
                practisQuestions.Visible = false;
                practiceFileLabel.Visible = false;
            }
        }
        private void questions_Click(object sender, EventArgs e)
        {
            questionFilePath = Import.ImportDialog();
            if (questionFilePath != null)
            theoryFileLabel.Text = questionFilePath.Split('\\').Last();
        }

        private void practisQuestions_Click(object sender, EventArgs e)
        {
            questionPractisFilePath = Import.ImportDialog();
            if (questionPractisFilePath != null)
            practiceFileLabel.Text = questionPractisFilePath.Split('\\').Last();
        }

        private void template_Click(object sender, EventArgs e)
        {
            var res = Import.ImportDialog();
            if (res != null)
            {
                templateFilePath = res;

                templateFileLabel.Text = templateFilePath.Split('\\').Last();
            }
               
        }

        private void output_Click(object sender, EventArgs e)
        {
            outputFilePath = Export.ExportDialog();
            if (outputFilePath != null)
            outputFileLabel.Text = outputFilePath.Split('\\').Last();
        }

        private void compute_Click(object sender, EventArgs e)
        {
            string questionFileName = "";
            if (doubleFileMode.Checked)
            {
                questionFileName = "Файл с теорией";
            }
            else
            {
                questionFileName = "Файл с вопросами";
            }

            if (string.IsNullOrEmpty(questionFilePath))
            {
                MessageBox.Show(questionFileName + " не задан"); return;
            }

            if (string.IsNullOrEmpty(templateFilePath))
            {
                MessageBox.Show("Файл с шаблоном не задан"); return;
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                MessageBox.Show("Выходной файл не задан"); return;
            }

            try
            {
               File.OpenRead(questionFilePath).Close();
            } catch
            {
                MessageBox.Show("Нет доступа к " + questionFilePath + ". \n\nПроверьте, что " + questionFileName.ToLower() + " закрыт."); return;
            }

            try
            {
                File.OpenRead(templateFilePath).Close();
            }
            catch
            {
                MessageBox.Show("Нет доступа к " + templateFilePath + ". \n\nПроверьте, что файл с шаблоном закрыт."); return;
            }

            try
            {
                File.OpenRead(outputFilePath).Close();
            }
            catch
            {
                MessageBox.Show("Нет доступа к " + outputFilePath + ". \n\nПроверьте, что выходной файл закрыт."); return;
            }



            var algorithm = new Algorithm();

            List<GeneratorsTask> tasks;
            if (doubleFileMode.Checked)
            {
                tasks = Import.ImportTasksDoubleFileMode(questionFilePath, questionPractisFilePath);
            } else
            {
                tasks = Import.ImportTasks(questionFilePath);
            }
           
            int tc = Convert.ToInt32(teorityText.Text);
            int pc = Convert.ToInt32(practisText.Text);
            double df = Convert.ToDouble(difficultyText.Text.Replace('.', ','));
            int vc = Convert.ToInt32(variantsText.Text);
            if (tc < 0 || pc < 0 || vc < 0) {
                MessageBox.Show("Параметры должны быть положительными"); return;
            }
            int tcFile = tasks.Where((task) => task.type == TaskType.Theory).Count();
            if (tc > tcFile)
            {
                MessageBox.Show("Задано " + tc + " теоритических задач, хотя в файле найдено только " + tcFile + "."); return;
            }
            int pcFile = tasks.Where((task) => task.type == TaskType.Practice).Count();
            if (pc > pcFile)
            {
                MessageBox.Show("Задано " + pc + " практических задач, хотя в файле найдено только " + pcFile +"."); return;
            }

            if (df < 1 || df > 5)
            {
                MessageBox.Show("Сложность должна быть в диапозоне от 1 до 5"); return;
            }



            ExamTest examTest = algorithm.Compute(tasks, tc, pc, df, vc);

            Export.ExportExamTest(examTest, outputFilePath, templateFilePath, onlyNumberMode.Checked);

            
            var res =  MessageBox.Show("Выходной файл успешно сгенерирован. \n\nОткрыть файл?", "Успешно", MessageBoxButtons.OKCancel);
            

            if( res == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("explorer.exe", "/open,\"" + outputFilePath + " \"");
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void variantsText_TextChanged(object sender, EventArgs e)
        {

        }

        private void vars_Click(object sender, EventArgs e)
        {

        }

        private void templateFileLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
