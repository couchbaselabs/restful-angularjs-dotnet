using System;
using System.Threading.Tasks;
using System.Web.Http;
using RestfulDotnet.Models;

namespace RestfulDotnet.Controllers
{
    public class RestfulController : ApiController
    {
        readonly RecordModel _model;

        public RestfulController()
        {
            _model = new RecordModel();
        }

        [Route("api/get")]
        public async Task<IHttpActionResult> Get(Guid? document_id)
        {
            if (!document_id.HasValue)
                return BadRequest("A document id is required");

            return Ok(await _model.GetByDocumentId(document_id.Value));
        }

        [Route("api/getAll")]
        public async Task<dynamic> GetAll()
        {
            return Ok(await _model.GetAll());
        }

        [Route("api/save")]
        [HttpPost]
        public async Task<IHttpActionResult> Save(Person body)
        {
            // validation
            if (string.IsNullOrEmpty(body.FirstName))
                return BadRequest("A firstname is required");
            if (string.IsNullOrEmpty(body.LastName))
                return BadRequest("A lastname is required");
            if (string.IsNullOrEmpty(body.Email))
                return BadRequest("An email is required");

            return Ok(await _model.Save(body));
        }

        [Route("api/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(Person body)
        {
            if (!body.Document_Id.HasValue)
                return BadRequest("A document id is required");

            return Ok(await _model.Delete(body.Document_Id.Value));
        }
    }
}
