using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundingBoxDiscordDiscovery.Offline
{
    public class RStar<T> : RTree<T>
    {
        private int height;
        private int[] numberCalledOFT;
        public Node<T> ChooseSubTree(Node<T> N, Rectangle E)
        {
            return null;
        }

        public void Split()
        {

        }

        public Tuple<List<int>,List<int>> ChooseSplitAxis()
        {
            return new Tuple<List<int>, List<int>>(new List<int>( new int[] {}), new List<int>(new int[] { }) );
        }

        public int ChooseSplitIndex()
        {
            return 0;
        }

        public void Insert(Rectangle E) { }

        public void ReInsert(int level) { }

        public void OverFlowTreatment(Node<T> N, int level) { }
    }
}
