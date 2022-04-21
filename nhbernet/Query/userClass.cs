using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nhbernet.Models;
using NHibernate;

namespace nhbernet.Query
{
    public class userClass : userInterface
    {
        private ISession session;

        public userClass() 
        {
            session = NHibernateSession.OpenSession();
        }
        public void save(user u) 
        {
            session.Save(u);
            session.Flush();
        }
        public List<user> list() 
        {
            var li = session.QueryOver<user>().List().ToList();
            return li;
        }

        public user getuser(int id) 
        {
            var t = session.QueryOver<user>().Where(x => x.id == id).SingleOrDefault();
            return t;
        }

        public void update(user u)
        {
            session.Update(u);
            session.Flush();
        }

        public void delete(user u) 
        {
            session.Delete(u);
            session.Flush();
        }

        public List<city> citylist() 
        {
            var t = session.QueryOver<city>().List().ToList();
            return t;
        }
        public void savecity(city city) 
        {
            session.Save(city);
            session.Flush();
        }

        public List<state> statelist() 
        {
            var t = session.QueryOver<state>().List().ToList();
            return t;
        }

        public void savestate(state state) 
        {
            session.Save(state);
            session.Flush();
        }
    }
}