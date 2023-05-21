using System.Linq;
using System.Threading.Tasks;
using api.DAL.data;
using api.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SOA.Controllers
{
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IEmail _em;
        public EmailController(IEmail em)
        {
            _em = em;
        }

         [Route("api/sentPasswordReset")]
         [HttpPost]
         public IActionResult postPasswordReset([FromBody] ModelSmallEmail smemail){
           
            if (ModelState.IsValid)
            {
                var result = _em.SendPasswordReset(smemail);
                return Ok(result);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return BadRequest(errors);
            }
         }

         [Route("api/sendProcedureNotification")]
         [HttpPost]
         public IActionResult postProcedureNotification([FromBody] ModelOpReport oprep){
           
            if (ModelState.IsValid)
            {
                var result = _em.SendOperativeReport(oprep);
                return Ok(result);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return BadRequest(errors);
            }
         }


         [Route("api/sendSubscriptionExtensionRequest")]
         [HttpPost]
         public IActionResult postSER([FromBody] ModelSER oprep){
           
            if (ModelState.IsValid)
            {
                var result = _em.SendSER(oprep);
                return Ok(result);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                return BadRequest(errors);
            }
         }


        [Route("api/sendEmail")]
        [HttpPost]
        public IActionResult postEmailAsync([FromBody] ModelEmail email)
        {
            if (ModelState.IsValid)
            {
                var result = _em.Send(email);
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
