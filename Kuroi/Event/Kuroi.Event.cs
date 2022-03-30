using Kuroi.Event;

namespace Kuroi;

public partial class Kuroi
{
    public event EventHandler<GroupMessageEventArgs>? OnGroupMessage;

    public void HandleGroupMessage(GroupMessageEventArgs args)
    {
        OnGroupMessage?.Invoke(this, args);
    }
}