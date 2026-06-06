using ChefRecetasCS.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ChefRecetasCS.Reports
{
    public class RptDetalleReceta
        : BaseReport
    {
        private readonly Receta receta;

        public RptDetalleReceta(
            Receta receta,
            string usuario,
            string titulo)
            : base(usuario, titulo)
        {
            this.receta = receta;
        }

        protected override void ComposeContent(
            IContainer container)
        {
            container.Column(col =>
            {
                col.Spacing(10);

                col.Item().Text(
                    $"Nombre: {receta.Nombre}");

                col.Item().Text(
                    $"Categoría: {receta.Categoria}");

                col.Item().Text(
                    $"Tiempo: {receta.TiempoPrepMin} min");

                col.Item().Text(
                    $"Porciones: {receta.Porciones}");

                col.Item().Text(
                    $"Dificultad: {receta.Dificultad}");

                col.Item().Text("Descripción:")
                    .Bold();

                col.Item().Text(
                    receta.Descripcion);
            });
        }
    }
}