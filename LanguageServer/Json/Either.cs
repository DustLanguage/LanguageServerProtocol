using System;

namespace LanguageServer.Json
{
  public abstract class Either<TLeft, TRight> : IEither
  {
    private TLeft left;
    private TRight right;
    private EitherTag tag;

    public Either()
    {
    }

    public Either(TLeft left)
    {
      tag = EitherTag.Left;
      this.left = left;
    }

    public Either(TRight right)
    {
      tag = EitherTag.Right;
      this.right = right;
    }

    public TLeft Left => tag == EitherTag.Left ? left : throw new InvalidOperationException();

    public TRight Right => tag == EitherTag.Right ? right : throw new InvalidOperationException();

    public bool IsLeft => tag == EitherTag.Left;

    public bool IsRight => tag == EitherTag.Right;

    public Type LeftType => typeof(TLeft);

    public Type RightType => typeof(TRight);

    object IEither.Left
    {
      get => Left;
      set
      {
        tag = EitherTag.Left;
        left = (TLeft) value;
        right = default(TRight);
      }
    }

    object IEither.Right
    {
      get => Right;
      set
      {
        tag = EitherTag.Right;
        left = default(TLeft);
        right = (TRight) value;
      }
    }

    EitherTag IEither.OnDeserializing(JsonDataType jsonType)
    {
      return OnDeserializing(jsonType);
    }

    protected abstract EitherTag OnDeserializing(JsonDataType jsonType);
  }
}