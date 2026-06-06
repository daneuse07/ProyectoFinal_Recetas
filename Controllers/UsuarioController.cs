using System.Collections.Generic;
using ChefRecetasCS.DAL;
using ChefRecetasCS.Models;

namespace ChefRecetasCS.Controllers
{
    public class UsuarioController
    {
        private UsuarioDAL dao =
            new UsuarioDAL();

        // =====================================
        // LOGIN
        // =====================================

        public Usuario Login(
            string usuario,
            string password)
        {
            return dao.Autenticar(
                usuario,
                password);
        }

        // =====================================
        // REGISTRAR
        // =====================================

        public void Registrar(
            string nombre,
            string usuario,
            string password,
            string rol)
        {
            dao.Registrar(
                nombre,
                usuario,
                password,
                rol);
        }

        // =====================================
        // OBTENER USUARIOS
        // =====================================

        public List<Usuario> ObtenerUsuarios()
        {
            return dao.ObtenerUsuarios();
        }

        // =====================================
        // GUARDAR USUARIO
        // =====================================

        public void GuardarUsuario(
            int id,
            string nombre,
            string usuario,
            string password,
            string rol)
        {
            if (id == 0)
            {
                dao.Registrar(
                    nombre,
                    usuario,
                    password,
                    rol);
            }
            else
            {
                dao.ActualizarUsuario(
                    id,
                    nombre,
                    usuario,
                    password,
                    rol);
            }
        }

        // =====================================
        // ELIMINAR
        // =====================================

        public void EliminarUsuario(int id)
        {
            dao.EliminarUsuario(id);
        }
    }
}