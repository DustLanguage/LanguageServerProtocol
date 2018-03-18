namespace LanguageServer.Parameters.TextDocument
{
  public class Command
  {
    public string Title { get; set; }

    // Not sure if this is correct
    public string Name { get; set; }

    public dynamic[] Arguments { get; set; }
  }
}