using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ChefRecetasCS.Reports
{
    public class RptEstadisticaUsuarios
        : BaseReport
    {
        private readonly Dictionary<string, int>
            datos;

        public RptEstadisticaUsuarios(
            Dictionary<string, int> datos,
            string usuario,
            string titulo)
            : base(usuario, titulo)
        {
            this.datos = datos;
        }

        protected override void ComposeContent(
            IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(3);
                    c.RelativeColumn();
                });

                table.Header(h =>
                {
                    h.Cell().Text("Rol");
                    h.Cell().Text("Cantidad");
                });

                foreach (var item in datos)
                {
                    table.Cell().Text(item.Key);
                    table.Cell().Text(
                        item.Value.ToString());
                }
            });
        }
    }
}