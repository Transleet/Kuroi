using System.Net.WebSockets;

using Kuroi.Event;
using Kuroi.Protocol;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Websocket.Client;

namespace Kuroi
{
    public partial class Kuroi
    {
        public SessionProtocol SessionProtocol { get; set; }
        private WebsocketClient? WebSocketClient { get; set; }
        private WebSocketState State => WebSocketClient is null ? WebSocketState.None : WebSocketClient.NativeClient.State;
        private ILogger Logger { get; set; } = NullLogger.Instance;
        private EventRouter EventRouter { get; set; } = new();
        private readonly ILoggerFactory _loggerFactory = new LoggerFactory();

        public void AddLogger(Action<ILoggerFactory> f)
        {
            f.Invoke(_loggerFactory);
            Logger = _loggerFactory.CreateLogger<Kuroi>();
        }

        public Kuroi(SessionProtocol protocol, Uri uri)
        {
            if (protocol != SessionProtocol.WS)
            {
                return;
            }
            WebSocketClient = new(uri);
            WebSocketClient.ReconnectTimeout = TimeSpan.FromSeconds(30);
            WebSocketClient.ErrorReconnectTimeout = TimeSpan.FromSeconds(30);
            WebSocketClient.ReconnectionHappened.Subscribe(msg => Logger.LogWarning("Client connecting..."));
            WebSocketClient.MessageReceived.Subscribe(msg => Logger.LogInformation(msg.Text));
            WebSocketClient.Start();
        }
    }
}