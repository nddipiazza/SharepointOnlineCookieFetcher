using System;
using System.Security;
using Microsoft.SharePoint.Client;
using Utility.CommandLine;


namespace SharepointOnlineCookieFetcher {
    class FormDigestGenerator {
        [Argument('w', "webUri")]
        private static string webUri { get; set; }

        [Argument('u', "userName")]
        private static string userName { get; set; }

        public static void Main(string[] args) {
            Arguments.Populate();
            if (webUri == null) {
                Console.WriteLine("-w or --webUri required");
                System.Environment.Exit(1);
            }
            if (userName == null) {
                Console.WriteLine("-u or --userName required");
                System.Environment.Exit(1);
            }
            Console.WriteLine(GetFormDigest());
        }

        public static SecureString GetPassword() {
            var pwd = new SecureString();
            while (true) {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter) {
                    break;
                } else if (i.Key == ConsoleKey.Backspace) {
                    if (pwd.Length > 0) {
                        pwd.RemoveAt(pwd.Length - 1);
                        Console.Write("\b \b");
                    }
                } else {
                    pwd.AppendChar(i.KeyChar);
                    Console.Write("*");
                }
            }
            return pwd;
        }

        protected static string GetFormDigest() {
            Console.WriteLine("Logging into {0} as {1}", webUri, userName);
            string password = Environment.GetEnvironmentVariable("SPPWD");
            SecureString securePassword;
            if (password == null) {
                Console.WriteLine("Enter password");
                securePassword = GetPassword();
                Console.WriteLine("");
            } else {
                securePassword = new SecureString();
                foreach (char c in password) {
                    securePassword.AppendChar(c);
                }
            }
            var credentials = new SharePointOnlineCredentials(userName, securePassword);
            var authCookie = credentials.GetAuthenticationCookie(new Uri(webUri));
            Console.WriteLine("Full authCookie {0}", authCookie);
            return authCookie.TrimStart("SPOIDCRL=".ToCharArray());
        }

    }

}
