using System;
using System.Collections.Generic;

namespace icFlow
{
    public class ExtendableList<T> : List<T> where T : new()
    {
        public int _actualCount;
        public int actualCount { 
        get { return _actualCount; }
            set { _actualCount = value;
                Extend(value);
            }
        }

        void Extend(int nElems)
        {
            int missing = nElems - Count;
            for (int i = 0; i < missing; i++) Add(new T());
        }
    }
}
