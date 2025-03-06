using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee_Xerri_DSAII_Home.Section3
{
    public class ArrayBasedVectorPriorityQueue<V>
    {
        private List<VertexDistance<V>> _priorityQueue = new List<VertexDistance<V>>();
        // private List<(V, int)> _priorityQueue = new List<(V, int)>();

        public ArrayBasedVectorPriorityQueue()
        {
        }
        public void Add(V vertexIdentifier, int distanceToSource = int.MaxValue)
        {
            if (vertexIdentifier == null)
            {
                throw new ArgumentNullException(nameof(vertexIdentifier));
            }

            _priorityQueue.Add(
                new VertexDistance<V>(vertexIdentifier, distanceToSource));
        }

        public void DecreaseKey(V vertexIdentifier, int newDistanceToSource)
        {
            // O(n) time
            VertexDistance<V>? getItem = _priorityQueue.SingleOrDefault(x => x.Vertex!.Equals(vertexIdentifier));

            if (getItem == null)
            {
                throw new ArgumentException("The vertexIdentifier in the parameter does not exist in the priority queue", nameof(getItem));
            }

            if (newDistanceToSource <= getItem.Distance)
            {
                getItem.Distance = newDistanceToSource;
            }
            else
            {
                throw new InvalidOperationException("You cannot increase the distance to source!");
            }
        }

        public bool IsEmpty()
        {
            return _priorityQueue.Count == 0;
        }

        public V RemoveMin()
        {
            VertexDistance<V>? getItem = _priorityQueue.MinBy(x => x.Distance);

            if (getItem == null)
            {
                throw new InvalidOperationException("The PriorityQueue is empty!");
            }

            _priorityQueue.Remove(getItem);
            return getItem.Vertex;
        }
    }
}
