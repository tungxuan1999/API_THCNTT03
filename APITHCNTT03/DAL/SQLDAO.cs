using APITHCNTT03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITHCNTT03.DAL
{
    public class SQLDAO
    {
        static String strCon = "Data Source=den1.mssql8.gear.host;Persist Security Info=True;User ID=thcntt03test;Password=Ih22wbpu-9I-";
        private MySQLDataContext db;

        public SQLDAO()
        {
             db = new MySQLDataContext(strCon);
        }

        public bool ConnectSQL()
        {
            if (db.DatabaseExists())
            {
                return true;
            }
            else return false;
        }

        public Object SelectALL()
        {
            //animals = (from p in db.Animals
            //           where p.KIND.Contains(keyword)
            //           select p).ToList();
            db.ObjectTrackingEnabled = false;

            var a = (from p in db.user_infos
                     select new { p.user, p.name, p.gender, p.sensor });

            return a.ToList();
        }

        public bool CreateAccount(user_info _Info)
        {
            try
            {
                db.user_infos.InsertOnSubmit(_Info);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LoginAccount(string user,string pass)
        {
            db.ObjectTrackingEnabled = false;

            var a = (from p in db.user_infos
                     where p.user.Contains(user)
                     select new { p.pass });
            for(int i = 0;i < a.ToList().Count;i++)
            {
                if (pass == a.ToList()[i].pass)
                    return true;
            }
            return false;
        }
    }
}