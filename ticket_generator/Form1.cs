using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ticket_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var tasks = Import.ImportTasks();

            var algorithm = new Algorithm();
            var examTest =   algorithm.Compute(tasks);

            

            // Тест, в первом билете теор, во втором практика
            //List<Ticket> tickets = new List<Ticket>();

            //for (int i = 0; i < input.Count; i++)
            //{
            //    tickets.Add(new Ticket(i + 1, input[i]));
            //}

            //var test = new ExamTest("Test Title", tickets);

            Export.ExportDialog(examTest);
        }
    }
}
