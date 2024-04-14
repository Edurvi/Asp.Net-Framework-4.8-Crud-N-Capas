using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUD.EntityLayer;
using System.Data;
using System.Data.SqlClient;

namespace CRUD.DataLayer
{
    public class DepartamentoDL
    {
        //Metodo que nos va a devolver la lista de departamentos
        public List<DEPARTAMENTO> Lista()
        {
            //Esta lista le vamos a poner informacion
            List<DEPARTAMENTO> lista = new List<DEPARTAMENTO>(); 
            using(SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("select * from FN_DEPARTAMENTOS()", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DEPARTAMENTO
                            {
                                idDepartamento = Convert.ToInt32(dr["ID_DEPARTAMENTO"].ToString()),
                                Nombre = dr["NOMBRE"].ToString()
                            });

                        }
                    }
                    return lista;
                } catch(Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}
