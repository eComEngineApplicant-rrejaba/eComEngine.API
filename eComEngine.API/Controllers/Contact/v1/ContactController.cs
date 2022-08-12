using eComEngine.API.DTO;
using eComEngine.API.Entitity;
using eComEngine.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace eComEngine.API.Controllers
{
    [ApiController]
    [Route("api/v1/Contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService ContactService;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="contactService"></param>
        public ContactController(IContactService contactService)
        {
            ContactService = contactService;
        }

        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet(), Route("")]
        public ActionResult<List<ContactDTO>> All()
        {
            IEnumerable<ContactEntity> contactEntityList = ContactService.FindAll();

            if (contactEntityList?.Count() == 0)
            {
                return NotFound();
            }

            List<ContactDTO> contactDTOList = new List<ContactDTO>();

            // do not return the actual entity, use DTO
            foreach (var entity in contactEntityList)
            {
                ContactDTO contactDTO = new ContactDTO();
                contactDTO.ToDTO(entity);
                contactDTOList.Add(contactDTO);
            }

            return Ok(contactDTOList);
        }

        /// <summary>
        /// Get Contact by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(), Route("{id}")]
        public ActionResult<ContactDTO> Get(int id)
        {
            ContactEntity contactEntity = ContactService.Find(id);

            if (contactEntity == default)
            {
                return NotFound();
            }

            // do not return the actual entity, use DTO
            ContactDTO contactDTO = new ContactDTO();
            contactDTO.ToDTO(contactEntity);

            return Ok(contactDTO);
        }

        /// <summary>
        /// Add Contact
        /// </summary>
        /// <param name="contactDTO"></param>
        /// <returns></returns>
        [HttpPost(), Route("")]
        public ActionResult Create([FromBody] ContactDTO contactDTO)
        {
            // manual check for validation error
            // those with DataAnnotation should be handled by model state such as emailAddress
            List<string> validationError = contactDTO.ValidateDTO();
            
            if(validationError.Count != 0)
            {
                foreach (var item in validationError)
                {
                    ModelState.AddModelError("ValidationError", item);
                }

                return UnprocessableEntity(ModelState);
            }

            ContactEntity contactEntity = contactDTO.ToEntity(contactDTO);
            
            var response = ContactService.Insert(contactEntity);

            return Ok(response);
        }

        /// <summary>
        /// Update Contact
        /// </summary>
        /// <param name="contactDTO"></param>
        /// <returns></returns>
        [HttpPut(), Route("")]
        public ActionResult Update([FromBody] ContactDTO contactDTO)
        {
            // manual check for validation error
            // those with DataAnnotation should be handled by model state such as emailAddress
            List<string> validationError = contactDTO.ValidateDTO();

            if (validationError.Count != 0)
            {
                foreach (var item in validationError)
                {
                    ModelState.AddModelError("ValidationError", item);
                }

                return UnprocessableEntity(ModelState);
            }

            ContactEntity contactEntity = contactDTO.ToEntity(contactDTO);

            var response = ContactService.Update(contactEntity);

            if (!response)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete Contact by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = ContactService.Delete(id);

            if (result)
                return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Delete all Contact
        /// </summary>
        /// <returns></returns>
        [HttpDelete("all")]
        public ActionResult DeleteAll()
        {
            ContactService.DeleteAll();

            return NoContent();
        }

        [HttpGet(), Route("call-list")]
        public ActionResult<List<ContactDTO>> CallList()
        {
            // dont know what to return on this endpoint
            return NoContent();
        }
    }
}
