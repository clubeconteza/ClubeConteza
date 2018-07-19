 using System;
using System.IO;
using System.Net;
using System.Web.Services;
using System.Text;
using Boleto.Controller;
using Boleto.Bancos;
using Root.Reports;

namespace Boleto
{
    /// <summary>
    /// Descrição resumida de ClubeConteza
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class ClubeConteza : System.Web.Services.WebService
    {

        [WebMethod(Description = "Gera Boleto")]
        public ParametrosSaida GerarBoleto
        (
            int TipoCedente,
            int Banco,
            Int64 EmpresaId,
            string Emissao,
            string codTipoVencimento,
            string Vencimento,
            string codEspDocumento,
            double Valor,
            double Abatimento,
            double IOF,
            Int64 codMunicipio,
            string cidade,
            string uf,
            string cep,
            string endereco,
            string bairro,
            string nomeSacado,
            string cpfCGC,
            Int64 Cobranca,
            string Instrucao1,
            string Instrucao2,
            string Instrucao3,
            string Instrucao4,
            string Instrucao5
        )
        {
            /*Parametros de Retorno*/
            ParametrosSaida     RetornoBoleto = new ParametrosSaida();
            if(TipoCedente ==1)
            { 
                /*Empresa do Grupo*/
                if(Banco == 756)
                {
                    /*SICOOB*/
                    SicoobNegocios Sicoob_N = new SicoobNegocios();
                    BancoController Banco_F = new BancoController();

                    Banco_F.TB018_Banco = Banco;
                    Banco_F.TB018_Tipo = TipoCedente;
                    Banco_F.TB018_EmpresaId = EmpresaId;
                    //Banco_F.TB018_Agencia = "4368";
                    //Banco_F.TB018_ContaCorrente = "412481";

                    //string dataEmissao = Emissao.Year.ToString() + Emissao.Month.ToString().PadLeft(2, '0') + Emissao.Day.ToString().PadLeft(2, '0');
                    //string dataVencimentoTit = Vencimento.Year.ToString() + Vencimento.Month.ToString().PadLeft(2, '0') + Vencimento.Day.ToString().PadLeft(2, '0');
                    string valorTitulo = Convert.ToString(Valor).Replace(",", ".");
                    string valorAbatimento = Convert.ToString(Abatimento).Replace(",", ".");
                    string valorIOF = Convert.ToString(IOF).Replace(",", ".");

                    RetornoBoleto = Sicoob_N.Emissao240(Banco_F, Emissao, codTipoVencimento, Vencimento, codEspDocumento, valorTitulo, valorAbatimento, valorIOF, codMunicipio.ToString(),nomeSacado, cpfCGC.Replace(".","").Replace("-", "").Replace("/", ""), endereco, bairro, cidade, cep.Replace("-","").Replace(".", "").Replace(" ", ""), uf.ToUpper(), Cobranca.ToString(), Instrucao1, Instrucao2, Instrucao3, Instrucao4, Instrucao5);
                }
            }
            else
            {/*Empresa Terceira*/ }

         

            return RetornoBoleto;
        }


        [WebMethod(Description = "Gravar PDF Contrato")]
        public bool GravarPDFContrato(long contrato)
        {
            string vArq = @"C:\temp\" + contrato + ".pdf";

     
            Report vPdf = new Report(new PdfFormatter());
            //FontDef vDef = new FontDef(vPdf, FontDef.StandardFont.TimesRoman);
            Page vPage = new Page(vPdf);
            FontDef vDef = new FontDef(vPdf, FontDef.StandardFont.TimesRoman);
            FontProp vDrop = new FontProp(vDef, 10);


   
            vPage.AddCB_MM(5, new RepString(vDrop, "Teste de criação pdf")); // Centraliza 
            vPdf.Save(vArq);

            return true;
        }

    }

  
}

