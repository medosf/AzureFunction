using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class CosmosTrigger1
    {
        private readonly ILogger _logger;

        public CosmosTrigger1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CosmosTrigger1>();
        }

        [Function("CosmosTrigger1")]
        public void Run([CosmosDBTrigger(
            databaseName: "%firstappcosmos_DATABASE%",
            containerName: "%firstappcosmos_CONTAINER%",
            Connection = "firstappcosmos_DOCUMENTDB",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<MyDocument> input)
        {
            Console.WriteLine("Hello World!");
            if (input != null && input.Count > 0)
            {
                _logger.LogInformation("Documents modified: " + input.Count);
                _logger.LogInformation("First document Id: " + input[0].id);
            }
        }

        [Function("CosmosTrigger2")]
        public void Run2([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            _logger.LogInformation("req context >>>>: " + req);

        }

        public class MyDocument
        {
            public string id { get; set; }

            public string Text { get; set; }

            public int Number { get; set; }

            public bool Boolean { get; set; }
        }
    }
