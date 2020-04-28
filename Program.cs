namespace DriverWithLogger
{
    using System;
    using System.Threading.Tasks;
    using Neo4j.Driver.V1;

    internal class Program
    {
        private static async Task Main()
        {
            const string uri = "bolt://localhost:7687";
            const string user = "neo4j";
            const string password = "neo";

            var logger = new ConsoleDriverLogger {Level = LogLevel.Debug};

            var driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password),
                Config.Builder
                    .WithEncryptionLevel(EncryptionLevel.Encrypted)
                    .WithDriverLogger(logger)
                    .ToConfig());

            var session = driver.Session(AccessMode.Read);
            var resultCursor = await session.RunAsync("MERGE (m:Movie {title: 'The Passion of the Chris'}) RETURN m");
            
            while (await resultCursor.FetchAsync()) 
                Console.WriteLine(resultCursor.Current["m"].As<INode>().Properties["title"].As<string>());

            await session.CloseAsync();
            await driver.CloseAsync();
        }
    }
}