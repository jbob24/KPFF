using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPFF.Business
{
    public interface ICache
    {
        object GetItem(string key);
        void InsertItem(string key, object value);
    }
}
