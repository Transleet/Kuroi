namespace Kuroi.Event;

public partial class Kuroi
{
	public event EventHandler<GroupMessageEventArgs> GroupMessage;

	public void GroupMessage(GroupMessageEventArgs args)
	{
		EventHandler<GroupMessageEventArgs> handler = GroupMessage;
		handler.Invoke(this, args);
	}
}