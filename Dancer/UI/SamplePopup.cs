using Dancer.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dancer.UI
{
    public class SamplePopup
    {
        public Form form;
        
        public Label nameLabel;
        public TextBox nameTextBox;

        public Label sampleLabel;
        public TextBox sampleTextBox;

        public Button previewButton;

        public Button saveButton;

        private Sample m_Sample;

        public SamplePopup(Sample sample)
        {
            m_Sample = sample;

            form = new Form();
            form.Text = "Sample Editor";
            form.Show();

            previewButton = new Button();
            previewButton.Text = "Preview Audio";
            previewButton.Click += PreviewButton_Click;
            previewButton.Dock = DockStyle.Bottom;

            form.Controls.Add(previewButton);

            saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Click += SaveButton_Click;
            saveButton.Dock = DockStyle.Bottom;

            form.Controls.Add(saveButton);

            Label titleLabel = new Label();
            titleLabel.Text = sample.title;
            titleLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            titleLabel.Dock = DockStyle.Top;

            sampleLabel = new Label();
            sampleLabel.Text = "Sample Path";
            sampleLabel.Dock = DockStyle.Top;
            sampleTextBox = new TextBox();
            sampleTextBox.Text = sample.filePath;
            sampleTextBox.Dock = DockStyle.Top;
            sampleTextBox.Width = 150;

            form.Controls.Add(sampleTextBox);
            form.Controls.Add(sampleLabel);

            nameLabel = new Label();
            nameLabel.Text = "Sample Name";
            nameLabel.Dock = DockStyle.Top;
            nameTextBox = new TextBox();
            nameTextBox.Text = sample.title;
            nameTextBox.Dock = DockStyle.Top;
            nameTextBox.Width = 150;

            form.Controls.Add(nameTextBox);

            form.Controls.Add(nameLabel);
            form.Controls.Add(titleLabel);
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(sampleTextBox.Text);
            simpleSound.Play();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MainApp.Instance.samples.Find(s => s.id == m_Sample.id).title = nameTextBox.Text;
            MainApp.Instance.samples.Find(s => s.id == m_Sample.id).filePath = sampleTextBox.Text;

            MainApp.Instance.RefreshSamples();

            form.Close();
        }
    }
}
