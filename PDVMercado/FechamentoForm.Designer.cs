namespace PDVMercado
{
    partial class FechamentoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            panel2 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label2 = new Label();
            lblTotalVenda = new Label();
            label3 = new Label();
            cmbFormaPagamento = new ComboBox();
            label4 = new Label();
            txtValorPago = new TextBox();
            label5 = new Label();
            lblTroco = new Label();
            panel1 = new Panel();
            dgvResumo = new DataGridView();
            label1 = new Label();
            lblTitulo = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResumo).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(panel2, 0, 2);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(lblTitulo, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 57.1428566F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 42.8571434F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(482, 553);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(tableLayoutPanel2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 252);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(20, 10, 20, 10);
            panel2.Size = new Size(479, 125);
            panel2.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(lblTotalVenda, 1, 0);
            tableLayoutPanel2.Controls.Add(label3, 0, 1);
            tableLayoutPanel2.Controls.Add(cmbFormaPagamento, 1, 1);
            tableLayoutPanel2.Controls.Add(label4, 0, 2);
            tableLayoutPanel2.Controls.Add(txtValorPago, 1, 2);
            tableLayoutPanel2.Controls.Add(label5, 0, 3);
            tableLayoutPanel2.Controls.Add(lblTroco, 1, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(20, 10);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(439, 105);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 8.25F);
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(109, 17);
            label2.TabIndex = 0;
            label2.Text = "Total da Venda:";
            // 
            // lblTotalVenda
            // 
            lblTotalVenda.AutoSize = true;
            lblTotalVenda.Font = new Font("Microsoft Sans Serif", 8.25F);
            lblTotalVenda.ImageAlign = ContentAlignment.MiddleRight;
            lblTotalVenda.Location = new Point(157, 0);
            lblTotalVenda.Name = "lblTotalVenda";
            lblTotalVenda.Size = new Size(58, 17);
            lblTotalVenda.TabIndex = 1;
            lblTotalVenda.Text = "R$ 0,00";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 8.25F);
            label3.Location = new Point(3, 32);
            label3.Name = "label3";
            label3.Size = new Size(148, 17);
            label3.TabIndex = 2;
            label3.Text = "Forma de Pagamento:";
            // 
            // cmbFormaPagamento
            // 
            cmbFormaPagamento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFormaPagamento.Font = new Font("Microsoft Sans Serif", 8.25F);
            cmbFormaPagamento.FormattingEnabled = true;
            cmbFormaPagamento.Location = new Point(157, 35);
            cmbFormaPagamento.Name = "cmbFormaPagamento";
            cmbFormaPagamento.Size = new Size(151, 25);
            cmbFormaPagamento.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 8.25F);
            label4.Location = new Point(3, 64);
            label4.Name = "label4";
            label4.Size = new Size(82, 17);
            label4.TabIndex = 4;
            label4.Text = "Valor Pago:";
            // 
            // txtValorPago
            // 
            txtValorPago.Font = new Font("Microsoft Sans Serif", 8.25F);
            txtValorPago.Location = new Point(157, 67);
            txtValorPago.Name = "txtValorPago";
            txtValorPago.Size = new Size(125, 23);
            txtValorPago.TabIndex = 5;
            txtValorPago.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 8.25F);
            label5.Location = new Point(3, 84);
            label5.Name = "label5";
            label5.Size = new Size(49, 17);
            label5.TabIndex = 6;
            label5.Text = "Troco:";
            // 
            // lblTroco
            // 
            lblTroco.AutoSize = true;
            lblTroco.Font = new Font("Microsoft Sans Serif", 8.25F);
            lblTroco.Location = new Point(157, 84);
            lblTroco.Name = "lblTroco";
            lblTroco.Size = new Size(58, 17);
            lblTroco.TabIndex = 7;
            lblTroco.Text = "R$ 0,00";
            lblTroco.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(dgvResumo);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 23);
            panel1.Name = "panel1";
            panel1.Size = new Size(479, 223);
            panel1.TabIndex = 1;
            // 
            // dgvResumo
            // 
            dgvResumo.AllowUserToAddRows = false;
            dgvResumo.AllowUserToDeleteRows = false;
            dgvResumo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResumo.BackgroundColor = Color.White;
            dgvResumo.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvResumo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResumo.Dock = DockStyle.Fill;
            dgvResumo.Location = new Point(0, 27);
            dgvResumo.Name = "dgvResumo";
            dgvResumo.ReadOnly = true;
            dgvResumo.RowHeadersVisible = false;
            dgvResumo.RowHeadersWidth = 51;
            dgvResumo.Size = new Size(477, 194);
            dgvResumo.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Microsoft Sans Serif", 8.25F);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Padding = new Padding(5, 5, 0, 5);
            label1.Size = new Size(167, 27);
            label1.TabIndex = 0;
            label1.Text = "📋 RESUMO DA VENDA";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Location = new Point(3, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(479, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "💰 FINALIZAR VENDA";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnConfirmar);
            flowLayoutPanel1.Controls.Add(btnCancelar);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(3, 383);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(0, 20, 20, 20);
            flowLayoutPanel1.Size = new Size(479, 166);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Enabled = false;
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.Font = new Font("Microsoft Sans Serif", 8.25F);
            btnConfirmar.Location = new Point(276, 23);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(180, 50);
            btnConfirmar.TabIndex = 0;
            btnConfirmar.Text = "✅ CONFIRMAR VENDA";
            btnConfirmar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Microsoft Sans Serif", 8.25F);
            btnCancelar.Location = new Point(150, 23);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 50);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "❌ CANCELAR";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FechamentoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 553);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FechamentoForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Fechamento de Venda";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResumo).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblTitulo;
        private Panel panel1;
        private DataGridView dgvResumo;
        private Label label1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private Label lblTotalVenda;
        private Label label3;
        private ComboBox cmbFormaPagamento;
        private Label label4;
        private TextBox txtValorPago;
        private Label label5;
        private Label lblTroco;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnConfirmar;
        private Button btnCancelar;
    }
}