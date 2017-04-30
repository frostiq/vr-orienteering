using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Tools
{
    public class LoginRequestResults
    {
        public string status { get; set; }
        public string userid { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }

    }
    public class RegisterRequestResult
    {
        public string status { get; set; }
    }
    public class Task
    {
        public string id { get; set; }
        
    }
    public class TaskListRequestResult
    {
        public string status { get; set; }
        public List<Task> tasks { get; set; }
    }
    class WebTools
    {
        private static  string loginURL;
        private static string registerURL;
        private string taskListURL;
        
        static public LoginRequestResults login(string login,string password)
        {
            LoginRequestResults loginResult=new LoginRequestResults();
            var res = UnityEngine.JsonUtility.ToJson(new { type = "login", login = login, password = password });
            byte[] body = Encoding.UTF8.GetBytes(res);
            var request = (System.Net.HttpWebRequest)WebRequest.Create(loginURL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
                using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
                {

                    using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objText = reader.ReadToEnd();
                        //results = (RequestResults)js.Deserialize(objText, typeof(RequestResults));
                        try
                        {
                            loginResult = js.Deserialize<LoginRequestResults>(objText);
                        }
                        catch(ArgumentException  e)
                        {
                            loginResult.status = "incorrect login or password";
                        }
                    }

                }
            }
            return loginResult;
        }
        static public RegisterRequestResult register(string login,string password,string fullName,string email)
        {
            RegisterRequestResult result;
            var res = UnityEngine.JsonUtility.ToJson(new { type = "register", login = login, password = password,fullName=fullName, email=email });
            byte[] body = Encoding.UTF8.GetBytes(res);
            var request = (System.Net.HttpWebRequest)WebRequest.Create(registerURL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
                using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
                {

                    using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objText = reader.ReadToEnd();
                        //results = (RequestResults)js.Deserialize(objText, typeof(RequestResults));
                        result = js.Deserialize<RegisterRequestResult>(objText);
                    }

                }
            }

            return result;
        }
        static public TaskListRequestResult getTaskList(string login, string password)
        {
            TaskListRequestResult result;
            var res = UnityEngine.JsonUtility.ToJson(new { type = "getTasks", login = login, password = password});
            byte[] body = Encoding.UTF8.GetBytes(res);
            var request = (HttpWebRequest)WebRequest.Create(registerURL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
                using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
                {

                    using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objText = reader.ReadToEnd();
                        //results = (RequestResults)js.Deserialize(objText, typeof(RequestResults));
                        result = js.Deserialize<TaskListRequestResult>(objText);
                    }

                }
            }
            return result;
        }
    }
}
