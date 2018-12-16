using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B_R.Models;
using NHibernate;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace B_R.Controllers
{
    public class EquipamentoController : Controller
    {
        // GET: Equipamento
        public ActionResult Index(int id)
        {
            Session["idArea"] = id;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var equipamentos = session.Query<Equipamento>().Where(x=>x.area.Id == id).ToList();
                session.Close();
                return View(equipamentos);
            }
        }

        [HttpGet]
        public ActionResult Cadastrar(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Equipamento equipamento,int area)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                      var equipamentos = session.Query<Equipamento>().OrderByDescending(x=>x.reg).Where(x=>x.area.Id == area).ToList();
                        var reg=0;
                        if (equipamentos.Count() == 0)
                        {
                            reg += 1;
                        }
                        else
                        {
                            reg = equipamentos.First().reg;
                            reg += 1;
                        }
                        equipamento.reg = reg;
                        equipamento.area = session.Get<Area>(area);
                        session.Save(equipamento);

                        LogEquipamento log = new LogEquipamento();
                        log.equipamento = session.Query<Equipamento>().Where(x => x.reg == equipamento.reg).FirstOrDefault();
                        log.acao = "Cadastrou";
                        var situacaoDesc = log.equipamento.situacao.Equals("ATIVO") ? situacao.ATIVO.ToString() : situacao.FORA_DE_OPERACAO.ToString();
                        log.anterior = "reg: " + log.equipamento.reg +
                            " tag: " + log.equipamento.tag +
                            " status do documento: " + log.equipamento.status_Doc +
                            " situação: " + situacaoDesc +
                            " descricao do equipamento: " + log.equipamento.descricao_equipamento +
                            " descricao: " + log.equipamento.descricao +
                            " área: " + log.equipamento.area.Id;
                        log.horario = DateTime.Now;
                        log.user = HomeController.user;
                        session.Save(log);

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
                        throw new Exception("Erro ao inserir Equipamento : " + ex.Message);
                    }
                }
                return View();
            }
        }

        public ActionResult Detalhe(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var logsEquipamentos = session.Query<LogEquipamento>().Where(x => x.equipamento.Id == id).ToList();
                session.Close();
                return View(logsEquipamentos);
            }
        }

        [HttpGet]
        public ActionResult UploadFile(int idequip,int idarea)
        {
            string strCaminho;
            strCaminho = this.Server.MapPath("~/EquipamentoFile/"+"area"+ idarea + "/"+ "equip" + idequip);
            if (!Directory.Exists(strCaminho)) //Se o diretório não existir...
            {
                System.IO.Directory.CreateDirectory(strCaminho);
            }
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/EquipamentoFile/" + "area" + idarea + "/"+ "equip" + idequip + "/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*"); List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            Session["idequip"]= idequip;
            Session["areaid"] = idarea;
            return View(items);
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file,int id, int idarea)
        {

            string strCaminho;
            strCaminho = this.Server.MapPath("~\\EquipamentoFile/" + "area" + idarea + "/"+"equip" + id +"/");
            if (!Directory.Exists(strCaminho)) //Se o diretório não existir...
            {
                System.IO.Directory.CreateDirectory(strCaminho);
            }

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~\\EquipamentoFile/" + "area" + idarea + "/" + "equip" + id + "/"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return RedirectToAction("UploadFile",new { idequip  = id, idarea = idarea });
            }
            catch(Exception ex)
            {
               var bola= ex.Message;
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("UploadFile", new { idequip = id , idarea = idarea });
            }
        }

        public FileResult Download(string ImageName,int id,int idarea)
        {
            var FileVirtualPath = "~/EquipamentoFile/" + "area" + idarea + "/" + "equip" + id + "/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var equipamento = session.Get<Equipamento>(id);
                Session["areaid"] = equipamento.area.Id;
                session.Close();
                return View(equipamento);
            }
        }


        [HttpPost]
        public ActionResult Editar(Equipamento equipamento,int idArea)
        {
            int dado;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {

                        LogEquipamento log = new LogEquipamento();
                        log.acao = "Editou";
                        log.equipamento = session.Get<Equipamento>(equipamento.Id);
                        var situacaoDesc = log.equipamento.situacao.Equals("ATIVO") ? situacao.ATIVO.ToString() : situacao.FORA_DE_OPERACAO.ToString();
                        var situacaoNovo = equipamento.situacao.Equals("ATIVO") ? situacao.ATIVO.ToString() : situacao.FORA_DE_OPERACAO.ToString();

                        log.alteracao =
                         " tag: " + equipamento.tag +
                         " status do documento: " + equipamento.status_Doc +
                         " situação: " + situacaoDesc +
                         " descricao do equipamento: " + equipamento.descricao_equipamento +
                         " descricao: " + equipamento.descricao;


                        log.anterior =
                         " tag: " + log.equipamento.tag +
                         " status do documento: " + log.equipamento.status_Doc +
                         " situação: " + situacaoDesc +
                         " descricao do equipamento: " + log.equipamento.descricao_equipamento +
                         " descricao: " + log.equipamento.descricao;
   




                        log.horario = DateTime.Now;
                        log.user = HomeController.user;
                        session.Save(log);

                        equipamento.area = session.Get<Area>(idArea);
                        dado = equipamento.area.Id;
                        equipamento.reg = log.equipamento.reg;
                        session.Merge(equipamento);
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
                        throw new Exception("Erro ao Atualizar Equipamento : " + ex.Message);
                    }
                }
                return RedirectToAction("Index", new { id = dado });
            }
        }

        //[HttpGet]
        //public ActionResult Deletar(int id)
        //{
        //    int dado;
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        using (ITransaction transacao = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                Equipamento equipamento = session.Get<Equipamento>(id);
        //                dado = equipamento.area.Id;
        //                session.Delete(equipamento);
        //                transacao.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                if (!transacao.WasCommitted)
        //                {
        //                    transacao.Rollback();
        //                }
        //                throw new Exception("Erro ao Deletar equipamento : " + ex.Message);
        //            }
        //        }
        //        return RedirectToAction("Index",new { id = dado });
        //    }
        //}
    }
}
