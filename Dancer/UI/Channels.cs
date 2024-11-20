using Dancer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dancer.UI
{
    public class Channels
    {
        public GroupBox rootBox;
        public CustomPanel samplesPanel;

        public Button addSampleButton;

        public int channelsLength;

        public Channels(int channelsLength = 10)
        {
            this.channelsLength = channelsLength;

            rootBox = new GroupBox();
            rootBox.Text = "Channels";
            rootBox.Dock = DockStyle.Fill;

            samplesPanel = new CustomPanel();
            samplesPanel.Dock = DockStyle.Fill;

            addSampleButton = new Button();
            addSampleButton.Dock = DockStyle.Bottom;
            addSampleButton.Text = "Add Sample";
            addSampleButton.Click += AddSampleButton_Click;

            rootBox.Controls.Add(addSampleButton);
            rootBox.Controls.Add(samplesPanel);
        }

        private void AddSampleButton_Click(object sender, EventArgs e)
        {
            MainApp.Instance.samples.Add(new Sample("", "Untitled", MainApp.Instance.samples.Count + 1, channelsLength));

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
    }
}
