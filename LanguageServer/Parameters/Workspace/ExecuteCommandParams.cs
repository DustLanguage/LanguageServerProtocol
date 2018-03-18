namespace LanguageServer.Parameters.Workspace
{
  public class ExecuteCommandParams
  {
    public string Command { get; set; }
    public dynamic[] Arguments { get; set; }
  }
}