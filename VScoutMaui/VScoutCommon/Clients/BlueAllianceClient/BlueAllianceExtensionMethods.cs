using Microsoft.Extensions.DependencyInjection;

namespace VScoutCommon.Clients.BlueAllianceClient
{
    public static class BlueAllianceExtensionMethods
    {
        private static class Constants
        {
            public const string Token = "cyExOuXEggqKQOdV2QfVOlHlq8fcmTrYcMMn5EMTNH5R41uCZct3dPbepdCqcRoq";
        }

        public static void AddBlueAllianceClient(this IServiceCollection services, Action<BlueAllianceClientOptions> action)
        {
            services.AddOptions<BlueAllianceClientOptions>().Configure(action);

            services.AddHttpClient<BlueAllianceClient>(client =>
            {
                client.BaseAddress = new Uri("https://www.thebluealliance.com/api/v3");
                client.DefaultRequestHeaders.Add("X-TBA-Auth-Key", Constants.Token);
            });
        }
    }
}
