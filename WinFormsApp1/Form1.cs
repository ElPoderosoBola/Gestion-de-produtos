using ClassLibrary1;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private AccesoDatos gestorDB;
        private int paginaActual = 1;
        private const int ProductosPorPagina = 4;
        private string ordenActual = "Nombre";
        private string filtroActual = "";

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                gestorDB = new AccesoDatos();
                await CargarPagina();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
                Application.Exit();
            }
        }

        private async Task CargarPagina()
        {
            try
            {
                var productos = await gestorDB.ListarProductos(ordenActual, paginaActual, ProductosPorPagina, filtroActual);
                dataGridView1.DataSource = productos;

                if (dataGridView1.Columns.Contains("Precio"))
                {
                    dataGridView1.Columns["Precio"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }

        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            var siguiente = await gestorDB.ListarProductos(ordenActual, paginaActual + 1, ProductosPorPagina, filtroActual);
            if (siguiente != null && siguiente.Count > 0)
            {
                paginaActual++;
                await CargarPagina();
            }
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                await CargarPagina();
            }
        }

        private async void btnAlfabetico_Click(object sender, EventArgs e)
        {
            ordenActual = "Nombre";
            paginaActual = 1;
            await CargarPagina();
        }

        private async void btnPrecio_Click(object sender, EventArgs e)
        {
            ordenActual = "Precio";
            paginaActual = 1;
            await CargarPagina();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtBusqueda.Text, out int id))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["ID"].Value != null && (int)row.Cells["ID"].Value == id)
                    {
                        row.Selected = true;
                        dataGridView1.CurrentCell = row.Cells[0];
                        return;
                    }
                }
            }
        }

        private async void btnInsertar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Debes rellenar nombre y precio");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Precio no válido");
                return;
            }

            try
            {
                Producto nuevo = new Producto
                {
                    Nombre = txtNombre.Text,
                    Precio = precio
                };

                await gestorDB.InsertarProducto(nuevo);
                txtNombre.Clear();
                txtPrecio.Clear();
                await CargarPagina();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar: " + ex.Message);
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un artículo para editar");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Debes rellenar nombre y precio");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Precio no válido");
                return;
            }

            try
            {
                var fila = dataGridView1.SelectedRows[0];
                int id = (int)fila.Cells["ID"].Value;

                Producto modificado = new Producto
                {
                    ID = id,
                    Nombre = txtNombre.Text,
                    Precio = precio
                };

                await gestorDB.ActualizarProducto(modificado);
                await CargarPagina();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar: " + ex.Message);
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un artículo para eliminar");
                return;
            }

            try
            {
                var fila = dataGridView1.SelectedRows[0];
                int id = (int)fila.Cells["ID"].Value;

                await gestorDB.BorrarProducto(id);
                MessageBox.Show("Artículo eliminado");
                await CargarPagina();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = dataGridView1.Rows[e.RowIndex];
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                txtPrecio.Text = fila.Cells["Precio"].Value?.ToString();
            }
        }
    }
}

