using System.ComponentModel;

namespace CityForum.Shared.ResultCodes;

public enum ResultCode
{
    [Description("User not found.")]
    USER_NOT_FOUND = 001,

    [Description("Identity server error.")]
    IDENTITY_SERVER_ERROR = 002,

    [Description("Login or password is incorrect.")]
    LOGIN_OR_PASSWORD_IS_INCORRECT = 003,

    [Description("User already exists.")]
    USER_ALREADY_EXISTS = 004,

    [Description("Topic not found.")]
    TOPIC_NOT_FOUND = 005,

    [Description("Message not found.")]
    MESSAGE_NOT_FOUND = 006,

    [Description("Topic already exists.")]
    TOPIC_ALREADY_EXISTS = 007
}