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
            //Message to BitArray
            BitArray bitarray = new BitArray(mbyte);
            int[] tmp = new int[200];
            byte[] blue = new byte[200];
            byte[] byteblue = new byte[200];
            bool[] boolarray = new bool[1600];
            
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
            int j = 0;
            //for(int i = bitarray.Length-1; i > -1; i--)
            for (int i = 0; i < bitarray.Length; i++)
            {
                int frameIdx = rand.Next(reader.Length);
                int x = rand.Next(reader.Width);
                int y = rand.Next(reader.Height);
                tmp[j] = frameIdx + x + y;

                Bitmap frame = bitmapL.ElementAt(frameIdx);
                Color c = frame.GetPixel(x,y);
                blue[j] = c.B;

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
                frame.SetPixel(x, y, Color.FromArgb(c.R, c.G, byteb));
                byteblue[j] = byteb;
                boolarray[j] = ((int)byteblue[j] %2 ) == 1;
                j++;
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

        public Message extract(String input)
        {
            String s = "";
            String[] sb = new String[4];
            int[] tmp = new int[200];
            bool[] boolarray = new bool[16000];
            byte[] extractedContent = new byte[16000];
            Message m = new Message("", "", extractedContent);

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
            //for (int i = bitarray.Length - 1; i > -1; i--)

            int i = 0;
            int messageLength = 1000;
            int dotCounter = 0;
            while (dotCounter < 3 || i < messageLength)
            {
                int frameIdx = rand.Next(reader.Length);
                int x = rand.Next(reader.Width);
                int y = rand.Next(reader.Height);
                //tmp[i] = frameIdx + x + y;

                Bitmap frame = bitmapL.ElementAt(frameIdx);
                Color c = frame.GetPixel(x, y);
                //blue[i] = c.B;

                //asumsi mode 1-LSB
                //byteblue[i] = c.B;
                boolarray[i] = ((int)c.B % 2) == 1;
                if (dotCounter < 3)
                {
                    s = System.Text.Encoding.ASCII.GetString(BoolArrayToBytes(boolarray));
                    dotCounter = s.Count(f => f == '.');
                    if (dotCounter == 3 && i%8==0)
                    {
                        m.filename = sb[1];
                        m.extension = sb[2];
                        int len = Convert.ToInt32(sb[0]);
                        messageLength = len-i;
                        i=-1;
                    }
                }
                if (dotCounter == 1 && i % 8 == 0)
                {
                    sb = s.Split('.');
                }
                i++;
            }
            m.content = BoolArrayToBytes(boolarray);
            reader.Close();
            return m;
        }

        public static byte[] BoolArrayToBytes(bool[] bools)
        {
            // basic - same count
            byte[] arr1 = Array.ConvertAll(bools, b => b ? (byte)1 : (byte)0);

            // pack (in this case, using the first bool as the lsb - if you want
            // the first bool as the msb, reverse things ;-p)
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

        /*public Message extract2(String input)
        {

            this.reader = new AVIReader();
            reader.Open(input);

            //Vigenere Decryption

            //inisialisasi
            bool[] boolarray = new bool[1600];
            String[] sb = new String[4];
            String s = "";

            int i = 0;
            int[] tmp = new int[200];
            byte[] blue = new byte[200];

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

            //mencari ukuran pesan, nama file, dan ekstensi file
            Random rand = new Random(seed);
            while (sb.Length < 3)
            {
                int frameIdx = rand.Next(reader.Length);
                int x = rand.Next(reader.Width);
                int y = rand.Next(reader.Height);
                tmp[i] = frameIdx + x + y;

                Bitmap frame = bitmapL.ElementAt(frameIdx);
                Color c = frame.GetPixel(x, y);

                //asumsi mode 1-LSB
                boolarray[i] = ((int)c.B % 2) == 1;
                blue[i] = c.B;
                i++;

                sb = System.Text.Encoding.ASCII.GetString(BoolArrayToBytes(boolarray)).Split('.');
            }

            int allMessageLength = Convert.ToInt32(sb[0]);

            //mencari seluruh pesan
            for (; i < allMessageLength; i++)
            {
                int frameIdx = rand.Next(reader.Length);
                int x = rand.Next(reader.Width);
                int y = rand.Next(reader.Height);

                Bitmap frame = bitmapL.ElementAt(frameIdx);
                Color c = frame.GetPixel(x, y);

                //asumsi mode 1-LSB
                boolarray[i] = ((int)c.B % 2) == 1;
            }

            s = System.Text.Encoding.ASCII.GetString(BoolArrayToBytes(boolarray));

            byte[] bytes = Encoding.ASCII.GetBytes(sb[3]);

            Message m = new Message(sb[1], sb[2], bytes);

            reader.Close();
            writer.Close();

            return m;
        }*/

    }
}