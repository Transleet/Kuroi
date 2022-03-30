using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace Kuroi.Event;
public class GroupMessageEventArgs : EventArgs
{
    [J("anonymous")] public object? Anonymous { get; set; }
    [J("font")] public long Font { get; set; }
    [J("group_id")] public long GroupId { get; set; }
    [J("message")] public string? Message { get; set; }
    [J("message_id")] public long MessageId { get; set; }
    [J("message_seq")] public long MessageSeq { get; set; }
    [J("message_type")] public string? MessageType { get; set; }
    [J("post_type")] public string? PostType { get; set; }
    [J("raw_message")] public string? RawMessage { get; set; }
    [J("self_id")] public long SelfId { get; set; }
    [J("sender")] public Sender? Sender { get; set; }
    [J("sub_type")] public string? SubType { get; set; }
    [J("time")] public long Time { get; set; }
    [J("user_id")] public long UserId { get; set; }
}

public class Sender
{
    [J("age")] public long Age { get; set; }
    [J("area")] public string? Area { get; set; }
    [J("card")] public string? Card { get; set; }
    [J("level")] public string? Level { get; set; }
    [J("nickname")] public string? Nickname { get; set; }
    [J("role")] public string? Role { get; set; }
    [J("sex")] public string? Sex { get; set; }
    [J("title")] public string? Title { get; set; }
    [J("user_id")] public long UserId { get; set; }
}