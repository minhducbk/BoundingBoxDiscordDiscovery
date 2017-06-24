using BoundingBoxDiscordDiscovery.Offline;
using BoundingBoxDiscordDiscovery.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoundingBoxDiscordDiscovery
{
    public partial class FormDiscordDiscovery : Form
    {
        public FormDiscordDiscovery()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            List<double> Data;
            Data = ReadFile.readStreamFile(txtBoxFileName.Text);
            //get N_LENGTH
            int NLength = Convert.ToInt16(txtBoxNLength.Text);
            List<SubSequence> listSubsequence = null;
            for (int i = 0; i <= Data.Count - NLength; i++)
                listSubsequence.Add(new SubSequence(Data.GetRange(i, NLength), NLength));
            RTree<int> rTree = new RTree<int>();
            rTree    
        }
    }
}
