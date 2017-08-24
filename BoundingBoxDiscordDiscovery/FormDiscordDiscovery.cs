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
        public RTree<int> this_RTree;

        // Store best_loc and best_dic from Run Offline:
        public int this_best_so_far_loc;
        public double this_best_so_far_dist;
        public int this_best_so_far_loc_offline;
        public double this_best_so_far_dist_offline;

        public int this_NLength;
        public int this_D;
        public int this_R;

        public List<Offline.Rectangle> this_recList;
        public List<int> this_id_itemList;
        public int this_id_item;

        public List<double> this_buffer;
        

        bool this_first_call_from_online;
        bool this_is_called_to_check;
        long this_exeTimeOffline;

        public FormDiscordDiscovery()
        {
            this.this_first_call_from_online = false;
            this.this_is_called_to_check = false;
            InitializeComponent();
        }

        private void btnRunOffline_Click(object sender, EventArgs e)
        {
            List<double> inputData;

            //get input data:
            if (this.this_first_call_from_online == true || this.this_is_called_to_check)
                inputData = this.this_buffer;     
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

            List<Offline.Rectangle>  recList = new List<Offline.Rectangle>();
            List<int>  id_itemList = new List<int>();

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
                    candidateList.AddRange(leafEntries.Select(mbr => mbr.getIndexSubSeq()));
                    beginIndexInner.AddRange(Enumerable.Range(1, leafEntries.Count).Select(x => beginIndex));
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
                    List<int> innerList = candidateList.GetRange(beginIndexInner[i], candidateList.Count - beginIndexInner[i]);
                    List<int> headCandidate = candidateList.GetRange(0, beginIndexInner[i]);
                    innerList.AddRange(headCandidate);

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

            if (this.this_first_call_from_online == true)
            {
                this.this_best_so_far_loc = best_so_far_loc;
                this.this_best_so_far_dist = best_so_far_dist;
                this.this_D = D;
                this.this_R = R;
                this.this_NLength = NLength;
                this.this_RTree = rtree;
                this.this_id_item = id_item;
                this.this_recList = recList;
                this.this_id_itemList = id_itemList;     
            }
            else
            {
                //return result to other variables:
                this.this_best_so_far_loc_offline = best_so_far_loc;
                this.this_best_so_far_dist_offline = best_so_far_dist;
                this.this_exeTimeOffline = elapsedMs;

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
            this.this_buffer = new List<double>();
            this_buffer.AddRange(training_data);

            // Call 'Run Offline' for the first time, store results into variables ("this" object):
            this.this_first_call_from_online = true;
            btnRunOffline_Click(sender, e);
            this.this_first_call_from_online = false;

            //store the last subsequence of the buffer:
            List<double> last_sub = this_buffer.GetRange(this_buffer.Count - this.this_NLength, this.this_NLength);

            this.this_is_called_to_check = true; // Edit the inputData (assign it to the Buffer) to check with online
            
            // STREAMING: keep streaming until we have no more data points
            for (int index_stream = 0; index_stream < streaming_data.Count; index_stream++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();///calc execution time

                double new_data_point = streaming_data[index_stream];

                //update last_sub at time t to get new_sub at time (t+1):
                last_sub.Add(new_data_point);
                last_sub.RemoveAt(0);
                List<double> new_sub = last_sub; // the same object

                // Insert the new entry into the tree:
                this.this_id_item++;

                // Add the new rec to the tree:
                Offline.Rectangle new_rec = new Offline.Rectangle(Utils.MathFunc.DTW_Min(new_sub, this.this_D, this.this_R).ToArray(), Utils.MathFunc.DTW_Max(new_sub, this.this_D, this.this_R).ToArray(), this_buffer.Count - this.this_NLength + 1 + index_stream);
                this.this_RTree.Add(new_rec, this_id_item);
                this_recList.Add(new_rec);
                this.this_id_itemList.Add(this_id_item);

                //remove the oldest entry:
                this.this_RTree.Delete(this.this_recList[index_stream], this.this_id_itemList[index_stream]);

                //get the first sub before update the buffer (help to find the small match in Liu's method)
                List<double> first_sub = this_buffer.GetRange(0, this.this_NLength);
                
                // update buffer:
                this_buffer.Add(new_data_point);
                this_buffer.RemoveAt(0);

                /* 'til now, we have already updated the tree.
                 from now on, almost just copy the offline code:
                 */

                //Method 1: just re-order the 2 loops:
                //RunOnline_Method1(this_buffer, index_stream);

                //Method 2: Liu's algorithm:
                //Note: Method_2 includes method_1 (case a)
                //RunOnline_LiuMethod_origin(this_buffer, index_stream, first_sub);

                //Method 3: motified_Liu's
                RunOnline_LiuMethod_edited(this_buffer, index_stream, first_sub);

                watch.Stop(); //stop timer
                long elapsedMs = watch.ElapsedMilliseconds;
                //this.txtExeTime.Text = elapsedMs.ToString();

                Console.WriteLine("ExeTime_Online=" + elapsedMs.ToString());

                //call offline version to assure the results and compare the time executions:
                btnRunOffline_Click(sender, e);

                //check:
                if (Math.Abs(this.this_best_so_far_dist - this.this_best_so_far_dist_offline) < 10e-7)
                {
                    Console.WriteLine("checked, ok.");
                    if (elapsedMs > this_exeTimeOffline)
                    {
                        Console.WriteLine("Online takes more time than Offline !!!");
                    }
                }
                else
                {
                    Console.WriteLine("this.best_so_far_dist = " + this.this_best_so_far_dist);
                    Console.WriteLine("this.best_so_far_dist_Offline = " + this.this_best_so_far_dist_offline);
                    Console.WriteLine("The results are different. Stop Streaming !!!");
                    return;
                }

                Console.WriteLine("------------------------");

            } // end For loop (streaming)
            this.this_is_called_to_check = false;

        } //end btnRunOnl_Click function

        public void RunOnline_Method1(List<double> buffer, int index_stream, List<int> candidate_list_reduced = null)
        {
            /* This function is almost  the same as Offline version. We just edit some lines*/

            List<int> candidateList;
            List<int> beginIndexInner = new List<int>();
            
            bool inner_loop_follows_reducedCandidates = false;

            Dictionary<int, Node<int>> nodeMap = this.this_RTree.getNodeMap();
            List<Node<int>> leafNodes = nodeMap.Values.Where(node => node.level == 1).OrderBy(node => node.entryCount).ToList();

            if (candidate_list_reduced != null) // the outer loop is calculated by Liu's algorithm
            {
                candidateList = candidate_list_reduced;
                inner_loop_follows_reducedCandidates = true;
            }
            else // Duc Lun's version:
            {
                candidateList = new List<int>();

                for (int i = 0; i < leafNodes.Count; i++)
                {
                    List<Offline.Rectangle> leafEntries = leafNodes[i].entries.Where(mbr => mbr != null).Select(mbr => mbr).ToList();
                    if (leafEntries.Count > 0)
                    {
                        int beginIndex = candidateList.Count;

                        // we change a bit at the following line, we subtract mbr indice by "index_stream + 1":
                        candidateList.AddRange(leafEntries.Select(mbr => mbr.getIndexSubSeq(index_stream + 1)));
                        beginIndexInner.AddRange(Enumerable.Range(1, leafEntries.Count).Select(x => beginIndex));
                    }
                }
            }
            
            double best_so_far_dist = 0;
            int best_so_far_loc = 0;

            double nearest_neighbor_dist = 0;
            double dist = 0;
            bool break_to_outer_loop = false;

            bool[] is_skip_at_p = new bool[buffer.Count];
            for (int i = 0; i < buffer.Count; i++)
                is_skip_at_p[i] = false;

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
                    List<int> innerList;

                    if (inner_loop_follows_reducedCandidates == false) // Duc Lun's method
                    {
                        innerList = candidateList.GetRange(beginIndexInner[i], candidateList.Count - beginIndexInner[i]);
                        List<int> headCandidate = candidateList.GetRange(0, beginIndexInner[i]);
                        innerList.AddRange(headCandidate);
                    }
                    else 
                    {
                        innerList = new List<int>();
                        for (int num = 0; num < leafNodes.Count; num++)
                        {
                            List<int> all_entry_IDs_from_a_node = leafNodes[num].entries.Where(mbr => (mbr != null) && (mbr.getIndexSubSeq(index_stream + 1) != p)).Select(mbr => mbr.getIndexSubSeq(index_stream + 1)).ToList();

                            if (all_entry_IDs_from_a_node.Count == leafNodes[num].entryCount) // if p in the outer loop is NOT in this leaf:
                            {
                                innerList.AddRange(all_entry_IDs_from_a_node);
                            }
                            else // If p is IN this leaf: We add all entry ids in the leaf to the head of the innerList 
                            {
                                innerList.InsertRange(0, all_entry_IDs_from_a_node);
                                // note: In this case, all_Entry_IDs_from_a_node doesnt include 'p' id.
                            }
                        }
                    }
                   
                    foreach (int q in innerList)// inner loop
                    {
                        //update q, because q will be greater when we're streaming:
                        //int q_edit = q - index_stream - 1;
                        int q_edit = q;

                        if (Math.Abs(p - q_edit) < this.this_NLength)
                        {
                            continue;// self-match => skip to the next one
                        }
                        else
                        {
                            //calculate the Distance between p and q
                            dist = MathFunc.EuDistance(buffer.GetRange(p, this.this_NLength), buffer.GetRange(q_edit, this.this_NLength));

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
            this.this_best_so_far_loc = best_so_far_loc;
            this.this_best_so_far_dist = best_so_far_dist;

            Console.WriteLine("index_stream=" + index_stream);
            Console.WriteLine("best_so_far_loc=" + best_so_far_loc);
            Console.WriteLine("best_so_far_dist=" + best_so_far_dist);

            return;
        } // end RunOffline_Copy function

        /* Called by RunOnline_LiuMethod_edited:*/
       public void Liu_edit(List<double> buffer, int index_stream, int first_candidate, int second_candidate, List<double> removed_sub)
        {
            List<int> candidateList = new List<int>();
            List<int> beginIndexInner = new List<int>();

            Dictionary<int, Node<int>> nodeMap = this.this_RTree.getNodeMap();
       
            List<Node<int>> leafNodes = nodeMap.Values.Where(node => ((node.level == 1))).OrderBy(node => node.entryCount).ToList();

            for (int num = 0; num < leafNodes.Count; num++)
            {
                List<Offline.Rectangle> leafEntries = leafNodes[num].entries.Where(mbr => mbr != null).Select(mbr => mbr).ToList();
                if (leafEntries.Count > 0)
                {
                    int beginIndex = candidateList.Count;
                    // we change a bit at the following line, we subtract mbr indice by "index_stream + 1":
                    candidateList.AddRange(leafEntries.Select(mbr => mbr.getIndexSubSeq(index_stream + 1)));
                    beginIndexInner.AddRange(Enumerable.Range(1, leafEntries.Count).Select(x => beginIndex));
                }
            } // end for

            // get the two first candidates to the head of candidateList
            int count = 0;
            int index = 0;
            while(count < 1)
            {
                if (candidateList[index] == first_candidate)
                {
                    candidateList[index] = candidateList[0];
                    int temp = beginIndexInner[index];
                    beginIndexInner[index] = beginIndexInner[0];
                    beginIndexInner[0] = temp;
                    count++;
                }
                if (candidateList[index] == second_candidate)
                {
                    candidateList[index] = candidateList[1];
                    int temp = beginIndexInner[index];
                    beginIndexInner[index] = beginIndexInner[1];
                    beginIndexInner[1] = temp;
                    count++;
                }
                index++;
            }
            candidateList[0] = first_candidate;
            candidateList[1] = second_candidate;

            double best_so_far_dist = 0;
            int best_so_far_loc = 0;

            double nearest_neighbor_dist = 0;
            double dist = 0;
            bool break_to_outer_loop = false;

            bool[] is_skip_at_p = new bool[buffer.Count];
            for (int i = 0; i < buffer.Count; i++)
                is_skip_at_p[i] = false;

            for (int i = 0; i < candidateList.Count; i++)
            {
                int p = candidateList[i];

                //check small_match:
                double small_match = Utils.MathFunc.EuDistance(buffer.GetRange(p, this_NLength), removed_sub);

                if (i >= 2 && small_match >= this_best_so_far_dist)
                {
                    continue;
                }
                if (is_skip_at_p[p])
                {
                    //p was visited at inner loop before
                    continue;
                }
                else
                {
                    nearest_neighbor_dist = Constant.INFINITE;
                    List<int> innerList;

                    innerList = candidateList.GetRange(beginIndexInner[i], candidateList.Count - beginIndexInner[i]);
                    List<int> headCandidate = candidateList.GetRange(0, beginIndexInner[i]);
                    innerList.AddRange(headCandidate);

                    foreach (int q in innerList)// inner loop
                    {
                        //update q, because q will be greater when we're streaming:
                        //int q_edit = q - index_stream - 1;
                        int q_edit = q;

                        if (Math.Abs(p - q_edit) < this.this_NLength)
                        {
                            continue;// self-match => skip to the next one
                        }
                        else
                        {
                            //calculate the Distance between p and q
                            dist = MathFunc.EuDistance(buffer.GetRange(p, this.this_NLength), buffer.GetRange(q_edit, this.this_NLength));

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
            } // end for

            bestSoFarDisVal.Text = best_so_far_dist.ToString();
            bestSoFarLocVal.Text = best_so_far_loc.ToString();

            //update the results:
            this.this_best_so_far_loc = best_so_far_loc;
            this.this_best_so_far_dist = best_so_far_dist;

            Console.WriteLine("index_stream = " + index_stream);
            Console.WriteLine("best_so_far_loc = " + best_so_far_loc);
            Console.WriteLine("best_so_far_dist = " + best_so_far_dist);

            return;
        } // end RunOnline_Liu_edit

        public void RunOnline_LiuMethod_origin(List<double> buffer, int index_stream, List<double> removed_sub)
        {
            /* Liu's algorithm */

            //calc 'currDist' which is the distance of the discord at time t and the new subsquence at time (t+1):
            double currDist = 0;
            if (this_best_so_far_loc > 0) // make sure we can calc CurrDist when 'this_best_so_far_loc - 1' >= 0
                currDist = Utils.MathFunc.EuDistance(buffer.GetRange(this_best_so_far_loc - 1, this_NLength), buffer.GetRange(buffer.Count - this_NLength, this_NLength));

            //if the case (a): Modify the Rtree: call method1
            if (currDist < this_best_so_far_dist || this_best_so_far_loc == 0)
            {
                Console.WriteLine("Running case (a)...");
                RunOnline_Method1(buffer, index_stream);
            }
            else //case (b): we can reduce the num of elements in the outer loop:
            {
                Console.WriteLine("Running case (b).........");
                List<int> candidate_list_reduced = new List<int>(); //store outer loop

                /* Find the candidate_list:*/
                //The local discord at time t:
                candidate_list_reduced.Add(this_best_so_far_loc - 1);

                //The subsequence (m-n+1, n)(t+1):
                candidate_list_reduced.Add(buffer.Count - this_NLength);

                //The small match of subsequence (1, n)(t):
                double small_match_dist = 0;
                for (int j = this_NLength - 1; j <= buffer.Count - this_NLength - 1; j++)
                {
                    small_match_dist = Utils.MathFunc.EuDistance(buffer.GetRange(j, this_NLength), removed_sub);
                    if (small_match_dist < this_best_so_far_dist)
                    {
                        if (this_best_so_far_loc-1 != j)
                            candidate_list_reduced.Add(j);
                    }
                }

                /*ok, 'til now we've already reduced at the outer loop, we do the rest by calling RunOnline_Method1:*/
                RunOnline_Method1(buffer, index_stream, candidate_list_reduced);
            } //end IF ELSE

        } // End RunOnline_LiuMethod

        public void RunOnline_LiuMethod_edited(List<double> buffer, int index_stream, List<double> removed_sub)
        {
            /* Liu's algorithm */

            //calc 'currDist' which is the distance of the discord at time t and the new subsquence at time (t+1):
            double currDist = 0;
            if (this_best_so_far_loc > 0) // make sure we can calc CurrDist when 'this_best_so_far_loc - 1' >= 0
                currDist = Utils.MathFunc.EuDistance(buffer.GetRange(this_best_so_far_loc - 1, this_NLength), buffer.GetRange(buffer.Count - this_NLength, this_NLength));

            //if the case (a): Modify the Rtree: call method1
            if (currDist < this_best_so_far_dist || this_best_so_far_loc == 0)
            {
                Console.WriteLine("Running case (a)...");
                RunOnline_Method1(buffer, index_stream);
            }
            else //case (b): we can reduce the num of elements in the outer loop:
            {
                Console.WriteLine("Running case (b).........");
                List<int> candidate_list_reduced = new List<int>(); //store outer loop

                /* Find the candidate_list:*/
                //The local discord at time t:
                int first_candidate = (this_best_so_far_loc - 1);

                //The subsequence (m-n+1, n)(t+1):
                int second_candidate = (buffer.Count - this_NLength);


                /* we will do the rest by calling Liu_edit:*/
                Liu_edit(buffer, index_stream, first_candidate, second_candidate, removed_sub);

            } //end IF ELSE

        } // End RunOnline_LiuMethod_edited



    } //end class
} // end file
