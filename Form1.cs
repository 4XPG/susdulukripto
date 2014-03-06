using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace susdulukripto
{
    public partial class Form1 : Form
    {
        private Microsoft.DirectX.AudioVideoPlayback.Video video;

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openMessageDialog.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(openMessageDialog.FileName);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(video.State != StateFlags.Running)
            {
                video.Play();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(video.State == StateFlags.Running)
            {
                video.Pause();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openAviDialog.ShowDialog() == DialogResult.OK)
            {
                int width = panel1.Width;
                int height = panel1.Height;

                video = new Microsoft.DirectX.AudioVideoPlayback.Video(openAviDialog.FileName);
                video.Owner = panel1;
                video.Stop();

                panel1.Size = new Size(width,height);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(video.State != StateFlags.Stopped)
            {
                video.Stop();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //susdulukriptio.video instatiation
            Video video = new Video(openAviDialog.FileName,textBox2.Text);
        }
    }
}
