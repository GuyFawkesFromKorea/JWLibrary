﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWLibrary.FFmpeg
{
    public class CommandConst
    {
        public static string SELECT_AV_DEVICE_INFOMATION_COMMAND()
        {
            return "-list_devices true -f dshow -i dummy";
        }

        public static string SELECT_DESKTOP_RECODING_COMMAND()
        {
            return @"-rtbufsize 2000M -f dshow -i video=""@videoSource"":audio=""@audioSource"" -vcodec libx264 -b:v 5M -pix_fmt yuv420p -r @framerate -framerate @framerate -threads 1 -preset @preset -crf 29 -vf scale=1024:-1 -vf crop=@width:@height:@x:@y -acodec aac -ac 2 -ab 192k -ar @audioRate -ab 64k -tune zerolatency @option1 -f @format ""@filename""";
        }

        public static string CONVERT_FILE()
        {
            return @" -y -i @SRCPATH @DESTPATH";
        }
    }
}