namespace ContezaAdmin.Atendimento
{
    partial class frmAssinatura
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
            this.mnuAssinatura = new System.Windows.Forms.MenuStrip();
            this.mnuAssinaturaCapturarImagem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picTela = new System.Windows.Forms.PictureBox();
            this.salvarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAssinatura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTela)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuAssinatura
            // 
            this.mnuAssinatura.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salvarToolStripMenuItem,
            this.mnuAssinaturaCapturarImagem,
            this.fecharToolStripMenuItem});
            this.mnuAssinatura.Location = new System.Drawing.Point(0, 0);
            this.mnuAssinatura.Name = "mnuAssinatura";
            this.mnuAssinatura.Size = new System.Drawing.Size(980, 24);
            this.mnuAssinatura.TabIndex = 0;
            this.mnuAssinatura.Text = "menuStrip1";
            // 
            // mnuAssinaturaCapturarImagem
            // 
            this.mnuAssinaturaCapturarImagem.Name = "mnuAssinaturaCapturarImagem";
            this.mnuAssinaturaCapturarImagem.Size = new System.Drawing.Size(112, 20);
            this.mnuAssinaturaCapturarImagem.Text = "Capturar Imagem";
            this.mnuAssinaturaCapturarImagem.Click += new System.EventHandler(this.mnuAssinaturaCapturarImagem_Click);
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fecharToolStripMenuItem.Text = "Fechar";
            this.fecharToolStripMenuItem.Click += new System.EventHandler(this.fecharToolStripMenuItem_Click);
            // 
            // picTela
            // 
            this.picTela.BackColor = System.Drawing.Color.White;
            this.picTela.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picTela.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picTela.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTela.Location = new System.Drawing.Point(0, 24);
            this.picTela.Name = "picTela";
            this.picTela.Size = new System.Drawing.Size(980, 568);
            this.picTela.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTela.TabIndex = 2;
            this.picTela.TabStop = false;
            this.picTela.Visible = false;
            this.picTela.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picTela_MouseDown);
            this.picTela.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picTela_MouseMove);
            this.picTela.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picTela_MouseUp);
            // 
            // salvarToolStripMenuItem
            // 
            this.salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            this.salvarToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.salvarToolStripMenuItem.Text = "Salvar";
            this.salvarToolStripMenuItem.Click += new System.EventHandler(this.salvarToolStripMenuItem_Click);
            // 
            // frmAssinatura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 592);
            this.Controls.Add(this.picTela);
            this.Controls.Add(this.mnuAssinatura);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.mnuAssinatura;
            this.Name = "frmAssinatura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAssinatura";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAssinatura_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseEvent_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseEvent_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseEvent_MouseUp);
            this.mnuAssinatura.ResumeLayout(false);
            this.mnuAssinatura.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTela)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuAssinatura;
        private System.Windows.Forms.ToolStripMenuItem mnuAssinaturaCapturarImagem;
        private System.Windows.Forms.PictureBox picTela;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarToolStripMenuItem;
    }
}