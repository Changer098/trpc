using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpc_test {
    class messagePrinter {
        public static void formatGeneralError() {
            Console.Write("ERROR! Improperly formatted requested." + Environment.NewLine + "Type help to list all commands" + Environment.NewLine);
        }
        public static void help() {
            Console.WriteLine("Listing all commands");
            Console.WriteLine("quit - exit application");
            Console.WriteLine("open - open a new connection");
            Console.WriteLine("close - close current connection");
            Console.WriteLine("test - send test values to connection");
        }
        public static void commandNotFound() {
            Console.Write("ERROR! Command not found" + Environment.NewLine + "Type help to list all commands" + Environment.NewLine);
        }
        public static void notConnected() {
            Console.WriteLine("ERROR! You are currently not connected to an services. Use 'open' to connect");
        }
        public static void openQuestion() {
            Console.WriteLine("#.#.#.# - IP address of the service you wish to communicate with (e.g. 192.168.2.12");
            Console.WriteLine("{address} - the address of the service you wish to communicate with (e.g. localhost)");
        }
        public static void testQuestion() {
            Console.WriteLine("POST - sends a POST request to the address of the connection");
            Console.WriteLine("GET - sends a GET request to the address of the connection");
        }
    }
}
