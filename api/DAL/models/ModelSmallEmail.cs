using System;
using System.ComponentModel.DataAnnotations;

namespace api.DAL.data
{
    public class ModelSmallEmail
    {
        public string To { get; set; }
        public string Callback { get; set; }
    }
}