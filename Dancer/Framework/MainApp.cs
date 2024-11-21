using Dancer.UI;
using NAudio.Wave;
using Retr0Log;
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

        public static MainApp Instance { get; set; }

        public Channels channels;
        public Patterns patternsUI;
        public List<Pattern> patterns;
        public bool shouldStop = true;

        public float mainTempo;
        public int channelsLength;

        public int currentPattern;

        public MainApp()
        {
            Instance = this;
        }

        public List<Sample> CreateDefaultSamplesPack()
        {
            return new List<Sample>()
            {
                new Sample("C:\\Users\\retr0\\Documents\\KICK_TEST.wav", "Kick", 1, 15),
                new Sample("C:\\Users\\retr0\\Documents\\HIHAT_TEST.wav", "HiHat", 2, 15),
                new Sample("C:\\Users\\retr0\\Documents\\SNARE_TEST.wav", "Snare", 3, 15)
            };
        }

        public void Run()
        {
            mainTempo = 0.1f;
            channelsLength = 15;
            currentPattern = 0;

            patterns = new List<Pattern>()
            {
                new Pattern("My Pattern #1", CreateDefaultSamplesPack())
            };

            Text = "Retr0A Dancer 0.1 Early Version";
            Size = new Size(1280, 720);
            DoubleBuffered = true;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

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

            channels = new Channels();
            channels.RefreshSamples(patterns[currentPattern].samples);

            patternsUI = new Patterns();
            patternsUI.RefreshPatterns(patterns);

            Controls.Add(channels.rootBox);
            Controls.Add(patternsUI.rootBox);
            Controls.Add(toolStrip);
        }

        public async Task StartPlayingAsync()
        {
            var playTasks = new List<Task>();

            foreach (var sample in patterns[currentPattern].samples)
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

                        Program.logger.Log($"Playing at {i} index in sample.", LogLevel.Debug);

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
            channels.RefreshSamples(patterns[currentPattern].samples);
        }

        public void RefreshPatterns()
        {
            patternsUI.RefreshPatterns(patterns);
        }
    }
}
