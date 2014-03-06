using System;
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

            byte[] result = new byte[head.Length+content.Length];
            for(int i=0; i<head.Length; i++)
            {
                result[i] = head[i];
            }
            for (int i = 0; i < content.Length; i++)
            {
                result[i] = content[i];
            }
            return result;
        }
    }
}
