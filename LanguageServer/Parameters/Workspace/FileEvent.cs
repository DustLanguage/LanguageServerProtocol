using System;

namespace LanguageServer.Parameters.Workspace
{
  public class FileEvent
  {
    public Uri Uri { get; set; }
    public FileChangeType Type { get; set; }
  }
}