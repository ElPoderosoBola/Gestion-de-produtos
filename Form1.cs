csharp WinFormsApp1\Form1.cs
using ClassLibrary1;
using System.Diagnostics;
using System.ComponentModel;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private AccesoDatos gestorDB;
        private Producto productoActual;

        private int paginaActual = 1;
        private string ordenActual = "Nombre";
        private string filtroActual = "";
        private bool estaEnModoNuevo = false;

        private List<Producto> listaProductosMaestro = new List<Producto>();
        private int paginaMaestro = 1;
        private const int ProductosPorPagina = 3;

        // Grid paging
        private int gridPagina = 1;
        private const int GridTamanoPagina = 10;

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                gestorDB = new AccesoDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            await CargarListaMaestra();
            await CargarProductoPaginado();
            await CargarGridPagina();
        }

        private async Task CargarListaMaestra()
        {
            try
            {
                listaProductosMaestro = await gestorDB.ListarProductos(
                        ordenActual,
                        paginaMaestro,
                        ProductosPorPagina,
                        filtroActual
                        );

                MostrarProductosMaestro();
                ActualizarBotonesMaestro();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista maestra: " + ex.Message);
            }
        }

        private void MostrarProductosMaestro()
        {
            if (listaProductosMaestro.Count > 0)
            {
                lblProducto1.Text = listaProductosMaestro[0].Nombre + " - " + listaProductosMaestro[0].Precio.ToString("C");
                lblProducto1.Tag = listaProductosMaestro[0];
                lblProducto1.Visible = true;
            }
            else
            {
                lblProducto1.Text = "(Vacío)";
                lblProducto1.Tag = null;
                lblProducto1.Visible = false;
            }

            if (listaProductosMaestro.Count > 1)
            {
                lblProducto2.Text = listaProductosMaestro[1].Nombre + " - " + listaProductosMaestro[1].Precio.ToString("C");
                lblProducto2.Tag = listaProductosMaestro[1];
                lblProducto2.Visible = true;
            }
            else
            {
                lblProducto2.Text = "(Vacío)";
                lblProducto2.Tag = null;
                lblProducto2.Visible = false;
            }

            if (listaProductosMaestro.Count > 2)
            {
                lblProducto3.Text = listaProductosMaestro[2].Nombre + " - " + listaProductosMaestro[2].Precio.ToString("C");
                lblProducto3.Tag = listaProductosMaestro[2];
                lblProducto3.Visible = true;
            }
            else
            {
                lblProducto3.Text = "(Vacío)";
                lblProducto3.Tag = null;
                lblProducto3.Visible = false;
            }
        }

        private void ActualizarBotonesMaestro()
        {
            lblPaginaMaestro.Text = "Página " + paginaMaestro.ToString();
        }

        private void lblProducto1_Click(object sender, EventArgs e)
        {
            if (lblProducto1.Tag is Producto producto)
            {
                productoActual = producto;
                MostrarProductoActual();
            }
        }

        private void lblProducto2_Click(object sender, EventArgs e)
        {
            if (lblProducto2.Tag is Producto producto)
            {
                productoActual = producto;
                MostrarProductoActual();
            }
        }

        private void lblProducto3_Click(object sender, EventArgs e)
        {
            if (lblProducto3.Tag is Producto producto)
            {
                productoActual = producto;
                MostrarProductoActual();
            }
        }

        private async void btnAnteriorMaestro_Click(object sender, EventArgs e)
        {
            if (paginaMaestro > 1)
            {
                paginaMaestro--;
                await CargarListaMaestra();
            }
        }

        private async void btnSiguienteMaestro_Click(object sender, EventArgs e)
        {
            var siguientePagina = await gestorDB.ListarProductos(
                ordenActual,
                paginaMaestro + 1,
                ProductosPorPagina,
                filtroActual
            );

            if (siguientePagina == null || siguientePagina.Count == 0)
            {
                return;
            }

            paginaMaestro++;
            await CargarListaMaestra();
        }

        private async Task CargarGridPagina()
        {
            try
            {
                var lista = await gestorDB.ListarProductos(ordenActual, gridPagina, GridTamanoPagina, filtroActual);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = new BindingList<Producto>(lista);

                lblGridPagina.Text = $"Página {gridPagina}";
                btnGridPrev.Enabled = gridPagina > 1;
                btnGridNext.Enabled = lista != null && lista.Count == GridTamanoPagina;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar grid: " + ex.Message);
            }
        }

        private async void btnGridPrev_Click(object sender, EventArgs e)
        {
            if (gridPagina > 1)
            {
                gridPagina--;
                await CargarGridPagina();
            }
        }

        private async void btnGridNext_Click(object sender, EventArgs e)
        {
            // Peek next page
            var siguiente = await gestorDB.ListarProductos(ordenActual, gridPagina + 1, GridTamanoPagina, filtroActual);
            if (siguiente == null || siguiente.Count == 0)
            {
                return;
            }

            gridPagina++;
            await CargarGridPagina();
        }

        private async Task CargarProductoPaginado()
        {
            try
            {
                int tamanoPagina = 1;
                List<Producto> lista = await gestorDB.ListarProductos(ordenActual, paginaActual, tamanoPagina, filtroActual);

                if (lista.Count > 0)
                {
                    productoActual = lista[0];
                    MostrarProductoActual();
                }
                else
                {
                    if (paginaActual > 1)
                    {
                        paginaActual--;
                    }
                    else
                    {
                        productoActual = null;
                        LimpiarFormulario();
                    }
                }

                txtPagina.Text = paginaActual.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void MostrarProductoActual()
        {
            if (productoActual == null)
            {
                LimpiarFormulario();
                return;
            }

            txtId.Text = productoActual.ID.ToString();
            txtNombre.Text = productoActual.Nombre;
            txtPrecio.Text = productoActual.Precio.ToString();
        }

        private void LimpiarFormulario(bool esModoNuevo = false)
        {
            if (esModoNuevo)
            {
                txtId.Text = "(Nuevo)";
            }
            else
            {
                txtId.Text = "";
            }
            txtNombre.Text = "";
            txtPrecio.Text = "0";

            if (esModoNuevo)
            {
                txtNombre.Focus();
            }
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!estaEnModoNuevo)
            {
                productoActual = null;
                LimpiarFormulario(esModoNuevo: true);
                estaEnModoNuevo = true;
            }
            else
            {
                await GuardarProductoNuevo();
            }
        }

        private async Task GuardarProductoNuevo()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo 'Nombre' no puede estar vacío.");
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text, out decimal precioValido) || precioValido < 0)
            {
                MessageBox.Show("El campo 'Precio' debe ser un número válido");
                return;
            }

            Producto p = new Producto();
            p.Nombre = txtNombre.Text;
            p.Precio = precioValido;

            try
            {
                await gestorDB.InsertarProducto(p);
                MessageBox.Show("Producto nuevo guardado");

                estaEnModoNuevo = false;

                paginaActual = 1;
                paginaMaestro = 1;
                gridPagina = 1;

                await CargarListaMaestra();
                await CargarProductoPaginado();
                await CargarGridPagina();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (estaEnModoNuevo || productoActual == null)
            {
                MessageBox.Show("No hay ningún producto para editar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Error");
                return;
            }
            if (!decimal.TryParse(txtPrecio.Text, out decimal precioValido) || precioValido < 0)
            {
                MessageBox.Show("Error");
                return;
            }

            Producto p = new Producto();
            p.ID = productoActual.ID;
            p.Nombre = txtNombre.Text;
            p.Precio = precioValido;

            try
            {
                MessageBox.Show("Actualizado");
                await gestorDB.ActualizarProducto(p);
                productoActual = p;
                MostrarProductoActual();

                await CargarListaMaestra();
                await CargarGridPagina();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (productoActual == null) return;

            try
            {
                await gestorDB.BorrarProducto(productoActual.ID);
                MessageBox.Show("Producto Borrado");

                await CargarListaMaestra();
                await CargarProductoPaginado();
                await CargarGridPagina();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                if (estaEnModoNuevo)
                {
                    estaEnModoNuevo = false;
                }
                paginaActual--;
                await CargarProductoPaginado();
            }
        }

        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (estaEnModoNuevo)
            {
                estaEnModoNuevo = false;
            }
            paginaActual++;
            await CargarProductoPaginado();
        }

        private async void btnPagina_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPagina.Text, out int paginaDeseada))
            {
                txtPagina.Text = paginaActual.ToString();
                return;
            }

            if (paginaDeseada < 1)
            {
                paginaDeseada = 1;
            }

            paginaActual = paginaDeseada;
            await CargarProductoPaginado();
        }

        private async void btnAlfabetico_Click(object sender, EventArgs e)
        {
            ordenActual = "Nombre";
            paginaActual = 1;
            paginaMaestro = 1;
            gridPagina = 1;
            await CargarListaMaestra();
            await CargarProductoPaginado();
            await CargarGridPagina();
        }

        private async void btnBarato_Click(object sender, EventArgs e)
        {
            ordenActual = "Precio";
            paginaActual = 1;
            paginaMaestro = 1;
            gridPagina = 1;
            await CargarListaMaestra();
            await CargarProductoPaginado();
            await CargarGridPagina();
        }

        private async void btnNombreBuscar_Click(object sender, EventArgs e)
        {
            filtroActual = txtNombreBuscar.Text.Trim();
            paginaActual = 1;
            paginaMaestro = 1;
            gridPagina = 1;
            productoActual = null;
            await CargarListaMaestra();
            await CargarProductoPaginado();
            await CargarGridPagina();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}