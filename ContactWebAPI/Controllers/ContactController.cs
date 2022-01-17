using Contact.IBL;
using Contact.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ContactWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactController : ApiController
    {
        private IContactRepository iContactRepository;
        public ContactController(IContactRepository _iContactRepository)
        {
            iContactRepository = _iContactRepository;
        }
        // GET: api/Contact
        public IEnumerable<ContactModel> Get()
        {
            IEnumerable<ContactModel> list = iContactRepository.GetAllContact();
            return list;
        }

        // GET: api/Contact/5
        public ContactModel Get(long id)
        {
            ContactModel model = iContactRepository.GetContact(id);
            return model;
        }

        // POST: api/Contact
        public int Post(JObject jsonResult)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
                return -1;
            }

            ContactModel model = JsonConvert.DeserializeObject<ContactModel>(jsonResult.ToString());
            return iContactRepository.AddContact(model);
        }

        // PUT: api/Contact/5
        public void Put(long id, JObject jsonResult)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
                //return -1;
            }

            ContactModel model = JsonConvert.DeserializeObject<ContactModel>(jsonResult.ToString());
            iContactRepository.UpdateContact(model);
        }

        // DELETE: api/Contact/5
        public void Delete(long id)
        {
            iContactRepository.DeleteContact(id);
        }
    }
}
