using System;

namespace LanguageServer.Json
{
  public sealed class NumberOrString : Either<long, string>, IEquatable<NumberOrString>
  {
    public NumberOrString()
    {
    }

    public NumberOrString(long left)
      : base(left)
    {
    }

    public NumberOrString(string right)
      : base(right)
    {
    }

    public bool Equals(NumberOrString other)
    {
      return IsLeft && other.IsLeft ? Left == other.Left :
        IsRight && other.IsRight ? Right == other.Right :
        !IsLeft && !IsRight && !other.IsLeft && !other.IsRight;
    }

    public static implicit operator NumberOrString(long left)
    {
      return new NumberOrString(left);
    }

    public static implicit operator NumberOrString(string right)
    {
      return new NumberOrString(right);
    }

    protected override EitherTag OnDeserializing(JsonDataType jsonType)
    {
      return
        jsonType == JsonDataType.Number ? EitherTag.Left :
        jsonType == JsonDataType.String ? EitherTag.Right :
        EitherTag.None;
    }

    public override int GetHashCode()
    {
      return IsLeft ? Left.GetHashCode() :
        IsRight ? Right.GetHashCode() :
        0;
    }

    public override bool Equals(object obj)
    {
      NumberOrString other = obj as NumberOrString;
      return other == null ? false : Equals(other);
    }
  }
}