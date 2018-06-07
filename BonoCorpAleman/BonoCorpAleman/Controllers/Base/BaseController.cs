using BonoCorpAleman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BonoCorpAleman.Controllers
{
    public class BaseController : Controller
    {
        protected BonoCorpAlemanEntities context = new BonoCorpAlemanEntities();

    }
}