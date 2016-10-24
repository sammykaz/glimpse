using Amazon.DynamoDBv2.DataModel;

namespace Glimpse.Core.Model
{
    [DynamoDBTable("VendorAccount")]
    class Vendor
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public int companyId { get; set; }
        public string personalPhoneNumber { get; set; }
        public string businessPhoneNumber { get; set; }
    }
}
