
namespace ticket_generator
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.questions = new System.Windows.Forms.Button();
            this.template = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.Button();
            this.compute = new System.Windows.Forms.Button();
            this.cumputeLabel = new System.Windows.Forms.Label();
            this.difficultyText = new System.Windows.Forms.TextBox();
            this.practisText = new System.Windows.Forms.TextBox();
            this.teorityText = new System.Windows.Forms.TextBox();
            this.teor = new System.Windows.Forms.Label();
            this.pract = new System.Windows.Forms.Label();
            this.diff = new System.Windows.Forms.Label();
            this.vars = new System.Windows.Forms.Label();
            this.variantsText = new System.Windows.Forms.TextBox();
            this.doubleFileMode = new System.Windows.Forms.CheckBox();
            this.practisQuestions = new System.Windows.Forms.Button();
            this.onlyNumberMode = new System.Windows.Forms.CheckBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.theoryFileLabel = new System.Windows.Forms.Label();
            this.practiceFileLabel = new System.Windows.Forms.Label();
            this.outputFileLabel = new System.Windows.Forms.Label();
            this.templateFileLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // questions
            // 
            this.questions.Location = new System.Drawing.Point(805, 64);
            this.questions.Name = "questions";
            this.questions.Size = new System.Drawing.Size(234, 45);
            this.questions.TabIndex = 1;
            this.questions.Text = "Выбрать файл с вопросами";
            this.questions.UseVisualStyleBackColor = true;
            this.questions.Click += new System.EventHandler(this.questions_Click);
            // 
            // template
            // 
            this.template.Location = new System.Drawing.Point(805, 217);
            this.template.Name = "template";
            this.template.Size = new System.Drawing.Size(232, 45);
            this.template.TabIndex = 2;
            this.template.Text = "Выбрать файл с шаблоном";
            this.template.UseVisualStyleBackColor = true;
            this.template.Click += new System.EventHandler(this.template_Click);
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(805, 166);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(236, 45);
            this.output.TabIndex = 3;
            this.output.Text = "Выбрать выходной файл";
            this.output.UseVisualStyleBackColor = true;
            this.output.Click += new System.EventHandler(this.output_Click);
            // 
            // compute
            // 
            this.compute.Location = new System.Drawing.Point(808, 590);
            this.compute.Name = "compute";
            this.compute.Size = new System.Drawing.Size(234, 45);
            this.compute.TabIndex = 4;
            this.compute.Text = "Сгенерировать";
            this.compute.UseVisualStyleBackColor = true;
            this.compute.Click += new System.EventHandler(this.compute_Click);
            // 
            // cumputeLabel
            // 
            this.cumputeLabel.AutoSize = true;
            this.cumputeLabel.Location = new System.Drawing.Point(802, 517);
            this.cumputeLabel.Name = "cumputeLabel";
            this.cumputeLabel.Size = new System.Drawing.Size(0, 16);
            this.cumputeLabel.TabIndex = 7;
            // 
            // difficultyText
            // 
            this.difficultyText.Location = new System.Drawing.Point(808, 549);
            this.difficultyText.Name = "difficultyText";
            this.difficultyText.Size = new System.Drawing.Size(100, 22);
            this.difficultyText.TabIndex = 11;
            this.difficultyText.Text = "3";
            // 
            // practisText
            // 
            this.practisText.Location = new System.Drawing.Point(808, 492);
            this.practisText.Name = "practisText";
            this.practisText.Size = new System.Drawing.Size(100, 22);
            this.practisText.TabIndex = 12;
            this.practisText.Text = "1";
            // 
            // teorityText
            // 
            this.teorityText.Location = new System.Drawing.Point(808, 433);
            this.teorityText.Name = "teorityText";
            this.teorityText.Size = new System.Drawing.Size(100, 22);
            this.teorityText.TabIndex = 13;
            this.teorityText.Text = "2";
            // 
            // teor
            // 
            this.teor.AutoSize = true;
            this.teor.Location = new System.Drawing.Point(805, 414);
            this.teor.Name = "teor";
            this.teor.Size = new System.Drawing.Size(173, 16);
            this.teor.TabIndex = 14;
            this.teor.Text = "Теоретических вопросов";
            // 
            // pract
            // 
            this.pract.AutoSize = true;
            this.pract.Location = new System.Drawing.Point(805, 472);
            this.pract.Name = "pract";
            this.pract.Size = new System.Drawing.Size(165, 16);
            this.pract.TabIndex = 15;
            this.pract.Text = "Практических вопросов";
            // 
            // diff
            // 
            this.diff.AutoSize = true;
            this.diff.Location = new System.Drawing.Point(805, 529);
            this.diff.Name = "diff";
            this.diff.Size = new System.Drawing.Size(242, 16);
            this.diff.TabIndex = 16;
            this.diff.Text = "Средняя сложность вопроса билета";
            // 
            // vars
            // 
            this.vars.AutoSize = true;
            this.vars.Location = new System.Drawing.Point(805, 355);
            this.vars.Name = "vars";
            this.vars.Size = new System.Drawing.Size(79, 16);
            this.vars.TabIndex = 18;
            this.vars.Text = "Вариантов";
            this.vars.Click += new System.EventHandler(this.vars_Click);
            // 
            // variantsText
            // 
            this.variantsText.Location = new System.Drawing.Point(805, 374);
            this.variantsText.Name = "variantsText";
            this.variantsText.Size = new System.Drawing.Size(100, 22);
            this.variantsText.TabIndex = 17;
            this.variantsText.Text = "10";
            this.variantsText.TextChanged += new System.EventHandler(this.variantsText_TextChanged);
            // 
            // doubleFileMode
            // 
            this.doubleFileMode.AutoSize = true;
            this.doubleFileMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.doubleFileMode.Location = new System.Drawing.Point(808, 30);
            this.doubleFileMode.Name = "doubleFileMode";
            this.doubleFileMode.Size = new System.Drawing.Size(241, 20);
            this.doubleFileMode.TabIndex = 19;
            this.doubleFileMode.Text = "Разделённая теория и практика";
            this.doubleFileMode.UseVisualStyleBackColor = true;
            this.doubleFileMode.CheckedChanged += new System.EventHandler(this.doubleFileMode_CheckedChanged);
            // 
            // practisQuestions
            // 
            this.practisQuestions.Enabled = false;
            this.practisQuestions.Location = new System.Drawing.Point(805, 115);
            this.practisQuestions.Name = "practisQuestions";
            this.practisQuestions.Size = new System.Drawing.Size(234, 45);
            this.practisQuestions.TabIndex = 20;
            this.practisQuestions.Text = "Выбрать файл с практикой";
            this.practisQuestions.UseVisualStyleBackColor = true;
            this.practisQuestions.Visible = false;
            this.practisQuestions.Click += new System.EventHandler(this.practisQuestions_Click);
            // 
            // onlyNumberMode
            // 
            this.onlyNumberMode.AutoSize = true;
            this.onlyNumberMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.onlyNumberMode.Location = new System.Drawing.Point(808, 277);
            this.onlyNumberMode.Name = "onlyNumberMode";
            this.onlyNumberMode.Size = new System.Drawing.Size(205, 20);
            this.onlyNumberMode.TabIndex = 21;
            this.onlyNumberMode.Text = "Писать только № практики";
            this.onlyNumberMode.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(2, 4);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(771, 926);
            this.webBrowser1.TabIndex = 22;
            // 
            // theoryFileLabel
            // 
            this.theoryFileLabel.AutoSize = true;
            this.theoryFileLabel.Location = new System.Drawing.Point(1046, 76);
            this.theoryFileLabel.Name = "theoryFileLabel";
            this.theoryFileLabel.Size = new System.Drawing.Size(44, 16);
            this.theoryFileLabel.TabIndex = 23;
            this.theoryFileLabel.Text = "label1";
            // 
            // practiceFileLabel
            // 
            this.practiceFileLabel.AutoSize = true;
            this.practiceFileLabel.Location = new System.Drawing.Point(1046, 129);
            this.practiceFileLabel.Name = "practiceFileLabel";
            this.practiceFileLabel.Size = new System.Drawing.Size(44, 16);
            this.practiceFileLabel.TabIndex = 24;
            this.practiceFileLabel.Text = "label1";
            // 
            // outputFileLabel
            // 
            this.outputFileLabel.AutoSize = true;
            this.outputFileLabel.Location = new System.Drawing.Point(1047, 180);
            this.outputFileLabel.Name = "outputFileLabel";
            this.outputFileLabel.Size = new System.Drawing.Size(44, 16);
            this.outputFileLabel.TabIndex = 25;
            this.outputFileLabel.Text = "label1";
            // 
            // templateFileLabel
            // 
            this.templateFileLabel.AutoSize = true;
            this.templateFileLabel.Location = new System.Drawing.Point(1047, 231);
            this.templateFileLabel.Name = "templateFileLabel";
            this.templateFileLabel.Size = new System.Drawing.Size(44, 16);
            this.templateFileLabel.TabIndex = 26;
            this.templateFileLabel.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1776, 932);
            this.Controls.Add(this.templateFileLabel);
            this.Controls.Add(this.outputFileLabel);
            this.Controls.Add(this.practiceFileLabel);
            this.Controls.Add(this.theoryFileLabel);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.onlyNumberMode);
            this.Controls.Add(this.practisQuestions);
            this.Controls.Add(this.doubleFileMode);
            this.Controls.Add(this.vars);
            this.Controls.Add(this.variantsText);
            this.Controls.Add(this.diff);
            this.Controls.Add(this.pract);
            this.Controls.Add(this.teor);
            this.Controls.Add(this.teorityText);
            this.Controls.Add(this.practisText);
            this.Controls.Add(this.difficultyText);
            this.Controls.Add(this.cumputeLabel);
            this.Controls.Add(this.compute);
            this.Controls.Add(this.output);
            this.Controls.Add(this.template);
            this.Controls.Add(this.questions);
            this.Name = "Form1";
            this.Text = "Ticket Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button questions;
        private System.Windows.Forms.Button template;
        private System.Windows.Forms.Button output;
        private System.Windows.Forms.Button compute;
        private System.Windows.Forms.Label cumputeLabel;
        private System.Windows.Forms.TextBox difficultyText;
        private System.Windows.Forms.TextBox practisText;
        private System.Windows.Forms.TextBox teorityText;
        private System.Windows.Forms.Label teor;
        private System.Windows.Forms.Label pract;
        private System.Windows.Forms.Label diff;
        private System.Windows.Forms.Label vars;
        private System.Windows.Forms.TextBox variantsText;
        private System.Windows.Forms.CheckBox doubleFileMode;
        private System.Windows.Forms.Button practisQuestions;
        private System.Windows.Forms.CheckBox onlyNumberMode;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label theoryFileLabel;
        private System.Windows.Forms.Label practiceFileLabel;
        private System.Windows.Forms.Label outputFileLabel;
        private System.Windows.Forms.Label templateFileLabel;
    }
}

