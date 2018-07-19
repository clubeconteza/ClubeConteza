using Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ContezaAdmin.Atendimento
{
    public partial class frmContezinoContrato : Form
    {
        public int Aceite               { get; set; }
        public string vNomeCompleto     { get; set; }
        public string vCPF              { get; set; }
        public string vRG               { get; set; }
        public string vOrgaoEmissor     { get; set; }
        public string vCidade           { get; set; }
        public string vDia              { get; set; }
        public string vMes              { get; set; }
        public string vAno              { get; set; }
        //public long  vTB012_id          { get; set; }

        public frmContezinoContrato(string NomeCompleto, string CPF, string RG, string OrgaoEmissor, string Cidade, string Dia, string Mes, string Ano)
        {
            InitializeComponent();
            vNomeCompleto   = NomeCompleto;
            vCPF            = CPF;
            vAno            = Ano;
            vRG             = RG;
            vOrgaoEmissor   = OrgaoEmissor;
            vCidade         = Cidade;
            vMes            = Mes;
            vDia            = Dia;

        }

        private void frmContezinoContrato_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "ContezaAdmin.RPT.RPT0001.rdlc";
            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[8];

            p[0] = new Microsoft.Reporting.WinForms.ReportParameter("PrNomeContezino", vNomeCompleto);
            p[1] = new Microsoft.Reporting.WinForms.ReportParameter("PrCPF", vCPF);
            p[2] = new Microsoft.Reporting.WinForms.ReportParameter("PrRG", vRG);
            p[3] = new Microsoft.Reporting.WinForms.ReportParameter("PrOrgaoEmissor", vOrgaoEmissor);
            p[4] = new Microsoft.Reporting.WinForms.ReportParameter("PrCidade", vCidade);
            p[5] = new Microsoft.Reporting.WinForms.ReportParameter("PrDia", vDia);
            p[6] = new Microsoft.Reporting.WinForms.ReportParameter("PrMes", vMes);
            p[7] = new Microsoft.Reporting.WinForms.ReportParameter("PrAno", vAno);
           
            reportViewer1.LocalReport.SetParameters(p);
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();
            //this.reportViewer1.RefreshReport();


            

        }


        private void chkAceiteContrato_Click(object sender, EventArgs e)
        {
            if(chkAceiteContrato.Checked==true)
            {
                Aceite = 1;
            }
            else
            {
                Aceite = 0;
            }
        }

   


        private void btnFechar_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
