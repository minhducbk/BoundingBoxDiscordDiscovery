using BoundingBoxDiscordDiscovery.Offline;
using BoundingBoxDiscordDiscovery.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
            var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time
            List<double> Data;
            Data = ReadFile.readFileIntoList(txtFileName.Text);
            List<double> normData = MathFunc.zScoreNorm(Data, Data.Count);
            //get NLength
            int NLength = Convert.ToInt16(txtNLength.Text);
            //get max entry per node
            int maxEntry = Convert.ToInt16(txtMaxEntry.Text);
            //get min entry per node
            int minEntry = Convert.ToInt16(txtMinEntry.Text);
            //get R
            int R = Convert.ToInt16(txtR.Text);
            //get D
            int D = Convert.ToInt16(txtD.Text);

            RTree<int> rTree = new RTree<int>(maxEntry, minEntry);

            List<int> candidateList = new List<int>();
            List<int> beginIndexInner = new List<int>();

            double best_so_far_dist = 0;
            int best_so_far_loc = 0;

            double nearest_neighbor_dist = 0;
            double dist = 0;
            bool break_to_outer_loop = false;

            bool[] is_skip_at_p = new bool[normData.Count];
            for (int i = 0; i < normData.Count; i++)
                is_skip_at_p[i] = false;

            if (minEntry > maxEntry/2)
            {
                MessageBox.Show("Requirement: MinNodePerEntry <= MaxNodePerEntry/2");
                return;
            }
            for (int i = 0; i <= normData.Count - NLength; i++)
            {
                List<double> subseq = normData.GetRange(i, NLength);
                Offline.Rectangle new_rec = new Offline.Rectangle(Utils.MathFunc.DTW_Min(subseq, D, R).ToArray(), Utils.MathFunc.DTW_Max(subseq, D, R).ToArray(), i);
                rTree.Add(new_rec);
            }

            Dictionary<int, Node<int>> nodeMap = rTree.getNodeMap();
            List<Node<int>> leafNodes = nodeMap.Values.Where(node => node.level == 1).OrderBy(node => node.entryCount).ToList();

            for (int i = 0; i < leafNodes.Count; i++)
            {
                List<Offline.Rectangle> leafEntries = leafNodes[i].entries.Where(mbr => mbr != null).Select(mbr => mbr).ToList();
                if (leafEntries.Count > 0)
                {
                    int beginIndex = candidateList.Count;
                    candidateList = candidateList.Concat(leafEntries.Select(mbr => mbr.getIndexSubSeq())).ToList();
                    beginIndexInner = beginIndexInner.Concat(Enumerable.Range(1, leafEntries.Count).Select(x => beginIndex)).ToList();
                }
            }

            for (int i = 0; i < candidateList.Count; i++)
            {
                int p = candidateList[i];
                if (is_skip_at_p[p])
                {
                    //p was visited at inner loop before
                    continue;
                }
                else
                {
                    nearest_neighbor_dist = Constant.INFINITE;
                    List<int> tailCandidate = candidateList.GetRange(beginIndexInner[i], candidateList.Count - beginIndexInner[i]);
                    List<int> headCandidate = candidateList.GetRange(0, beginIndexInner[i]);
                    List<int> innerList = tailCandidate.Concat(headCandidate).ToList();

                    foreach (int q in innerList)// inner loop
                    {
                        if (Math.Abs(p - q) < NLength)
                        {
                            continue;// self-match => skip to the next one
                        }
                        else
                        {
                            //calculate the Distance between p and q
                            dist = MathFunc.EuDistance(Data.GetRange(p, NLength), Data.GetRange(q, NLength));

                            if (dist < best_so_far_dist)
                            {
                                //skip the element q at oute_loop, 'cuz if (p,q) is not a solution, so does (q,p).
                                is_skip_at_p[q] = true;

                                break_to_outer_loop = true; //break, to the next loop at outer_loop
                                break;// break at inner_loop first
                            }

                            if (dist < nearest_neighbor_dist)
                            {
                                nearest_neighbor_dist = dist;
                            }
                        }
                    }
                    if (break_to_outer_loop)
                    {
                        break_to_outer_loop = false;//reset
                        continue;//go to the next p in outer loop
                    }

                    if (nearest_neighbor_dist > best_so_far_dist)
                    {
                        best_so_far_dist = nearest_neighbor_dist;
                        best_so_far_loc = p;
                    }
                }
            }
            bestSoFarDisVal.Text = best_so_far_dist.ToString();
            bestSoFarLocVal.Text = best_so_far_loc.ToString();
            watch.Stop(); //stop timer
            long elapsedMs = watch.ElapsedMilliseconds;
            this.txtExeTime.Text = elapsedMs.ToString();
        }
    }
}
