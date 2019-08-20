using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace api
{
    public class JwtBearerMiddlewareDiagnostics
    {
        static Func<AuthenticationFailedContext, Task> onAuthenticationFailed;
        static Func<MessageReceivedContext, Task> onMessageReceived;
        static Func<TokenValidatedContext, Task> onTokenValidated;
        static Func<JwtBearerChallengeContext, Task> onChallenge;
        public static JwtBearerEvents Subscribe(JwtBearerEvents events)
        {
            if (events == null)
            {
                events = new JwtBearerEvents();
            }

            onAuthenticationFailed = events.OnAuthenticationFailed;
            events.OnAuthenticationFailed = OnAuthenticationFailed;

            onMessageReceived = events.OnMessageReceived;
            events.OnMessageReceived = OnMessageReceived;

            onTokenValidated = events.OnTokenValidated;
            events.OnTokenValidated = OnTokenValidated;

            onChallenge = events.OnChallenge;
            events.OnChallenge = OnChallenge;

            return events;
        }

        static async Task OnMessageReceived(MessageReceivedContext context)
        {
            Debug.WriteLine($"1. Begin {nameof(OnMessageReceived)}");
            await onMessageReceived(context);
            Debug.WriteLine($"1. End - {nameof(OnMessageReceived)}");
        }

        static async Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            Debug.WriteLine($"99. Begin {nameof(OnAuthenticationFailed)}");
            await onAuthenticationFailed(context);
            Debug.WriteLine($"99. End - {nameof(OnAuthenticationFailed)}");
        }

        static async Task OnTokenValidated(TokenValidatedContext context)
        {
            Debug.WriteLine($"2. Begin {nameof(OnTokenValidated)}");
            await onTokenValidated(context);
            Debug.WriteLine($"2. End - {nameof(OnTokenValidated)}");
        }

        static async Task OnChallenge(JwtBearerChallengeContext context)
        {
            Debug.WriteLine($"55. Begin {nameof(OnChallenge)}");
            await onChallenge(context);
            Debug.WriteLine($"55. End - {nameof(OnChallenge)}");
        }
    }
}
