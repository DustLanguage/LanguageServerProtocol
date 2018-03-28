using LanguageServer.Json;

namespace LanguageServer.Parameters.Client
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
      NumberOrString ns = new NumberOrString(id);
      id = id == long.MaxValue ? 0L : id + 1;
      return ns;
    }
  }
}