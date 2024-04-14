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
    public class EmpleadoDL
    {
        //Metodo que nos va a devolver la lista de departamentos
        public List<Empleado> Lista()
        {
            //Esta lista le vamos a poner informacion
            List<Empleado> lista = new List<Empleado>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("select * from FN_EMPLEADOS()", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Empleado
                            {
                                IdEmpleado = Convert.ToInt32(dr["ID_EMPLEADO"].ToString()),
                                NombreCompleto = dr["NOMBRE_COMPLETO"].ToString(),
                                DEPARTAMENTO = new DEPARTAMENTO
                                {
                                    idDepartamento = Convert.ToInt32(dr["ID_DEPARTAMENTO"].ToString()),
                                    Nombre = dr["NOMBRE"].ToString()
                                },
                                Sueldo = (decimal)dr["SUELDO"],
                                FechaContrato = dr["FECHA_CONTRATO"].ToString()
                            });
                        }
                    }
                    return lista;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public Empleado Obtener(int idEmpleado)
        {
            Empleado entidad = new Empleado();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("select * from FN_EMPLEADO(@ID_EMPLEADO)", oConexion);
                cmd.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleado);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad.IdEmpleado = Convert.ToInt32(dr["ID_EMPLEADO"].ToString());
                            entidad.NombreCompleto = dr["NOMBRE_COMPLETO"].ToString();
                            entidad.DEPARTAMENTO = new DEPARTAMENTO
                            {
                                idDepartamento = Convert.ToInt32(dr["ID_DEPARTAMENTO"].ToString()),
                                Nombre = dr["NOMBRE"].ToString()
                            };
                            entidad.Sueldo = (decimal)dr["SUELDO"];
                            entidad.FechaContrato = dr["FECHA_CONTRATO"].ToString();

                        }
                    }
                    return entidad;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool Crear(Empleado entidad)
        {

            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {

                SqlCommand cmd = new SqlCommand("SP_CREAR_EMPLEADO", oConexion);

                cmd.Parameters.AddWithValue("@NOMBRE_COMPLETO",entidad.NombreCompleto);
                cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", entidad.DEPARTAMENTO.idDepartamento);
                cmd.Parameters.AddWithValue("@SUELDO", entidad.Sueldo);
                cmd.Parameters.AddWithValue("@FECHA_CONTRATO", entidad.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool Editar(Empleado entidad)
        {

            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {

                SqlCommand cmd = new SqlCommand("SP_EDITAR_EMPLEADO", oConexion);
                cmd.Parameters.AddWithValue("@ID_EMPLEADO", entidad.IdEmpleado);
                cmd.Parameters.AddWithValue("@NOMBRE_COMPLETO", entidad.NombreCompleto);
                cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", entidad.DEPARTAMENTO.idDepartamento);
                cmd.Parameters.AddWithValue("@SUELDO", entidad.Sueldo);
                cmd.Parameters.AddWithValue("@FECHA_CONTRATO", entidad.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool Eliminar(int idEmpleado)
        {

            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {

                SqlCommand cmd = new SqlCommand("SP_ELIMINAR_EMPLEADO", oConexion);
                cmd.Parameters.AddWithValue("@ID_EMPLEADO",idEmpleado);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}
