using System;

namespace LanguageServer.Parameters.TextDocument
{
  public class DocumentLink
  {
    public Range Range { get; set; }

    public Uri Target { get; set; }
  }
}