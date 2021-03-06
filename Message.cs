﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace susdulukripto
{
    class Message
    {
        public String filename { get; set; }
        public String extension { get; set; }
        public byte[] content { get; set; }

        public Message(String filename, String extension, byte[] content)
        {
            this.filename = filename;
            this.extension = extension;
            this.content = content;
        }

        public byte[] compose()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(filename);
            sb.Append(".");
            sb.Append(extension);
            sb.Append(".");
            byte[] head = Encoding.ASCII.GetBytes(sb.ToString());
            
            //masukin panjang messagenya biar pas ekstrak tau seberapa panjang
            int mLen = head.Length+content.Length;
            String mLenLength = mLen.ToString();
            mLen = mLen + mLenLength.Length + 1;
            StringBuilder newsb = new StringBuilder();
            newsb.Append(mLen).Append(".");
            byte[] allMessageLength = Encoding.ASCII.GetBytes(newsb.ToString());


            byte[] result = new byte[allMessageLength.Length+head.Length+content.Length];
            for (int i = 0; i < allMessageLength.Length; i++)
            {
                result[i] = allMessageLength[i];
            }
            for (int i = allMessageLength.Length; i < (allMessageLength.Length+head.Length); i++)
            {
                result[i] = head[i - allMessageLength.Length];
            }
            for (int i = (allMessageLength.Length+head.Length); i < (allMessageLength.Length+head.Length+content.Length); i++)
            {
                result[i] = content[i - (allMessageLength.Length + head.Length)];
            }

            Console.WriteLine(newsb.Append(sb).Append(content.ToString()));
            return result;
        }

        public void VigenereEncrypt(String key)
        {
            StringBuilder s = new StringBuilder();
            s.Append(Encoding.ASCII.GetString(this.content));
            int j = 0;
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = (char)((s[i] + key[j]) % 128);
                if (key.Length == j+1) j = 0; else j++;
            }
            this.content = Encoding.ASCII.GetBytes(s.ToString());

            Console.WriteLine(s.ToString());
        }

        public void VigenereDecrypt(string key)
        {
            StringBuilder s = new StringBuilder();
            s.Append(Encoding.ASCII.GetString(this.content));
            int j = 0;
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = (char)((s[i] - key[j] + 128) % 128);
                if (key.Length == j+1) j = 0; else j++;
            }
            this.content = Encoding.ASCII.GetBytes(s.ToString());
        }

    }
}
