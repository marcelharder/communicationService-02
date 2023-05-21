using System;
using System.IO;
using System.Net;
using api.DAL.data;
using api.DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Vonage;
using Vonage.Request;

namespace api.DAL.Implementations
{
    public class SMS : ISMS
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<Helpers.SMSSettings> _smsConfig;


        public SMS(
            IWebHostEnvironment env,
            IConfiguration configuration,
            IOptions<Helpers.SMSSettings> smsConfig)
        {

            _env = env;
            _configuration = configuration;
            _smsConfig = smsConfig;

        }

        public string Send(ModelSMS model)
        {
            var result = "";
            try
            {
                WebClient client = new WebClient();
                // Add a user agent header in case the requested URI contains a query.
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.QueryString.Add("user", _smsConfig.Value.smsUser);
                client.QueryString.Add("password", _smsConfig.Value.smsPassword);
                client.QueryString.Add("api_id", _smsConfig.Value.api_id);
                client.QueryString.Add("to", model.Phone);
                client.QueryString.Add("text", model.Body);
                string baseurl = "https://api.clickatell.com/http/sendmsg";
                Stream data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                return s;

            }
            catch (Exception e)
            {
                result = "Something went wrong with sending the SMS";
                Console.WriteLine(e.InnerException);
            }
            result = "Successfully sent the SMS";
            return result;
        }

        public string SendProvider2(ModelSmallSMS model)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
            _smsConfig.Value.vonage_key,
            _smsConfig.Value.vonage_api
            );

            var VonageClient = new VonageClient(credentials);
            var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
            {
                To = model.To,
                From = model.From,
                Text = model.Text
            });
            return "SMS sent";
        }




    }
}