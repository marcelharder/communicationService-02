using System;
using System.ComponentModel.DataAnnotations;

namespace api.DAL.data
{
    public class ModelSER
    {
        public string subject { get; set; }
        [Required]
        public string userId { get; set; }
        public string userName { get; set; }
        public string extensionPeriod { get; set; }
        public string additionalComments { get; set; }
        public string to { get; set; }

    }
}