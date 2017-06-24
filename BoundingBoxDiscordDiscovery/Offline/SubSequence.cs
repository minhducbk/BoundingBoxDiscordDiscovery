using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundingBoxDiscordDiscovery.Offline
{
    public class SubSequence
    {
        /// <summary>
        /// Number of dimensions in a time series subsequence.
        /// </summary>
        private int DIMENSIONS;

        /// <summary>
        /// The (x, y) coordinates of the point.
        /// </summary>
        internal double[] coordinates;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">The x coordinate of the point</param>
        /// <param name="y">The y coordinate of the point</param>
        /// <param name="z">The z coordinate of the point</param>
        public SubSequence(List<double> timeSeries, int nLength)
        {
            DIMENSIONS = nLength;
            coordinates = new double[DIMENSIONS];
            for (int i =0; i < DIMENSIONS; i++)
            {
                coordinates[i] = timeSeries[i];
            }
        }
    }
}
