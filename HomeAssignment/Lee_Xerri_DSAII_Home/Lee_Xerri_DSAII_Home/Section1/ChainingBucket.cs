using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee_Xerri_DSAII_Home.Section1
{
    class ChainingBucket<K, V> where K : IEquatable<K>
    {
        internal ChainingBucket(K key, V value)
        {
            Key = key;
            Value = value;
            Next = null;
        }

        internal K Key { get; set; }

        internal V Value { get; set; }

        internal ChainingBucket<K, V>? Next { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(Key.ToString());
            stringBuilder.Append(":");

            // Value?.ToString() will return the ToString if the Value is NOT null
            // Value?.ToString() will return NULL if the Value is NULL
            // X ?? newValue will return X is X is NOT NULL and newValue otherwise
            stringBuilder.Append(Value?.ToString() ?? "NULL");

            return stringBuilder.ToString();
        }
    }
}
