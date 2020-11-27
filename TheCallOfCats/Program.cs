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
using System.Text;
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


            byte[] byteArray = Encoding.ASCII.GetBytes("{\n  \"type\": \"service_account\",\n  \"project_id\": \"praxis-gasket-296907\",\n  \"private_key_id\": \"7e1e9efb29b01bb4370cb7c16578c9dcb45a497b\",\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCwsEkE29YR351N\\numtzaQjsWe3ZwXfwfAQnP5GiYIpWnm/nhFseP9rTtORNsqElWSntQOW+Ez6QQrDT\\nyLKlVCjDU2aqiCOyNbZfFuCByQw51jq+6pHFYyonqKbagWz7/Kufz9zdEgaQVO6b\\n7mlDKSqB2sGx6nMsh915MK25UrlyQolTQggmtlTwc8QEim2/mNteW/uZjxufFmGJ\\ngRiUYkuCQzgsm2Z5Pi/9Tev8Udss1DWnkmvHaGlOPznP9PZHSD+HeG7UhvVhGglO\\ncRTTqiS9Y5nF3ox6boguUdtg5zMC1S9DcHz51Hii90aBEjKbGjJaJxA5OZ5swu4+\\nCk8yIBvJAgMBAAECggEADHS3BxXI4PxwylmfqNSRJZle4lSoZA6YG9y4Q3hXFE1p\\n0epTop2pi3ZziFM1F00SfrhuIdDZPqIG9X0u8+cLkx6KbK/5hT8SXuB93gRqfVrQ\\nDCS890t6/a3+6kVIxbhtsG/e0AJs7kighCNh7hNFPP+LG2Pt7+gZmckv3BJcArUW\\nSnItMb+LSxOznrxgmQuzjVInfIdnBieO9opvAZ+3U2IU1s35k4nm+/5HSqXXg79k\\nzhV6IyzrSwsRHWjKaqFpaEF/zqSXifjnxFZMJh90eDOIXUzg2uWLTM42loDEzy22\\nsIBeY8vECo5woS6N5Xx/zoxyqEQuk/YloQdamTM/cQKBgQDj9i4Wzr3Xz9OX/R/H\\nE23cxHO79dOG7nlIHYN/60SOew8IDRIqIuGfEwfTGvFtBnYKxxwzbrDMfB4RYzGD\\niquTh0iGwpTrEZYeb6SaiPdLlfcVuunK50TuJwfJrvuNXJpW7seR48kNogFaq0Uq\\ncfaMkR8UqQoZnjRS2HB1KZK/OQKBgQDGa6xadcnmlhZ0N6272ane22r2xcR1EYxo\\nU6aSnF+aRFGsp+//60+lu/UdKG6rIx/giDE5AouFoki2Mu9NAdB/qeRBPoVypooW\\n3bb1bjeq0Nwg0Cx0gylef7/ZNOV1YGJ027fH9EG+DxkcJxOrBYYC6Z5oo6oEgSo0\\n2fSbF4OxEQKBgA/Z1ViWpZK1C8R8wFHqYvA/5MBEFqJCQMKn8rXZWRoDKodnyUSO\\nyltySk/+hp0LHvge4jIdV0PQuZKB8DZyVP3cIUnli89QmLyjDLIGJhPmaJjlN055\\niEx7AEqg1TL+Jmk3Dz9wtFEzW56/W/7Yj8k0ahHYszAW1RKRpDgxXNfZAoGBAIX7\\nHMV0deHDBORy2FhZELBQHvkE77RRJisoGPk0qAWGDlR0jcWUQhLaSRfoPlpG/HZj\\n4EDrrASPYWPOHvIzwHMD6AH6UqfpNqNcEPfG9cXdBFcP2oTOMDgEMSpc4ngQnkrk\\nNkPJdj78+OqepFQ+BZCf6Xwkz1/9S9+mMNos1ZrRAoGAHyQMBJHkj+DmrPMXs3BZ\\nHrdg5oOkcL0UjnnmtB8wuumHspSQrrhoCltkAebWdjK5UsoIuq5N4i2XsbPs9Wu0\\nUHe5hyrZBuHIHsjhxvKDRMLm/e4FrwRN5Gt96p0Cr4bdlLbS5GaG9ah85gocXneN\\n0YXHjftYJyKorrJcUQmMcko=\\n-----END PRIVATE KEY-----\\n\",\n  \"client_email\": \"texttospeech@praxis-gasket-296907.iam.gserviceaccount.com\",\n  \"client_id\": \"101099057167999415608\",\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/texttospeech%40praxis-gasket-296907.iam.gserviceaccount.com\"\n}");
            
            using (Stream stream = new MemoryStream(byteArray))
            //var stream=new FileStream("praxis-gasket-296907-7e1e9efb29b0.json", FileMode.Open, FileAccess.Read))
            {
                ClientSecrets clientSecrets = new ClientSecrets();

                clientSecrets.ClientId = "101099057167999415608";
                clientSecrets.ClientSecret = "-----BEGIN PRIVATE KEY-----\\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCwsEkE29YR351N\\numtzaQjsWe3ZwXfwfAQnP5GiYIpWnm/nhFseP9rTtORNsqElWSntQOW+Ez6QQrDT\\nyLKlVCjDU2aqiCOyNbZfFuCByQw51jq+6pHFYyonqKbagWz7/Kufz9zdEgaQVO6b\\n7mlDKSqB2sGx6nMsh915MK25UrlyQolTQggmtlTwc8QEim2/mNteW/uZjxufFmGJ\\ngRiUYkuCQzgsm2Z5Pi/9Tev8Udss1DWnkmvHaGlOPznP9PZHSD+HeG7UhvVhGglO\\ncRTTqiS9Y5nF3ox6boguUdtg5zMC1S9DcHz51Hii90aBEjKbGjJaJxA5OZ5swu4+\\nCk8yIBvJAgMBAAECggEADHS3BxXI4PxwylmfqNSRJZle4lSoZA6YG9y4Q3hXFE1p\\n0epTop2pi3ZziFM1F00SfrhuIdDZPqIG9X0u8+cLkx6KbK/5hT8SXuB93gRqfVrQ\\nDCS890t6/a3+6kVIxbhtsG/e0AJs7kighCNh7hNFPP+LG2Pt7+gZmckv3BJcArUW\\nSnItMb+LSxOznrxgmQuzjVInfIdnBieO9opvAZ+3U2IU1s35k4nm+/5HSqXXg79k\\nzhV6IyzrSwsRHWjKaqFpaEF/zqSXifjnxFZMJh90eDOIXUzg2uWLTM42loDEzy22\\nsIBeY8vECo5woS6N5Xx/zoxyqEQuk/YloQdamTM/cQKBgQDj9i4Wzr3Xz9OX/R/H\\nE23cxHO79dOG7nlIHYN/60SOew8IDRIqIuGfEwfTGvFtBnYKxxwzbrDMfB4RYzGD\\niquTh0iGwpTrEZYeb6SaiPdLlfcVuunK50TuJwfJrvuNXJpW7seR48kNogFaq0Uq\\ncfaMkR8UqQoZnjRS2HB1KZK/OQKBgQDGa6xadcnmlhZ0N6272ane22r2xcR1EYxo\\nU6aSnF+aRFGsp+//60+lu/UdKG6rIx/giDE5AouFoki2Mu9NAdB/qeRBPoVypooW\\n3bb1bjeq0Nwg0Cx0gylef7/ZNOV1YGJ027fH9EG+DxkcJxOrBYYC6Z5oo6oEgSo0\\n2fSbF4OxEQKBgA/Z1ViWpZK1C8R8wFHqYvA/5MBEFqJCQMKn8rXZWRoDKodnyUSO\\nyltySk/+hp0LHvge4jIdV0PQuZKB8DZyVP3cIUnli89QmLyjDLIGJhPmaJjlN055\\niEx7AEqg1TL+Jmk3Dz9wtFEzW56/W/7Yj8k0ahHYszAW1RKRpDgxXNfZAoGBAIX7\\nHMV0deHDBORy2FhZELBQHvkE77RRJisoGPk0qAWGDlR0jcWUQhLaSRfoPlpG/HZj\\n4EDrrASPYWPOHvIzwHMD6AH6UqfpNqNcEPfG9cXdBFcP2oTOMDgEMSpc4ngQnkrk\\nNkPJdj78+OqepFQ+BZCf6Xwkz1/9S9+mMNos1ZrRAoGAHyQMBJHkj+DmrPMXs3BZ\\nHrdg5oOkcL0UjnnmtB8wuumHspSQrrhoCltkAebWdjK5UsoIuq5N4i2XsbPs9Wu0\\nUHe5hyrZBuHIHsjhxvKDRMLm/e4FrwRN5Gt96p0Cr4bdlLbS5GaG9ah85gocXneN\\n0YXHjftYJyKorrJcUQmMcko=\\n-----END PRIVATE KEY-----\\n";
                //ClientSecrets secrets = GoogleClientSecrets.F(stream);


                credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientSecrets,
                    new[] { "https://accounts.google.com/o/oauth2/auth" ,
                                  "https://www.googleapis.com/auth/cloud-platform" },
                    "praxis-gasket-296907", CancellationToken.None, new FileDataStore("AutomationServices"));
            }

            Channel channel = new Channel(SpeechClient.DefaultEndpoint, credentials.ToChannelCredentials());

            var speech = SpeechClient.Create();

            var response = speech.Recognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = "en",
                // Model
            }, RecognitionAudio.FromFile(DEMO_FILE));

            Console.WriteLine(response.ToString());

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
