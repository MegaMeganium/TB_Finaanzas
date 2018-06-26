using BonoCorpAleman.ViewModels;
using BonoCorpAleman.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using System.Diagnostics;
using BonoCorpAleman.Models;
using BonoCorpAleman.DAO.Implements;
using BonoCorpAleman.DAO.Interface;

namespace BonoCorpAleman.Controllers
{
    public class HomeController : BaseController
    {
        IEntidad EntidadDAO = new EntidadImplements();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Error()
        {
            return View("Error");
        }
        [HttpGet]
        public ActionResult Login()
        {
            var viewModel = new LoginViewModel();
            viewModel.FindUsuario(EntidadDAO.context);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var Usuario = context.Entidad.FirstOrDefault(x => x.ID_email == model.Email && x.Password == model.Password);
            model.FindUsuario(context);
            if (Usuario == null)
            {
                ViewBag.Mensaje = "Email o Contraseña Incorrectos. ";
                //return RedirectToAction("Index", "Home");
                return View(model);
            }
            Session.SetUsuarioId(model.Usuario.ID_email);
            Session.SetUsuario(model.Usuario);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.SetUsuarioId(null);
            Session.SetUsuario(null);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AddEditUser(String Email)
        {
            var viewModel = new AddEditUserViewModel();
            viewModel.CargarDatos(EntidadDAO.context, Email);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEditUser(AddEditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TryUpdateModel(model);
                Debug.WriteLine("AEuser - no es valido");
                return View(model);
            }
            try
            {
                /*TODO O NADA*/
                using(var transactionScope = new TransactionScope())
                {
                    EntidadDAO.AddEditEntity(model);
                    Debug.WriteLine("AEuser - antes de transacionComplete");
                    transactionScope.Complete();
                    Debug.WriteLine("AEuser - redireccionando al Index");

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                model.CargarDatos(EntidadDAO.context, model.Email);
                Debug.WriteLine("AEuser - catch");
                TryUpdateModel(model);
                return View(model);
            }
        }
    }
}