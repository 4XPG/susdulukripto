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
        public String modeLSB { get; set; }
        public Size size { get; set; }
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

        public void hide(byte[] mbyte, bool bitLSB)
        {
            //Message to BitArray
            BitArray bitarray = new BitArray(mbyte);
            //bool[] bools = new bool[200000];
            //byte[] bytes = new byte[200000];

            //Convert key to seed
            byte[] k_ = Encoding.ASCII.GetBytes(key);
            Int32 seed = 0;
            foreach(byte b_ in k_)
            {
                seed += b_;
            }

            //Get all frame from .avi
            bitmapL = new List<Bitmap>();
            while(reader.Position - reader.Start < reader.Length)
            {
                bitmapL.Add(reader.GetNextFrame());
            }

            //Find frame and pixel position
            Random rand = new Random(seed);
            //for(int i = bitarray.Length-1; i > -1; i--)
            for (int i = 0; i < bitarray.Length; i++)
            {
                int frameIdx = rand.Next(reader.Length);
                int x = rand.Next(reader.Width);
                int y = rand.Next(reader.Height);

                Bitmap frame = bitmapL.ElementAt(frameIdx);
                Color c = frame.GetPixel(x,y);

                //mode 1-LSB
                if (bitLSB)
                {
                    byte byteb = c.B;
                    if (bitarray.Get(i)) byteb = (byte)((byteb & MED) | (HEX_1));
                    else byteb = (byte)((byteb & MED) | (HEX_0));
                    frame.SetPixel(x, y, Color.FromArgb(c.R, c.G, byteb));
                    //c = frame.GetPixel(x, y);
                    //bools[i] = c.B % 2 == 1; bytes[i] = c.B;
                }
                else
                //mode 2-LSB
                {
                    byte byteb = c.B;
                    byte byteg = c.G;
                    if (bitarray.Get(i)) byteb = (byte)((byteb & MED) | (HEX_1));
                    else byteb = (byte)((byteb & MED) | (HEX_0));
                    i++;
                    if (bitarray.Get(i)) byteg = (byte)((byteg & MED) | (HEX_1));
                    else byteg = (byte)((byteg & MED) | (HEX_0));

                    frame.SetPixel(x, y, Color.FromArgb(c.R, byteg, byteb));
                    //c = frame.GetPixel(x, y);
                    //bools[i] = c.B % 2 == 1; bytes[i] = c.B;
                    //bools[i] = c.G % 2 == 1; bytes[i] = c.G;
                }
            }

            //String s = System.Text.Encoding.ASCII.GetString(BoolArrayToBytes(bools));
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

        public Message extract(String input, bool bitLSB)
        {
            String s = "";
            String[] sb = new String[4];
            bool[] boolarray = new bool[200000];
            byte[] bytes = new byte[200000];
            Message m = new Message("", "", bytes);

            //Convert key to seed
            byte[] k_ = Encoding.ASCII.GetBytes(key);
            Int32 seed = 0;
            foreach (byte b_ in k_)
            {
                seed += b_;
            }

            //Get all frame from .avi
            bitmapL = new List<Bitmap>();
            while (reader.Position - reader.Start < reader.Length)
            {
                bitmapL.Add(reader.GetNextFrame());
            }

            //Find frame and pixel position
            Random rand = new Random(seed);

            int i = 0;
            int messageLength = 100;
            int contentLength = 0;
            int headLength = 1;
            int dotCounter = 0;
            Boolean firstTime = true;
            while (dotCounter < 3 || i < messageLength*8)
            {
                int frameIdx = rand.Next(reader.Length);
                int x = rand.Next(reader.Width);
                int y = rand.Next(reader.Height);

                Bitmap frame = bitmapL.ElementAt(frameIdx);
                Color c = frame.GetPixel(x, y);

                //mode 1-LSB
                if (bitLSB) boolarray[i] = (c.B % 2) == 1;
                //mode 2-LSB
                else
                {
                    boolarray[i] = (c.B % 2) == 1;
                    boolarray[++i] = (c.G % 2) == 1;
                }
                //bytes[i] = c.B;
                if (dotCounter < 3)
                {
                    s = System.Text.Encoding.ASCII.GetString(BoolArrayToBytes(boolarray));
                    dotCounter = s.Count(f => f == '.');
                    if (dotCounter == 1 && firstTime)
                    {
                        sb = s.Split('.');
                        firstTime = false;
                    }
                    if (dotCounter == 3)
                    {
                        sb = s.Split('.');
                        m.filename = sb[1];
                        m.extension = sb[2];
                        messageLength = Convert.ToInt32(sb[0]);
                        headLength = i/8 + 1;
                        contentLength = messageLength - headLength;
                        boolarray = new bool[messageLength*8];
                        dotCounter++;
                    }
                }
                i++;
            }
            m.content = BoolArrayToBytes(boolarray);
            String ssb = Encoding.ASCII.GetString(m.content);
            ssb = ssb.Substring(headLength, ssb.Length - headLength-1);
            m.content = Encoding.ASCII.GetBytes(ssb.ToString());

            reader.Close();
            return m;
        }

        public static byte[] BoolArrayToBytes(bool[] bools)
        {
            byte[] arr1 = Array.ConvertAll(bools, b => b ? (byte)1 : (byte)0);
            int bytes = bools.Length / 8;
            if ((bools.Length % 8) != 0) bytes++;
            byte[] arr2 = new byte[bytes];
            int bitIndex = 0, byteIndex = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                {
                    arr2[byteIndex] |= (byte)(((byte)1) << bitIndex);
                }
                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }
            return arr2;
        }

        public double compPSNR(Video video2)
        {
            double sum_sq = 0;
            double mse;

            if (this.reader.Height != video2.reader.Height
                || this.reader.Width != video2.reader.Width) return -1;

            else
            {
                //Get all frame from .avi
                bitmapL = new List<Bitmap>();
                while (reader.Position - reader.Start < reader.Length)
                {
                    bitmapL.Add(reader.GetNextFrame());
                    video2.bitmapL.Add(video2.reader.GetNextFrame());
                }

                for (int k = 0; k < reader.Length; k++ )
                {
                    for (int i = 0; i < reader.Height; i++)
                    {
                        for (int j = 0; j < reader.Width; j++)
                        {
                            Bitmap frame1 = bitmapL.ElementAt(k);
                            Color c1 = frame1.GetPixel(j, i);

                            Bitmap frame2 = video2.bitmapL.ElementAt(k);
                            Color c2 = frame2.GetPixel(j, i);

                            double err = Math.Sqrt(Math.Pow((double)c2.R - (double)c1.R, 2) + Math.Pow((double)c2.G - (double)c1.G, 2) + Math.Pow((double)c2.B - (double)c1.B, 2));
                            sum_sq += (err * err);
                        }
                    }
                }
                mse = sum_sq / (double)(this.reader.Height * this.reader.Width);
                return ((double)(10 * Math.Log10(Math.Pow(this.reader.Height * this.reader.Width, 2) / mse)));
            }
        }
    }
}