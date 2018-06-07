using BonoCorpAleman.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonoCorpAleman.ViewModels
{
    public class AddEditUserViewModel
    {
        [Required(ErrorMessage ="Ingrese un Nombre")]
        [Display(Name ="Nombre")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Ingrese un Apellido")]
        [Display(Name = "Apellido")]
        public String LastName { get; set; }
        [Required(ErrorMessage = "Ingrese un correo")]
        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage ="Ingrese una direccion de correo valido")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Ingrese una contraseña")]
        [Display(Name = "Contraseña")]
        [StringLength(8,MinimumLength =4,ErrorMessage ="Longitud invalida")]
        public String Password { get; set; }

        public Entidad getEntidad()
        {
            return new Entidad{
                Nombre = Name,
                Apellido = LastName,
                ID_email = Email,
                Password = this.Password
            };
        }

        public bool Exist(BonoCorpAlemanEntities context)
        {
            return (context.Entidad.Find(Email) != null);
        }

        public void CargarDatos(BonoCorpAlemanEntities context, String Email)/*si es que se edita se le llama*/
        {/*para editar*/
            var user = context.Entidad.Find(Email);
            if (user != null)
            {
                Name = user.Nombre;
                LastName = user.Apellido;
                Email = user.ID_email;
                Password = user.Password;
            }
        }
    }
}