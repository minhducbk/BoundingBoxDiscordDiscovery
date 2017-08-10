using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundingBoxDiscordDiscovery.Utils
{
    class MathFunc
    {

        static public List<double> zScoreNorm(List<double> raw_data, int raw_data_len)
        {
            List<double> normalized_seg = new List<double>();
            double sum = 0, mean = 0, std = 0, sum_square_differences = 0;

            //calc mean:
            sum = raw_data.Sum();
            mean = sum / (raw_data_len * 1.0);

            //calc std:
            sum_square_differences = raw_data.Sum(x => (x - mean) * (x - mean));
            std = Math.Sqrt(sum_square_differences / (raw_data_len * 1.0));

            //calc normalized points:
            for (int i = 0; i < raw_data_len; i++)
            {
                normalized_seg.Add((raw_data[i] - mean) / std);
            }

            return normalized_seg;
        }

        static public double EuDistance(List<double> t1, List<double> t2)
        {
            double dist = Math.Sqrt(t1.Zip(t2, (a, b) => (a - b) * (a - b)).Sum());
            return dist;
        }

        static public double CalcMean(List<double> data)
        {
            return data.Sum() / (data.Count * 1.0);
        }

        static public double CalcStd(List<double> data, double mean)
        {
            //for (int i = 0; i < data.Count; i++)
            //    data[i] = Math.Pow(data[i] - mean, 2);
            double sum_square_differences = data.Sum(x => (x - mean) * (x - mean));
            double std = Math.Sqrt(sum_square_differences / (data.Count * 1.0));
            return std;
        }

        static public double CalcStdMeanZero(List<double> data)
        {
            double sum_square_differences = data.Sum(x => x * x);
            double std = Math.Sqrt(sum_square_differences / data.Count);
            return std;
        }

        static public double CalcNewMean(double old_mean, int N_length, double old_value, double new_value)
        {
            old_mean = (old_mean * N_length + (new_value - old_value)) / (N_length * 1.0);
            return old_mean;
        }

        static public List<double> Upper(List<double> sequence, int r)
        {
            List<double> U = new List<double>();
            for (int i = 0; i < sequence.Count; i++)
            {
                U.Add(sequence[i]);
                for (int j = 1; j <= r; j++)
                {
                    if (i - j >= 0)
                        U[i] = Math.Max(U[i], sequence[i - j]);
                    if (i + j < sequence.Count)
                        U[i] = Math.Max(U[i], sequence[i + j]);
                }
            }
            return U;
        }

        static public List<double> Lower(List<double> sequence, int r)
        {
            List<double> U = new List<double>();
            for (int i = 0; i < sequence.Count; i++)
            {
                U.Add(sequence[i]);
                for (int j = 1; j <= r; j++)
                {
                    if (i - j >= 0)
                        U[i] = Math.Min(U[i], sequence[i - j]);
                    if (i + j < sequence.Count)
                        U[i] = Math.Min(U[i], sequence[i + j]);
                }
            }
            return U;
        }

        static public List<double> PAA(List<double> sequence, int W_LENGTH)
        {
            int N_LENGTH = sequence.Count;
            List<double> c_w = new List<double>();
            if (N_LENGTH % W_LENGTH != 0)
            {
                c_w.Clear();// reset C_w

                for (int j = 0; j < W_LENGTH; j++)
                    c_w.Add(0);//set initial value for C_w

                for (int j = 0; j < N_LENGTH * W_LENGTH; j++)
                {
                    c_w[j / N_LENGTH] += sequence[j / W_LENGTH] / N_LENGTH;
                }
            }
            else
            {
                double c_i;
                int from_index, to_index;
                c_w.Clear();// reset C_w
                
                //Calculate C_w 
                for (int w_start = 0; w_start < W_LENGTH; w_start++)
                {
                    from_index = (N_LENGTH / W_LENGTH) * w_start;
                    to_index = (N_LENGTH / W_LENGTH) * (w_start + 1) - 1;

                    c_w[w_start] = 0;
                    for (int j = from_index; j <= to_index; j++)
                    {
                        c_w[w_start] += sequence[j];
                    }
                    c_w[w_start] *=(W_LENGTH / (double)(N_LENGTH));
                }
            }
            return c_w;
        }

        static public List<double> DTW_Max(List<double> sequence, int W_LENGTH, int r)
        {
            int N_LENGTH = sequence.Count;
            List<double> c_w = new List<double>();
            sequence = Upper(sequence, r);
            if (N_LENGTH % W_LENGTH != 0)
            {
                c_w.Clear();// reset C_w
                string s = String.Empty;//initialize the SAX word

                for (int j = 0; j < W_LENGTH; j++)
                    c_w.Add(-Constant.INFINITE);//set initial value for C_w

                for (int j = 0; j < N_LENGTH * W_LENGTH; j++)
                {
                    c_w[j / N_LENGTH] = Math.Max(c_w[j / N_LENGTH], sequence[j / W_LENGTH]);
                }
            }
            else
            {
                int from_index, to_index;
                c_w.Clear();// reset C_w
                
                //Calculate C_w 
                for (int w_start = 0; w_start < W_LENGTH; w_start++)
                {
                    from_index = (N_LENGTH / W_LENGTH) * w_start;
                    to_index = (N_LENGTH / W_LENGTH) * (w_start + 1) - 1;

                    c_w[w_start] = -Constant.INFINITE;
                    for (int j = from_index; j <= to_index; j++)
                    {
                        c_w[w_start] = Math.Max(c_w[w_start], sequence[j]);
                    }
                }
            }
            return c_w;
        }

        static public List<double> DTW_Min(List<double> sequence, int W_LENGTH, int r)
        {
            int N_LENGTH = sequence.Count;
            List<double> c_w = new List<double>();
            sequence = Lower(sequence, r);
            if (N_LENGTH % W_LENGTH != 0)
            {
                c_w.Clear();// reset C_w
                string s = String.Empty;//initialize the SAX word

                for (int j = 0; j < W_LENGTH; j++)
                    c_w.Add(Constant.INFINITE);//set initial value for C_w

                for (int j = 0; j < N_LENGTH * W_LENGTH; j++)
                {
                    c_w[j / N_LENGTH] = Math.Min(c_w[j / N_LENGTH], sequence[j / W_LENGTH]);
                }
            }
            else
            {
                int from_index, to_index;
                c_w.Clear();// reset C_w

                //Calculate C_w 
                for (int w_start = 0; w_start < W_LENGTH; w_start++)
                {
                    from_index = (N_LENGTH / W_LENGTH) * w_start;
                    to_index = (N_LENGTH / W_LENGTH) * (w_start + 1) - 1;

                    c_w[w_start] = Constant.INFINITE;
                    for (int j = from_index; j <= to_index; j++)
                    {
                        c_w[w_start] = Math.Min(c_w[w_start], sequence[j]);
                    }
                }
            }
            return c_w;
        }

        static double LB_PAA(List<double> C, Offline.Rectangle Q)
        {
            double distance = 0;
            List<double> CMacron = PAA(C, Q.DIMENSIONS);
            for (int i = 0; i < Q.DIMENSIONS; i++)
            {
                if (CMacron[i] > Q.max[i])
                    distance += Math.Pow(CMacron[i] - Q.max[i], 2);
                else
                    if ((CMacron[i] < Q.min[i]))
                    distance += Math.Pow(CMacron[i] - Q.min[i], 2);
            }
            return Math.Sqrt(distance);
        }

        static double MINDIST(Offline.Rectangle Q, Offline.Rectangle R)
        {
            double distance = 0;
            for (int i = 0; i < Q.DIMENSIONS; i++)
            {
                if (R.min[i] > Q.max[i])
                    distance += Math.Pow(R.min[i] - Q.max[i], 2);
                else
                    if ((R.max[i] < Q.min[i]))
                    distance += Math.Pow(R.max[i] - Q.min[i], 2);
            }
            return Math.Sqrt(distance);
        }
    }
}
