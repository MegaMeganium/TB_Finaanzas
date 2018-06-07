using BonoCorpAleman.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonoCorpAleman.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Coloque su contraseña")]
        [StringLength(8, MinimumLength=4, ErrorMessage ="La contraseña es invalida")]
        [Display(Name ="Contraseña")]
        public String Password { get; set; }
        [Required(ErrorMessage ="Coloque su email")]
        [Display(Name = "Correo")]
        public String Email { get; set; }
        public Entidad Usuario { get; set; }

        public void FindUsuario(BonoCorpAlemanEntities context)
        {
            Usuario = context.Entidad.FirstOrDefault(x => x.ID_email == this.Email && x.Password == this.Password);
        }
    }
}