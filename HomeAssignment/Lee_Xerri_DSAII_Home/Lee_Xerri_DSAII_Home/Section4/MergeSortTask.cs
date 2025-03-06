using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee_Xerri_DSAII_Home.Section4
{
    public class MergeSortTask
    {
        public static List<int> MergeSort(List<int> numbers)
        {
            List<int> numbersCopy = new List<int>(numbers);

            return MergeSortRecursive(numbersCopy);
        }

        private static List<int> MergeSortRecursive(List<int> numbers)
        {
            if (numbers.Count <= 1)
            {
                return numbers;
            }

            int middle = numbers.Count / 2;

            List<int> leftHalf = numbers.GetRange(0, middle);
            List<int> rightHalf = numbers.GetRange(middle, numbers.Count - middle);

            leftHalf = MergeSortRecursive(leftHalf);
            rightHalf = MergeSortRecursive(rightHalf);

            return Merge(leftHalf, rightHalf);
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            List<int> merged = new List<int>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i] <= right[j])
                {
                    merged.Add(left[i]);
                    i++;
                }
                else
                {
                    merged.Add(right[j]);
                    j++;
                }
            }

            while (i < left.Count)
            {
                merged.Add(left[i]);
                i++;
            }

            while (j < right.Count)
            {
                merged.Add(right[j]);
                j++;
            }

            return merged;
        }
    }
}
