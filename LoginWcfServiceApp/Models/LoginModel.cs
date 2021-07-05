using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using System.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Newtonsoft.Json;

namespace LoginWcfServiceApp.Models
{
    /// <summary>
    /// Предоставляет свойства и методы для работы с Логином
    /// </summary>
    [DataContract]
    public class LoginModel
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }

        public LoginModel(string login = null, string password = null)
        {
            Login = login;
            Password = password;
        }

        /// <summary>
        /// Проверяет сущестует ли данный логин в базе
        /// </summary>
        /// <returns>Если существуие возращает true иначе false</returns>
        public bool IsLoginInDataBase(DataBaseModel db)
        {
            return db.GetLoginModel(this) != null;
        }
    }
}