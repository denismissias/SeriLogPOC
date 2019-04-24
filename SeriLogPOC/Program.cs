using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;

namespace SeriLogPOC
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();


            var usuario = new { Name = "Denis", Age = 27, Endereco = new { Rua = "Paraca das Gaivotas" } };

            Log log = new Log
            {
                EventId = "Teste",
                Message = new Message
                {
                    RunId = Guid.NewGuid()
                },
                Exception = new DivideByZeroException()
            };

            ILogger logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                //.WriteTo.Console(new CompactJsonFormatter())
                //.WriteTo.File(new CompactJsonFormatter(), "log.json")
                //.MinimumLevel.Verbose()
                .CreateLogger();

            //logger.Verbose(usuario.Name, usuario.Age);

            //logger.Debug("Início do log");

            //logger.Information("{@l} {@i} {@m}", log.Level, log.EventId, log.Message);

            logger.Error("{@Log}", log, usuario);

            logger.Debug("Fim do log");
        }
    }

    public class Log
    {
        public Exception Exception { get; set; }

        public string EventId { get; set; }

        public Message Message { get; set; }

    }

    public class Message
    {
        public Guid RunId { get; set; }

        public string Service { get; set; }
    }
}
