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
        string templateFilePath = "../../template.docx";
        string outputFilePath;

        public Form1()
        {
            InitializeComponent();
            templateLabel.Text = templateFilePath;
        }

        private void questions_Click(object sender, EventArgs e)
        {
            questionFilePath = Import.ImportDialog();
            questionsLabel.Text = "Выбрано";
        }

        private void template_Click(object sender, EventArgs e)
        {
            templateFilePath = Import.ImportDialog();
            templateLabel.Text = "Выбрано";
        }

        private void output_Click(object sender, EventArgs e)
        {
             outputFilePath = Export.ExportDialog();
             outputLabel.Text = "Выбрано";
        }

        private void compute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(questionFilePath))
            {
                MessageBox.Show("Файл с вопросами не задан");
                questionsLabel.Text = "Не выбрано";
                return;
            }

            if (string.IsNullOrEmpty(templateFilePath))
            {
                MessageBox.Show("Шаблон не задан");
                templateLabel.Text = "Не выбрано";
                return;
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                MessageBox.Show("Выходной файл не задан");
                outputLabel.Text = "Не выбрано";
                return;
            }

            var algorithm = new Algorithm();

            var tasks = Import.ImportTasks(questionFilePath);
            
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

                Export.ExportExamTest(examTest, outputFilePath, templateFilePath);

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
