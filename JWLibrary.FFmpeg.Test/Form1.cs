﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JWLibrary.FFmpeg.Test
{
    public partial class Form1 : Form
    {
        JWLibrary.FFmpeg.FFMpegCaptureAV _ffmpegCav
            = new FFmpeg.FFMpegCaptureAV();

        public Form1()
        {
            InitializeComponent();

            _ffmpegCav.FFmpegDataReceived += (s, e) =>
            {
                this.Invoke(
                    new MethodInvoker(delegate ()
                    {
                        lblFps.Text = e.Fps;
                        lblFrame.Text = e.Frame;
                        lblTime.Text = e.Time;
                    })                  
                );
            };

            _ffmpegCav.FrameDroped += (s, e) =>
            {
                Console.WriteLine("Frame drop!!!");
            };
        }

        private void btnRecStart_Click(object sender, EventArgs e)
        {
            if (!_ffmpegCav.IsRunning)
            {
                if (_ffmpegCav.Initialize())
                {
                    _ffmpegCav.Register();

                    JWLibrary.FFmpeg.FFmpegCommandModel model = new FFmpeg.FFmpegCommandModel
                    {
                        AudioQuality = JWLibrary.FFmpeg.FFmpegCommandParameterSupport.GetSupportAudioQuality()[0],
                        Format = "mp4",
                        FrameRate = JWLibrary.FFmpeg.FFmpegCommandParameterSupport.GetSupportFrameRate()[0],
                        Height = "1440",
                        Width = "2560",
                        OffsetX = "0",
                        OffsetY = "0",
                        Preset = JWLibrary.FFmpeg.FFmpegCommandParameterSupport.GetSupportPreset()[0],
                        FullFileName = @"C:\test.mp4"
                    };
                    var command = JWLibrary.FFmpeg.FFmpegCommandBuilder.BuildRecordingCommand(FFmpeg.RecordingTypes.Local, model);
                    _ffmpegCav.FFmpegCommandExcute(command);
                }
            }
        }

        private void btnRecStop_Click(object sender, EventArgs e)
        {
            _ffmpegCav.FFmpegCommandStop();
            _ffmpegCav.UnRegister();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_ffmpegCav != null)
            {
                _ffmpegCav.Dispose();
            }

            base.OnClosing(e);
        }
    }
}
