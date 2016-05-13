using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace rpc_test {
    class Connection {

        bool isIP = false;
        bool synchronousWait = false;
        HttpClient client;
        HttpMessageHandler handler;
        HttpResponseMessage response;
        Uri address;
        public Connection(string address) {
            //builds the HTTPClient and such
            Regex reg = new Regex(@"\b\d{ 1, 3 }\.\d{ 1,3}\.\d{ 1,3}\.\d{ 1,3}\b");
            if (reg.IsMatch(address)) {
                isIP = true;
                Console.WriteLine("IP Address supplied");
            }
            handler = new HttpClientHandler();
            client = new HttpClient(handler);
            //clean up address
            if (isIP) {
                address = "http://" + address;
            }
            else {
                if (!address.Contains("://")) {
                    Console.WriteLine("No protocol included, automatically prepending http://");
                    address = "http://" + address;
                }
            }
            try {
                this.address = new Uri(address);
            }
            catch (UriFormatException e) {
                Console.WriteLine(e.Message);
                handler = null;
                client = null;
                isIP = false;
            }
        }

        private async void sendRequest(HttpRequestMessage request) {
            synchronousWait = true;
            Console.WriteLine("sending Request");
            try {
                response = await client.SendAsync(request);
                Console.WriteLine("Successfully got response");
                printResponse();
                synchronousWait = false;
            }
            catch (Exception e) {
                Console.Write(e.Message + Environment.NewLine);
                synchronousWait = false;
            }
        }
        private async void printResponse() {
            if (response != null) {
                string content = await response.Content.ReadAsStringAsync();
                Console.Write(content + Environment.NewLine);
            }
        }
        public bool testPOST() {
            HttpRequestMessage request;
            try {
                request = new HttpRequestMessage(HttpMethod.Post, address);
                sendRequest(request);
                while (synchronousWait) System.Threading.Thread.Sleep(100);
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }   
        }
        public bool testGET() {
            HttpRequestMessage request;
            try {
                request = new HttpRequestMessage(HttpMethod.Get, address);
                sendRequest(request);
                while (synchronousWait) System.Threading.Thread.Sleep(100);
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
