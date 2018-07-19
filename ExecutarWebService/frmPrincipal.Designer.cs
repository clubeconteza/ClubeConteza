namespace ExecutarWebService
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbxCartaoParaImpressaoFamiliar = new System.Windows.Forms.CheckBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.trmFinalizar = new System.Windows.Forms.Timer(this.components);
            this.cbxCartaoParaImpressaoCorporativo = new System.Windows.Forms.CheckBox();
            this.cbxEnviarSMS = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbxCartaoParaImpressaoFamiliar
            // 
            this.cbxCartaoParaImpressaoFamiliar.AutoSize = true;
            this.cbxCartaoParaImpressaoFamiliar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxCartaoParaImpressaoFamiliar.Location = new System.Drawing.Point(35, 29);
            this.cbxCartaoParaImpressaoFamiliar.Name = "cbxCartaoParaImpressaoFamiliar";
            this.cbxCartaoParaImpressaoFamiliar.Size = new System.Drawing.Size(162, 17);
            this.cbxCartaoParaImpressaoFamiliar.TabIndex = 0;
            this.cbxCartaoParaImpressaoFamiliar.Text = "CartaoParaImpressaoFamiliar";
            this.cbxCartaoParaImpressaoFamiliar.UseVisualStyleBackColor = true;
            this.cbxCartaoParaImpressaoFamiliar.MouseLeave += new System.EventHandler(this.cbxCartaoParaImpressao_MouseLeave);
            this.cbxCartaoParaImpressaoFamiliar.MouseHover += new System.EventHandler(this.cbxCartaoParaImpressao_MouseHover);
            // 
            // lblDescricao
            // 
            this.lblDescricao.Location = new System.Drawing.Point(528, 30);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(301, 288);
            this.lblDescricao.TabIndex = 1;
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(35, 295);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 2;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // trmFinalizar
            // 
            this.trmFinalizar.Interval = 9000;
            this.trmFinalizar.Tick += new System.EventHandler(this.trmFinalizar_Tick);
            // 
            // cbxCartaoParaImpressaoCorporativo
            // 
            this.cbxCartaoParaImpressaoCorporativo.AutoSize = true;
            this.cbxCartaoParaImpressaoCorporativo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxCartaoParaImpressaoCorporativo.Location = new System.Drawing.Point(35, 63);
            this.cbxCartaoParaImpressaoCorporativo.Name = "cbxCartaoParaImpressaoCorporativo";
            this.cbxCartaoParaImpressaoCorporativo.Size = new System.Drawing.Size(181, 17);
            this.cbxCartaoParaImpressaoCorporativo.TabIndex = 3;
            this.cbxCartaoParaImpressaoCorporativo.Text = "CartaoParaImpressaoCorporativo";
            this.cbxCartaoParaImpressaoCorporativo.UseVisualStyleBackColor = true;
            this.cbxCartaoParaImpressaoCorporativo.MouseLeave += new System.EventHandler(this.cbxCartaoParaImpressaoCorporativo_MouseLeave);
            this.cbxCartaoParaImpressaoCorporativo.MouseHover += new System.EventHandler(this.cbxCartaoParaImpressaoCorporativo_MouseHover);
            // 
            // cbxEnviarSMS
            // 
            this.cbxEnviarSMS.AutoSize = true;
            this.cbxEnviarSMS.Checked = true;
            this.cbxEnviarSMS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxEnviarSMS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxEnviarSMS.Location = new System.Drawing.Point(35, 98);
            this.cbxEnviarSMS.Name = "cbxEnviarSMS";
            this.cbxEnviarSMS.Size = new System.Drawing.Size(194, 17);
            this.cbxEnviarSMS.TabIndex = 4;
            this.cbxEnviarSMS.Text = "Envio SMS agendado internamente";
            this.cbxEnviarSMS.UseVisualStyleBackColor = true;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 327);
            this.Controls.Add(this.cbxEnviarSMS);
            this.Controls.Add(this.cbxCartaoParaImpressaoCorporativo);
            this.Controls.Add(this.btnExecutar);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.cbxCartaoParaImpressaoFamiliar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPrincipal";
            this.Text = "Executar WebService";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxCartaoParaImpressaoFamiliar;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.Timer trmFinalizar;
        private System.Windows.Forms.CheckBox cbxCartaoParaImpressaoCorporativo;
        private System.Windows.Forms.CheckBox cbxEnviarSMS;
    }
}

