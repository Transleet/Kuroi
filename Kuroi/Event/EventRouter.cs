using System.Collections.Concurrent;
using System.Reflection;

namespace Kuroi.Event;

public class EventRouter
{
    public ConcurrentDictionary<string, MethodInfo> Methods { get; set; }

    public void Route(string msg)
    {

    }

    [KuroiEvent("")]
    Action<Kuroi> OnGroupMessage(string msg)
    {
        return new Action<Kuroi>(k =>
        {

        });
    }
}

public class KuroiEventAttribute : Attribute
{
    public KuroiEventAttribute(string @event)
    {
        Event = @event;
    }

    public string Event { get; set; }
}