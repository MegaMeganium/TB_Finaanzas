using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonoCorpAleman.Models;
using BonoCorpAleman.ViewModels.Bono;

namespace BonoCorpAleman.DAO.Interface
{
    interface IBono : IBase<Models.Bono, AddEditBonoViewModel, long>
    {
        IList<Models.Bono> GetAllByUserId(string id);
        IList<Models.Bono> GetAllByUserIdAndFilter(string id, string filter);
    }
}
