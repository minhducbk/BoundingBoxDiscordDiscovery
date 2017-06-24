using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundingBoxDiscordDiscovery.Utils
{
    class WriteFile
    {
        static public void WriteFile_Function(List<double> data, int N_LENGTH, int best_so_far_loc)
        {
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Output\", "output_SAX.csv");
            string delimiter = ",";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(delimiter, new string[] { "index", "data", "is_discord" }));
            for (int index = 0; index < data.Count; index++)
            {
                string is_discord = "N";
                if (index >= best_so_far_loc && index <= best_so_far_loc + N_LENGTH - 1)
                    is_discord = "Y";
                sb.AppendLine(string.Join(delimiter, new string[] { index.ToString(), data[index].ToString(), is_discord }));
            }
            System.IO.File.WriteAllText(filePath, sb.ToString());
        }

        static public void WriteFile_2(List<double> dist_pq, string name = "dist_pq.csv")
        {
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Output\", name);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(name);
            for (int index = 0; index < dist_pq.Count; index++)
            {

                sb.AppendLine(dist_pq[index].ToString());
            }
            System.IO.File.WriteAllText(filePath, sb.ToString());
        }

        static public void rewriteStreamFile(string file_name, int num_line_to_skip = 0, int number_data = 2000)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data_Stream\", file_name);
            List<double> data_stream = new List<double>();

            //read file
            string[] lines = System.IO.File.ReadAllLines(path);

            //skip the header
            lines = lines.Skip(num_line_to_skip).ToArray();

            int i = 0;
            using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(path, false))
            {
                foreach (string line in lines)
                {
                    if (i < number_data)
                    {
                        string[] values = line.Split(',');
                        if (i + values.Count() <= number_data)
                        {
                            file.WriteLine(line);
                            i += values.Count();
                        }
                        else
                        {
                            file.WriteLine(String.Join(",", values.Take(number_data - i)));
                            i += number_data - i;
                        }

                    }
                }

            }

        }
    }
}
