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
        public String content { get; set; }

        public Message(String filename, String extension, String content)
        {
            this.filename = filename;
            this.extension = extension;
            this.content = content;
        }

        public String compose()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(filename);
            sb.Append(".");
            sb.Append(extension);
            sb.Append(".");
            sb.Append(content);

            return sb.ToString();
        }
    }
}
