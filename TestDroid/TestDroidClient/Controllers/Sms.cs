using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDroidClient.Controllers
{
    class Sms
    {
        public Sms()
        {}

        /// <summary>
        /// Handles the 'sms' command
        /// </summary>
        /// <param name="id">Id of the command</param>
        /// <param name="args">The command as argument </param>
        /// <returns>True if command was sent</returns>
        public async Task<bool> HandleSms(int id, string[] args)
        {
            bool didSucceed = false;

            switch (args.Length)
            {
                case 2: //sms send
                case 3: //sms send txt
                case 4: //sms send txt phoneNr
                    didSucceed = await HandleSmsAction(id, args);
                    break;
                default:
                    // First find out if there were no more arguments than the actual command (few) or too many (many):
                    string tooFewOrTooManyArgs = (args.Length == 1) ? "few" : "many";

                    // Create and print the error:
                    string argumentError = string.Format("Error: Too {0} arguments ({1}). Expected 1. " +
                                                            "Use call help for more information.",
                                                            tooFewOrTooManyArgs, (args.Length - 1));
                    Console.WriteLine(argumentError);
                    didSucceed = false;
                    break;
            }
            return didSucceed;
        }

        private async Task<bool> HandleSmsAction(int id, string[] args)
        {
            bool didSucceed;

            switch (args[1])
            {
                case "send":
                    didSucceed = await SendSMS(id, args);
                    break;
                case "help":
                    Console.WriteLine("Sms usage: sms <send [Text] [Phone number]>");
                    didSucceed = false;
                    break;
                default:
                    string errorMessage = string.Format("Unknown argument: {0}. " +
                                                            "Use 'sms help' for more information", args[1]);
                    Console.WriteLine(errorMessage);
                    didSucceed = false;
                    break;
            }

            return didSucceed;
        }

        private async Task<bool> SendSMS(int id, string[] args)
        {
            bool didSucceed = false;
            TCPConnection tcp = TCPConnection.GetInstance();
            string fullCommand = (id + " " + string.Join(" ", args));
            didSucceed = await tcp.SendCommand(id, fullCommand);
            return didSucceed;
        }
    }
}
