using System.Linq;
using System.Threading.Tasks;
using api.DAL.data;
using api.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SOA.Controllers
{
    [ApiController]
    public class SMSController : ControllerBase
    {
        private ISMS _sms;
        public SMSController(ISMS sms)
        {
            _sms = sms;
        }
        [Route("api/sendSMS")]
        [HttpPost]
        public IActionResult sendSMS([FromBody] ModelSMS sms)
        {
            if (ModelState.IsValid)
            {
                var result = _sms.Send(sms);
                return Ok(result);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return BadRequest(errors);
            }
        }
        [Route("api/sendSMSWithVonage")]
        [HttpPost]
        public IActionResult sendProvider2SMS([FromBody] ModelSmallSMS sms)
        {
            if (ModelState.IsValid)
            {
                var result = _sms.SendProvider2(sms);
                return Ok(result);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return BadRequest(errors);
            }
        }
    }
}