namespace LanguageServer.Json
{
  public sealed class ArrayOrObject<TElement, TObject> : Either<TElement[], TObject>
  {
    public ArrayOrObject()
    {
    }

    public ArrayOrObject(TElement[] left)
      : base(left)
    {
    }

    public ArrayOrObject(TObject right)
      : base(right)
    {
    }

    public static implicit operator ArrayOrObject<TElement, TObject>(TElement[] left)
    {
      return new ArrayOrObject<TElement, TObject>(left);
    }

    public static implicit operator ArrayOrObject<TElement, TObject>(TObject right)
    {
      return new ArrayOrObject<TElement, TObject>(right);
    }

    protected override EitherTag OnDeserializing(JsonDataType jsonType)
    {
      return
        jsonType == JsonDataType.Array ? EitherTag.Left :
        jsonType == JsonDataType.Object ? EitherTag.Right :
        EitherTag.None;
    }
  }
}