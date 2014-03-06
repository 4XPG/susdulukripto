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
        public String message { get; set; }
        public String modeLSB { get; set; }
        public List<Bitmap> bitmapL;

        //Hide Constructor
        public Video(String input, String output, String key)
        {
            this.input = input;
            this.key = key;
            this.output = output;

            this.reader = new AVIReader();
            reader.Open(input);
        }

        public void hide(String message)
        {
            //Vigenere Encryption

            //Message to BitArray
            byte[] b = Encoding.ASCII.GetBytes(message);
            BitArray bitarray = new BitArray(b);

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
                if(bitarray.Get(i))
                {
                    byteb = (byte)((byteb & MED) | (HEX_1));
                }
                else
                {
                    byteb = (byte)((byteb & MED) | (HEX_0));
                }
                frame.SetPixel(x, y, Color.FromArgb(c.R,c.G,byteb));
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
