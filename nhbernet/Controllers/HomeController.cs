using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using nhbernet.Models;
using nhbernet.Query;
using System.Xml;

namespace nhbernet.Controllers
{
    public class HomeController : Controller
    {
        userInterface ui;
        List<dll> dll;

        public HomeController() 
        {
            ui = new userClass();

            dll = new List<Models.dll>(){
            
            new dll{
            id="",
            name="Plase Select"
            }
            };
            var citylist = ui.citylist();
            var statelist = ui.statelist();
            if (statelist.Count == 0) {

                //Load the XML file in XmlDocument.
                XmlDocument doc = new XmlDocument();
                var path= System.Web.HttpContext.Current.Server.MapPath("/xml/stateuser.xml");
                doc.Load(path);

                //Loop through the selected Nodes.
                foreach (XmlNode node in doc.SelectNodes("/states/state"))
                {
                    //Fetch the Node values and assign it to Model.

                    state s = new state();
                    s.id = int.Parse(node["Id"].InnerText);
                    s.statename = node["Name"].InnerText;
                    ui.savestate(s);
                 
                    XmlDocument doc1 = new XmlDocument();
                    var path1 = System.Web.HttpContext.Current.Server.MapPath("/xml/cityuser.xml");
                    doc1.Load(path1);

                    //Loop through the selected Nodes.
                    foreach (XmlNode node1 in doc1.SelectNodes("/cities/city"))
                    {
                        //Fetch the Node values and assign it to Model.
                        if(s.id==int.Parse(node1["stateid"].InnerText))
                        {
                        city c = new city();
                        c.id = int.Parse(node1["Id"].InnerText);
                        c.cityname = node1["Name"].InnerText;
                        c.stateid = s;
                        ui.savecity(c);
                        }

                    }

                }
             //for (int i = 0; i < 3; i++)
             //   {
             //       state s = new state();
             //       if (i == 0)
             //           s.statename = "Maharashtra";
             //       else if (i == 1)
             //           s.statename = "Panjab";
             //       else if (i == 2)
             //           s.statename = "UP";
             //       else
             //           s.statename = "MP";
             //       ui.savestate(s);
             //   }
            }
        }

        public ActionResult Index()
        {
            List<usermodel> li = new List<usermodel>();
            var user = ui.list();
           foreach (var i in user) 
           {
               usermodel u = new usermodel();
               u.id = i.id;
               u.name = i.name;
               u.address = i.address;
               //u.city = ui.citylist().Where(x=>x.id==Convert.ToInt32(i.city)).SingleOrDefault().cityname;
               u.city = i.cityid.cityname;
               u.state = i.stateid.statename;
               li.Add(u);
           }
            return View(li);
        }

        // GET: /Home/Create

        public ActionResult Create()
        {
            usermodel u = new usermodel();
            u.cities = new SelectList(dll, "id", "name");
            u.states = new SelectList(ui.statelist(), "id", "statename");
            return View(u);
        } 

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(usermodel us)
        {
            usermodel um = new usermodel();
            //um.cities = new SelectList(dll, "id", "name");
            um.cities = new SelectList(dll, "id", "name");
            um.states = new SelectList(ui.statelist(), "id", "statename");
            var city = ui.citylist().Where(x => x.id == Convert.ToInt32(us.city)).SingleOrDefault();
            var state = ui.statelist().Where(x => x.id == Convert.ToInt32(us.state)).SingleOrDefault();
            try
            {
                user u = new user();
                u.name = us.name;
                u.address = us.address;
                u.cityid = city;
                u.stateid = state;
                ui.save(u);
                bool msg = false;
                return Json(msg);
                //return RedirectToAction("Index");
            }
            catch
            {
                bool msg = true;
                return Json(msg);
            }
        }
        
        
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            var t =ui.getuser(id);
            usermodel u = new usermodel();
            //u.cities = new SelectList(dll, "id", "name");
            var selectedcity = ui.citylist().Where(x => x.stateid.id == t.stateid.id).ToList();
            u.cities = new SelectList(selectedcity, "id", "cityname");
            u.states = new SelectList(ui.statelist(), "id", "statename");
            u.id = t.id;
            u.name = t.name;
            u.address = t.address;
            u.city = t.cityid.id.ToString();
            u.state = t.stateid.id.ToString();
            return View(u);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(usermodel us)

        {
            var selectedcity = ui.citylist().Where(x => x.id ==Convert.ToInt32(us.city)).ToList();
            us.cities = new SelectList(selectedcity, "id", "cityname");
            us.states = new SelectList(ui.statelist(), "id", "statename");
            var city = ui.citylist().Where(x => x.id ==Convert.ToInt32(us.city)).SingleOrDefault(); //take city object
            var state = ui.statelist().Where(x => x.id == Convert.ToInt32(us.state)).SingleOrDefault();
            try
            {
                var t = ui.getuser(us.id);
                t.id = us.id;
                t.name = us.name;
                t.address = us.address;
                t.cityid = city; //pass city object city
                t.stateid = state;
                ui.update(t);
                return RedirectToAction("Index");
                //bool msg = false;
                //return Json(msg);
            }
            catch
            {
                return View(us);
            }
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            var t = ui.getuser(id);
            ui.delete(t);
            return RedirectToAction("Index");
        }

        //
        // POST: /Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetcityBystateId(int stateid)
        {
            List<city> li = new List<city>();
            var lblcity = ui.citylist().Where(x => x.stateid.id == stateid).ToList();
            usermodel f = new usermodel();
            f.cities = new SelectList(lblcity, "id", "cityname");
            return Json(f.cities, JsonRequestBehavior.AllowGet);
        }
    
    }
}
