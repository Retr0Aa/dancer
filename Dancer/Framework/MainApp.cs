using Dancer.UI;
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
    public class MainApp
    {
        Form form;
        ToolStrip toolStrip;
        ToolStripButton playStripButton;

        public List<Sample> samples;

        public static MainApp Instance { get; set; }

        public Channels channels;
        public bool isPlaying = true;

        public MainApp()
        {
            Instance = this;
        }

        public void Run()
        {
            Application.SetCompatibleTextRenderingDefault(true);
            Application.EnableVisualStyles();

            samples = new List<Sample>() { new Sample("C:\\Users\\Retr0A\\Documents\\Alarm01.wav", "Deez Nuts", 1) };

            form = new Form();
            form.Text = "Retr0A Dancer 0.1 Early Version";
            form.Size = new Size(1080, 720);

            playStripButton = new ToolStripButton();
            playStripButton.Text = "Play";
            playStripButton.Click += (object sender, EventArgs args) => {
                StartPlaying();
            };

            toolStrip = new ToolStrip();
            toolStrip.Items.Add(playStripButton);

            channels = new Channels();
            channels.RefreshSamples(samples);

            form.Controls.Add(channels.rootBox);
            form.Controls.Add(toolStrip);

            Application.Run(form);
        }

        public void StartPlaying()
        {
            foreach (var sample in samples)
            {
                foreach (var point in sample.loadedPoints)
                {
                    if (point)
                    {
                        SoundPlayer simpleSound = new SoundPlayer(sample.filePath);
                        simpleSound.Play();
                    }
                }
            }
        }

        public void RefreshSamples()
        {
            channels.RefreshSamples(samples);
        }
    }
}
