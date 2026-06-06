using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ChefRecetasCS.Reports
{
    public class RptEstadisticaRecetas
        : BaseReport
    {
        private readonly Dictionary<string, int>
            datos;

        public RptEstadisticaRecetas(
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
                    h.Cell()
                        .Border(1)
                        .Padding(5)
                        .Text("Categoría")
                        .Bold();

                    h.Cell()
                        .Border(1)
                        .Padding(5)
                        .Text("Total")
                        .Bold();
                });

                foreach (var item in datos)
                {
                    table.Cell()
                        .Border(1)
                        .Padding(3)
                        .Text(item.Key);

                    table.Cell()
                        .Border(1)
                        .Padding(3)
                        .Text(item.Value.ToString());
                }
            });
        }
    }
}