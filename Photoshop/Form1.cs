using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static Photoshop.Filters;


namespace Photoshop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            labelError.Visible = false;
        }

        private void открытьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif";
                    saveFileDialog.Title = "Сохранить изображение как...";
                    saveFileDialog.FileName = "processed_image.jpg";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        pictureBox1.Image.Save(filePath);
                        MessageBox.Show("Изображение успешно сохранено.", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Изображение не выбрано.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBrightness_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                try
                {
                    CheckString(textBox1.Text);
                    int brightness = int.Parse(textBox1.Text);
                    pictureBox1.Image = Filters.AdjustBrightness((Bitmap)pictureBox1.Image, brightness);
                    labelError.Visible = false;
                }
                catch (Exception ex)
                {
                    labelError.Text = ex.Message;
                    labelError.Visible = true;
                }
            }
        }

        private void buttonContrast_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                try
                {
                    CheckString(textBox2.Text);
                    float contrast = float.Parse(textBox2.Text);
                    pictureBox1.Image = Filters.AdjustContrast((Bitmap)pictureBox1.Image, contrast);
                    labelError.Visible = false;
                }
                catch (Exception ex)
                {
                    labelError.Text = ex.Message;
                    labelError.Visible = true;
                }
            }
        }

        private void buttonBlur_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                try
                {
                    CheckString(textBox3.Text);
                    int blurRadius = int.Parse(textBox3.Text);
                    pictureBox1.Image = Filters.ApplyBlur((Bitmap)pictureBox1.Image, blurRadius);
                    labelError.Visible = false;
                }
                catch (Exception ex)
                {
                    labelError.Text = ex.Message;
                    labelError.Visible = true;
                }
            }
        }

        static void CheckString(string inputString)
        {
            string pattern = @"^[-\d.,]+$";

            if (!Regex.Match(inputString, pattern).Success)
            {
                throw new Exception("Строка содержит недопустимые символы.");
            }
        }
    }
}
