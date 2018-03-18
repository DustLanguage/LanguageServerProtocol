using LanguageServer.Json;

namespace LanguageServer.Client
{
  public class IdGenerator
  {
    public static IdGenerator instance = new IdGenerator();

    private long id;

    public IdGenerator()
    {
      id = 0L;
    }

    public IdGenerator(long initialValue)
    {
      id = initialValue;
    }

    public NumberOrString Next()
    {
      var ns = new NumberOrString(id);
      id = id == long.MaxValue ? 0L : id + 1;
      return ns;
    }
  }
}