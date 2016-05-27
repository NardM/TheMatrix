namespace MatrixIDE
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.TResult = new System.Windows.Forms.TextBox();
            this.LEditor = new System.Windows.Forms.Label();
            this.LResult = new System.Windows.Forms.Label();
            this.LErrors = new System.Windows.Forms.Label();
            this.Загрузить = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Run = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.closeCur = new System.Windows.Forms.Button();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.TErrors = new System.Windows.Forms.ListBox();
            this.RunToConsole = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // TResult
            // 
            this.TResult.Location = new System.Drawing.Point(393, 98);
            this.TResult.Multiline = true;
            this.TResult.Name = "TResult";
            this.TResult.Size = new System.Drawing.Size(214, 263);
            this.TResult.TabIndex = 1;
            this.TResult.TextChanged += new System.EventHandler(this.TResult_TextChanged);
            // 
            // LEditor
            // 
            this.LEditor.AutoSize = true;
            this.LEditor.Location = new System.Drawing.Point(17, 82);
            this.LEditor.Name = "LEditor";
            this.LEditor.Size = new System.Drawing.Size(55, 13);
            this.LEditor.TabIndex = 2;
            this.LEditor.Text = "Редактор";
            // 
            // LResult
            // 
            this.LResult.AutoSize = true;
            this.LResult.Location = new System.Drawing.Point(390, 82);
            this.LResult.Name = "LResult";
            this.LResult.Size = new System.Drawing.Size(59, 13);
            this.LResult.TabIndex = 3;
            this.LResult.Text = "Результат";
            // 
            // LErrors
            // 
            this.LErrors.AutoSize = true;
            this.LErrors.Location = new System.Drawing.Point(17, 419);
            this.LErrors.Name = "LErrors";
            this.LErrors.Size = new System.Drawing.Size(88, 13);
            this.LErrors.TabIndex = 5;
            this.LErrors.Text = "Список ошибок:";
            this.LErrors.Click += new System.EventHandler(this.label1_Click);
            // 
            // Загрузить
            // 
            this.Загрузить.Location = new System.Drawing.Point(20, 24);
            this.Загрузить.Name = "Загрузить";
            this.Загрузить.Size = new System.Drawing.Size(75, 23);
            this.Загрузить.TabIndex = 6;
            this.Загрузить.Text = "Загрузить";
            this.Загрузить.UseVisualStyleBackColor = true;
            this.Загрузить.Click += new System.EventHandler(this.Загрузить_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(118, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(393, 367);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(88, 45);
            this.Run.TabIndex = 8;
            this.Run.Text = "Запустить";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.run_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(20, 98);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(367, 314);
            this.tabControl1.TabIndex = 9;
            // 
            // closeCur
            // 
            this.closeCur.Location = new System.Drawing.Point(212, 24);
            this.closeCur.Name = "closeCur";
            this.closeCur.Size = new System.Drawing.Size(149, 23);
            this.closeCur.TabIndex = 10;
            this.closeCur.Text = "Закр. текущую вкладку";
            this.closeCur.UseVisualStyleBackColor = true;
            this.closeCur.Click += new System.EventHandler(this.closeCur_Click);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // TErrors
            // 
            this.TErrors.FormattingEnabled = true;
            this.TErrors.Location = new System.Drawing.Point(20, 435);
            this.TErrors.Name = "TErrors";
            this.TErrors.Size = new System.Drawing.Size(587, 82);
            this.TErrors.TabIndex = 11;
            this.TErrors.SelectedIndexChanged += new System.EventHandler(this.TErrors_SelectedIndexChanged);
            // 
            // RunToConsole
            // 
            this.RunToConsole.Location = new System.Drawing.Point(487, 367);
            this.RunToConsole.Name = "RunToConsole";
            this.RunToConsole.Size = new System.Drawing.Size(120, 45);
            this.RunToConsole.TabIndex = 12;
            this.RunToConsole.Text = "Запустить в \r\nконсоли";
            this.RunToConsole.UseVisualStyleBackColor = true;
            this.RunToConsole.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 523);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(587, 10);
            this.progressBar1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 565);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.RunToConsole);
            this.Controls.Add(this.TErrors);
            this.Controls.Add(this.closeCur);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Загрузить);
            this.Controls.Add(this.LErrors);
            this.Controls.Add(this.LResult);
            this.Controls.Add(this.LEditor);
            this.Controls.Add(this.TResult);
            this.Name = "Form1";
            this.Text = "+";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox TResult;
        private System.Windows.Forms.Label LEditor;
        private System.Windows.Forms.Label LResult;
        private System.Windows.Forms.Label LErrors;
        private System.Windows.Forms.Button Загрузить;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button closeCur;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.ListBox TErrors;
        private System.Windows.Forms.Button RunToConsole;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

