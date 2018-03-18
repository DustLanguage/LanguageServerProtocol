namespace LanguageServer.Parameters.TextDocument
{
  public class SignatureHelp
  {
    public SignatureInformation[] Signatures { get; set; }

    public int? ActiveSignature { get; set; }

    public int? ActiveParameter { get; set; }
  }
}