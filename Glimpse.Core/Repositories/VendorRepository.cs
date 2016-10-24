using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Glimpse.Core.Contracts.Repository;
using Amazon.DynamoDBv2.DataModel;

namespace Glimpse.Core.Repositories
{
    public class VendorRepository : BaseRepository, IVendorRepository
    {
        public VendorRepository() { }

        public async void PutItem()
        {
            /*
                      // AmazonDynamoDBClient client = new AmazonDynamoDBClient();

                        //TODO  Placed here temporarily, possible to put this into a asyn task?

                        PutItemRequest request = new PutItemRequest();
                       request.TableName = "VendorAccount";
                       request.Item = new Dictionary<string, AttributeValue>();

                       AttributeValue value1 = new AttributeValue();
                       value1.S = "111";
                       request.Item.Add("id", value1);
                       AttributeValue value2 = new AttributeValue();
                       value2.S = "Balls";
                       request.Item.Add("lastName", value2);

                       PutItemResponse response = await client.PutItemAsync(request);
                   */
            var basicAWSCreds = new BasicAWSCredentials("AKIAJO5TSFBKWVDWLOUA", "K+sH0xADftyggpX2hIwDqwblG/gUFKpYecWUvSm+");
            
            AmazonDynamoDBConfig adcConfig = new AmazonDynamoDBConfig()
            {
                ServiceURL = "https://dynamodb.us-east-1.amazonaws.com",
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            var client = new AmazonDynamoDBClient(basicAWSCreds, adcConfig);
            var context = new DynamoDBContext(client);

            var request1 = new PutItemRequest
                        {
                            TableName = "VendorAccount",
                            Item = new Dictionary<string, AttributeValue>
              {
                { "id", new AttributeValue { S = "123" }},
                { "firstName", new AttributeValue { S = "Dog" }},
                { "lastName", new AttributeValue { S = "Fido" }}
              }
                        };

                       PutItemResponse response1 = await client.PutItemAsync(request1);




        }
     

        }
    
}