using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PDVMercado.Forms
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
            
            // Limpar recursos customizados
            if (disposing)
            {
                if (timerRelogio != null)
                {
                    timerRelogio.Stop();
                    timerRelogio.Dispose();
                }
            }
            
            base.Dispose(disposing);
        }
    }
}
