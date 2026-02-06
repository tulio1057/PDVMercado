namespace PDVMercado
{
    partial class CaixaForm
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
            splitContainer1 = new SplitContainer();
            dgvProdutos = new DataGridView();
            txtPesquisa = new TextBox();
            dgvItens = new DataGridView();
            lblTotal = new Label();
            lblQtdItens = new Label();
            panel1 = new Panel();
            txtCodigoBarras = new TextBox();
            menuStrip1 = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            sairToolStripMenuItem = new ToolStripMenuItem();
            logoutToolStripMenuItem = new ToolStripMenuItem();
            vendaToolStripMenuItem = new ToolStripMenuItem();
            novaToolStripMenuItem = new ToolStripMenuItem();
            finalizarToolStripMenuItem = new ToolStripMenuItem();
            cancelarToolStripMenuItem = new ToolStripMenuItem();
            relatóriosToolStripMenuItem = new ToolStripMenuItem();
            ajudaToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvItens).BeginInit();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 28);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(txtPesquisa);
            splitContainer1.Panel1.Controls.Add(dgvProdutos);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(dgvItens);
            splitContainer1.Size = new Size(800, 422);
            splitContainer1.SplitterDistance = 466;
            splitContainer1.TabIndex = 0;
            // 
            // dgvProdutos
            // 
            dgvProdutos.AllowUserToAddRows = false;
            dgvProdutos.AllowUserToDeleteRows = false;
            dgvProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProdutos.Dock = DockStyle.Fill;
            dgvProdutos.Location = new Point(0, 0);
            dgvProdutos.Name = "dgvProdutos";
            dgvProdutos.ReadOnly = true;
            dgvProdutos.RowHeadersWidth = 51;
            dgvProdutos.Size = new Size(466, 422);
            dgvProdutos.TabIndex = 0;
            // 
            // txtPesquisa
            // 
            txtPesquisa.Dock = DockStyle.Top;
            txtPesquisa.Location = new Point(0, 0);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.PlaceholderText = "Pesquisar produto...";
            txtPesquisa.Size = new Size(466, 27);
            txtPesquisa.TabIndex = 1;
            // 
            // dgvItens
            // 
            dgvItens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItens.Dock = DockStyle.Fill;
            dgvItens.Location = new Point(0, 0);
            dgvItens.Name = "dgvItens";
            dgvItens.RowHeadersWidth = 51;
            dgvItens.Size = new Size(330, 422);
            dgvItens.TabIndex = 0;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(22, 83);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(97, 20);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "Total: R$ 0,00";
            // 
            // lblQtdItens
            // 
            lblQtdItens.AutoSize = true;
            lblQtdItens.Location = new Point(22, 53);
            lblQtdItens.Name = "lblQtdItens";
            lblQtdItens.Size = new Size(55, 20);
            lblQtdItens.TabIndex = 1;
            lblQtdItens.Text = "Itens: 0";
            // 
            // panel1
            // 
            panel1.Controls.Add(txtCodigoBarras);
            panel1.Controls.Add(lblTotal);
            panel1.Controls.Add(lblQtdItens);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 272);
            panel1.Name = "panel1";
            panel1.Size = new Size(330, 150);
            panel1.TabIndex = 1;
            // 
            // txtCodigoBarras
            // 
            txtCodigoBarras.Dock = DockStyle.Top;
            txtCodigoBarras.Location = new Point(0, 0);
            txtCodigoBarras.Name = "txtCodigoBarras";
            txtCodigoBarras.PlaceholderText = "Código de barras...";
            txtCodigoBarras.Size = new Size(330, 27);
            txtCodigoBarras.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem, vendaToolStripMenuItem, relatóriosToolStripMenuItem, ajudaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sairToolStripMenuItem, logoutToolStripMenuItem });
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            arquivoToolStripMenuItem.Size = new Size(79, 24);
            arquivoToolStripMenuItem.Text = "Arquivo ";
            // 
            // sairToolStripMenuItem
            // 
            sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            sairToolStripMenuItem.Size = new Size(224, 26);
            sairToolStripMenuItem.Text = "Sair";
            // 
            // logoutToolStripMenuItem
            // 
            logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            logoutToolStripMenuItem.Size = new Size(224, 26);
            logoutToolStripMenuItem.Text = "Logout";
            // 
            // vendaToolStripMenuItem
            // 
            vendaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { novaToolStripMenuItem, finalizarToolStripMenuItem, cancelarToolStripMenuItem });
            vendaToolStripMenuItem.Name = "vendaToolStripMenuItem";
            vendaToolStripMenuItem.Size = new Size(64, 24);
            vendaToolStripMenuItem.Text = "Venda";
            // 
            // novaToolStripMenuItem
            // 
            novaToolStripMenuItem.Name = "novaToolStripMenuItem";
            novaToolStripMenuItem.Size = new Size(224, 26);
            novaToolStripMenuItem.Text = "Nova";
            // 
            // finalizarToolStripMenuItem
            // 
            finalizarToolStripMenuItem.Name = "finalizarToolStripMenuItem";
            finalizarToolStripMenuItem.Size = new Size(224, 26);
            finalizarToolStripMenuItem.Text = "Finalizar";
            // 
            // cancelarToolStripMenuItem
            // 
            cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            cancelarToolStripMenuItem.Size = new Size(224, 26);
            cancelarToolStripMenuItem.Text = "Cancelar";
            // 
            // relatóriosToolStripMenuItem
            // 
            relatóriosToolStripMenuItem.Name = "relatóriosToolStripMenuItem";
            relatóriosToolStripMenuItem.Size = new Size(90, 24);
            relatóriosToolStripMenuItem.Text = "Relatórios";
            // 
            // ajudaToolStripMenuItem
            // 
            ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            ajudaToolStripMenuItem.Size = new Size(62, 24);
            ajudaToolStripMenuItem.Text = "Ajuda";
            // 
            // CaixaForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "CaixaForm";
            Text = "Caixa";
            WindowState = FormWindowState.Maximized;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvItens).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private TextBox txtPesquisa;
        private DataGridView dgvProdutos;
        private DataGridView dgvItens;
        private Panel panel1;
        private TextBox txtCodigoBarras;
        private Label lblTotal;
        private Label lblQtdItens;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem sairToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem vendaToolStripMenuItem;
        private ToolStripMenuItem novaToolStripMenuItem;
        private ToolStripMenuItem finalizarToolStripMenuItem;
        private ToolStripMenuItem cancelarToolStripMenuItem;
        private ToolStripMenuItem relatóriosToolStripMenuItem;
        private ToolStripMenuItem ajudaToolStripMenuItem;
    }
}