using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

// new for this
using System.Security;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
        private static void Log(LogLevel level, string message, bool containsPii)
        {
            if (containsPii)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine($"{level} {message}");
            Console.ResetColor();
        }
        private static async Task RunAsync()
        {
            AuthenticationConfig config = AuthenticationConfig.ReadFromJsonFile("appsettings.json");
            IPublicClientApplication app;

            app = PublicClientApplicationBuilder.Create(config.ClientId)
                .WithAuthority(new Uri(config.Authority)) // TODO maybe not needed
                .WithLogging(Log,
                    LogLevel.Info,
                    enablePiiLogging: true,
                    enableDefaultPlatformLogging: true)
                .Build();


            string[] scopes = new string[] {
                $"https://{config.Domain}/api/.default"
            };

            // Get Token
            string username = "[USERNAME]";
            SecureString password = new SecureString();
            foreach (char a in "[PASSWORD]") {
                password.AppendChar(a);
            }

            AuthenticationResult result = null;
            try
            {
                // TODO first check if somebody is logged in
                result = await app.AcquireTokenByUsernamePassword(scopes, username, password)
                    .ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Token acquired");
                Console.ResetColor();
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Scope provided is not supported");
                Console.ResetColor();
            }
            catch (MsalUiRequiredException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("MISC UI Required Exception, see sample to handle more robustly: https://github.com/Azure-Samples/active-directory-dotnetcore-console-up-v2/blob/db1941a634228585f6badc658d8228e0f35dc538/up-console/PublicAppUsingUsernamePassword.cs");
                Console.ResetColor();
            }

            // Use Token
            if (result != null)
            {
                var httpClient = new HttpClient();
                var apiCaller = new ProtectedApiCallHelper(httpClient);
                await apiCaller.CallWebApiAndProcessResultASync("http://localhost:1040/api/todolist", result.AccessToken, Display);
            }
        }

        private static void Display(string result)
        {
            Console.WriteLine(result);
        }
    }
}