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
        }

        private void questions_Click(object sender, EventArgs e)
        {
            questionFilePath = Import.ImportDialog();
            questionsLabel.Text = questionFilePath;
        }

        private void template_Click(object sender, EventArgs e)
        {

        }

        private void output_Click(object sender, EventArgs e)
        {
             outputFilePath = Export.ExportDialog();
             outputLabel.Text = outputFilePath;
        }

        private void compute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(questionFilePath))
            {
                 MessageBox.Show("Файл с вопросами не задан"); return;
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                MessageBox.Show("Выходной файл не задан"); return;
            }

            var algorithm = new Algorithm();

            var tasks = Import.ImportTasks(questionFilePath);
            
            var examTest = algorithm.Compute(tasks);

            Export.ExportExamTest(examTest, outputFilePath, templateFilePath);

            cumputeLabel.Text = outputFilePath;

            MessageBox.Show("Билеты успешно сгенерированы"); return;
        }
    }
}
