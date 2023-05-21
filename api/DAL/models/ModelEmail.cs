using System;
using System.ComponentModel.DataAnnotations;

namespace api.DAL.data
{
    public class ModelEmail
    {
        public int Id { get; set; }
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Surgeon { get; set; }
        public string Surgeon_image { get; set; }
        public string Soort { get; set; }
        public string Hash { get; set; }
        public string Callback { get; set; }

       
    }
}