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
        public Panel samplesPanel;

        public Button addSampleButton;

        public Channels()
        {
            rootBox = new GroupBox();
            rootBox.Text = "Channels";
            rootBox.Dock = DockStyle.Fill;
            
            samplesPanel = new Panel();
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
            MainApp.Instance.samples.Add(new Sample("", "Untitled", MainApp.Instance.samples.Count));

            MainApp.Instance.RefreshSamples();
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
