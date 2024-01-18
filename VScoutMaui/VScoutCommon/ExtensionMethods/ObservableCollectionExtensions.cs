using System;
using System.Collections.ObjectModel;

namespace VScoutCommon.ExtensionMethods
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> @this, IEnumerable<T> itemsToAdd)
        {
            foreach (T item in itemsToAdd)
            {
                @this.Add(item);
            }
        }
    }
}
