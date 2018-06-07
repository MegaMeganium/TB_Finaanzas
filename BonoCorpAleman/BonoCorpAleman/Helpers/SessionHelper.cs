using BonoCorpAleman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonoCorpAleman.Helpers
{
    public static class SessionHelper
    {
        public static String GetUsuarioId(this HttpSessionStateBase session, String def = "error")
        {
            return session["UsuarioId"].To_String(def);
        }
        public static void SetUsuarioId(this HttpSessionStateBase session, String val)
        {
            session["UsuarioId"] = val;
        }
        public static Entidad GetUsuario(this HttpSessionStateBase session)
        {
            return (Entidad)session["Usuario"];
        }
        public static void SetUsuario(this HttpSessionStateBase session, Entidad val)
        {
            session["Usuario"] = val;
        }
    }
}