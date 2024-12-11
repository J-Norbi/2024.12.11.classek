using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace _2024._12._11.javító_dolgozat_megoldása
{
    internal class Electron:UserControl
    {
        public int speed { get; set; }
        public Electron()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Electron
            // 
            this.Name = "Electron";
            this.Size = new Size(10, 10);
            this.BackColor = Color.Lime;
            this.ResumeLayout(false);
        }
    }
}
