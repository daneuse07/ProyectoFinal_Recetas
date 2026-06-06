using ChefRecetasCS.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ChefRecetasCS.Reports
{
    public class RptDetalleCategoria
        : BaseReport
    {
        private readonly Categoria categoria;

        public RptDetalleCategoria(
            Categoria categoria,
            string usuario,
            string titulo)
            : base(usuario, titulo)
        {
            this.categoria = categoria;
        }

        protected override void ComposeContent(
            IContainer container)
        {
            container.Column(col =>
            {
                col.Spacing(10);

                col.Item().Text(
                    $"ID: {categoria.IdCategoria}");

                col.Item().Text(
                    $"Nombre: {categoria.Nombre}");

                col.Item().Text(
                    $"Descripción:");

                col.Item().Text(
                    categoria.Descripcion ?? "");
            });
        }
    }
}