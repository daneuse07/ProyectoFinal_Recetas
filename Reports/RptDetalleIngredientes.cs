using ChefRecetasCS.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ChefRecetasCS.Reports
{
    public class RptDetalleIngrediente
        : BaseReport
    {
        private readonly Ingrediente ingrediente;

        public RptDetalleIngrediente(
            Ingrediente ingrediente,
            string usuario,
            string titulo)
            : base(usuario, titulo)
        {
            this.ingrediente = ingrediente;
        }

        protected override void ComposeContent(
            IContainer container)
        {
            container.Column(col =>
            {
                col.Spacing(10);

                col.Item().Text(
                    $"ID: {ingrediente.IdIngrediente}");

                col.Item().Text(
                    $"Nombre: {ingrediente.Nombre}");

                col.Item().Text(
                    $"Unidad: {ingrediente.UnidadMedida}");

                col.Item().Text(
                    $"Calorías: {ingrediente.CaloriasPorU}");
            });
        }
    }
}