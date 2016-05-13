using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace rpc_test {
    class Program {
        public static Connection connection;
        static void Main(string[] args) {
            bool quit = false;
            do {
                Console.Write("tRPC ->");
                string response = Console.ReadLine();
                quit = consoleParser.parseRequestReturnQuit(response);
            }
            while (!quit);
        }
    }
}
