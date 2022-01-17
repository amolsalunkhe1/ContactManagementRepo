using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using ContactWebApplication.Models;
using Newtonsoft.Json;
using System.Text;

namespace ContactWebApplication.Controllers
{
    public class ContactController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:58654/api");
        HttpClient client;
        public ContactController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        // GET: Contact
        public ActionResult Index()
        {
            List<ContactModel> list = new List<ContactModel>();
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/contact").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<ContactModel>>(data);
            }
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContactModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"{client.BaseAddress}/contact", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(long id)
        {
            ContactModel model = new ContactModel();
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/contact/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ContactModel>(data);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ContactModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync($"{client.BaseAddress}/contact/{model.ContactId}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(long id)
        {
            HttpResponseMessage response = client.DeleteAsync($"{client.BaseAddress}/contact/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}