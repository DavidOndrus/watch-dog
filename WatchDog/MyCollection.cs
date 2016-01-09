using System;
using System.Collections.ObjectModel;
using System.IO;

namespace WatchDog
{
    public class MyCollection
    {
        private string[] source;

        public MyCollection(string[] source)
        {
            this.source = source;
        }

        public ObservableCollection<ProgramItem> makeCollection()
        {
            if (source.Length == 0)
                throw new InvalidDataException("MyCollection.source array is empty. Can't make collection.");

            ObservableCollection<ProgramItem> collection = new ObservableCollection<ProgramItem>();
            foreach (string line in source)
            {
                string[] lineParts = line.Split(new string[] { Constants.SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
                collection.Add(new ProgramItem(lineParts[0], lineParts[1]));
            }
            
            return collection;
        }
    }
}
