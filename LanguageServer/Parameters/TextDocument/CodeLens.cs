namespace LanguageServer.Parameters.TextDocument
{
  public class CodeLens
  {
    public Range Range { get; set; }

    public Command Command { get; set; }

    public dynamic Any { get; set; }
  }
}