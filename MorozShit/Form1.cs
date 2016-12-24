using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MorozShit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static unsafe Double InvSqrt(float x)
        {
            float HalfNumber = x * (float)0.5;
            int i = *(int*)&x;
            i = 0x5f3759df - (i >> 1);
            x = *(float*)&i;
            x = x * ((float)1.5 - HalfNumber * x * x);
            x = x * ((float)1.5 - HalfNumber * x * x);
            return x;
        }
        private Double FindMin(Double start, Double end, Double step, Int32 Accuracy)
        {

            //tb3.Text = start.ToString();
            tb4.Text = (Convert.ToInt32(tb4.Text) + 1).ToString();
            Double perdole = ((double)1.00000000 / Math.Pow(10, Accuracy));
            if (step < perdole)
                return start;
            Double min = 10000, tmp, minid = 0;
            for (Double i = start; i < end; i += step)
            {
                tmp = InvSqrt((float)i);
                if (tmp < min)
                {
                    min = tmp; minid = i;
                }
            }
            tb5.Text = tb5.Text + minid + " : " + min + "\r\n";
            return FindMin(minid - step, minid + step, step / 10, Accuracy);
        }
        private Double FindMinDiff(Double start, Double end, Double step, Int32 Accuracy)
        {

            //tb3.Text = start.ToString();
            try
            {
                tb4.Text = (Convert.ToInt32(tb4.Text) + 1).ToString();
            }
            catch (Exception ex) { }
            Double perdole = ((double)1.00000000 / Math.Pow(10, Accuracy));
            if (step < perdole)
                return start;
            Double min = 10000, tmp, minid = 0;
            for (Double i = start; i < end; i += step)
            {
                tmp = (1 / Math.Sqrt(i)) - InvSqrt((float)i);
                if (tmp < min)
                {
                    min = tmp; minid = i;
                }
            }
            try
            {
                tb5.Text = tb5.Text + minid + " : " + min + " " + (1 / Math.Sqrt(minid)) + " " + InvSqrt((float)minid) + "\r\n";
            } 
            catch (Exception ex) { }
            Console.Beep(300, 100);
            return FindMinDiff(minid - step, minid + step, step / 10, Accuracy);
        }

        void FindAll(Double start, Double end, Double step)
        {
            Graphics g = pictureBox1.CreateGraphics();

            PointF[] ptf = new PointF[(int)((end - start) / step)];

            // число волн


            // Очистим PictureBox
            g.Clear(pictureBox1.BackColor);
            int dick = 0, scale = 150;
            for (Double i = start; i < end; i += step)
            {
                ptf[dick].X = pictureBox1.Width - (float)i * scale;
                ptf[dick].Y = (float)InvSqrt((float)i) * scale;
                //ptf[dick].Y = (float)(1 / Math.Sqrt(i)) * scale;
                dick++;
                try
                {
                    tb5.Text = tb5.Text + i + " : " + InvSqrt((float)i) + "\r\n";
                }
                catch (Exception ex)
                { }
            }
            g.DrawLines(Pens.Red, ptf);
            g.RotateTransform(30.0F);
            //g.Dispose();
        }
        void FindAll2(Double start, Double end, Double step)
        {
            Graphics g = pictureBox1.CreateGraphics();

            PointF[] ptf = new PointF[(int)((end - start) / step)];

            // число волн


            // Очистим PictureBox
            g.Clear(pictureBox1.BackColor);
            int dick = 0, scale = 150;
            for (Double i = start; i < end; i += step)
            {
                ptf[dick].X = pictureBox1.Width - (float)i * scale;
                //ptf[dick].Y = (float)InvSqrt((float)i) * scale;
                ptf[dick].Y = (float)(1 / Math.Sqrt(i)) * scale;
                dick++;
                try
                {
                    tb5.Text = tb5.Text + i + " : " + InvSqrt((float)i) + "\r\n";
                }
                catch (Exception ex)
                { }
            }
            g.DrawLines(Pens.Green, ptf);
            g.RotateTransform(30.0F);
            //g.Dispose();
        }
        void FindDiff(Double start, Double end, Double step)
        {
            Graphics g = pictureBox1.CreateGraphics();

            PointF[] ptf = new PointF[(int)((end - start) / step)];

            // число волн


            // Очистим PictureBox
            g.Clear(pictureBox1.BackColor);
            int dick = 0, scale = 150;
            for (Double i = start; i < end; i += step)
            {
                ptf[dick].X = /*pictureBox1.Width - */(float)i * scale;
                //ptf[dick].Y = (float)InvSqrt((float)i) * scale;
                ptf[dick].Y = ((float)InvSqrt((float)i) - (float)(1 / Math.Sqrt(i))) * scale*10000000;
                dick++;
                try
                {
                    tb5.Text = tb5.Text + i + " : " + InvSqrt((float)i) + "\r\n";
                }
                catch (Exception ex)
                { }
            }
            g.DrawLines(Pens.Pink, ptf);
            //g.RotateTransform(30.0F);
            //g.Dispose();
        }
        private void Calculate()
        {
            this.Text = "Working on it...";
            //Double z = FindMin(0.5, 2, 0.0001, 20);
            //FindAll(0.5, 2, 0.01);
            //FindAll2(0.5, 2, 0.01);
            FindDiff(0.5, 2, 0.01);
            //FindMinDiff(0.5,  2, 0.0001, 16);//Больше 16 не работает!
            //FindMinDiff(1.3, 2, 0.0001, 20);
            //FindMinDiff(0.9, 2, 0.0001, 20);
            this.Text = "Done!";
        }

        private void tb1_TextChanged(object sender, EventArgs e)
        {

            //FindMinDiff(0.5, 2, 0.0001, 26);
            //0.5 - 2.0 1*2-23 відносна похибка, визначити максимальну відносну 1 - 4
            Task.Factory.StartNew(Calculate);
        }

    }
}
