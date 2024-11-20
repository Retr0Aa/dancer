using Dancer.UI;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dancer.Framework
{
    public class MainApp : Form
    {
        ToolStrip toolStrip;
        ToolStripButton playStripButton;

        public List<Sample> samples;

        public static MainApp Instance { get; set; }

        public Channels channels;
        public bool shouldStop;

        public float mainTempo;

        public MainApp()
        {
            Instance = this;
        }

        public void Run()
        {
            samples = new List<Sample>() {
                new Sample("C:\\Users\\retr0\\Documents\\KICK_TEST.wav", "Kick", 1, 15),
                new Sample("C:\\Users\\retr0\\Documents\\HIHAT_TEST.wav", "HiHat", 2, 15),
                new Sample("C:\\Users\\retr0\\Documents\\SNARE_TEST.wav", "Snare", 3, 15)
            };
            mainTempo = 0.1f;

            this.Text = "Retr0A Dancer 0.1 Early Version";
            this.Size = new Size(1080, 720);
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            playStripButton = new ToolStripButton();
            playStripButton.Text = "Play";
            playStripButton.Click += async (object sender, EventArgs args) =>
            {
                shouldStop = !shouldStop;

                if (!shouldStop)
                {
                    playStripButton.Text = "Stop Playing";

                    await StartPlayingAsync();
                }
                else
                {
                    playStripButton.Text = "Play";
                }
            };

            toolStrip = new ToolStrip();
            toolStrip.Items.Add(playStripButton);

            channels = new Channels(15);
            channels.RefreshSamples(samples);

            this.Controls.Add(channels.rootBox);
            this.Controls.Add(toolStrip);
        }

        public async Task StartPlayingAsync()
        {
            var playTasks = new List<Task>();

            foreach (var sample in samples)
            {
                playTasks.Add(Task.Run(async () =>
                {
                    for (int i = 0; i < sample.loadedPoints.Length; i++)
                    {
                        if (shouldStop)
                            return;

                        var point = sample.loadedPoints[i];

                        if (point)
                        {
                            var audioFile = new AudioFileReader(sample.filePath);
                            var outputDevice = new WaveOutEvent();
                            outputDevice.Init(audioFile);
                            outputDevice.Play();
                        }

                        Console.WriteLine($"Playing at {i} index in sample.");

                        // Wait for the specified delay without blocking other operations
                        await Task.Delay((int)Math.Round(mainTempo * 1000));
                    }
                }));
            }

            // Wait for all play tasks to complete
            await Task.WhenAll(playTasks);

            if (!shouldStop)
                await StartPlayingAsync();
        }

        public void RefreshSamples()
        {
            channels.RefreshSamples(samples);
        }
    }
}
