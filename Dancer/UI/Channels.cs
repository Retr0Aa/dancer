using Dancer.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dancer.UI
{
    public class Channels
    {
        public GroupBox rootBox;
        public CustomPanel markerPanel;
        public CustomPanel samplesPanel;

        public NonSelectableButton addSampleButton;

        public Channels()
        {
            rootBox = new GroupBox();
            rootBox.Text = "Channels";
            rootBox.Dock = DockStyle.Fill;

            markerPanel = new CustomPanel();
            markerPanel.Dock = DockStyle.Top;
            markerPanel.BackColor = Color.DarkGray;
            markerPanel.Height = 10;
            //markerPanel.AutoSize = true;

            samplesPanel = new CustomPanel();
            samplesPanel.Dock = DockStyle.Fill;

            addSampleButton = new NonSelectableButton();
            addSampleButton.Dock = DockStyle.Bottom;
            addSampleButton.Text = "Add Sample";
            addSampleButton.Click += AddSampleButton_Click;

            for (int i = 0; i <= MainApp.Instance.defaultLength; i++)
            {
                NonSelectableButton markerButton = new NonSelectableButton();
                markerButton.FlatStyle = FlatStyle.Flat;
                markerButton.Name = "marker_" + i;
                markerButton.Dock = DockStyle.Right;
                markerButton.Width = 50;
                markerButton.Enabled = false;
                markerButton.BackColor = Color.Black;
                markerPanel.Controls.Add(markerButton);
            }

            rootBox.Controls.Add(samplesPanel);
            rootBox.Controls.Add(markerPanel);
            rootBox.Controls.Add(addSampleButton);
        }

        private void AddSampleButton_Click(object sender, EventArgs e)
        {
            MainApp.Instance.patterns[MainApp.Instance.currentPattern].samples.Add(new Sample("", "Untitled", MainApp.Instance.patterns[MainApp.Instance.currentPattern].samples.Count + 1, MainApp.Instance.channelsLength));

            MainApp.Instance.SuspendLayout();
            MainApp.Instance.RefreshSamples();
            MainApp.Instance.ResumeLayout();
        }

        public void RefreshSamples(List<Sample> samples)
        {
            samplesPanel.Controls.Clear();

            foreach (var sample in samples)
            {
                SampleDisplay sampleDisplay = new SampleDisplay(sample);
                samplesPanel.Controls.Add(sampleDisplay.rootPanel);
            }
        }

        public void RefreshMarkers(int currentIndexPlaying)
        {
            BlackoutMarkers();

            markerPanel.Controls[currentIndexPlaying == 0 ? MainApp.Instance.defaultLength : currentIndexPlaying - 1].BackColor = Color.Black;
            markerPanel.Controls[currentIndexPlaying].BackColor = Color.Red;
        }

        public void BlackoutMarkers()
        {
            for (int i = 0; i < markerPanel.Controls.Count; i++)
            {
                markerPanel.Controls[i].BackColor = Color.Black;
            }
        }
    }
}
