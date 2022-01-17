using Contact.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.IBL
{
    public interface IContactRepository
    {
        List<ContactModel> GetAllContact();

        int AddContact(ContactModel contactModel);

        ContactModel GetContact(long contactId);

        void UpdateContact(ContactModel contactModel);

        void DeleteContact(long contactId);
    }
}
