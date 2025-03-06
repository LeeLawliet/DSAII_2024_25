using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Lee_Xerri_DSAII_Home.Section1
{
    class ChainingHashTable<K, V> where K : IEquatable<K>
    {
        private const int DEFAULT_ARRAY_LENGTH = 3;
        private const float MAX_LOAD_FACTOR = 0.8f;

        public int Size { get; private set; } = 0;

        public double LoadFactor
        {
            get
            {
                return Convert.ToDouble(Size) / Convert.ToDouble(_array.Length);
            }
        }

        private void Rehash()
        {
            ChainingBucket<K, V>[] oldArray = _array;
            _array = new ChainingBucket<K, V>[oldArray.Length * 2];
            Size = 0;

            foreach (var bucket in oldArray)
            {
                ChainingBucket<K, V>? current = bucket;
                while (current != null)
                {
                    AddOrUpdate(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }

        private ChainingBucket<K, V>[] _array = new ChainingBucket<K, V>[DEFAULT_ARRAY_LENGTH];

        public void AddOrUpdate(K key, V value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), $"The value of {nameof(key)} is NULL. The key must not be NULL!");
            }

            if (LoadFactor > MAX_LOAD_FACTOR)
            {
                Rehash();
            }

            int hashValue = (key.GetHashCode() & 0x7FFFFFFF)
                            % _array.Length;

            ChainingBucket<K, V>? bucket = _array[hashValue];

            if (bucket == null)
            {
                _array[hashValue] = new ChainingBucket<K, V>(key, value);
                Size++;
                return;
            }

            ChainingBucket<K, V>? cursor = bucket;
            int bucketLength = 1;

            while (cursor != null)
            {
                if (cursor.Key.Equals(key))
                {
                    cursor.Value = value;
                    return;
                }

                if (cursor.Next == null)
                    break;

                cursor = cursor.Next;
                bucketLength++;
            }

            cursor.Next = new ChainingBucket<K, V>(key, value);
            Size++;

            if (bucketLength >= DEFAULT_ARRAY_LENGTH)
                Rehash();
        }

        public V Get(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), $"The value of {nameof(key)} is NULL. The key must not be NULL!");
            }

            int hashValue = (key.GetHashCode() & 0x7FFFFFFF)
                            % _array.Length;

            ChainingBucket<K, V>? bucket = _array[hashValue];

            if (bucket == null)
            {
                throw new KeyNotFoundException();
            }

            while (bucket != null)
            {
                if (bucket.Key.Equals(key))
                {
                    return bucket.Value;
                }

                bucket = bucket.Next;
            }

            throw new KeyNotFoundException();
        }

        public List<K> GetAll()
        {
            List<K> keys = new List<K>();

            for (int i = 0; i < _array.Length; i++)
            {
                ChainingBucket<K, V>? bucket = _array[i];

                while (bucket != null)
                {
                    keys.Add(bucket.Key);
                    bucket = bucket.Next;
                }
            }

            return keys;
        }

        public V Remove(K key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "The key must not be NULL!");
            }

            int hashValue = (key.GetHashCode() & 0x7FFFFFFF) % _array.Length;
            ChainingBucket<K, V>? bucket = _array[hashValue];

            if (bucket == null)
            {
                throw new KeyNotFoundException("Key not found.");
            }

            ChainingBucket<K, V>? previous = null;
            ChainingBucket<K, V>? current = bucket;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    V value = current.Value;

                    if (previous == null)
                    {
                        _array[hashValue] = current.Next; // Update head of the list
                    }
                    else
                    {
                        previous.Next = current.Next; // Remove the node
                    }

                    Size--;
                    return value;
                }

                previous = current;
                current = current.Next;
            }

            throw new KeyNotFoundException("Key not found.");
        }
    }
}