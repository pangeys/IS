using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpamClassifierApp.Helpers
{
    internal class QuickSortHelper
    {
        public static void QuickSort(List<(double Distance, string Label)> list, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(list, low, high);
                QuickSort(list, low, pivotIndex - 1);
                QuickSort(list, pivotIndex + 1, high);
            }
        }

        private static int Partition(List<(double Distance, string Label)> list, int low, int high)
        {
            double pivot = list[high].Distance;
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (list[j].Distance < pivot)
                {
                    i++;
                    (list[i], list[j]) = (list[j], list[i]);
                }
            }

            (list[i + 1], list[high]) = (list[high], list[i + 1]);
            return i + 1;
        }
    }
}
