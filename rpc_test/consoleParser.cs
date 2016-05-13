using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpc_test {
    class consoleParser {
        //return true if quit is called
        public static bool isConnected = false;
        public static bool parseRequestReturnQuit(string request) {
            string[] requestLines = request.Split(' ');             //split by spaces
            if (requestLines.Length == 0) {
                messagePrinter.formatGeneralError();
            }
            //single method commands
            if (string.Equals(requestLines[0], "quit", StringComparison.CurrentCultureIgnoreCase)) {
                //quit command issued
                return true;
            }
            else if (string.Equals(requestLines[0], "help", StringComparison.CurrentCultureIgnoreCase)) {
                //help command issued
                messagePrinter.help();
            }
            else if (string.Equals(requestLines[0], "close", StringComparison.CurrentCultureIgnoreCase)) {
                //close command issued
                Console.WriteLine("Closing current connection and cleaning up");
                Program.connection = null;
                Console.WriteLine("Closed connection");
            }
            //commands with parameters
            else if (string.Equals(requestLines[0], "open", StringComparison.CurrentCultureIgnoreCase)) {
                //open command issued
                if (requestLines.Length == 1) {
                    messagePrinter.formatGeneralError();
                    return false;
                }
                if (requestLines[1] == "?") {
                    //question operator
                    messagePrinter.openQuestion();
                }
                if (Program.connection != null) {
                    Console.Write("Current connection must be closed before creating a new connection."
                        + Environment.NewLine + "Would you like to close the current connection? y/n ");
                    do {
                        string yesno = Console.ReadLine();
                        if (string.Equals(yesno, "yes", StringComparison.CurrentCultureIgnoreCase)
                            || string.Equals(yesno, "y", StringComparison.CurrentCultureIgnoreCase)) {
                            Console.WriteLine("Opening new connection on address: " + requestLines[1]);
                            Program.connection = new Connection(requestLines[1]);
                            return false;
                        }
                        else if (string.Equals(yesno, "no", StringComparison.CurrentCultureIgnoreCase)
                            || string.Equals(yesno, "n", StringComparison.CurrentCultureIgnoreCase)) {
                            return false;
                        }
                    } while (true);
                }
                else {
                    Console.WriteLine("Opening connection on address: " + requestLines[1]);
                    Program.connection = new Connection(requestLines[1]);
                }
            }
            else if (string.Equals(requestLines[0], "test", StringComparison.CurrentCultureIgnoreCase)) {
                if (requestLines.Length == 1) {
                    messagePrinter.formatGeneralError();
                    return false;
                }
                if (requestLines[1] == "?") {
                    //question operator
                    messagePrinter.testQuestion();
                }
                else {
                    if (string.Equals(requestLines[1], "post", StringComparison.CurrentCultureIgnoreCase)) {
                        if (Program.connection == null) {
                            messagePrinter.notConnected();
                            return false;
                        }
                        Program.connection.testPOST();
                    }
                    else if (string.Equals(requestLines[1], "get", StringComparison.CurrentCultureIgnoreCase)) {
                        if (Program.connection == null) {
                            messagePrinter.notConnected();
                            return false;
                        }
                        Program.connection.testGET();
                    }
                }
            }
            else {
                messagePrinter.commandNotFound();
            }
            
            return false;
        } 
    }
}
