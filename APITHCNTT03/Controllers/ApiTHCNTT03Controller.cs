using APITHCNTT03.DAL;
using APITHCNTT03.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APITHCNTT03.Controllers
{
    public class ApiTHCNTT03Controller : ApiController
    {
        //[HttpGet]
        //[Route("api/ConnectSQL")]
        //public IHttpActionResult ConnectSQL()
        //{
        //    return Json(new SQLDAO().ConnectSQL());
        //}

        [HttpGet]
        [Route("api/setvaluechay")]
        public IHttpActionResult SetValueChay(int isFire, string user, string token)
        {
            if (token.CompareTo(new FirebaseDAO().getToken(user)) == 0)
            {
                if (new FirebaseDAO().SetValueChay(isFire))
                    return Json(new { status = true, data = new Object(), response = "Set Value Success" });
                else
                {
                    return Json(new { status = true, data = new Object(), response = "Set Value False" });
                }
            }
            else
            {
                return Json(new { status = false, data = new Object(), response = "Expiration Token" });
            }
        }

        [HttpGet]
        [Route("api/setvaluedotnhap")]
        public IHttpActionResult SetValueDotNhap(int isTrom, string user, string token)
        {
            if (token.CompareTo(new FirebaseDAO().getToken(user)) == 0)
            {
                if (new FirebaseDAO().SetValueTrom(isTrom))
                    return Json(new { status = true, data = new Object(), response = "Set Value Success" });
                else
                {
                    return Json(new { status = true, data = new Object(), response = "Set Value False" });
                }
            }
            else
            {
                return Json(new { status = false, data = new Object(), response = "Expiration Token" });
            }
        }

        [HttpGet]
        [Route("api/getrealtime")]
        public IHttpActionResult getrealtime(string user, string token)
        {
            if (token.CompareTo(new FirebaseDAO().getToken(user)) == 0)
                return Json(new
                {
                    status = true,
                    data = new FirebaseDAO().getrealtime(),
                    response = ""
                });
            else
            {
                return Json(new { status = false, data = new Object(), response = "Expiration Token" });
            }
        }


        [HttpGet]
        [Route("api/getall")]
        public IHttpActionResult GetALL(string user, string token)
        {
            if (token.CompareTo(new FirebaseDAO().getToken(user)) == 0)
                return Json(new { status = true, data = new SQLDAO().SelectALL(), response = "" });
            else
            {
                return Json(new { status = false, data = new Object(), response = "Expiration Token" });
            }
        }

        [HttpPost]
        [Route("api/CreateAccount")]
        public IHttpActionResult CreateAccount(user_info _Info)
        {
            return Json(new SQLDAO().CreateAccount(_Info));
        }

        [HttpPost]
        [Route("api/LoginAccount")]
        public IHttpActionResult LoginAccount(Account account)
        {
            string token = "";
            if (new SQLDAO().LoginAccount(account.user, account.pass))
            {
                token = getRandomToken();
                if (new FirebaseDAO().setToken(account.user, token))
                    return Json(token);
                else
                {
                    token = "";
                }
            }
            return Json(token);
        }

        public class Account
        {
            public string user { get; set; }
            public string pass { get; set; }
        }

        string getRandomToken()
        {
            Random random = new Random();
            int sttran = random.Next(100, 200);
            char[] array = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'O', 'P', 'Q', 'R', 'S', 'X', 'M', 'N', 'T', 'V', 'W' };
            string result = "";
            for (int i = 0; i < sttran; i++)
            {
                result += array[random.Next(0, 31)];
            }
            return result;
        }
    }
}