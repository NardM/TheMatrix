using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MatrixExecutor;
using MatrixExecutor.Exeptions;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace MatrixIDE
{
    public partial class Form1 : Form
    {
        Utils util = new Utils();
        Script script;
        string fileName;
        Thread myThread;
        Dictionary<int, FormatException> errorsAndNumberLine;

        public Form1()
        {
            InitializeComponent();
        }

        private void run_Click(object sender, EventArgs e)
        {
            Run.Enabled = false;
            ClearTexts();
            try
            {
                // Свойство для безопасной работы с потоками
                Control.CheckForIllegalCrossThreadCalls = false;
                // Создаем экземпляр класса с функцией, которую необходимо запустить в отдельном потоке
                myThread = new Thread(threadRun);
                // Запускаем поток
                myThread.Start();
            }
            catch (Exception exep)
            {
                this.TErrors.Items.Add("Не известная ошибка " + exep.Message);
                return;
            }
            Run.Enabled = true;
        }

        private void threadRun()
        {
            errorsAndNumberLine = new Dictionary<int, FormatException>();
            script = new Script();
            // Словарь для запоминания ошибки и в какой строке она записана в окне вывода ошибок
            errorsAndNumberLine = new Dictionary<int, FormatException>();
            
            // Извлекаем RichTextBox
            RichTextBox rtb = getRtb(); 
            // Подписка обработчика синтаксических ошибок на соответствующее событие
            script.packetSintaxExeptions += HendlerDisasemblExeption;
            // Разбираем написанный скрипт на операторы
            if (rtb != null)
            {
                rtb.SelectAll();
                rtb.SelectionColor = Color.Black;
                util.writeTempFile(rtb.Text);
            }
            else
            {
                this.TErrors.Items.Add("Файл не открыт");
                return;
            }
            // Подписываем обработчика ошибок на событие, вызываемое при ошибке
            script.packetPerformExeptions += performExeptionHendler;
            // Подписываем обработчик результата на соответствующее событие
            script.packetResultHendlers += resultHendler;
            // Запускаем
            script.SplitFile(Utils.TEMP_FILE);
            script.Run();
        }

        private void Загрузить_Click(object sender, EventArgs e)
        {
            Загрузить.Enabled = false;
            OpenFileDialog openning = new OpenFileDialog();
            openning.InitialDirectory = Application.StartupPath;
            openning.Filter = "Файлы сценариев (*.txt)|*.txt|All files (*.*)|*.*";
            if (openning.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = openning.FileName;
                StreamReader file = new StreamReader(openning.FileName);

                // Получаем имя файла, которое и будет именем вкладки
                string title = util.getFileName(fileName);
                // Добавляеться новая вкладка с содержимым открытого файла
                TabPage myTabPage = new TabPage(title);
                tabControl1.TabPages.Add(myTabPage);
                tabControl1.SelectedTab = myTabPage; // Делаем активной, только что созданную вкладку
                myTabPage.Controls.Add(new RichTextBox());
                var rtb = tabControl1.SelectedTab.Controls.OfType<RichTextBox>().FirstOrDefault();
                if (rtb != null)
                {
                    rtb.Size = new Size(320, 280);
                    rtb.Text = file.ReadToEnd();
                }
                file.Close();
            }
            Загрузить.Enabled = true;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog seving = new SaveFileDialog();
            seving.InitialDirectory = Application.StartupPath;
            seving.Filter = "Файлы сценариев (*.txt)|*.txt|All files (*.*)|*.*";
            if (seving.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = seving.FileName;
                StreamWriter file = new StreamWriter(fileName, false);
                var rtb = tabControl1.SelectedTab.Controls.OfType<RichTextBox>().FirstOrDefault();
                if (rtb != null)
                {
                    file.Write(rtb.Text);
                }
                file.Close();
            }
        }

        private void closeCur_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
        }

        private void TErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            RichTextBox rtb = getRtb();
            rtb.SelectAll();
            rtb.SelectionColor = Color.Black;
            int num = this.TErrors.SelectedIndex;
            try
            {
                SintaxExeption ex = (SintaxExeption)this.errorsAndNumberLine[num];
                util.selectLineWithError(ex.LineNumber, rtb);
            }
            catch (InvalidCastException exep)
            {
                PerformExeption ex = (PerformExeption)this.errorsAndNumberLine[num];
                util.selectLineWithError(ex.LineNumber, rtb);
            }
        }
        Process runInConsole;
        private void button1_Click(object sender, EventArgs e)
        {
            ClearTexts();
            try {
                util.writeTempFile(getRtb().Text);
                RunToConsole.Enabled = false;
            }
            catch (Exception)
            {
                this.TErrors.Items.Add("Файл не открыт");
                return;
            }
            runInConsole = new Process();
            runInConsole.StartInfo.FileName = @"ConsoleView.exe";
            runInConsole.StartInfo.Arguments = Utils.TEMP_FILE;
            // Делает возможным отследить закрытие окна консоли
            runInConsole.EnableRaisingEvents = true;
            // Подписываемся на событие закрытия консоли
            runInConsole.Exited += new EventHandler(P_Exited);
            Control.CheckForIllegalCrossThreadCalls = false;
            runInConsole.Start();
        }

        //Очищаем поля вывода
        private void ClearTexts()
        {
            this.TErrors.Items.Clear();
            this.TResult.Text = String.Empty;
            progressBar1.Value = 0;
            // И задание ему максимального значения и шага изменения
            progressBar1.Step = 1;
            count = 0;
        }

        //следит, закрылась ли консоль
        private void P_Exited(object sender, EventArgs e)
        {
            RunToConsole.Enabled = true;
            StreamReader file = new StreamReader("tempResult.txt");
            this.TResult.Text = file.ReadToEnd();
            file.Close();
        }

        private RichTextBox getRtb()
        {
            RichTextBox rtb = null;
            try
            {
                var rtbVar = tabControl1.SelectedTab.Controls.OfType<RichTextBox>().FirstOrDefault();
                rtb = (RichTextBox)rtbVar;
                return rtb;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        // Обработчик синтаксических ошибок
        void HendlerDisasemblExeption(SintaxExeption e)
        {
            this.TErrors.Items.Add(e.Message);
            errorsAndNumberLine.Add(errorsAndNumberLine.Count, e);
        }

        int count = 0;
        private void performExeptionHendler(PerformExeption e)
        {
            if (count == 0)
            {
                this.TErrors.Items.Add(e.Message);
                errorsAndNumberLine.Add(errorsAndNumberLine.Count, e);
                count++;
            }
        }

        // Обработчик результата
        private void resultHendler(string result)
        {
            if (result.Contains("#"))
            {
                progressBar1.Maximum = util.extractCountOperators(result);
                progressBar1.PerformStep();
            }
            else
            {
                this.TResult.Text += result + "\r\n";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void TResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
