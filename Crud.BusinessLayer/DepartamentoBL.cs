using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUD.DataLayer;
using CRUD.EntityLayer;

namespace Crud.BusinessLayer
{
    public class DepartamentoBL
    {
        DepartamentoDL departamentoDL = new DepartamentoDL();
        public List<DEPARTAMENTO> Lista()
        {
            try
            {
                return departamentoDL.Lista();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
