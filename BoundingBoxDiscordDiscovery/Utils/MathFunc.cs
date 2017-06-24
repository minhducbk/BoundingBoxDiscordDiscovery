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
    }
}
