using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheCallOfCats
{
    public class Program
    {
        public static string DEMO_FILE = "audio.raw";

        public static async Task Main(string[] args)
        {
            UserCredential credentials;

            using(var stream=new FileStream("praxis-gasket-296907-7e1e9efb29b0.json", FileMode.Open, FileAccess.Read))
            {
                credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { "https://www.googleapis.com/auth/cloud-platform" },
                    "user", CancellationToken.None, new FileDataStore("AutomationServices"));
            }

            Channel channel = new Channel(
                SpeechClient.DefaultEndpoint.Host, SpeechClient.DefaultEndpoint.Port, credentials.ToChannelCredentials());

            var speech = SpeechClient.Create();

            var response = speech.Recognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = "en",
                // Model
            }, RecognitionAudio.FromFile(DEMO_FILE));

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
