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
    public class SampleDisplay
    {
        public CustomPanel rootPanel;

        public Button sampleButton;

        private Sample m_Sample;

        public SampleDisplay(Sample sample)
        {
            m_Sample = sample;

            rootPanel = new CustomPanel();
            rootPanel.Dock = DockStyle.Top;
            rootPanel.BackColor = Color.LightGray;
            rootPanel.AutoSize = true;

            sampleButton = new Button();
            sampleButton.Text = sample.title;
            sampleButton.Click += SampleButton_Click;

            for (int i = 0; i < sample.loadedPoints.Length; i++)
            {
                Button btn = new Button();
                btn.Text = i.ToString();
                btn.Dock = DockStyle.Right;
                btn.Width = 50;
                btn.Name = i.ToString();

                if (sample.loadedPoints[i])
                {
                    btn.BackColor = Color.Green;
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.BackColor = Color.Black;
                    btn.ForeColor = Color.White;
                }

                btn.Click += (object sender, EventArgs e) =>
                {
                    Console.WriteLine(MainApp.Instance.patterns[MainApp.Instance.currentPattern].samples[m_Sample.id - 1].loadedPoints[int.Parse(((Control) sender).Name)]);
                    
                    MainApp.Instance.patterns[MainApp.Instance.currentPattern].samples[m_Sample.id - 1].loadedPoints[int.Parse(((Control) sender).Name)] =
                        !MainApp.Instance.patterns[MainApp.Instance.currentPattern].samples[m_Sample.id - 1].loadedPoints[int.Parse(((Control)sender).Name)];

                    MainApp.Instance.SuspendLayout();
                    MainApp.Instance.RefreshSamples();
                    MainApp.Instance.ResumeLayout();
                };

                rootPanel.Controls.Add(btn);
            }

            rootPanel.Controls.Add(sampleButton);
        }

        private void SampleButton_Click(object sender, EventArgs e)
        {
            SamplePopup samplePopup = new SamplePopup(m_Sample);
        }
    }
}
