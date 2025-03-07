﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee_Xerri_DSAII_Home
{
    public class VertexDistance<V>
    {
        public V Vertex { get; set; }

        public int Distance { get; set; }

        public VertexDistance(V vertex, int distance)
        {
            Vertex = vertex;
            Distance = distance;
        }
    }
}

