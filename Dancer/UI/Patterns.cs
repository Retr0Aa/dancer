using Dancer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dancer.UI
{
    public class Patterns
    {
        public GroupBox rootBox;
        public ListBox patternsPanel;

        public NonSelectableButton addPatternButton;

        public Patterns()
        {
            rootBox = new GroupBox();
            rootBox.Text = "Patterns";
            rootBox.Dock = DockStyle.Left;

            patternsPanel = new ListBox();
            patternsPanel.Dock = DockStyle.Fill;
            patternsPanel.SelectedIndexChanged += PatternsPanel_SelectedIndexChanged;

            addPatternButton = new NonSelectableButton();
            addPatternButton.Dock = DockStyle.Bottom;
            addPatternButton.Text = "Add Pattern";
            addPatternButton.Click += AddPatternButton_Click;

            rootBox.Controls.Add(addPatternButton);
            rootBox.Controls.Add(patternsPanel);
        }

        private void PatternsPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainApp.Instance.currentPattern = patternsPanel.SelectedIndex;
            MainApp.Instance.RefreshSamples();
        }

        private void AddPatternButton_Click(object sender, EventArgs e)
        {
            MainApp.Instance.patterns.Add(new Pattern("My Pattern #" + (MainApp.Instance.patterns.Count + 1), MainApp.Instance.CreateDefaultSamplesPack()));

            MainApp.Instance.SuspendLayout();
            MainApp.Instance.RefreshPatterns();
            MainApp.Instance.ResumeLayout();
        }
        public void RefreshPatterns(List<Pattern> patterns)
        {
            patternsPanel.Items.Clear();

            foreach (var pattern in patterns)
            {
                patternsPanel.Items.Add(pattern.name);
            }

            patternsPanel.SelectedIndex = MainApp.Instance.currentPattern;
        }
    }
}
