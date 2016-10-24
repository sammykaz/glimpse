using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Glimpse.Core.Contracts.Repository;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glimpse.Core.Repositories
{
    public class VendorRepository : BaseRepository, IVendorRepository
    {
        public async void PutItem()
        {
            //TODO  Placed here temporarily, possible to put this into a asyn task?

            //Place your own Access Key and Secret Access Key below:
            AmazonDynamoDBClient client = new AmazonDynamoDBClient("AKIAJO5TSFBKWVDWLOUA", "K+sH0xADftyggpX2hIwDqwblG/gUFKpYecWUvSm+");

            PutItemRequest request = new PutItemRequest();
            request.TableName = "VendorAccount";
            request.Item = new Dictionary<string, AttributeValue>();

            AttributeValue value1 = new AttributeValue();
            value1.S = "111";
            request.Item.Add("Id", value1);
            AttributeValue value2 = new AttributeValue();
            value2.S = "Balls";
            request.Item.Add("lastName", value2);

            PutItemResponse response = await client.PutItemAsync(request);
            
        }
    }
}
