using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        private void doubleFileMode_CheckedChanged(object sender, EventArgs e)
        {
            if (doubleFileMode.Checked)
            {
                questions.Text = questions.Text.Replace("вопросами", "теорией");
                practisQuestions.Enabled = true;
                practisQuestions.Visible = true;
            } else
            {
                questions.Text = questions.Text.Replace("теорией", "вопросами");
                practisQuestions.Enabled = false;
                practisQuestions.Visible = false;
            }
        }
        private void questions_Click(object sender, EventArgs e)
        {
            if (doubleFileMode.Checked)
            {
                questionFilePath = Import.ImportDialog();
                questions.Text = "Файл с теорией выбран";
            } else
            {
                questionFilePath = Import.ImportDialog();
                questions.Text = "Файл с вопросами выбран";
            }
        }

        private void practisQuestions_Click(object sender, EventArgs e)
        {
            questionPractisFilePath = Import.ImportDialog();
            practisQuestions.Text = "Файл с практикой выбран";
        }

        private void template_Click(object sender, EventArgs e)
        {
            templateFilePath = Import.ImportDialog();
            template.Text = "Файл с шаблоном выбран";
        }

        private void output_Click(object sender, EventArgs e)
        {
             outputFilePath = Export.ExportDialog();
             output.Text = "Выходной файл выбран";
        }

        private void compute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(questionFilePath))
            {
                if (doubleFileMode.Checked)
                {
                    MessageBox.Show("Файл с теорией не задан");
                    questions.Text = "Выбрать файл с теорией";
                } else
                {
                    MessageBox.Show("Файл с вопросами не задан");
                    questions.Text = "Выбрать файл с вопросами";
                }
                return;
            }

            if (string.IsNullOrEmpty(templateFilePath))
            {
                MessageBox.Show("Шаблон не задан");
                template.Text = "Выбрать файл с шаблоном";
                return;
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                MessageBox.Show("Выходной файл не задан");
                output.Text = "Выбрать выходной файл";
                return;
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
            
            try
            {
                int tc = Convert.ToInt32(teorityText.Text);
                int pc = Convert.ToInt32(practisText.Text);
                double df = Convert.ToDouble(difficultyText.Text.Replace('.', ','));
                int vc = Convert.ToInt32(variantsText.Text);
                if (df < 1 || df > 5)
                {
                    throw new OverflowException();
                }
                ExamTest examTest = algorithm.Compute(tasks, tc, pc, df, vc);

                Export.ExportExamTest(examTest, outputFilePath, templateFilePath, onlyNumberMode.Checked);

                MessageBox.Show("Билеты успешно сгенерированы"); return;

            } catch
            {
                MessageBox.Show("Неправильный формат введённый данных!");
            }

            
        }

        private void questionsLabel_Click(object sender, EventArgs e)
        {

        }

        private void outputLabel_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
