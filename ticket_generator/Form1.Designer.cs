
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
            this.questionsLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.cumputeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // questions
            // 
            this.questions.Location = new System.Drawing.Point(187, 142);
            this.questions.Name = "questions";
            this.questions.Size = new System.Drawing.Size(123, 23);
            this.questions.TabIndex = 1;
            this.questions.Text = "questions";
            this.questions.UseVisualStyleBackColor = true;
            this.questions.Click += new System.EventHandler(this.questions_Click);
            // 
            // template
            // 
            this.template.Location = new System.Drawing.Point(978, 514);
            this.template.Name = "template";
            this.template.Size = new System.Drawing.Size(123, 23);
            this.template.TabIndex = 2;
            this.template.Text = "template";
            this.template.UseVisualStyleBackColor = true;
            this.template.Click += new System.EventHandler(this.template_Click);
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(489, 39);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(123, 23);
            this.output.TabIndex = 3;
            this.output.Text = "output";
            this.output.UseVisualStyleBackColor = true;
            this.output.Click += new System.EventHandler(this.output_Click);
            // 
            // compute
            // 
            this.compute.Location = new System.Drawing.Point(346, 356);
            this.compute.Name = "compute";
            this.compute.Size = new System.Drawing.Size(123, 23);
            this.compute.TabIndex = 4;
            this.compute.Text = "compute";
            this.compute.UseVisualStyleBackColor = true;
            this.compute.Click += new System.EventHandler(this.compute_Click);
            // 
            // questionsLabel
            // 
            this.questionsLabel.AutoSize = true;
            this.questionsLabel.Location = new System.Drawing.Point(206, 180);
            this.questionsLabel.Name = "questionsLabel";
            this.questionsLabel.Size = new System.Drawing.Size(76, 16);
            this.questionsLabel.TabIndex = 5;
            this.questionsLabel.Text = "Не задано";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(508, 75);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(76, 16);
            this.outputLabel.TabIndex = 6;
            this.outputLabel.Text = "Не задано";
            // 
            // cumputeLabel
            // 
            this.cumputeLabel.AutoSize = true;
            this.cumputeLabel.Location = new System.Drawing.Point(357, 394);
            this.cumputeLabel.Name = "cumputeLabel";
            this.cumputeLabel.Size = new System.Drawing.Size(0, 16);
            this.cumputeLabel.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 549);
            this.Controls.Add(this.cumputeLabel);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.questionsLabel);
            this.Controls.Add(this.compute);
            this.Controls.Add(this.output);
            this.Controls.Add(this.template);
            this.Controls.Add(this.questions);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button questions;
        private System.Windows.Forms.Button template;
        private System.Windows.Forms.Button output;
        private System.Windows.Forms.Button compute;
        private System.Windows.Forms.Label questionsLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Label cumputeLabel;
    }
}

