using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using api.DAL.data;
using api.DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Microsoft.Extensions.Options;

namespace api.DAL.Implementations
{
    public class Mail : IEmail
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        private readonly IOptions<Helpers.ComSettings> _mailtrapConfig;

        public Mail(
            IWebHostEnvironment env,
            IConfiguration configuration,
            IOptions<Helpers.ComSettings> mailtrapConfig)
        {

            _env = env;
            _configuration = configuration;
            _mailtrapConfig = mailtrapConfig;

        }
        public string Send(ModelEmail model)
        {

            var _values = new NameValueCollection();
            _values.Add("apiKey", _configuration.GetSection("ElasticMailKey").Value);
            _values.Add("from", "marcel@cardiacsoftwaredevelopers.com");
            _values.Add("fromName", "CSD");
            _values.Add("to", model.To);
            _values.Add("subject", model.Subject);
            _values.Add("bodyText", model.Body);
            //_values.Add("bodyHtml", "<h1>Html Body</h1>");
            _values.Add("isTransactional", "true");


            string address = "https://api.elasticemail.com/v2/email/send";
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] apiResponse = client.UploadValues(address, _values);
                    return Encoding.UTF8.GetString(apiResponse);
                }
                catch (Exception ex)
                {
                    return "" + ex.Message + "\n" + ex.StackTrace;
                }
            }
        }

        public string SendTemplate(ModelEmail model)
        {

            var _values = new NameValueCollection();
            _values.Add("apiKey", _configuration.GetSection("ElasticMailKey").Value);
            _values.Add("from", "marcel@cardiacsoftwaredevelopers.com");
            _values.Add("fromName", "CSD");
            _values.Add("to", model.To);
            _values.Add("subject", model.Subject);
            // _values.Add("bodyText", model.Body);
            _values.Add("bodyHtml", model.Body);
            _values.Add("isTransactional", "true");


            string address = "https://api.elasticemail.com/v2/email/send";
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] apiResponse = client.UploadValues(address, _values);
                    return Encoding.UTF8.GetString(apiResponse);
                }
                catch (Exception ex)
                {
                    return "" + ex.Message + "\n" + ex.StackTrace;
                }
            }
        }

        public string SendPasswordReset(ModelSmallEmail model)
        {
            var client = new RestClient("https://send.api.mailtrap.io/api/send");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", _mailtrapConfig.Value.Authorization);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"from\": {\"email\":\"mailtrap@surgical-outcomes.nl\",\"name\":\"Password Reset\"},\"to\":[{\"email\":\"" + model.To + "\"}],\"template_uuid\":\"89f27f90-0078-4d3f-ac65-ed870a6c4b60\",\"template_variables\":{\"user_email\":\"" + model.To + "\",\"pass_reset_link\":\"" + model.Callback + "\"}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            return "";
        }

        public string SendOperativeReport(ModelOpReport model)
        {
            var client = new RestClient("https://send.api.mailtrap.io/api/send");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", _mailtrapConfig.Value.Authorization);
            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", "{\"from\":{\"email\":\"mailtrap@surgical-outcomes.nl\",\"name\":\"Procedure notification\"},\"to\":[{\"email\":\"" + model.To + "\"}],\"template_uuid\":\"12a63a1b-768c-4898-9b4f-7d70260614ba\",\"template_variables\":{\"ref\":\"" + model.Ref_phys + "\",\"operative_report_link\":\"" + model.Callback + "\",\"hours\":\"" + model.Hours + "\",\"surgeon\":\"" + model.Surgeon + "\",\"hospital\":\"" + model.Hospital + "\"}}", ParameterType.RequestBody);
            request.AddParameter("application/json", "  {\"from\":{\"email\":\"mailtrap@surgical-outcomes.nl\",\"name\":\"Procedure notification\"},\"to\":[{\"email\":\"" + model.To + "\"}],\"template_uuid\":\"12a63a1b-768c-4898-9b4f-7d70260614ba\",\"template_variables\":{\"imageUrl\":\"" + model.ImageUrl + "\",\"ref_phys\":\"" + model.Ref_phys + "\",\"message\":\"" + model.Message + "\",\"callback\":\"" + model.Callback + "\",\"hours\":\"" + model.Hours + "\",\"surgeon\":\"" + model.Surgeon + "\",\"hospital\":\"" + model.Hospital + "\"}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return "";
        }

        public string SendSER(ModelSER model)
        {
            var client = new RestClient("https://send.api.mailtrap.io/api/send");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", _mailtrapConfig.Value.Authorization);
            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", "{\"from\":{\"email\":\"mailtrap@surgical-outcomes.nl\",\"name\":\"Procedure notification\"},\"to\":[{\"email\":\"" + model.To + "\"}],\"template_uuid\":\"12a63a1b-768c-4898-9b4f-7d70260614ba\",\"template_variables\":{\"ref\":\"" + model.Ref_phys + "\",\"operative_report_link\":\"" + model.Callback + "\",\"hours\":\"" + model.Hours + "\",\"surgeon\":\"" + model.Surgeon + "\",\"hospital\":\"" + model.Hospital + "\"}}", ParameterType.RequestBody);
            request.AddParameter  ("application/json", "{\"from\":{\"email\":\"mailtrap@surgical-outcomes.nl\",\"name\":\"Subscription Extension Request\"},\"to\":[{\"email\":\"" + model.to + "\"}],\"template_uuid\":\"ceef52d7-010e-4cd5-b2f4-85f48e0577e5\",\"template_variables\":{\"subject\":\"" + model.subject + "\",\"user_name\":\"" + model.userName + "\",\"user_id\":\"" + model.userId + "\",\"extension_period\":\"" + model.extensionPeriod + "\",\"additionalCommnent\":\"" + model.additionalComments + "\",\"next_step_link\":\"Test_Next_step_link\",\"get_started_link\":\"Test_Get_started_link\",\"onboarding_video_link\":\"Test_Onboarding_video_link\"}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return "";
    
        }

       

    }


}
