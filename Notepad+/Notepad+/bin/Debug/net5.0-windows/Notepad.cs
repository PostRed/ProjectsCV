using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Notepad_
{
    public partial class Note : Form
    {
        /// <summary>
        /// Инициализация, полям присваиваются значения.
        /// Вызывается метод, который смотрит дефолтные настройки.
        /// </summary>
        public Note()
        {
            InitializeComponent();
            pages = new List<string>();
            textBoxes = new List<RichTextBox>();
            startTexts = new List<string>();
            startTextsChange = new List<bool>();
            startTextsChange.Add(false);
            autoSaveBox.SelectedIndexChanged += AutoSave;
            CurrentTextPage1.ContextMenuStrip = contextMenu;
            timer = new Timer();
            filePathes = new List<string>();
            CurrentTextPage1.Dock = DockStyle.Fill;
            tabControlPages.Dock = DockStyle.Fill;
            pages.Add("Page1");
            textBoxes.Add(CurrentTextPage1);
            UnpakingSettings();
            color = Properties.Settings.Default.defaultColor;
            ColorTheme(Properties.Settings.Default.defaultColor);
            if (Properties.Settings.Default.timerTick > 0)
            {
                TimerSettings(Properties.Settings.Default.timerTick);
            }
        }


        /// <summary>
        /// Присваивает форме дефолтные настройки.
        /// </summary>
        private void UnpakingSettings()
        {
            try
            {
                int number = 0;
                if (Properties.Settings.Default.defaultPages != null && Properties.Settings.Default.defaultPages.Count != 0)
                {
                    foreach (string path in Properties.Settings.Default.defaultPages)
                    {
                        if (File.Exists(path))
                        {
                            if (number != 0)
                            {
                                NewPage();
                                tabControlPages.SelectedTab = tabControlPages.TabPages[^1];

                            }
                            else
                            {
                                filePathes.Add(path);
                                startTexts.Add("");
                            }
                            OpenFileWithPath(path);
                            number++;
                        }
                    }
                }
                if (startTexts.Count == 0)
                {
                    startTexts.Add("");
                    filePathes.Add("");
                }
            }
            catch
            {
                MessageBox.Show("Востановить настройки не получилось, очень жаль(");
            }
        }


        /// <summary>
        /// Главный цвет темы.
        /// </summary>
        private Color color;


        /// <summary>
        ///  Пути к файлам для каждой вкладки.
        /// </summary>
        private List<string> filePathes;


        /// <summary>
        ///  Начальный текст для каждой вкладки.
        /// </summary>
        private List<string> startTexts;


        /// <summary>
        /// Был ли изменет текст на каждой вкладке.
        /// </summary>
        private List<bool> startTextsChange;


        /// <summary>
        /// Список вкладок, которые создает пользователь.
        /// </summary>
        private List<string> pages;


        /// <summary>
        /// Текста с каждой вкладки.
        /// </summary>
        private List<RichTextBox> textBoxes;


        /// <summary>
        ///  Значение таймера.
        /// </summary>
        private Timer timer;



        /// <summary>
        /// Горячим клавишам присваиваются значения.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void NotepadKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.S))
            {
                SaveFile();
            }
            else if (e.KeyData == (Keys.Control | Keys.N))
            {
                CreateInNewPage(sender, e);
            }
            else if (e.KeyData == (Keys.Control | Keys.S | Keys.Shift))
            {
                SaveAll(sender, e);
            }
            else if (e.KeyData == (Keys.Control | Keys.N | Keys.Shift))
            {
                CreateInNewWindow(sender, e);
            }
            else if (e.KeyData == (Keys.Control | Keys.Z))
            {
                Application.Exit();
            }

            else if (e.KeyData == (Keys.Control | Keys.D))
            {
                DeletePage();
            }
        }


        /// <summary>
        ///  Автосохранение и установка периода автосохранения.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void AutoSave(object sender, EventArgs e)
        {
            try
            {
                int sec;
                if (autoSaveBox.SelectedItem != null && int.TryParse(autoSaveBox.SelectedItem.ToString(), out sec))
                {
                    SaveAll(sender, e);
                    timer.Interval = sec * 1000;
                    timer.Start();
                    timer.Tick += new EventHandler(this.SaveAll);
                }
            }
            catch
            {
                MessageBox.Show("Не удалось настроить автосохранение(");
            }
        }



        /// <summary>
        /// Настраивает таймер по дефолтным настройкам.
        /// </summary>
        /// <param name="timerTick"> Дфолтный интервал авто сохранения. </param>
        private void TimerSettings(int timerTick)
        {
            timer.Interval = timerTick;
            timer.Start();
            timer.Tick += new EventHandler(this.SaveAll);
        }

        /// <summary>
        /// Создание файла.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void CreateFile(object sender, EventArgs e)
        {
            bool returnText = WantToSave();
            if (!returnText)
            {
                CurrentTextBox.Text = "";
                filePathes[NumberPage] = "";
                startTexts[NumberPage] = "";
            }
        }


        /// <summary>
        /// Вызов формы с инструкцией.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Info(object sender, EventArgs e)
        {
            Instruction info = new Instruction();
            info.BackColor = color;
            info.Show();
        }



        /// <summary>
        /// Создание нового окна.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void NewWindow(object sender, EventArgs e)
        {
            try
            {
                Note newForm = new Note();
                newForm.Show();
            }
            catch
            {
                MessageBox.Show("Не удалось создать новое окно, очень жаль(");
            }
        }



        /// <summary>
        /// Создание файла в новом окне.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void CreateInNewWindow(object sender, EventArgs e)
        {
            NewWindow(sender, e);
            CreateFile(sender, e);
        }


        /// <summary>
        /// Сохраняет все открытые файлы.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void SaveAll(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage page in tabControlPages.TabPages)
                {
                    tabControlPages.SelectedTab = page;
                    SaveFile();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить все файлы(");
            }
        }

        /// <summary>
        /// Открытие файла в новом окне.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void OpenInNewWindow(object sender, EventArgs e)
        {
            NewWindow(sender, e);
            OpenFileClick(sender, e);
        }


        /// <summary>
        /// По активной вкладке выводит текст.
        /// </summary>
        /// <returns> Текст с активной вкладки. </returns>
        private RichTextBox CurrentTextBoxIndex()
        {
            int index = Array.IndexOf(pages.ToArray(), tabControlPages.SelectedTab.Name);
            Console.WriteLine(tabControlPages.SelectedTab.Name);
            return textBoxes[index];
        }


        /// <summary>
        /// Возвращает номер в списке вкладок активной вкладки.
        /// </summary>
        private int NumberPage => Array.IndexOf(pages.ToArray(), tabControlPages.SelectedTab.Name);


        /// <summary>
        /// Создание новой вкладки.
        /// Создание в ней текстбокса.
        /// </summary>
        private void NewPage()
        {
            try
            {
                string name = "Page" + (tabControlPages.TabCount + 1).ToString();
                TabPage newTabPage = new TabPage(name);
                newTabPage.Dock = DockStyle.Fill;
                newTabPage.Name = name;
                newTabPage.BackColor = text.BackColor;
                tabControlPages.TabPages.Add(newTabPage);
                pages.Add(name);
                string nameTextBox = "CurrentText" + name;
                RichTextBox newTextBox = new RichTextBox();
                newTextBox.Dock = DockStyle.Fill;
                newTextBox.BackColor = newTabPage.BackColor;
                newTextBox.Name = nameTextBox;
                newTextBox.Parent = newTabPage;
                filePathes.Add("");
                newTextBox.ContextMenuStrip = contextMenu;
                tabControlPages.SelectedTab = newTabPage;
                textBoxes.Add(newTextBox);
                startTexts.Add("");
                startTextsChange.Add(false);
            }
            catch
            {
                MessageBox.Show("Создать новую вкладку не получилось, очень жаль(");
            }
        }



        /// <summary>
        ///  ОТкрытие файла в новой вкладке.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void OpenInNewPage(object sender, EventArgs e)
        {
            NewPage();
            OpenFileClick(sender, e);
        }




        /// <summary>
        /// Создание файла в новой вкладке.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void CreateInNewPage(object sender, EventArgs e)
        {
            NewPage();
            CreateFile(sender, e);
        }


        /// <summary>
        ///  Получение текстбокса с активной вкладки.
        /// </summary>
        private RichTextBox CurrentTextBox => CurrentTextBoxIndex();


        /// <summary>
        ///  Вырезать фрагмент.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Сut(object sender, EventArgs e)
        {
            CurrentTextBox.Cut();
        }


        /// <summary>
        ///  Копировать фрагмент.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Copy(object sender, EventArgs e)
        {
            CurrentTextBox.Copy();
        }


        /// <summary>
        ///  Вставить фрагмент.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Paste(object sender, EventArgs e)
        {
            CurrentTextBox.Paste();
        }


        /// <summary>
        ///  Выделить весь текст.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void SelectAllText(object sender, EventArgs e)
        {
            CurrentTextBox.SelectAll();
        }


        /// <summary>
        ///  Сохранение файла в формате txt.
        /// </summary>
        private void SaveTxt()
        {
            try
            {
                File.WriteAllText(filePathes[NumberPage], CurrentTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить txt файл(");
            }
        }


        /// <summary>
        /// Сохранение файла в формате rtf.
        /// </summary>
        private void SaveRtf()
        {
            try
            {
                CurrentTextBox.SaveFile(filePathes[NumberPage], RichTextBoxStreamType.RichText);
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить rtf файл(");
            }
        }


        /// <summary>
        /// Вызов метода, который отвечает за сохранение файла.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void SaveClick(object sender, EventArgs e)
        {
            Save();
        }


        /// <summary>
        ///  Сохранение файла в нужном формате.
        ///  Проверка на корректность.
        /// </summary>
        private void Save()
        {
            try
            {
                string text = CurrentTextBox.Text;
                startTexts[NumberPage] = text;
                if (filePathes[NumberPage][^3..] == "rtf")
                {
                    SaveRtf();
                }
                else
                {
                    SaveTxt();
                }

            }
            catch
            {
                MessageBox.Show("Сохранить файл не получилось, очень жаль(");
            }
        }

        /// <summary>
        /// Изменяет стиль шрифта на нужный.
        /// </summary>
        /// <param name="fontStyle"> Стиль шрифта, на который изменяется текущий стиль шрифта. </param>
        private void Format(FontStyle fontStyle)
        {
            startTextsChange[NumberPage] = true;
            if (CurrentTextBox.SelectedText == "")
            {
                CurrentTextBox.Font = new Font(CurrentTextBox.Font, fontStyle | CurrentTextBox.Font.Style);

            }
            else
            {
                try
                {
                    CurrentTextBox.SelectionFont = new Font(CurrentTextBox.SelectionFont,
                    fontStyle | CurrentTextBox.SelectionFont.Style);
                }
                catch (Exception)
                {
                    CurrentTextBox.SelectionFont = new Font(CurrentTextBox.SelectionFont,
                    fontStyle);
                }
            }

        }


        /// <summary>
        ///  Сделать Жирный шрифт.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Bold(object sender, EventArgs e)
        {
            Format(FontStyle.Bold);
        }


        /// <summary>
        /// Сделать подчеркнутый шрифт.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void UnderLine(object sender, EventArgs e)
        {
            Format(FontStyle.Underline);
        }


        /// <summary>
        /// Сделать зачеркнутый шрифт.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Line(object sender, EventArgs e)
        {
            Format(FontStyle.Strikeout);
        }


        /// <summary>
        /// Сделать курсив.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Italics(object sender, EventArgs e)
        {
            Format(FontStyle.Italic);
        }


        /// <summary>
        /// Меняет сам шрифт на нужный.
        /// </summary>
        /// <param name="fontFamily"> шрифт, на который меняется основной шрифт. </param>
        private void TypeFont(FontFamily fontFamily)
        {
            startTextsChange[NumberPage] = true;
            if (CurrentTextBox.SelectedText == "")
            {
                CurrentTextBox.Font = new Font(fontFamily, 10, CurrentTextBox.Font.Style);
            }
            else
            {
                CurrentTextBox.SelectionFont = new Font(fontFamily, 10, CurrentTextBox.SelectionFont.Style);
            }
        }


        /// <summary>
        /// Сменить шрифт на дефолтный "Segoe UI".
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void SegoeUI(object sender, EventArgs e)
        {
            FontFamily fontFamily = new FontFamily("Segoe UI");
            TypeFont(fontFamily);
        }


        /// <summary>
        ///  Сменить шрифт на "Arial".
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Arial(object sender, EventArgs e)
        {
            FontFamily fontFamily = new FontFamily("Arial");
            TypeFont(fontFamily);
        }


        /// <summary>
        ///  Сменить шрифт на "Times New Roman".
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void TimesNewRoman(object sender, EventArgs e)
        {
            FontFamily fontFamily = new FontFamily("Times New Roman");
            TypeFont(fontFamily);
        }


        /// <summary>
        /// Сменить тему на розовую.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Pig(object sender, EventArgs e)
        {
            ColorTheme(Color.LightPink);
        }

        /// <summary>
        /// Сменить тему на голубую.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void SkyBlue(object sender, EventArgs e)
        {
            ColorTheme(Color.LightCyan);
        }


        /// <summary>
        ///  Изменение цвета всех элементов меню.
        /// </summary>
        /// <param name="toolStripItemCollection"> Коллекция меню. </param>
        /// <param name="theme"> Цвет темы. </param>
        private void AllMenuInColor(ToolStripItemCollection toolStripItemCollection, Color theme)
        {
            foreach (ToolStripItem itm in toolStripItemCollection)
            {
                itm.BackColor = theme;
                ToolStripDropDownItem dropDown = itm as ToolStripDropDownItem;
                if (dropDown != null)
                    AllMenuInColor(dropDown.DropDownItems, theme);
            }
        }


        /// <summary>
        /// Изменение цвета темы.
        /// </summary>
        /// <param name="mainColor"> Главный цвет. </param>
        private void ColorTheme(Color mainColor)
        {
            text.BackColor = mainColor;
            foreach (RichTextBox rtb in textBoxes)
            {
                rtb.BackColor = mainColor;
            }
            color = mainColor;
            AllMenuInColor(text.Items, mainColor);
        }


        /// <summary>
        ///  Сменить тему на белую.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void White(object sender, EventArgs e)
        {
            ColorTheme(Color.White);
        }


        /// <summary>
        /// Сделать стиль шрифта обычным.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void Normal(object sender, EventArgs e)
        {
            if (CurrentTextBox.SelectedText == "")
            {
                CurrentTextBox.Font = new Font(CurrentTextBox.Font, FontStyle.Regular);
            }
            else
            {
                CurrentTextBox.SelectionFont = new Font(CurrentTextBox.SelectionFont, FontStyle.Regular);
            }
        }


        /// <summary>
        ///  Вызывает метод, который отвечает за сохранение файла.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void SaveFileClick(object sender, EventArgs e)
        {
            SaveFile();
        }


        /// <summary>
        ///  Сохранение файла, если указан путь для сохранения.
        ///  Иначе вызывается метод, который просит ввести путь для сохранения файла.
        /// </summary>
        private void SaveFile()
        {

            if (filePathes[NumberPage] != "")
            {
                Save();
            }
            else
            {
                SaveAsFile();
            }
        }


        /// <summary>
        /// Вызывает метод, который отвечает за сохранение файла с выбором пути.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void SaveAsFileClick(object sender, EventArgs e)
        {
            SaveAsFile();
        }



        /// <summary>
        /// Просит выбрать путь для сохранения файла и сохраняет файл. 
        /// </summary>
        private void SaveAsFile()
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        filePathes[NumberPage] = saveFileDialog.FileName;
                        Save();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить файл, очень жаль(");
            }
        }


        /// <summary>
        /// Если пользователь передумал закрывать приложение, происходит отмена закрытия.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void SaveWithClose(object sender, FormClosingEventArgs e)
        {
            bool returnText = WantToSave();
            if (returnText)
            {
                e.Cancel = true;
            }
        }


        /// <summary>
        /// Выясняет, хочет ли пользователь сохранить изменения в файле.
        /// </summary>
        /// Если пользователь выбрал "отмена", то изменения не происходят.
        /// <returns> Выбрал ли пользователь "Отмена". </returns>
        private bool WantToSave()
        {
            bool returnText = false;
            if (CurrentTextBox.Text != startTexts[NumberPage] || startTextsChange[NumberPage] == true)
            {
                DialogResult message = MessageBox.Show("Хотите сохранить файл?", "В Файле есть несохраненные изменения!",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (message == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (message == DialogResult.Cancel)
                {
                    returnText = true;
                }
            }
            return returnText;
        }


        /// <summary>
        /// Открывает файл txt.
        /// </summary>
        private void OpenTxt()
        {
            try
            {
                string text;
                text = File.ReadAllText(filePathes[NumberPage]);
                startTexts[NumberPage] = text;
                CurrentTextBox.Text = text;
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить txt файл(");
            }
        }


        /// <summary>
        /// Открывает файл rtf.
        /// </summary>
        private void OpenRtf()
        {
            CurrentTextBox.LoadFile(filePathes[NumberPage], RichTextBoxStreamType.RichText);
            startTexts[NumberPage] = CurrentTextBox.Text;
        }


        /// <summary>
        /// Открывает файл по заданному пути.
        /// </summary>
        /// <param name="path"> Заданный путь. </param>
        private void OpenFileWithPath(string path)
        {
            try
            {
                filePathes[NumberPage] = path;

                if (path[^3..] == "rtf")
                {
                    OpenRtf();
                }
                else
                {
                    OpenTxt();
                }
            }
            catch
            {
                MessageBox.Show("Открыть файл не получилось, очень жаль(");
            }
        }


        /// <summary>
        /// Открытие файлов.
        /// Проверка на корректность.
        /// </summary>
        private void OpenFile()
        {
            bool returnText = WantToSave();
            if (!returnText)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePathes[NumberPage] = openFileDialog.FileName;
                        try
                        {

                            if (filePathes[NumberPage][^3..] == "rtf")
                            {
                                OpenRtf();
                            }
                            else
                            {
                                OpenTxt();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Открыть файл не получилось, очень жаль(");
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Вызывает метод, который отвечает за открытие файлов.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void OpenFileClick(object sender, EventArgs e)
        {
            OpenFile();
        }


        /// <summary>
        /// Сохранение текущих настроек.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void FormSettings(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.defaultPages = new StringCollection();
                List<string> defaultP = new List<string>();
                foreach (string path in filePathes)
                {
                    if (path != "")
                    {
                        defaultP.Add(path);
                    }
                }
                if (defaultP.Count != 0)
                {
                    Properties.Settings.Default.defaultPages.AddRange(filePathes.ToArray());
                }
                Properties.Settings.Default.defaultColor = color;
                if (timer.Enabled)
                {
                    Properties.Settings.Default.timerTick = timer.Interval;
                }
                else
                {
                    Properties.Settings.Default.timerTick = 0;
                }
                Properties.Settings.Default.Save();
                MessageBox.Show("Настройки сохранены!");
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить текущие настройки(");
            }
        }


        /// <summary>
        /// Удаляет текущую вкладку и все элементы, связанные с ней.
        /// Если не осталось открытых вкладок - создаёт новую устую вкладку.
        /// </summary>
        private void DeletePage()
        {
            try
            {
                bool returnText = WantToSave();
                if (!returnText)
                {
                    int index = NumberPage;
                    tabControlPages.TabPages.RemoveAt(index);
                    pages.RemoveAt(index);
                    filePathes.RemoveAt(index);
                    startTexts.RemoveAt(index);
                    startTextsChange.RemoveAt(index);
                    if (pages.Count == 0)
                    {
                        NewPage();
                    }
                }               
            }
            catch
            {
                MessageBox.Show("Не удалось удалить страницу(");
            }
        }


        /// <summary>
        /// Сбрасывает автосохранение.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void StopTimer(object sender, EventArgs e)
        {
            timer.Stop();
        }


        /// <summary>
        /// Вызывает метод, который удаляет вкладку.
        /// </summary>
        /// <param name="sender"> Объект, вызвавший событие. </param>
        /// <param name="e"> Параметр, содержащий данные о событии. </param>
        private void DeletePageClick(object sender, EventArgs e)
        {
            DeletePage();
        }
    }
}
