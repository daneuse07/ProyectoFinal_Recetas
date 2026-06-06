using ChefRecetasCS.Controllers;
using ChefRecetasCS.DAL;
using ChefRecetasCS.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ChefRecetasCS.Views
{
    public partial class FrmRecetas : Form
    {
        // =====================================
        // COMPONENTES
        // =====================================

        private DataGridView dgv =
            new DataGridView();

        private TextBox txtBuscar =
            new TextBox();

        private Button btnBuscar =
            new Button
            {
                Text = "Buscar"
            };

        private Button btnLimpiar =
            new Button
            {
                Text = "Limpiar"
            };

        private Button btnVer =
            new Button
            {
                Text = "Ver Completa"
            };

        private Label lblTotal =
            new Label();

        private ComboBox cboFiltroCategoria =
            new ComboBox
            {
                Width = 150,
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

        private CheckBox chkFechas =
            new CheckBox
            {
                Text = "Usar Fechas"
            };

        private DateTimePicker dtInicio =
            new DateTimePicker();

        private DateTimePicker dtFin =
            new DateTimePicker();

        // =====================================
        // FORMULARIO
        // =====================================

        private ComboBox cboCat =
            new ComboBox
            {
                Width = 180
            };

        private TextBox txtNombre =
            new TextBox
            {
                Width = 180
            };

        private RichTextBox txtDesc =
            new RichTextBox
            {
                Width = 180,
                Height = 60
            };

        private TextBox txtTiempo =
            new TextBox
            {
                Width = 60
            };

        private TextBox txtPorc =
            new TextBox
            {
                Width = 60
            };

        private ComboBox cboDif =
            new ComboBox
            {
                Width = 120
            };

        private PictureBox picFoto =
            new PictureBox
            {
                Width = 120,
                Height = 120,
                SizeMode =
                    PictureBoxSizeMode.Zoom,
                BorderStyle =
                    BorderStyle.FixedSingle
            };

        private Button btnFoto =
            new Button
            {
                Text = "Foto..."
            };

        private ListBox lstPasos =
    new ListBox();

        private TextBox txtPaso =
            new TextBox();

        private Button btnAgregarPaso =
            new Button()
            {
                Text = "Agregar Paso"
            };

        private Button btnNuevo =
            new Button
            {
                Text = "Nuevo"
            };

        private Button btnGuardar =
            new Button
            {
                Text = "Guardar"
            };

        private Button btnEliminar =
            new Button
            {
                Text = "Eliminar"
            };

        // =====================================
        // CONTROLADORES
        // =====================================

        private RecetaController _ctrlR =
            new RecetaController();

        private CategoriaController _ctrlC =
            new CategoriaController();

        private PasoPreparacionDAL _pasosDAL =
    new PasoPreparacionDAL();

        private RecetaIngredienteDAL _riDAL =
            new RecetaIngredienteDAL();

        // =====================================
        // VARIABLES
        // =====================================

        private Usuario _usuario;
        private byte[] _fotoBytes = null;
        private int _idSeleccion = 0;
        private SplitContainer panel;
        // =====================================
        // CONSTRUCTOR
        // =====================================

        public FrmRecetas(Usuario u)
        {
            _usuario = u;

            Dock = DockStyle.Fill;

            BackColor = Color.Beige;

            ConfigurarDGV();

            ConfigurarCombos();

            // =====================================
            // PANEL SUPERIOR
            // =====================================

            var topPanel =
                new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 90,
                    BackColor = Color.Beige
                };

            txtBuscar.Width = 180;
            txtBuscar.Location =
                new Point(10, 10);

            cboFiltroCategoria.Location =
                new Point(200, 10);

            chkFechas.Location =
                new Point(370, 12);

            dtInicio.Location =
                new Point(480, 10);

            dtFin.Location =
                new Point(650, 10);

            btnBuscar.Location =
                new Point(10, 45);

            btnLimpiar.Location =
                new Point(110, 45);

            btnVer.Location =
                new Point(210, 45);

            lblTotal.Location =
                new Point(340, 50);

            lblTotal.AutoSize = true;

            // =====================================
            // ESTILOS
            // =====================================

            Button[] botones =
            {
                btnBuscar,
                btnLimpiar,
                btnVer
            };

            foreach (var b in botones)
            {
                b.BackColor =
                    Color.SandyBrown;

                b.ForeColor =
                    Color.White;

                b.FlatStyle =
                    FlatStyle.Flat;
            }

            btnVer.BackColor =
                Color.Peru;

            // =====================================
            // EVENTOS
            // =====================================

            btnBuscar.Click +=
                (s, e) => Buscar();

            btnLimpiar.Click +=
                (s, e) =>
                {
                    txtBuscar.Clear();

                    cboFiltroCategoria.SelectedIndex = 0;

                    chkFechas.Checked = false;

                    CargarDGV();
                };

            btnVer.Click +=
                (s, e) =>
                {
                    if (dgv.CurrentRow == null)
                        return;

                    var r =
                        (Receta)dgv
                            .CurrentRow
                            .DataBoundItem;

                    new FrmDetalleReceta(r)
                        .ShowDialog();
                };

            topPanel.Controls.AddRange(
                new Control[]
                {
                    txtBuscar,
                    cboFiltroCategoria,
                    chkFechas,
                    dtInicio,
                    dtFin,
                    btnBuscar,
                    btnLimpiar,
                    btnVer,
                    lblTotal
                });

            btnAgregarPaso.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(
                    txtPaso.Text))
                    return;

                lstPasos.Items.Add(
                    txtPaso.Text);

                txtPaso.Clear();
            };

            // =====================================
            // SPLIT
            // =====================================

            panel = new SplitContainer
        {
            Dock = DockStyle.Fill,
            SplitterDistance = 650
        };

            panel.Panel1.Padding =
                new Padding(5);

            panel.Panel1.Controls.Add(dgv);

            panel.Panel2.Controls.Add(
                PanelFormulario());

            Controls.Add(panel);

            Controls.Add(topPanel);

            // =====================================
            // EVENTOS DGV
            // =====================================

            dgv.SelectionChanged +=
                DGV_SelectionChanged;

            dgv.CellDoubleClick +=
                (s, e) =>
                {
                    if (dgv.CurrentRow != null)
                    {
                        var r =
                            (Receta)dgv
                                .CurrentRow
                                .DataBoundItem;

                        new FrmDetalleReceta(r)
                            .ShowDialog();
                    }
                };

            // =====================================
            // EVENTOS BOTONES
            // =====================================

            btnNuevo.Click +=
                (s, e) => Limpiar();

            btnGuardar.Click +=
                (s, e) => Guardar();

            btnEliminar.Click +=
                (s, e) => Eliminar();

            btnFoto.Click +=
                (s, e) => SeleccionarFoto();

            // =====================================
            // CARGAS
            // =====================================

            CargarCategorias();

            CargarDGV();

            AplicarPermisos();
        }

        // =====================================
        // CONFIGURAR COMBOS
        // =====================================

        private void ConfigurarCombos()
        {
            cboDif.Items.AddRange(
                new[]
                {
                    "Fácil",
                    "Media",
                    "Difícil"
                });

            cboDif.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboCat.DropDownStyle =
                ComboBoxStyle.DropDownList;
        }

        // =====================================
        // PERMISOS
        // =====================================
        private void AplicarPermisos()
        {
            if (_usuario == null)
            {
                MessageBox.Show(
                    "Usuario no recibido.");
                return;
            }

            if (_usuario.Rol == "consultor")
            {
                // Oculta todo el formulario de edición
                panel.Panel2Collapsed = true;

                // Seguridad adicional
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = false;
                btnFoto.Enabled = false;
            }

            if (_usuario.Rol == "operador")
            {
                btnEliminar.Enabled = false;
                btnEliminar.Visible = false;
            }
        }

        // =====================================
        // CONFIGURAR DGV
        // =====================================

        private void ConfigurarDGV()
        {
            dgv.Dock = DockStyle.Fill;

            dgv.ReadOnly = true;

            dgv.SelectionMode =
                DataGridViewSelectionMode
                    .FullRowSelect;

            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode
                    .Fill;

            dgv.AllowUserToAddRows = false;

            dgv.BackgroundColor =
                Color.White;
        }

        // =====================================
        // PANEL FORMULARIO
        // =====================================

        private Panel PanelFormulario()
        {
            var p =
                new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = Color.Beige
                };

            int y = 40;

            void Fila(
                string etiq,
                Control ctrl)
            {
                p.Controls.Add(
                    new Label
                    {
                        Text = etiq,
                        Location =
                            new Point(5, y + 3),
                        AutoSize = true,
                        Font =
                            new Font(
                                "Segoe UI",
                                10,
                                FontStyle.Bold)
                    });

                ctrl.Location =
                    new Point(120, y);

                p.Controls.Add(ctrl);

                y += ctrl.Height + 12;
            }

            Fila("Categoría:", cboCat);
            Fila("Nombre:", txtNombre);
            Fila("Descripción:", txtDesc);
            Fila("Tiempo:", txtTiempo);
            Fila("Porciones:", txtPorc);
            Fila("Dificultad:", cboDif);

            picFoto.Location =
                new Point(120, y);

            btnFoto.Location =
                new Point(250, y + 40);

            btnFoto.BackColor =
                Color.SandyBrown;

            btnFoto.ForeColor =
                Color.White;

            btnFoto.FlatStyle =
                FlatStyle.Flat;

            p.Controls.AddRange(
                new Control[]
                {
                    new Label
                    {
                        Text = "Foto:",
                        Location =
                            new Point(5, y + 3),
                        AutoSize = true,
                        Font =
                            new Font(
                                "Segoe UI",
                                10,
                                FontStyle.Bold)
                    },

                    picFoto,
                    btnFoto
                });

            y += 150;

            btnNuevo.Location =
                new Point(10, y);

            btnGuardar.Location =
                new Point(100, y);

            btnEliminar.Location =
                new Point(190, y);

            btnNuevo.BackColor =
                Color.SandyBrown;

            btnGuardar.BackColor =
                Color.SandyBrown;

            btnEliminar.BackColor =
                Color.Firebrick;

            btnNuevo.ForeColor =
                Color.White;

            btnGuardar.ForeColor =
                Color.White;

            btnEliminar.ForeColor =
                Color.White;

            btnNuevo.FlatStyle =
                FlatStyle.Flat;

            btnGuardar.FlatStyle =
                FlatStyle.Flat;

            btnEliminar.FlatStyle =
                FlatStyle.Flat;

            p.Controls.AddRange(
                new Control[]
                {
                    btnNuevo,
                    btnGuardar,
                    btnEliminar
                });

            return p;
        }

        // =====================================
        // CARGAR CATEGORIAS
        // =====================================

        private void CargarCategorias()
        {
            var categorias =
                _ctrlC.ObtenerTodas();

            cboCat.DataSource =
                categorias;

            cboCat.DisplayMember =
                "Nombre";

            cboCat.ValueMember =
                "IdCategoria";

            // filtro
            cboFiltroCategoria.Items.Clear();

            cboFiltroCategoria.Items.Add(
                "Todas");

            foreach (var c in categorias)
            {
                cboFiltroCategoria.Items.Add(
                    c.Nombre);
            }

            cboFiltroCategoria.SelectedIndex = 0;
        }

        // =====================================
        // CARGAR DGV
        // =====================================

        private void CargarDGV()
        {
            dgv.DataSource = null;

            var lista =
                _ctrlR.ObtenerTodas();

            dgv.DataSource = lista;

            lblTotal.Text =
                $"Total registros: {lista.Count}";
        }

        // =====================================
        // BUSCAR
        // =====================================

        private void Buscar()
        {
            var lista =
                _ctrlR.ObtenerTodas();

            // texto
            if (!string.IsNullOrWhiteSpace(
                txtBuscar.Text))
            {
                lista =
                    lista.Where(r =>
                        r.Nombre
                            .ToLower()
                            .Contains(
                                txtBuscar.Text
                                    .ToLower())
                        ||
                        r.Descripcion
                            .ToLower()
                            .Contains(
                                txtBuscar.Text
                                    .ToLower()))
                    .ToList();
            }

            // categoria
            if (cboFiltroCategoria.SelectedIndex > 0)
            {
                lista =
                    lista.Where(r =>
                        r.Categoria ==
                        cboFiltroCategoria.Text)
                    .ToList();
            }

            dgv.DataSource = null;

            dgv.DataSource = lista;

            lblTotal.Text =
                $"Total registros: {lista.Count}";
        }

        // =====================================
        // SELECCION TABLA
        // =====================================

        private void DGV_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgv.CurrentRow == null)
                return;

            if (dgv.CurrentRow.DataBoundItem
                == null)
                return;

            var r =
                (Receta)dgv
                    .CurrentRow
                    .DataBoundItem;

            _idSeleccion =
                r.IdReceta;

            txtNombre.Text =
                r.Nombre;

            txtDesc.Text =
                r.Descripcion;

            txtTiempo.Text =
                r.TiempoPrepMin
                    .ToString();

            txtPorc.Text =
                r.Porciones
                    .ToString();

            cboDif.Text =
                r.Dificultad;

            cboCat.SelectedValue =
                r.IdCategoria;

            _fotoBytes =
                r.Foto;

            if (r.Foto != null)
            {
                picFoto.Image =
                    Image.FromStream(
                        new MemoryStream(
                            r.Foto));
            }
            else
            {
                picFoto.Image = null;
            }


        }

        // =====================================
        // FOTO
        // =====================================

        private void SeleccionarFoto()
        {
            using var ofd =
                new OpenFileDialog
                {
                    Filter =
                        "Imágenes|*.jpg;*.jpeg;*.png;*.bmp"
                };

            if (ofd.ShowDialog()
                == DialogResult.OK)
            {
                _fotoBytes =
                    File.ReadAllBytes(
                        ofd.FileName);

                picFoto.Image =
                    Image.FromFile(
                        ofd.FileName);
            }
        }

        // =====================================
        // GUARDAR
        // =====================================

        private void Guardar()
        {
            if (!int.TryParse(
                txtTiempo.Text,
                out int t))
            {
                t = 0;
            }

            if (!int.TryParse(
                txtPorc.Text,
                out int p))
            {
                p = 0;
            }

            var r =
                new Receta
                {
                    IdReceta =
                        _idSeleccion,

                    IdCategoria =
                        (int)(
                            cboCat.SelectedValue
                            ?? 0),

                    Nombre =
                        txtNombre.Text.Trim(),

                    Descripcion =
                        txtDesc.Text.Trim(),

                    TiempoPrepMin = t,

                    Porciones = p,

                    Dificultad =
                        cboDif.Text,

                    Foto =
                        _fotoBytes
                };

            _ctrlR.Guardar(r);

            _pasosDAL.EliminarPorReceta(r.IdReceta);
            for (int i = 0; i < lstPasos.Items.Count; i++)
            {
                _pasosDAL.Insertar(
                    r.IdReceta,
                    i + 1,
                    lstPasos.Items[i].ToString());
            }

            CargarDGV();

            Limpiar();

            MessageBox.Show(
                "Receta guardada correctamente.");
        }

        // =====================================
        // ELIMINAR
        // =====================================

        private void Eliminar()
        {
            if (_idSeleccion == 0)
                return;

            if (MessageBox.Show(
                "¿Eliminar receta?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                _ctrlR.Eliminar(
                    _idSeleccion);

                CargarDGV();

                Limpiar();

                MessageBox.Show(
                    "Receta eliminada.");
            }
        }

        // =====================================
        // LIMPIAR
        // =====================================

        private void Limpiar()
        {
            _idSeleccion = 0;

            _fotoBytes = null;

            txtNombre.Clear();

            txtDesc.Clear();

            txtTiempo.Clear();

            txtPorc.Clear();

            cboDif.SelectedIndex = -1;

            picFoto.Image = null;
        }
    }
}
