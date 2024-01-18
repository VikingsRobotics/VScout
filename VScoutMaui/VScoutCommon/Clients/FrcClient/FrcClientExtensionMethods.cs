using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace VScoutCommon.Clients.FrcClient
{
    public static class FrcClientExtensionMethods
    {
        private static class Constants
        {
            public const string UserName = "jcheadle";
            public const string Token = "E0029612-F61A-4500-9D31-A84419C32041";
        }

        public static void AddFrcClient(this IServiceCollection services, Action<FrcClientOptions> action)
        {
            string encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Constants.UserName}:{Constants.Token}"));

            services.AddOptions<FrcClientOptions>().Configure(action);

            services.AddHttpClient<FrcClient>(client =>
            {
                client.BaseAddress = new Uri("https://frc-api.firstinspires.org/v2.0/");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encodedToken);
            });
        }
    }
}