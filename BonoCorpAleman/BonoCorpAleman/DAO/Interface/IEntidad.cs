using BonoCorpAleman.Models;
using BonoCorpAleman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonoCorpAleman.DAO.Interface
{
    interface IEntidad : IBase<Entidad, AddEditUserViewModel, string>
    {
        List<Entidad> FilterName(string filter);
    }
}
