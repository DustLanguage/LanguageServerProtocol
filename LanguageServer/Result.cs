using System;

namespace LanguageServer
{
  internal enum ResultTag
  {
    Success = 1,
    Error = 2
  }

  public class Result<T, TError>
  {
    private readonly ResultTag tag;

    private Result(ResultTag tag, T success, TError error)
    {
      this.tag = tag;
      SuccessValue = success;
      ErrorValue = error;
    }

    public T SuccessValue { get; }

    public TError ErrorValue { get; }

    public bool IsSuccess => tag == ResultTag.Success;
    public bool IsError => tag == ResultTag.Error;

    public static Result<T, TError> Success(T success)
    {
      return new Result<T, TError>(ResultTag.Success, success, default(TError));
    }

    public static Result<T, TError> Error(TError error)
    {
      return new Result<T, TError>(ResultTag.Error, default(T), error);
    }

    public Result<TResult, TError> Select<TResult>(Func<T, TResult> func)
    {
      return IsSuccess
        ? Result<TResult, TError>.Success(func(SuccessValue))
        : Result<TResult, TError>.Error(ErrorValue);
    }

    public Result<TResult, TErrorResult> Select<TResult, TErrorResult>(Func<T, TResult> funcSuccess, Func<TError, TErrorResult> funcError)
    {
      return IsSuccess ? Result<TResult, TErrorResult>.Success(funcSuccess(SuccessValue)) :
        IsError ? Result<TResult, TErrorResult>.Error(funcError(ErrorValue)) :
        default(Result<TResult, TErrorResult>);
    }

    public TResult Handle<TResult>(Func<T, TResult> funcSuccess, Func<TError, TResult> funcError)
    {
      return IsSuccess ? funcSuccess(SuccessValue) :
        IsError ? funcError(ErrorValue) :
        default(TResult);
    }

    public void Handle(Action<T> actionSuccess, Action<TError> actionError)
    {
      if (IsSuccess)
        actionSuccess(SuccessValue);
      else if (IsError) actionError(ErrorValue);
    }

    public T HandleError(Func<TError, T> func)
    {
      return IsError
        ? func(ErrorValue)
        : SuccessValue;
    }
  }

  public class VoidResult<TError>
  {
    private readonly ResultTag tag;

    private VoidResult(ResultTag tag, TError error)
    {
      this.tag = tag;
      ErrorValue = error;
    }

    public TError ErrorValue { get; }

    public bool IsSuccess => tag == ResultTag.Success;
    public bool IsError => tag == ResultTag.Error;

    public static VoidResult<TError> Success()
    {
      return new VoidResult<TError>(ResultTag.Success, default(TError));
    }

    public static VoidResult<TError> Error(TError error)
    {
      return new VoidResult<TError>(ResultTag.Error, error);
    }

    public Result<TResult, TError> Select<TResult>(Func<TResult> func)
    {
      return IsSuccess
        ? Result<TResult, TError>.Success(func())
        : Result<TResult, TError>.Error(ErrorValue);
    }

    public Result<TResult, TErrorResult> Select<TResult, TErrorResult>(Func<TResult> funcSuccess, Func<TError, TErrorResult> funcError)
    {
      return IsSuccess ? Result<TResult, TErrorResult>.Success(funcSuccess()) :
        IsError ? Result<TResult, TErrorResult>.Error(funcError(ErrorValue)) :
        default(Result<TResult, TErrorResult>);
    }

    public TResult Handle<TResult>(Func<TResult> funcSuccess, Func<TError, TResult> funcError)
    {
      return IsSuccess ? funcSuccess() :
        IsError ? funcError(ErrorValue) :
        default(TResult);
    }

    public void Handle(Action actionSuccess, Action<TError> actionError)
    {
      if (IsSuccess)
        actionSuccess();
      else if (IsError) actionError(ErrorValue);
    }

    public void HandleError(Action<TError> actionError)
    {
      if (IsError) actionError(ErrorValue);
    }
  }
}