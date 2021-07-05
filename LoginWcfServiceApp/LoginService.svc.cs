using System;
using System.ServiceModel.Web;
using Newtonsoft.Json;
using LoginWcfServiceApp.Models;
namespace LoginWcfServiceApp
{
    public class LoginService : ILoginService
    {
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        public string Login(string login, string password)
        {
            try
            {
                LoginModel model = new LoginModel(login, password);
                DataBaseModel dataBaseModel = new DataBaseModel();
                dataBaseModel.InitializeLoginBase();
                return JsonConvert.SerializeObject(model.IsLoginInDataBase(dataBaseModel));
            }
            catch(Exception e)
            {
                /*Тут надо бы записать в лог, но я не разобрался как его добавить, поэтому пока так*/
                return JsonConvert.SerializeObject(e.Message);
            }
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }

}