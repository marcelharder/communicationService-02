using System;
using System.ComponentModel.DataAnnotations;

namespace api.DAL.data
{
    public class ModelOpReport
    {
        [Required]
        public string Ref_phys { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string Message { get; set; }
        public string Surgeon { get; set; }
        public string Hospital { get; set; }
        public string Hours { get; set; }
        [Required]
        public string Callback { get; set; }
    }
}