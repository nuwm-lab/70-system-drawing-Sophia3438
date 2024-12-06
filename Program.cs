using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        Graphics graph;
        bool graph_displayed;

        public Form1()
        {
            InitializeComponent();
            graph = CreateGraphics();
            graph_displayed = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graph_displayed = true;
            this.Invalidate();  // Перерисовує форму, щоб змалювати графік

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (graph_displayed)
            {
                button1_Click(sender, e);  // Якщо графік вже намальовано, перерисовуємо його при зміні розміру
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (graph_displayed)
            {
                // Отримуємо графічний об'єкт для малювання
                graph = e.Graphics;
                graph.Clear(Color.FromName("Control"));  // Очищаємо фон

                Color slateBlue = Color.FromName("SlateBlue");
                float width = 3;
                Pen pen = new Pen(slateBlue, width);  // Створюємо ручку для малювання ліній

                float xMin = 4.8f;
                float yMin = (float)Math.Pow((3 * xMin + 2), 2) / (float)(Math.Sin(xMin) + 3);
                float y = yMin, y2, x = xMin;

                float sizeX = (this.Width - 60) / (7.9f - 4.8f);  // Масштаб для осі X
                float sizeY = (this.Height - 50) / ((float)Math.Pow((3 * 7.9 + 2), 2) / (float)(Math.Sin(7.9) + 3) - y);  // Масштаб для осі Y

                // Малюємо осі X та Y
                graph.DrawLine(pen, 50, this.Height - 50, this.Width - 20, this.Height - 50);  // Ось X
                graph.DrawLine(pen, 50, this.Height - 50, 50, 20);  // Ось Y

                // Додаємо підпис для осі X
                for (float i = 4.8f; i <= 7.9f; i += 0.4f)
                {
                    float xLabel = (float)((i - xMin) * sizeX) + 50;
                    graph.DrawString(i.ToString(), this.Font, Brushes.Black, xLabel, this.Height - 50);  // Підпис для осі X
                }

                // Додаємо підпис для осі Y
                for (float i = (float)yMin; i <= (float)(Math.Pow((3 * 7.9 + 2), 2) / (Math.Sin(7.9) + 3)); i += 3)
                {
                    float yLabel = this.Height - (float)((i - yMin) * sizeY) - 50;
                    graph.DrawString(i.ToString(), this.Font, Brushes.Black, 20, yLabel);  // Підпис для осі Y
                }

                // Малюємо графік
                for (float x2 = 4.8f; x2 <= 7.9f; x2 += 0.4f)
                {
                    y2 = (float)Math.Pow((3 * x2 + 2), 2) / (float)(Math.Sin(x2) + 3);

                    graph.DrawLine(pen, (x - xMin) * sizeX + 60, this.Height - (y - yMin) * sizeY - 50, (x2 - xMin) * sizeX + 60, this.Height - (y2 - yMin) * sizeY - 50);

                    x = x2;
                    y = y2;
                }
            }
        }
    }
}
