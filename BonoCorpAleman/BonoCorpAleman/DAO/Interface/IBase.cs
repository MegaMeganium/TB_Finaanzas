using BonoCorpAleman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BonoCorpAleman.DAO
{
    interface IBase <T, AddEditViewModel, ID>
    {
        BonoCorpAlemanEntities1 context { get; set; }
        IList<T> GetAll();
        void Delete(T obj);
        void Delete(ID id);
        void AddEditEntity(AddEditViewModel model);
        T Find(ID id);
    }
}
