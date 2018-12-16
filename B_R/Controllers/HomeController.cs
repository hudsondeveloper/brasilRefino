using B_R.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace B_R.Controllers
{
    public class HomeController : Controller
    {
        public static User user;
        public ActionResult Index()
        {
            //using (ISession session = NHibernateHelper.AbrirSession())
            //{
            //    var users = session.Query<User>().ToList();
            //    return View(users);
            //}
            return View();
        }
        [HttpPost]
        public ActionResult Login(string login,string password)
        {
            var log = login;
            var senha = password;
            using (ISession session = NHibernateHelper.OpenSession())
            {

                if (( session.Query<User>().Where(x=>x.Nome == log && x.senha == senha).Count() > 0))
                {
                    Session["user"] = login;
                    user = session.Query<User>().Where(x => x.Nome == log && x.senha == senha).FirstOrDefault();
                    session.Close();
                    return RedirectToAction("Index","Panel");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                };
            }
        }
        [HttpGet]
        public ActionResult Cadastrar()
        {
          return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(user);
                        transacao.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                            session.Close();
                        }
                        throw new Exception("Erro ao inserir Cliente : " + ex.Message);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}