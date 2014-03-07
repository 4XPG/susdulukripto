using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AForge.Video.VFW;
using AForge.Video;

namespace susdulukripto
{
    class Video
    {
        public static byte HEX_0 = 0x00;
        public static byte HEX_1 = 0x01;
        public static byte MED = 0xFE;

        private String key { get; set; }
        private String input { get; set; }
        private String output { get; set; }

        public AVIReader reader { get; set; }
        public AVIWriter writer { get; set; }
        public int modeLSB { get; set; }
        public List<Bitmap> bitmapL { get; set; }

        //Hide Constructor
        public Video(String input, String output, String key)
        {
            this.input = input;
            this.key = key;
            this.output = output;

            this.reader = new AVIReader();
            reader.Open(input);

            this.bitmapL = new List<Bitmap>();
        }

        public void hide(byte[] mbyte)
        {
            //Vigenere Encryption

            //Message to BitArray
            BitArray bitarray = new BitArray(mbyte);

            //Convert key to seed
            byte[] k_ = Encoding.ASCII.GetBytes(key);
            Int32 seed = 0;
            foreach(byte b_ in k_)
            {
                seed += b_;
            }

            //Get all frame from .avi
            while(reader.Position - reader.Start < reader.Length)
            {
                bitmapL.Add(reader.GetNextFrame());
            }

            //Find frame and pixel position
            Random rand = new Random(seed);
            for(int i = bitarray.Length-1; i > -1; i--)
            {
                int frameIdx = rand.Next(reader.Length);
                int x = rand.Next(reader.Width);
                int y = rand.Next(reader.Height);

                Bitmap frame = bitmapL.ElementAt(frameIdx);
                Color c = frame.GetPixel(x,y);

                //asumsi mode 1-LSB
                byte byteb = c.B;
                if(modeLSB == 1)
                {
                    if (bitarray.Get(i))
                    {
                        byteb = (byte)((byteb & MED) | (HEX_1));
                    }
                    else
                    {
                        byteb = (byte)((byteb & MED) | (HEX_0));
                    }
                    frame.SetPixel(x, y, Color.FromArgb(c.R, c.G, byteb));
                }
                else
                {
                    byte byteg = c.G;

                    //bitarray index to get
                    int basis = bitarray.Length - i;

                    //change blue byte
                    if (bitarray.Get(bitarray.Length - ((basis*2) - 1)))
                    {
                        byteb = (byte)((byteb & MED) | (HEX_1));
                    }
                    else
                    {
                        byteb = (byte)((byteb & MED) | (HEX_0));
                    }
                    //change green byte
                    if (bitarray.Get(bitarray.Length - (basis*2)))
                    {
                        byteg = (byte)((byteg & MED) | (HEX_1));
                    }
                    else
                    {
                        byteg = (byte)((byteg & MED) | (HEX_0));
                    }
                    frame.SetPixel(x, y, Color.FromArgb(c.R, byteg, byteb));

                    //asumsi panjang bitarray pasti genap
                    if (i == (bitarray.Length / 2)) break;
                }
            }

            //write new .avi file
            AVIWriter writer = new AVIWriter();
            writer.Open(output,reader.Width,reader.Height);
            foreach(Bitmap bitmap in bitmapL)
            {
                writer.AddFrame(bitmap);
            }

            reader.Close();
            writer.Close();
        }
    }
}
