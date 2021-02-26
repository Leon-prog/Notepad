using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notebook
{
    public partial class Form1 : Form
    {
        string fileName = string.Empty;
        public Form1()
        {
            InitializeComponent();

            //задаём фильтр с расширением файла, который хотим открыть или сохранить
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*"; 


        }

        //меню : файл, правка, формат, справка

        #region файл

        private void create_Click(object sender, EventArgs e)
        {
            fileName = string.Empty;
            richTextBox1.Clear();

        }

        private async void save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileName)) // если имя файла указано то просто сохраняем, если имени нет, то создаём файл
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            {
                                await sw.WriteLineAsync(richTextBox1.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }                
            }
            else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fileName))
                    {
                        await sw.WriteLineAsync(richTextBox1.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void save_as_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        fileName = sfd.FileName;
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            await sw.WriteLineAsync(richTextBox1.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())//запускаю файловое окно
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try 
                    {
                        using (StreamReader sr = new StreamReader(dialog.FileName))
                        {
                            fileName = dialog.FileName;
                            Task<string> text = sr.ReadToEndAsync();
                            richTextBox1.Text = text.Result;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region правка
        private void вырезатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            вырезатьToolStripMenuItem_Click(sender, e);
        }

        private void копироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            копироватьToolStripMenuItem_Click(sender, e);
        }

        private void вставитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            вставитьToolStripMenuItem_Click(sender, e);
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            удалитьToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region формат
        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SelectionColor = cd.Color;
                }
            }
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SelectionFont = fd.Font;
                }
            }
        }

        #endregion

        // справка
        private void reference_Click(object sender, EventArgs e)
        {
            using (Form2 form = new Form2())
            {
                form.ShowDialog();
            }

        }

        // вырезать, копировать, вставить, удалить; шрифт, цвет

        #region контекстное меню
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
            richTextBox1.SelectedText = string.Empty;
        }
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
           string pasteText = Clipboard.GetText();
            if(richTextBox1.SelectedText == "")
            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, pasteText);
            else
                richTextBox1.SelectedText = pasteText;

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = string.Empty;
        }
        private void шрифтToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            шрифтToolStripMenuItem_Click(sender, e);
        }

        private void цветToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            цветToolStripMenuItem_Click(sender, e);
        }
        #endregion
    }
}
