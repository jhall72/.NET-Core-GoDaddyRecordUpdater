using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace GoDaddyRecordUpdater
{
    public class ClientFactory
    {

        public async Task UpdateRecord(DNSRecord OldRec)
        {
            //Create a new DNS Record that will be updated with the correct IP if needed.
            DNSRecord NewRec = new DNSRecord();
            NewRec = await DataFilledRecordAsync(OldRec);
            //Calls the GoDaddy API with the proper domain and record.
            //Documentation found at: https://developer.godaddy.com/doc/endpoint/domains#/v1/recordReplaceTypeName
            string URL = Program.URLPrefix + "v1/domains/" + NewRec.Domain + "/records/" + NewRec.Type + "/" + NewRec.Name;
            HttpClient client = new HttpClient();
            //Adds Authorization headers from the config file.
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("sso-key", ConfigurationManager.AppSettings["APIKey"] + ":" + ConfigurationManager.AppSettings["SecretKey"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //GoDaddy wants all records added in an array (even if it's just one record)
            List<DNSRecord> ListRecords = new List<DNSRecord>();
            ListRecords.Add(NewRec);
            DNSRecord[] dns_records = ListRecords.ToArray();
            var json = JsonConvert.SerializeObject(dns_records);
            var response = await client.PutAsync(URL, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("PASSED!");
            }
            else
            {
                Console.WriteLine("FAILED: " + response.ReasonPhrase);
            }
            return;
        }

        //If the IP Address is Null this function will find the current machines public IP address.
        public async Task<DNSRecord> DataFilledRecordAsync(DNSRecord OldRec)
        {
            DNSRecord NewRec = OldRec;
            if (NewRec.Data == null)
            {
                NewRec.Data = await RetrievedIPAsync();
            }
            return NewRec;
        }

        public async Task<string> RetrievedIPAsync()
        {
            HttpClient newClient = new HttpClient();
            string ReturnValue = "";
            ReturnValue = await newClient.GetStringAsync(ConfigurationManager.AppSettings["IPAddressProvider"]);
            return ReturnValue;
        }
    }
}
