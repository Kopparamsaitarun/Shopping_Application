using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ShoppingApp.Models
{
    public class User 
    {
        [JsonPropertyName("userId")]
        public int userId { get; set; }
        [JsonPropertyName("firstName")]
        public string firstName { get; set; }
        [JsonPropertyName("lastName")]
        public string lastName { get; set; }
        [JsonPropertyName("email")]
        public string email { get; set; }
        [JsonPropertyName("password")]
        public string password { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string phoneNumber { get; set; }
        [JsonPropertyName("policyFlag")]
        public bool policyFlag { get; set; }
        [JsonPropertyName("id")]
        public long id { get; set; }
        [JsonPropertyName("ModifiedDate")]
        public DateTime ModifiedDate { get; set; }
        [JsonPropertyName("IpAddress")]
        public string IpAddress { get; set; }
    }
}
