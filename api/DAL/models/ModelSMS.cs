using System;
using System.ComponentModel.DataAnnotations;

namespace api.DAL.data
{
    public class ModelSMS
    {
        public string From { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string api_id { get; set; }
        
    }
}