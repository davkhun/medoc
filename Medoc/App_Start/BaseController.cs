using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedocData;
using System.Web.Security;
using MedocDto;

namespace Medoc.App_Start
{
    public class BaseController: Controller
    {
        internal UserContext _userContext = new UserContext();
        internal ClientContext _clientContext = new ClientContext();
        internal DictionaryContext _dictionaryContext = new DictionaryContext();
        internal ContractContext _contractContext = new ContractContext();
        internal FileContext _fileContext = new FileContext();
        public UserModel GetUser(string login=null)
        {
            if (string.IsNullOrEmpty(login))
                login = System.Web.HttpContext.Current.User.Identity.Name;
            return _userContext.GetUser(login);
        }

    }
}