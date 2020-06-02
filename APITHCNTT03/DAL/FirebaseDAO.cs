using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace APITHCNTT03.DAL
{
    public class FirebaseDAO
    {
        //is... 0 false 1 true

        //kind 1: chay 2: trom
        public void SetValueLogs(int isBoolen, int kind)
        {
            DateTime date = DateTime.Now;
            string Date = date.ToString("dd:MM:yyyy");
            string Time = date.ToString("HH:mm:ss");
            string Response = "";

            if (isBoolen == 0) Response += "Không có";
            else Response += "Có";

            if (kind == 1) Response += " cháy";
            else Response += " đột nhập";

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Name = Time,
                Value = Date,
                Response = Response
            });

            var request = WebRequest.CreateHttp("https://smarthome-a93f9.firebaseio.com/Logs.json");
            request.Method = "POST";
            request.ContentType = "application/json";
            var buffer = Encoding.UTF8.GetBytes(json);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
        }
        public bool SetValueChay(int isFire)
        {
            try
            {

                var json = "";
                switch (isFire)
                {
                    case 0:
                        {
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                PhatHienChay = "Không có cháy"

                            });
                        }
                        break;
                    case 1:
                        {
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                PhatHienChay = "Có cháy"

                            });
                        }
                        break;
                    default:
                        {
                            return false;
                        }
                        break;
                }

                var request = WebRequest.CreateHttp("https://smarthome-a93f9.firebaseio.com/.json");
                request.Method = "PATCH";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                SetValueLogs(isFire, 1);
                //var response = request.GetResponse();
                //json = (new StreamReader(response.GetResponseStream())).ReadToEnd();


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool SetValueTrom(int isTrom)
        {
            try
            {

                var json = "";
                switch (isTrom)
                {
                    case 0:
                        {
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                PhatHienDotNhap = "Không có đột nhập"

                            });
                        }
                        break;
                    case 1:
                        {
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                            {
                                PhatHienDotNhap = "Có đột nhập"

                            });
                        }
                        break;
                    default:
                        {
                            return false;
                        }
                        break;
                }

                var request = WebRequest.CreateHttp("https://smarthome-a93f9.firebaseio.com/.json");
                request.Method = "PATCH";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                SetValueLogs(isTrom, 2);
                //var response = request.GetResponse();
                //json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool setToken(string user, string token)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Token = token

                });
                var request = WebRequest.CreateHttp("https://smarthome-a93f9.firebaseio.com/user/" + user + ".json");
                request.Method = "PATCH";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Object getToken(string user)
        {
            var request = WebRequest.CreateHttp("https://smarthome-a93f9.firebaseio.com/user/" + user + ".json");
            request.Method = "GET";
            request.ContentType = "application/json";

            var response = request.GetResponse();
            var json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
            JSONResponseUSER rs = JsonConvert.DeserializeObject<JSONResponseUSER>(json);
            return rs.Token;
        }

        public Object getrealtime()
        {

            var request = WebRequest.CreateHttp("https://smarthome-a93f9.firebaseio.com/.json");
            request.Method = "GET";
            request.ContentType = "application/json";

            var response = request.GetResponse();
            var json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
            JSONResponse rs = JsonConvert.DeserializeObject<JSONResponse>(json);

            return rs;
        }

        public class JSONResponse
        {
            public string PhatHienChay { get; set; }
            public string PhatHienDotNhap { get; set; }
        }

        public class JSONResponseUSER
        {
            public string Token { get; set; }
        }
    }
}