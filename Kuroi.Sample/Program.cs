using Kuroi.Protocol;

using Serilog;

namespace Kuroi.Sample;

public class Program
{
    static void Main(string[] args)
    {
        var logger = new LoggerConfiguration().MinimumLevel.Debug().Enrich.FromLogContext().WriteTo.Console()
            .CreateLogger();
        var k = new Kuroi(SessionProtocol.WS, new Uri("ws://127.0.0.1:6700"));
        k.AddLogger(f => f.AddSerilog(logger));
        while (true)
        {
            Console.ReadLine();
        }
    }
}