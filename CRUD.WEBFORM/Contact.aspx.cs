using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CRUD.EntityLayer;
using Crud.BusinessLayer;
using System.Globalization;

namespace CRUD.WEBFORM
{
    public partial class Contact : Page
    {
        private static int idEmpleado = 0;
        DepartamentoBL DepartamentoBL = new DepartamentoBL();
        EmpleadoBL empleadoBL = new EmpleadoBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["idEmpleado"] != null)
                {
                    idEmpleado = Convert.ToInt32(Request.QueryString["idEmpleado"].ToString());
                    if(idEmpleado != 0)
                    {
                        lblTitulo.Text = "Editar Empleado";
                        btnSubmit.Text = "Actualizar";
                        Empleado empleado = empleadoBL.Obtener(idEmpleado);
                        txtNombreCompleto.Text = empleado.NombreCompleto;
                        CargarDepartamentos(empleado.DEPARTAMENTO.idDepartamento.ToString());
                        txtSueldo.Text = empleado.Sueldo.ToString();
                        txtFechaContrato.Text = Convert.ToDateTime(empleado.FechaContrato, new CultureInfo("es-EC")).ToString("yyy-MM-dd");
                    }
                    else
                    {
                        lblTitulo.Text = "Nuevo Empleado";
                        btnSubmit.Text = "Actualizar";
                        CargarDepartamentos();
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }


        private void CargarDepartamentos(string idDepartamento = "")
        {
            List<DEPARTAMENTO> lista = DepartamentoBL.Lista();
            ddlDepartamento.DataTextField = "Nombre";
            ddlDepartamento.DataValueField = "idDepartamento";
            ddlDepartamento.DataSource = lista;
            ddlDepartamento.DataBind();

            if (idDepartamento != "")
                ddlDepartamento.SelectedValue = idDepartamento;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Empleado entidad = new Empleado()
            {
                IdEmpleado = idEmpleado,
                NombreCompleto = txtNombreCompleto.Text,
                DEPARTAMENTO = new DEPARTAMENTO() { idDepartamento = Convert.ToInt32(ddlDepartamento.SelectedValue) },
                Sueldo = Convert.ToDecimal(txtSueldo.Text, new CultureInfo("es-EC")),
                FechaContrato = txtFechaContrato.Text
            };
            bool respuesta;
            if (idEmpleado != 0)
                respuesta = empleadoBL.Editar(entidad);
            else
                respuesta = empleadoBL.Crear(entidad);

            if (respuesta)
                Response.Redirect("~/Default.aspx");
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('No se pudo realizar la operacion')", true);

        }
    }
}