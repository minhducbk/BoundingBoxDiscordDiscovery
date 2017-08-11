using BoundingBoxDiscordDiscovery.Offline;
using BoundingBoxDiscordDiscovery.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoundingBoxDiscordDiscovery
{
    public partial class FormDiscordDiscovery : Form
    {
        public RTree<int> rTree;

        // Store best_loc and best_dic from Run Offline:
        public int best_so_far_loc;
        public double best_so_far_dist;
        public int best_so_far_loc_offline;
        public double best_so_far_dist_offline;

        public int N_length;
        public int D;
        public int R;

        public List<Offline.Rectangle> recList;
        public List<int> id_itemList;
        public int id_item;

        public List<double> buffer;

        bool is_call_from_online;


        public FormDiscordDiscovery()
        {
            this.is_call_from_online = false;
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            List<double> inputData;

            //get input data:
            if (this.is_call_from_online == true)
                inputData = this.buffer;     
  
            else
                inputData = ReadFile.readFileIntoList(txtFileName.Text);

            var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time

            //get NLength
            int NLength = Convert.ToInt16(txtNLength.Text);
            //get max entry per node
            int maxEntry = Convert.ToInt16(txtMaxEntry.Text);
            //get min entry per node
            int minEntry = Convert.ToInt16(txtMinEntry.Text);
            //get R, it should be 5-10 percent
            int R = Convert.ToInt16(txtR.Text);
            //get D (reduced dimesion)
            int D = Convert.ToInt16(txtD.Text);

            int id_item = int.MinValue;
            RTree<int> rtree = new RTree<int>(maxEntry, minEntry);

            List<int> candidateList = new List<int>();
            List<int> beginIndexInner = new List<int>();

            double best_so_far_dist = 0;
            int best_so_far_loc = 0;

            double nearest_neighbor_dist = 0;
            double dist = 0;
            bool break_to_outer_loop = false;

            bool[] is_skip_at_p = new bool[inputData.Count];
            for (int i = 0; i < inputData.Count; i++)
                is_skip_at_p[i] = false;

            if (minEntry > maxEntry / 2)
            {
                MessageBox.Show("Requirement: MinNodePerEntry <= MaxNodePerEntry/2");
                return;
            }

            this.recList = new List<Offline.Rectangle>();
            this.id_itemList = new List<int>();

            for (int i = 0; i <= inputData.Count - NLength; i++)
            {
                List<double> subseq = inputData.GetRange(i, NLength);
                id_item++;
                Offline.Rectangle new_rec = new Offline.Rectangle(Utils.MathFunc.DTW_Min(subseq, D, R).ToArray(), Utils.MathFunc.DTW_Max(subseq, D, R).ToArray(), i);
                rtree.Add(new_rec, id_item);
                recList.Add(new_rec);
                id_itemList.Add(id_item);
            }


            Dictionary<int, Node<int>> nodeMap = rtree.getNodeMap();
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
                            dist = MathFunc.EuDistance(inputData.GetRange(p, NLength), inputData.GetRange(q, NLength));

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

            if (this.is_call_from_online == true)
            {
                this.best_so_far_loc = best_so_far_loc;
                this.best_so_far_dist = best_so_far_dist;
                this.D = D;
                this.R = R;
                this.N_length = NLength;
                this.rTree = rtree;
                this.id_item = id_item;

                this.is_call_from_online = false;
            }
            else
            {
                //return result to other variables:
                this.best_so_far_loc_offline = best_so_far_loc;
                this.best_so_far_dist_offline = best_so_far_dist;

                //print timeExe to the console:
                Console.WriteLine("ExeTime_offline=" + elapsedMs.ToString());
            }
           

            return;
        }

        private void btnRunOnl_Click(object sender, EventArgs e)
        {
            // Read training data:  (demo with 'ECG_5000' data)
            String training_filename = "ECG_5000_train.txt";
            List<double> training_data = ReadFile.readFileIntoList(training_filename);

            // Read streaming data: 
            String streaming_filename = "ECG_5000_stream.txt";
            List<double> streaming_data = ReadFile.readFileIntoList(streaming_filename);
        
            //create a new buffer. In this demo, the buffer is training_data (edit later):
            this.buffer = new List<double>();
            buffer.AddRange(training_data);

            // Call 'Run Offline' for the first time, store results into variables ("this" object):
            this.is_call_from_online = true;
            btnRun_Click(sender, e);

            //store the last subsequence of the buffer:
            List<double> last_sub = buffer.GetRange(buffer.Count - this.N_length, this.N_length);

            // keep streaming until we have no more data points
            for (int index_stream = 0; index_stream < streaming_data.Count; index_stream++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time

                double new_data_point = streaming_data[index_stream];

                //update last_sub at time t to get new_sub at time (t+1):
                last_sub.Add(new_data_point);
                last_sub.RemoveAt(0);
                List<double> new_sub = last_sub; // the same object

                // Insert the new entry into the tree:
                this.id_item++;

                // Add the new rec to the tree:
                Offline.Rectangle new_rec = new Offline.Rectangle(Utils.MathFunc.DTW_Min(new_sub, this.D, this.R).ToArray(), Utils.MathFunc.DTW_Max(new_sub, this.D, this.R).ToArray(), buffer.Count - this.N_length + 1 + index_stream);
                this.rTree.Add(new_rec, id_item);
                recList.Add(new_rec);
                this.id_itemList.Add(id_item);

                //remove the oldest entry:
                this.rTree.Delete(this.recList[index_stream], this.id_itemList[index_stream]);
                
                // update buffer:
                buffer.Add(new_data_point);
                buffer.RemoveAt(0);

                /* 'til now, we have already updated the tree.
                 from now on, almost just copy the offline code:
                 */

                RunOffline_Copy(buffer, index_stream);

                watch.Stop(); //stop timer
                long elapsedMs = watch.ElapsedMilliseconds;
                //this.txtExeTime.Text = elapsedMs.ToString();

                Console.WriteLine("ExeTime_Online=" + elapsedMs.ToString());

                //call offline version to assure the results and compare the time executions:
                btnRun_Click(sender, e);

                //check:
                if (this.best_so_far_loc == this.best_so_far_loc_offline)
                    Console.WriteLine("checked, ok.");
                else
                {
                    Console.WriteLine("The results are different. Stop Streaming !!!");
                    return;
                }

                Console.WriteLine("------------------------");

            } // end For loop (streaming)

        } //end btnRunOnl_Click function


        public void RunOffline_Copy(List<double> buffer, int index_stream)
        {
            /* This function is almost  the same as Offline version. We just edit some lines*/

            List<int> candidateList = new List<int>();
            List<int> beginIndexInner = new List<int>();

            double best_so_far_dist = 0;
            int best_so_far_loc = 0;

            double nearest_neighbor_dist = 0;
            double dist = 0;
            bool break_to_outer_loop = false;

            bool[] is_skip_at_p = new bool[buffer.Count];
            for (int i = 0; i < buffer.Count; i++)
                is_skip_at_p[i] = false;


            Dictionary<int, Node<int>> nodeMap = this.rTree.getNodeMap();
            List<Node<int>> leafNodes = nodeMap.Values.Where(node => node.level == 1).OrderBy(node => node.entryCount).ToList();

            for (int i = 0; i < leafNodes.Count; i++)
            {
                List<Offline.Rectangle> leafEntries = leafNodes[i].entries.Where(mbr => mbr != null).Select(mbr => mbr).ToList();
                if (leafEntries.Count > 0)
                {
                    int beginIndex = candidateList.Count;

                    // we change a bit at the following line, we subtract mbr indice by "index_stream + 1":
                    candidateList = candidateList.Concat(leafEntries.Select(mbr => mbr.getIndexSubSeq(index_stream+1))).ToList();
                    beginIndexInner = beginIndexInner.Concat(Enumerable.Range(1, leafEntries.Count).Select(x => beginIndex)).ToList();
                }
            }

            List<int> candidateList_Distinct = candidateList.Distinct().ToList();

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
                    //Console.WriteLine("innerList.Min()=" + innerList.Min());

                    foreach (int q in innerList)// inner loop
                    {
                        //update q, because q will be greater when we're streaming:
                        //int q_edit = q - index_stream - 1;
                        int q_edit = q;

                        if (Math.Abs(p - q_edit) < this.N_length)
                        {
                            continue;// self-match => skip to the next one
                        }
                        else
                        {
                            //calculate the Distance between p and q
                            dist = MathFunc.EuDistance(buffer.GetRange(p, this.N_length), buffer.GetRange(q_edit, this.N_length));

                            if (dist < best_so_far_dist)
                            {
                                //skip the element q at oute_loop, 'cuz if (p,q) is not a solution, so does (q,p).
                                is_skip_at_p[q_edit] = true;

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

            //update the results:
            this.best_so_far_loc = best_so_far_loc;
            this.best_so_far_dist = best_so_far_dist;

            Console.WriteLine("index_stream=" + index_stream);
            Console.WriteLine("best_so_far_loc=" + best_so_far_loc);
            Console.WriteLine("best_so_far_dist=" + best_so_far_dist);
            Console.WriteLine("------------------------");

            return;
        } // end RunOffline_Copy function
    } //end class
} // end file
