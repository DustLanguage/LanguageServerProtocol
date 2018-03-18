using System.Collections.Generic;

namespace LanguageServer
{
  internal class SyncDictionary<TKey, TValue>
  {
    private readonly Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
    private readonly object lockObj = new object();

    public void Set(TKey key, TValue value)
    {
      lock (lockObj)
      {
        dict[key] = value;
      }
    }

    public bool TryRemove(TKey key, out TValue value)
    {
      lock (lockObj)
      {
        if (dict.TryGetValue(key, out value))
        {
          dict.Remove(key);
          return true;
        }
      }

      return false;
    }

    public bool Remove(TKey key)
    {
      lock (lockObj)
      {
        return dict.Remove(key);
      }
    }
  }
}