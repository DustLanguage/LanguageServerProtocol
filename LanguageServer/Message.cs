using LanguageServer.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LanguageServer
{
  public static class Message
  {
    public static Result<T, TError> ToResult<T, TError>(ResponseMessage<T, TError> response)
      where TError : ResponseError
    {
      return response.Error == null
        ? Result<T, TError>.Success(response.Result)
        : Result<T, TError>.Error(response.Error);
    }

    public static VoidResult<TError> ToResult<TError>(VoidResponseMessage<TError> response)
      where TError : ResponseError
    {
      return response.Error == null
        ? VoidResult<TError>.Success()
        : VoidResult<TError>.Error(response.Error);
    }

    public static ResponseError ParseError()
    {
      return new ResponseError {Code = ErrorCodes.ParseError, Message = "Parse error"};
    }

    public static ResponseError<T> ParseError<T>(T data)
    {
      return new ResponseError<T> {Code = ErrorCodes.ParseError, Message = "Parse error", Data = data};
    }

    public static ResponseError InvalidRequest()
    {
      return new ResponseError {Code = ErrorCodes.InvalidRequest, Message = "Invalid Request"};
    }

    public static ResponseError<T> InvalidRequest<T>(T data)
    {
      return new ResponseError<T> {Code = ErrorCodes.InvalidRequest, Message = "Invalid Request", Data = data};
    }

    public static ResponseError MethodNotFound()
    {
      return new ResponseError {Code = ErrorCodes.MethodNotFound, Message = "Method not found"};
    }

    public static ResponseError<T> MethodNotFound<T>(T data)
    {
      return new ResponseError<T> {Code = ErrorCodes.MethodNotFound, Message = "Method not found", Data = data};
    }

    public static ResponseError InvalidParams()
    {
      return new ResponseError {Code = ErrorCodes.InvalidParams, Message = "Invalid params"};
    }

    public static ResponseError<T> InvalidParams<T>(T data)
    {
      return new ResponseError<T> {Code = ErrorCodes.InvalidParams, Message = "Invalid params", Data = data};
    }

    public static TResponseError InternalError<TResponseError>()
      where TResponseError : ResponseError, new()
    {
      return new TResponseError {Code = ErrorCodes.InternalError, Message = "Internal error"};
    }

    public static ResponseError InternalError()
    {
      return new ResponseError {Code = ErrorCodes.InternalError, Message = "Internal error"};
    }

    public static ResponseError<T> InternalError<T>(T data)
    {
      return new ResponseError<T> {Code = ErrorCodes.InternalError, Message = "Internal error", Data = data};
    }

    public static ResponseError ServerError(ErrorCodes code)
    {
      return new ResponseError {Code = code, Message = "Server error"};
    }

    public static ResponseError<T> ServerError<T>(ErrorCodes code, T data)
    {
      return new ResponseError<T> {Code = code, Message = "Server error", Data = data};
    }
  }

  internal class MessageTest
  {
    public string Jsonrpc { get; set; }

    public NumberOrString Id { get; set; }

    public string Method { get; set; }

    public bool IsMessage => Jsonrpc == "2.0";

    public bool IsRequest => IsMessage && Id != null && Method != null;

    public bool IsResponse => IsMessage && Id != null && Method == null;

    public bool IsNotification => IsMessage && Id == null && Method != null;

    public bool IsCancellation => IsNotification && Method == "$/cancelRequest";
  }

  public abstract class MessageBase
  {
    public string Jsonrpc { get; set; } = "2.0";
  }

  public abstract class MethodCall : MessageBase
  {
    public string Method { get; set; }
  }

  public abstract class RequestMessageBase : MethodCall
  {
    public NumberOrString Id { get; set; }
  }

  public class VoidRequestMessage : RequestMessageBase
  {
  }

  public class RequestMessage<T> : RequestMessageBase
  {
    public T Params { get; set; }
  }

  public abstract class ResponseMessageBase : MessageBase
  {
    public NumberOrString Id { get; set; }
  }

  public class VoidResponseMessage<TError> : ResponseMessageBase
    where TError : ResponseError
  {
    public TError Error { get; set; }
  }

  public class ResponseMessage<T, TError> : ResponseMessageBase
    where TError : ResponseError
  {
    public T Result { get; set; }

    public TError Error { get; set; }
  }

  public abstract class NotificationMessageBase : MethodCall
  {
  }

  public class VoidNotificationMessage : NotificationMessageBase
  {
  }

  public class NotificationMessage<T> : NotificationMessageBase
  {
    public T Params { get; set; }
  }

  public class ResponseError
  {
    public ErrorCodes Code { get; set; }

    public string Message { get; set; }
  }

  public class ResponseError<T> : ResponseError
  {
    public T Data { get; set; }
  }

  public enum ErrorCodes
  {
    ParseError = -32700,
    InvalidRequest = -32600,
    MethodNotFound = -32601,
    InvalidParams = -32602,
    InternalError = -32603,
    ServerErrorStart = -32099,
    ServerErrorEnd = -32000,
    ServerNotInitialized = -32002,
    UnknownErrorCode = -32001,
    RequestCancelled = -32800
  }
}