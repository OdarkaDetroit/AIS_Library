
namespace Library.Worker
{
    partial class BookRegistration
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
            this.genreGrid = new System.Windows.Forms.DataGridView();
            this.itemGrid = new System.Windows.Forms.DataGridView();
            this.bookGrid = new System.Windows.Forms.DataGridView();
            this.authorGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.authorAdding = new System.Windows.Forms.Button();
            this.authorEdit = new System.Windows.Forms.Button();
            this.genreEdit = new System.Windows.Forms.Button();
            this.genreAdding = new System.Windows.Forms.Button();
            this.bookEdit = new System.Windows.Forms.Button();
            this.bookAdding = new System.Windows.Forms.Button();
            this.itemEdit = new System.Windows.Forms.Button();
            this.itemAdding = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.allAuthors = new System.Windows.Forms.Button();
            this.allGenres = new System.Windows.Forms.Button();
            this.allBooks = new System.Windows.Forms.Button();
            this.allItems = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.genreGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // genreGrid
            // 
            this.genreGrid.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.genreGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.genreGrid.Location = new System.Drawing.Point(641, 65);
            this.genreGrid.Name = "genreGrid";
            this.genreGrid.RowHeadersWidth = 51;
            this.genreGrid.RowTemplate.Height = 29;
            this.genreGrid.Size = new System.Drawing.Size(590, 190);
            this.genreGrid.TabIndex = 1;
            // 
            // itemGrid
            // 
            this.itemGrid.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.itemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemGrid.Location = new System.Drawing.Point(641, 362);
            this.itemGrid.Name = "itemGrid";
            this.itemGrid.RowHeadersWidth = 51;
            this.itemGrid.RowTemplate.Height = 29;
            this.itemGrid.Size = new System.Drawing.Size(590, 190);
            this.itemGrid.TabIndex = 2;
            // 
            // bookGrid
            // 
            this.bookGrid.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.bookGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bookGrid.Location = new System.Drawing.Point(25, 362);
            this.bookGrid.Name = "bookGrid";
            this.bookGrid.RowHeadersWidth = 51;
            this.bookGrid.RowTemplate.Height = 29;
            this.bookGrid.Size = new System.Drawing.Size(590, 190);
            this.bookGrid.TabIndex = 3;
            // 
            // authorGrid
            // 
            this.authorGrid.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.authorGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.authorGrid.Location = new System.Drawing.Point(25, 65);
            this.authorGrid.Name = "authorGrid";
            this.authorGrid.RowHeadersWidth = 51;
            this.authorGrid.RowTemplate.Height = 29;
            this.authorGrid.Size = new System.Drawing.Size(590, 190);
            this.authorGrid.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(25, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 31);
            this.label1.TabIndex = 5;
            this.label1.Text = "Перегляд авторів";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(641, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 31);
            this.label2.TabIndex = 6;
            this.label2.Text = "Перегляд жанрів";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(25, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 31);
            this.label3.TabIndex = 7;
            this.label3.Text = "Перегляд книг";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(641, 328);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 31);
            this.label4.TabIndex = 8;
            this.label4.Text = "Перегляд примірників";
            // 
            // authorAdding
            // 
            this.authorAdding.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.authorAdding.Location = new System.Drawing.Point(25, 271);
            this.authorAdding.Name = "authorAdding";
            this.authorAdding.Size = new System.Drawing.Size(129, 29);
            this.authorAdding.TabIndex = 9;
            this.authorAdding.Text = "Додати автора";
            this.authorAdding.UseVisualStyleBackColor = false;
            // 
            // authorEdit
            // 
            this.authorEdit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.authorEdit.Location = new System.Drawing.Point(170, 271);
            this.authorEdit.Name = "authorEdit";
            this.authorEdit.Size = new System.Drawing.Size(129, 29);
            this.authorEdit.TabIndex = 10;
            this.authorEdit.Text = "Редагувати автора";
            this.authorEdit.UseVisualStyleBackColor = false;
            // 
            // genreEdit
            // 
            this.genreEdit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.genreEdit.Location = new System.Drawing.Point(786, 271);
            this.genreEdit.Name = "genreEdit";
            this.genreEdit.Size = new System.Drawing.Size(153, 29);
            this.genreEdit.TabIndex = 12;
            this.genreEdit.Text = "Редагувати автора";
            this.genreEdit.UseVisualStyleBackColor = false;
            // 
            // genreAdding
            // 
            this.genreAdding.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.genreAdding.Location = new System.Drawing.Point(641, 271);
            this.genreAdding.Name = "genreAdding";
            this.genreAdding.Size = new System.Drawing.Size(129, 29);
            this.genreAdding.TabIndex = 11;
            this.genreAdding.Text = "Додати жанр";
            this.genreAdding.UseVisualStyleBackColor = false;
            // 
            // bookEdit
            // 
            this.bookEdit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bookEdit.Location = new System.Drawing.Point(170, 567);
            this.bookEdit.Name = "bookEdit";
            this.bookEdit.Size = new System.Drawing.Size(155, 29);
            this.bookEdit.TabIndex = 14;
            this.bookEdit.Text = "Редагувати книгу";
            this.bookEdit.UseVisualStyleBackColor = false;
            // 
            // bookAdding
            // 
            this.bookAdding.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bookAdding.Location = new System.Drawing.Point(25, 567);
            this.bookAdding.Name = "bookAdding";
            this.bookAdding.Size = new System.Drawing.Size(129, 29);
            this.bookAdding.TabIndex = 13;
            this.bookAdding.Text = "Додати книгу";
            this.bookAdding.UseVisualStyleBackColor = false;
            // 
            // itemEdit
            // 
            this.itemEdit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.itemEdit.Location = new System.Drawing.Point(826, 567);
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.Size = new System.Drawing.Size(188, 29);
            this.itemEdit.TabIndex = 16;
            this.itemEdit.Text = "Редагувати примірник";
            this.itemEdit.UseVisualStyleBackColor = false;
            // 
            // itemAdding
            // 
            this.itemAdding.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.itemAdding.Location = new System.Drawing.Point(641, 567);
            this.itemAdding.Name = "itemAdding";
            this.itemAdding.Size = new System.Drawing.Size(169, 29);
            this.itemAdding.TabIndex = 15;
            this.itemAdding.Text = "Додати примірник";
            this.itemAdding.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.allItems);
            this.panel1.Controls.Add(this.allBooks);
            this.panel1.Controls.Add(this.allGenres);
            this.panel1.Controls.Add(this.allAuthors);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1233, 616);
            this.panel1.TabIndex = 17;
            // 
            // allAuthors
            // 
            this.allAuthors.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.allAuthors.Location = new System.Drawing.Point(306, 258);
            this.allAuthors.Name = "allAuthors";
            this.allAuthors.Size = new System.Drawing.Size(129, 29);
            this.allAuthors.TabIndex = 18;
            this.allAuthors.Text = "Оновити таблицю";
            this.allAuthors.UseVisualStyleBackColor = false;
            this.allAuthors.Click += new System.EventHandler(this.allAuthors_Click);
            // 
            // allGenres
            // 
            this.allGenres.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.allGenres.Location = new System.Drawing.Point(944, 258);
            this.allGenres.Name = "allGenres";
            this.allGenres.Size = new System.Drawing.Size(129, 29);
            this.allGenres.TabIndex = 19;
            this.allGenres.Text = "Оновити таблицю";
            this.allGenres.UseVisualStyleBackColor = false;
            this.allGenres.Click += new System.EventHandler(this.allGenres_Click);
            // 
            // allBooks
            // 
            this.allBooks.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.allBooks.Location = new System.Drawing.Point(332, 554);
            this.allBooks.Name = "allBooks";
            this.allBooks.Size = new System.Drawing.Size(129, 29);
            this.allBooks.TabIndex = 20;
            this.allBooks.Text = "Оновити таблицю";
            this.allBooks.UseVisualStyleBackColor = false;
            this.allBooks.Click += new System.EventHandler(this.allBooks_Click);
            // 
            // allItems
            // 
            this.allItems.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.allItems.Location = new System.Drawing.Point(1022, 554);
            this.allItems.Name = "allItems";
            this.allItems.Size = new System.Drawing.Size(129, 29);
            this.allItems.TabIndex = 21;
            this.allItems.Text = "Оновити таблицю";
            this.allItems.UseVisualStyleBackColor = false;
            this.allItems.Click += new System.EventHandler(this.allItems_Click);
            // 
            // AddBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 641);
            this.Controls.Add(this.itemEdit);
            this.Controls.Add(this.itemAdding);
            this.Controls.Add(this.bookEdit);
            this.Controls.Add(this.bookAdding);
            this.Controls.Add(this.genreEdit);
            this.Controls.Add(this.genreAdding);
            this.Controls.Add(this.authorEdit);
            this.Controls.Add(this.authorAdding);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.authorGrid);
            this.Controls.Add(this.bookGrid);
            this.Controls.Add(this.itemGrid);
            this.Controls.Add(this.genreGrid);
            this.Controls.Add(this.panel1);
            this.Name = "AddBook";
            this.Text = "AddBook";
            ((System.ComponentModel.ISupportInitialize)(this.genreGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView genreGrid;
        private System.Windows.Forms.DataGridView itemGrid;
        private System.Windows.Forms.DataGridView bookGrid;
        private System.Windows.Forms.DataGridView authorGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button authorAdding;
        private System.Windows.Forms.Button authorEdit;
        private System.Windows.Forms.Button genreEdit;
        private System.Windows.Forms.Button genreAdding;
        private System.Windows.Forms.Button bookEdit;
        private System.Windows.Forms.Button bookAdding;
        private System.Windows.Forms.Button itemEdit;
        private System.Windows.Forms.Button itemAdding;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button allAuthors;
        private System.Windows.Forms.Button allItems;
        private System.Windows.Forms.Button allBooks;
        private System.Windows.Forms.Button allGenres;
    }
}