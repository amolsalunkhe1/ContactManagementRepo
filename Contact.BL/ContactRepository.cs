using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.DAL;
using Contact.IBL;
using Contact.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace Contact.BL
{
    public class ContactRepository : IContactRepository
    {
        private ContactDBEntities contactDBEntities;

        public ContactRepository()
        {
            contactDBEntities = new ContactDBEntities();
        }

        public int AddContact(ContactModel contactModel)
        {
            DAL.Contact contact = new DAL.Contact()
            {
                FirstName = contactModel.FirstName,
                LastName = contactModel.LastName,
                EmailId = contactModel.EmailId,
                MobileNumber = contactModel.MobileNumber,
                Status = contactModel.Status
            };
            contactDBEntities.Contacts.Add(contact);
            return contactDBEntities.SaveChanges();
        }

        public List<ContactModel> GetAllContact()
        {
            List<ContactModel> list = (from obj in contactDBEntities.Contacts
                                       select new ContactModel()
                                       {
                                           ContactId = obj.ContactId,
                                           FirstName = obj.FirstName,
                                           LastName = obj.LastName,
                                           EmailId = obj.EmailId,
                                           MobileNumber = obj.MobileNumber,
                                           Status = obj.Status
                                       }).ToList();
            return list;
        }

        public ContactModel GetContact(long contactId)
        {
            var modelEntity = contactDBEntities.Contacts.FirstOrDefault(v => v.ContactId == contactId);
            ContactModel model = new ContactModel()
            {
                ContactId = modelEntity.ContactId,
                FirstName = modelEntity.FirstName,
                LastName = modelEntity.LastName,
                EmailId = modelEntity.EmailId,
                MobileNumber = modelEntity.MobileNumber,
                Status = modelEntity.Status
            };
            return model;
        }

        public void UpdateContact(ContactModel contactModel)
        {
            DAL.Contact contact = new DAL.Contact()
            {
                FirstName = contactModel.FirstName,
                LastName = contactModel.LastName,
                EmailId = contactModel.EmailId,
                MobileNumber = contactModel.MobileNumber,
                Status = contactModel.Status
            };
            contactDBEntities.Entry(contact).State = EntityState.Modified;
            try
            {
                contactDBEntities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }
        }

        public void DeleteContact(long contactId)
        {
            var model = contactDBEntities.Contacts.FirstOrDefault(v => v.ContactId == contactId);
            contactDBEntities.Contacts.Remove(model);
            contactDBEntities.SaveChanges();
        }
    }
}
