using System;

namespace LanguageServer.Json
{
  public sealed class NumberOrObject<TNumber, TObject> : Either<TNumber, TObject>
    where TNumber : struct, IComparable
  {
    public NumberOrObject()
    {
    }

    public NumberOrObject(TNumber left)
      : base(left)
    {
    }

    public NumberOrObject(TObject right)
      : base(right)
    {
    }

    public static implicit operator NumberOrObject<TNumber, TObject>(TNumber left)
    {
      return new NumberOrObject<TNumber, TObject>(left);
    }

    public static implicit operator NumberOrObject<TNumber, TObject>(TObject right)
    {
      return new NumberOrObject<TNumber, TObject>(right);
    }

    protected override EitherTag OnDeserializing(JsonDataType jsonType)
    {
      return
        jsonType == JsonDataType.Number ? EitherTag.Left :
        jsonType == JsonDataType.Object ? EitherTag.Right :
        EitherTag.None;
    }
  }
}