using System;
using System.IO;
using Newtonsoft.Json;

namespace LoginWcfServiceApp.Models
{
    /// <summary>
    /// Предоставляет методы для работы с импровизированной базой
    /// </summary>
    public class DataBaseModel
    {
        /// <summary>
        /// Путь по которому должна находиться база
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="connectionString">Путь по которому должна находиться база</param>
        public DataBaseModel(string connectionString = "LoginBase.txt")
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Добавляет логин в базу
        /// </summary>
        /// <param name="model">Логин, который надо добавить в базу</param>
        public void AddLoginToDataBase(LoginModel model)
        {
            using (StreamWriter writer = new StreamWriter(ConnectionString))
            {
                writer.WriteLine(JsonConvert.SerializeObject(model));
            }
        }
        /// <summary>
        /// Находит в базе логин со схожими Login и Password и возвращает его
        /// </summary>
        /// <returns>Логин со схожими Login и Password из базы. Если такого логина нет, возвращает null</returns>
        public LoginModel GetLoginModel(LoginModel loginModel)
        {
            try
            {
                LoginModel resultModel = null;
                using (StreamReader reader = new StreamReader(ConnectionString))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        resultModel = JsonConvert.DeserializeObject<LoginModel>(line);
                        if (resultModel.Password == loginModel.Password && resultModel.Login == loginModel.Login)
                        {
                            return resultModel;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                /*Пока просто записываем в коносль, но это типа логгер*/
                Console.WriteLine(e.Message);
            }
            return null;
        }
        /// <summary>
        /// Создает файл, который будет служить базой данных логинов
        /// </summary>
        private void CreateLoginBase()
        {
            try
            {
                if (!DataBaseExists())
                {
                    using (Stream myStream = File.Create(ConnectionString)) { }
                }
            }
            catch(Exception e)
            {
                /*Пока просто записываем в коносль, но это типа логгер*/
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Создает файл по пути ConnectionString, который будет служить базой данных
        /// и сразу вносит в нее первый логин "Login" : "admin", "Password" : "admin" в формате json
        /// </summary>
        public void InitializeLoginBase()
        {
            try
            {
                CreateLoginBase();
                LoginModel loginModel = new LoginModel("admin", "admin");
                if (!loginModel.IsLoginInDataBase(this))
                {
                    AddLoginToDataBase(new LoginModel("admin", "admin"));
                }
            }
            catch (Exception e)
            {
                /*Пока просто записываем в коносль, но это типа логгер*/
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Проверяет создана ли база данных
        /// </summary>
        /// <returns>Возвращает true, если существует файл с названием, указанным в ConnectionString. (По факту запускает File.Exists(ConnectionString).)</returns>
        private bool DataBaseExists()
        {
            try
            {
                return File.Exists(ConnectionString);
            }
            catch (Exception e)
            {
                /*Пока просто записываем в коносль, но это типа логгер*/
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}