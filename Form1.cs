using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;
using System.IO;

namespace susdulukripto
{
    public partial class Form1 : Form
    {
        private Microsoft.DirectX.AudioVideoPlayback.Video video;

        public bool isHideAllowed;
        internal SaveFileDialog SaveFileDialog1;
        
        public Form1()
        {
            InitializeComponent();

            isHideAllowed = false;
            this.SaveFileDialog1 = new SaveFileDialog();
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
                /*video = new Microsoft.DirectX.AudioVideoPlayback.Video(openAviDialog.FileName);
                video.Owner = panel1;
                video.Stop();*/

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
                if (VigenereMode.Text == "Use Vigenere")
                {
                    m.VigenereEncrypt(textBox2.Text);
                }

                //save file dialog
                if (saveAviDialog.ShowDialog() == DialogResult.OK)
                {
                    //susdulukriptio.video instatiation
                    Video video = new Video(openAviDialog.FileName, saveAviDialog.FileName, textBox2.Text);
                    video.hide(m.compose(), lsbMode.Text == "1 LSB");
                }
            }
            else
            {
                MessageBox.Show("Lengkapi data & uji payload terlebih dahulu", "Warning", MessageBoxButtons.OK);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Video video = new Video(openAviDialog.FileName, saveAviDialog.FileName, textBox2.Text);
            Message m = video.extract(openAviDialog.FileName, lsbMode.Text=="1 LSB");
            if (VigenereMode.Text == "Use Vigenere")
            {
                m.VigenereDecrypt(textBox2.Text);
            }

            SaveFileDialog1.FileName = m.filename;
            SaveFileDialog1.DefaultExt = m.extension;

            DialogResult result = SaveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                File.WriteAllBytes(SaveFileDialog1.FileName, m.content);
            }
        }

        private void PSNR_Click(object sender, EventArgs e)
        {
            PSNRlabel.Text = "Processing PSNR";
            if (openMessageDialog.ShowDialog() == DialogResult.OK)
            {
                String fname = openMessageDialog.FileName;
                Video video = new Video(openAviDialog.FileName, saveAviDialog.FileName, textBox2.Text);
                Video video2 = new Video(fname, saveAviDialog.FileName, textBox2.Text);
                PSNRlabel.Text = "PSNR = "+ (Math.Round(video.compPSNR(video2)*100)/100).ToString();
            }
        }
    }
}
