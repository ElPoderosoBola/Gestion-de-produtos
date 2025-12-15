namespace WinFormsApp1
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code


        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            txtNombre = new TextBox();
            txtPrecio = new TextBox();
            txtBusqueda = new TextBox();
            lblNombre = new Label();
            lblPrecio = new Label();
            label1 = new Label();
            btnInsertar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnBuscar = new Button();
            btnAlfabetico = new Button();
            btnPrecio = new Button();
            lblOrden = new Label();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 84);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(503, 243);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(653, 185);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(125, 27);
            txtNombre.TabIndex = 2;
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(653, 300);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(125, 27);
            txtPrecio.TabIndex = 3;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(653, 424);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(125, 27);
            txtBusqueda.TabIndex = 4;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(556, 192);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(67, 20);
            lblNombre.TabIndex = 6;
            lblNombre.Text = "Nombre:";
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(556, 307);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(53, 20);
            lblPrecio.TabIndex = 7;
            lblPrecio.Text = "Precio:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(524, 431);
            label1.Name = "label1";
            label1.Size = new Size(99, 20);
            label1.TabIndex = 8;
            label1.Text = "Buscar por Id:";
            // 
            // btnInsertar
            // 
            btnInsertar.Location = new Point(12, 365);
            btnInsertar.Name = "btnInsertar";
            btnInsertar.Size = new Size(94, 29);
            btnInsertar.TabIndex = 9;
            btnInsertar.Text = "Insertar";
            btnInsertar.UseVisualStyleBackColor = true;
            btnInsertar.Click += btnInsertar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(12, 422);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 10;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(12, 475);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(653, 475);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 12;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnAlfabetico
            // 
            btnAlfabetico.Location = new Point(328, 411);
            btnAlfabetico.Name = "btnAlfabetico";
            btnAlfabetico.Size = new Size(94, 29);
            btnAlfabetico.TabIndex = 13;
            btnAlfabetico.Text = "Alfabético";
            btnAlfabetico.UseVisualStyleBackColor = true;
            btnAlfabetico.Click += btnAlfabetico_Click;
            // 
            // btnPrecio
            // 
            btnPrecio.Location = new Point(328, 475);
            btnPrecio.Name = "btnPrecio";
            btnPrecio.Size = new Size(94, 29);
            btnPrecio.TabIndex = 14;
            btnPrecio.Text = "Barato";
            btnPrecio.UseVisualStyleBackColor = true;
            btnPrecio.Click += btnPrecio_Click;
            // 
            // lblOrden
            // 
            lblOrden.AutoSize = true;
            lblOrden.Location = new Point(227, 442);
            lblOrden.Name = "lblOrden";
            lblOrden.Size = new Size(93, 20);
            lblOrden.TabIndex = 15;
            lblOrden.Text = "Ordenar por:";
            // 
            // btnAnterior
            // 
            btnAnterior.Location = new Point(175, 347);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(94, 29);
            btnAnterior.TabIndex = 16;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(312, 347);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(94, 29);
            btnSiguiente.TabIndex = 17;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 680);
            Controls.Add(btnSiguiente);
            Controls.Add(btnAnterior);
            Controls.Add(lblOrden);
            Controls.Add(btnPrecio);
            Controls.Add(btnAlfabetico);
            Controls.Add(btnBuscar);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnInsertar);
            Controls.Add(label1);
            Controls.Add(lblPrecio);
            Controls.Add(lblNombre);
            Controls.Add(txtBusqueda);
            Controls.Add(txtPrecio);
            Controls.Add(txtNombre);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Gestión de Productos - Maestro/Detalle";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView1;
        private TextBox txtNombre;
        private TextBox txtPrecio;
        private TextBox txtBusqueda;
        private Label lblNombre;
        private Label lblPrecio;
        private Label label1;
        private Button btnInsertar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBuscar;
        private Button btnAlfabetico;
        private Button btnPrecio;
        private Label lblOrden;
        private Button btnAnterior;
        private Button btnSiguiente;
    }
}
