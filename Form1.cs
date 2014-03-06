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

        public bool isHideAllowed;

        public Form1()
        {
            InitializeComponent();

            isHideAllowed = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //get message size in bit
            int mbit = System.IO.File.ReadAllBytes(openMessageDialog.FileName).Length * 8;

            //get cover object size in byte
            byte[] csize = System.IO.File.ReadAllBytes(openAviDialog.FileName);
            int cbyte = csize.Length;

            //compare
            if(cbyte < mbit)
            {
                MessageBox.Show("Ukuran file terlalu besar","Warning",MessageBoxButtons.OK);
            }
            else
            {
                isHideAllowed = true;
                MessageBox.Show("Proses penyembunyian pesan bisa dilakukan", "Info", MessageBoxButtons.OK);
            }
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
            if(isHideAllowed)
            {
                //create message
                String fname = openMessageDialog.SafeFileName;
                String[] fstr = fname.Split('.');
                Message m = new Message(fstr[0], fstr[1], System.IO.File.ReadAllBytes(openMessageDialog.FileName));

                //save file dialog
                if (saveAviDialog.ShowDialog() == DialogResult.OK)
                {
                    //susdulukriptio.video instatiation
                    Video video = new Video(openAviDialog.FileName, saveAviDialog.FileName, textBox2.Text);
                    video.hide(m.compose());
                }
            }
            else
            {
                MessageBox.Show("Lengkapi data & uji payload terlebih dahulu", "Warning", MessageBoxButtons.OK);
            }
        }
    }
}
