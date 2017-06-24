using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundingBoxDiscordDiscovery.Utils
{
    class ReadFile
    {
        static public List<double> readFileIntoList(string file_name, int num_line_to_skip = 1)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", file_name);
            List<double> data = new List<double>();

            //read file
            string[] lines = System.IO.File.ReadAllLines(path);

            //skip the header
            lines = lines.Skip(num_line_to_skip).ToArray();


            foreach (string line in lines)
            {
                data.Add(Convert.ToDouble(line)); //convert into a double list then add to 'data'
            }

            return data;
        }

        static public List<double> readStreamFile(string file_name, int num_line_to_skip = 1)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data_Stream\", file_name);
            List<double> data_stream = new List<double>();

            //read file
            string[] lines = System.IO.File.ReadAllLines(path);

            //skip the header
            lines = lines.Skip(num_line_to_skip).ToArray();


            foreach (string line in lines)
            {
                data_stream.Add(Convert.ToDouble(line)); //convert into a double list then add to 'data'
                //string[] values = line.Split(',');
                //foreach(string val in values)
                //    data_stream.Add(Convert.ToDouble(val));
            }

            return data_stream;
        }

        static public void readStatisticalFile(string file_name, string algorithm, ref List<double> dist, ref List<double> loc, ref double time)
        {
            dist = new List<double>();
            loc = new List<double>();
            string extension = System.IO.Path.GetExtension(file_name);
            file_name = System.IO.Path.GetFileNameWithoutExtension(file_name);
            string dist_path = Path.Combine(Environment.CurrentDirectory, @"Output\" + file_name + "\\" + algorithm + "\\", "dist" + extension);
            string loc_path = Path.Combine(Environment.CurrentDirectory, @"Output\" + file_name + "\\" + algorithm + "\\", "loc" + extension);
            string time_path = Path.Combine(Environment.CurrentDirectory, @"Output\" + file_name + "\\" + algorithm + "\\", "time" + extension);


            //read file
            string[] lines = System.IO.File.ReadAllLines(dist_path).ToArray();

            foreach (string line in lines)
            {
                dist.Add(Convert.ToDouble(line));
            }

            //read file
            lines = System.IO.File.ReadAllLines(loc_path).ToArray();

            foreach (string line in lines)
            {
                loc.Add(Convert.ToDouble(line));
            }

            //read file
            lines = System.IO.File.ReadAllLines(time_path).ToArray();
            time = Convert.ToDouble(lines[0]);
        }
    }
}
