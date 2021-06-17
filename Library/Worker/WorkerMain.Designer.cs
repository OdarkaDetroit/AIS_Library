
namespace Library.Worker
{
    partial class WorkerMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.deletingBook = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.returnBook = new System.Windows.Forms.Button();
            this.lendingBook = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // deletingBook
            // 
            this.deletingBook.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.deletingBook.Location = new System.Drawing.Point(30, 176);
            this.deletingBook.Margin = new System.Windows.Forms.Padding(4);
            this.deletingBook.Name = "deletingBook";
            this.deletingBook.Size = new System.Drawing.Size(186, 80);
            this.deletingBook.TabIndex = 0;
            this.deletingBook.Text = "Списання";
            this.deletingBook.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button2.Location = new System.Drawing.Point(30, 62);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(186, 80);
            this.button2.TabIndex = 1;
            this.button2.Text = "Зареєструвати книгу";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Location = new System.Drawing.Point(15, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1230, 721);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.returnBook);
            this.panel2.Controls.Add(this.lendingBook);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.deletingBook);
            this.panel2.Location = new System.Drawing.Point(1241, 15);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 721);
            this.panel2.TabIndex = 0;
            // 
            // returnBook
            // 
            this.returnBook.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.returnBook.Location = new System.Drawing.Point(30, 404);
            this.returnBook.Margin = new System.Windows.Forms.Padding(4);
            this.returnBook.Name = "returnBook";
            this.returnBook.Size = new System.Drawing.Size(186, 80);
            this.returnBook.TabIndex = 3;
            this.returnBook.Text = "Повернення книг";
            this.returnBook.UseVisualStyleBackColor = false;
            // 
            // lendingBook
            // 
            this.lendingBook.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lendingBook.Location = new System.Drawing.Point(30, 290);
            this.lendingBook.Margin = new System.Windows.Forms.Padding(4);
            this.lendingBook.Name = "lendingBook";
            this.lendingBook.Size = new System.Drawing.Size(186, 80);
            this.lendingBook.TabIndex = 2;
            this.lendingBook.Text = "Видача книг";
            this.lendingBook.UseVisualStyleBackColor = false;
            this.lendingBook.Click += new System.EventHandler(this.lendingBook_Click);
            // 
            // WorkerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 751);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WorkerMain";
            this.Text = "WorkerMain";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deletingBook;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button lendingBook;
        private System.Windows.Forms.Button returnBook;
    }
}