using ChefRecetasCS.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ChefRecetasCS.Reports
{
    public class RptDetalleUsuario
        : BaseReport
    {
        private readonly Usuario usuario;

        public RptDetalleUsuario(
            Usuario usuario,
            string usuarioGenera,
            string titulo)
            : base(usuarioGenera, titulo)
        {
            this.usuario = usuario;
        }

        protected override void ComposeContent(
            IContainer container)
        {
            container.Column(col =>
            {
                col.Spacing(10);

                col.Item().Text(
                    $"ID: {usuario.IdUsuario}");

                col.Item().Text(
                    $"Nombre: {usuario.Nombre}");

                col.Item().Text(
                    $"Usuario: {usuario.UserName}");

                col.Item().Text(
                    $"Rol: {usuario.Rol}");

                col.Item().Text(
                    $"Activo: {(usuario.Activo ? "Sí" : "No")}");
            });
        }
    }
}