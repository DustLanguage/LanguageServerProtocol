namespace LanguageServer.Json
{
  public sealed class StringOrObject<TObject> : Either<string, TObject>
  {
    public StringOrObject()
    {
    }

    public StringOrObject(string left)
      : base(left)
    {
    }

    public StringOrObject(TObject right)
      : base(right)
    {
    }

    public static implicit operator StringOrObject<TObject>(string left)
    {
      return new StringOrObject<TObject>(left);
    }

    public static implicit operator StringOrObject<TObject>(TObject right)
    {
      return new StringOrObject<TObject>(right);
    }

    protected override EitherTag OnDeserializing(JsonDataType jsonType)
    {
      return
        jsonType == JsonDataType.String ? EitherTag.Left :
        jsonType == JsonDataType.Object ? EitherTag.Right :
        EitherTag.None;
    }
  }
}