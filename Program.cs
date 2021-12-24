using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoDaddyRecordUpdater
{
    internal class Program
    {
        public static string URLPrefix = "";
        public static async Task Main(string[] args)
        {
            #if DEBUG
                Console.WriteLine("Mode=Debug");
                URLPrefix = "https://api.ote-godaddy.com/"; //GoDaddys OTE environment
            #else
                Console.WriteLine("Mode=Release");
                URLPrefix = "https://api.godaddy.com/"; //GoDaddys Prod API
            #endif

            
            using (var db = new GDContext())
            {
                foreach (DNSRecord Record in db.DNSRecords) //Loops through all DNS Records in the context and sends them to the Client Factory
                {
                    ClientFactory newFactory = new ClientFactory();
                    await newFactory.UpdateRecord(Record);
                }
            }
        }
    }
}
