using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Kuroi.Event;

public class EventRouter
{
    public ConcurrentDictionary<(string, string), MethodInfo> Methods { get; set; } = new();

    public EventRouter() => Init();

    private void Init()
    {
        var type = typeof(EventRouter);
        var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(m => m.GetCustomAttributes().FirstOrDefault(attr => attr is KuroiEventAttribute) is not null).ToArray();
        foreach (var method in methods)
        {
            var (msgType, postType) = method.GetCustomAttribute<KuroiEventAttribute>()!;
            Methods.TryAdd((msgType, postType), method);
        }
    }

    public Action<Kuroi>? Route(string msg)
    {
        var json = JsonNode.Parse(msg);
        var msgType = json!["message_type"]?.GetValue<string>();
        var postType = json!["post_type"]?.GetValue<string>();
        if (msgType is not null && postType is not null)
        {
            if (Methods.TryGetValue((msgType, postType), out var method))
            {
                return (Action<Kuroi>)method.Invoke(this, new object?[] { JsonSerializer.Deserialize<GroupMessageEventArgs>(msg) })!;
            }

            return null;
        }

        return null;
    }

    [KuroiEvent("group", "message")]
    Action<Kuroi> OnGroupMessage(GroupMessageEventArgs msg)
    {
        return k =>
        {
            k.HandleGroupMessage(msg);
        };
    }
}

public class KuroiEventAttribute : Attribute
{
    public string MessageType { get; set; }
    public string PostType { get; set; }

    public KuroiEventAttribute(string messageType, string postType)
    {
        MessageType = messageType;
        PostType = postType;
    }

    public void Deconstruct(out string messageType, out string postType)
    {
        messageType = MessageType;
        postType = PostType;
    }
}