using CityForum.Shared.ResultCodes;

namespace CityForum.Shared.Exceptions;

public class LogicException : Exception
{
    public ResultCode? Code { get; set; }

    public LogicException(string message) : base(message) { }

    public LogicException(ResultCode code) : base(code.GetDescription())
    {
        Code = code;
    }
}