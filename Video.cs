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
        private AVIReader reader { get; set; }
        private AVIWriter writer { get; set; }
        private String input { get; set; }
        private String output { get; set; }
        private String message { get; set; }
        private String key { get; set; }
        private String modeLSB { get; set; }
        private List<Bitmap> bmpList;

        //Hide Constructor
        public Video(String input, String key)
        {
            this.input = input;
            this.key = key;

            this.reader = new AVIReader();

        }
    }
}
