namespace LanguageServer.Client
{
  public sealed class Proxy
  {
    private ClientProxy client;
    private Connection connection;
    private TextDocumentProxy textDocument;
    private WindowProxy window;
    private WorkspaceProxy workspace;

    public Proxy(Connection connection)
    {
      this.connection = connection;
    }

    public WindowProxy Window
    {
      get
      {
        window = window ?? new WindowProxy(connection);
        return window;
      }
    }

    public ClientProxy Client
    {
      get
      {
        client = client ?? new ClientProxy(connection);
        return client;
      }
    }

    public WorkspaceProxy Workspace
    {
      get
      {
        workspace = workspace ?? new WorkspaceProxy(connection);
        return workspace;
      }
    }

    public TextDocumentProxy TextDocument
    {
      get
      {
        textDocument = textDocument ?? new TextDocumentProxy(connection);
        return textDocument;
      }
    }
  }
}