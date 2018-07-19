using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using static System.String;

namespace Negocios
{
    public class ParcelaNegocios
    {
        public short parcelasGeradasContrato(long tb012Id)
        {
            try
            {

                return new ParcelaDao().parcelasGeradasContrato(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> GerarParcelasFamiliar(short nParcelas, DateTime vencimento, int diaVencimento, long reponsavel, long contrato, int ciclo, int exportacao, int aberturacontrato, long TB037_Id, double TB037_Comissao)
        {
            try
            {
                var retorno = new ContratoFamiliarDao().ContratoFamiliarDadosParcela(contrato);
                var parcelas = new List<ParcelaController>();

                for (var i = 0; i < nParcelas; i++)
                {
                    double ComissaoAdesao = 0;
                    double ComissaoMensalidade = 0;

                    if (i > 0)
                    {
                        vencimento                                  = new DateTime(parcelas[parcelas.Count - 1].TB016_Vencimento.Year, parcelas[parcelas.Count - 1].TB016_Vencimento.Month, retorno.TB012_DiaVencimento);
                        vencimento                                  = vencimento.AddMonths(1);
                    }
                    /**/
                    var objTitular                                  = new PessoaNegocios().pessoaSelectId(retorno.Titular.TB013_id);
                    var pontoDevendaEmpresa                         = new PontoDeVendaNegocios().PontoDeVendaEmpresa(retorno.TB002_id);
                    var parcela                                     = new ParcelaController
                    {
                        Pessoa                                      = new PessoaController
                        {
                            Municipio                               = new MunicipioController { Estado = new EstadoController() }
                        }
                    };
                    var parcelaItensL                               = new List<ParcelaProdutosController>();
                    parcela.Plano                                   = new PlanoController();
                    parcela.Empresa                                 = new EmpresaController { TB001_id = pontoDevendaEmpresa.Empresa.TB001_id };
                    parcela.Titular                                 = objTitular;
                    parcela.TB033_Parcela                           = Convert.ToInt16(i) + 1;
                    parcela.TB016_TotalParcelas                     = nParcelas;
                    parcela.TB016_Emissao                           = DateTime.Now;
                    parcela.TB016_Vencimento                        = vencimento;
                    parcela.TB016_DiaVencimento                     = vencimento.Day;
                    parcela.TB016_DiaFechamento                     = vencimento.Day - 2;
                    parcela.TB016_Beneficiario                      = pontoDevendaEmpresa.Empresa.TB001_RazaoSocial;
                    parcela.TB016_BeneficiarioCidade                = pontoDevendaEmpresa.Empresa.Cidade;
                    parcela.TB016_BeneficiarioCPFCNPJ               = pontoDevendaEmpresa.Empresa.TB001_CNPJ;
                    parcela.TB016_BeneficiarioEndereco              = pontoDevendaEmpresa.Empresa.TB001_Logradouro;
                    parcela.TB016_BeneficiarioUF                    = pontoDevendaEmpresa.Empresa.TB001_UF;
                    parcela.TB016_CadastradoEm                      = DateTime.Now;
                    parcela.TB016_CadastradoPor                     = reponsavel;
                    parcela.TB016_AlteradoEm                        = DateTime.Now;
                    parcela.TB016_AlteradoPor                       = reponsavel;                   
                    parcela.Pessoa.Municipio.TB006_Municipio        = objTitular.Municipio.TB006_Municipio.TrimEnd().ToUpper();
                    parcela.Pessoa.Municipio.Estado.TB005_Estado    = objTitular.Municipio.Estado.TB005_Sigla;
                    parcela.TB016_CPFCNPJ                           = objTitular.TB013_CPFCNPJ.Replace(".", "").Replace(",", "").Replace("/", "")
                                                                    .Replace("-", "").Trim();
                    parcela.TB016_Pagador                           = objTitular.TB013_NomeCompleto.TrimEnd().ToUpper().Replace("'", "/")
                                                                    .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                    parcela.TB016_PagadorCEP                        = objTitular.TB004_Cep;
                    parcela.TB016_PagadorCidade                     = objTitular.Municipio.TB006_Municipio.TrimEnd().ToUpper().Replace("'", "/")
                                                                    .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                    parcela.TB016_PagadorUF                         = objTitular.Municipio.Estado.TB005_Sigla.ToUpper().Replace("'", "/")
                                                                    .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                    parcela.TB012_id                                = contrato;
                    parcela.TB016_EnderecoPagador                   = objTitular.TB013_Logradouro.TrimEnd().ToUpper() + ", " +
                                                                    objTitular.TB013_Numero.TrimEnd().ToUpper();
                    parcela.TB016_EnderecoPagador                   = parcela.TB016_EnderecoPagador.Replace("'", "/").Replace("*", "")
                                                                    .Replace("%", "/").Replace("&", "E").Replace("#", "");
                    parcela.TB012_CicloContrato                     = ciclo;
                    parcela.TB016_FormaPagamentoS                   = "1";
                    parcela.TB016_EmitirBoleto                      = 1;
                    parcela.TB016_StatusS                           = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(0));
                    var participantesL =
                        new PessoaNegocios().MembrosAtivosDoConrato(Convert.ToInt64(contrato),
                            parcela.TB016_Vencimento);
                    var dadosFiltroIdade =
                        new CategoriaIdadeNegocios().DistribuicaoIsencaoIdade(participantesL);
                    //Localiar Plano's conforme itens de filtro
                    var filtro = new PlanoController();
                    var objPontoDeVenda             = new PontoDeVendaController();
                    filtro.PontoDeVenda             = objPontoDeVenda;
                    filtro.TB015_Maiores            = dadosFiltroIdade.Maior;
                    filtro.TB015_Menores            = dadosFiltroIdade.Menor;
                    filtro.TB015_Isentos            = dadosFiltroIdade.Isento;
                    filtro.PontoDeVenda.TB002_id    = retorno.TB002_id;

                    var plano = new PlanoNegocios().PlanoVendaContezino(filtro, 1, 0,1);

                    parcela.Plano.TB015_Maiores     = filtro.TB015_Maiores;
                    parcela.Plano.TB015_Menores     = filtro.TB015_Menores;
                    parcela.Plano.TB015_Isentos     = filtro.TB015_Isentos;

                    parcela.Plano.TB015_IOF         = Convert.ToDouble(plano.Tables[0].Rows[0]["TB015_IOF"].ToString());
                    parcela.TB016_TipoVencimento    = Convert.ToInt16(plano.Tables[0].Rows[0]["TB015_TipoVencimento"].ToString());
                    parcela.TB016_EspecieDocumento  = plano.Tables[0].Rows[0]["TB015_EspecieDocumento"].ToString();
                    parcela.TB016_BoletoDesc1       = plano.Tables[0].Rows[0]["TB015_BoletoDesc1"].ToString();
                    parcela.TB016_BoletoDesc2       = plano.Tables[0].Rows[0]["TB015_BoletoDesc2"].ToString();
                    parcela.TB016_BoletoDesc3       = plano.Tables[0].Rows[0]["TB015_BoletoDesc3"].ToString();
                    parcela.TB016_BoletoDesc4       = plano.Tables[0].Rows[0]["TB015_BoletoDesc4"].ToString();
                    parcela.TB016_BoletoDesc5       = plano.Tables[0].Rows[0]["TB015_BoletoDesc5"].ToString();
                    parcela.TB016_Multa             = Convert.ToDouble(plano.Tables[0].Rows[0]["TB015_Multa"].ToString());
                    parcela.TB016_Juros             = Convert.ToDouble(plano.Tables[0].Rows[0]["TB015_Juros"].ToString());


                    parcela.TB016_TipoSacadoS       = "1";
                    parcela.TB031_TipoVencimento    = 1;
                    parcela.TB016_EspecieDocumento  = plano.Tables[0].Rows[0]["TB015_EspecieDocumento"].ToString();
                    parcela.TB016_Valor             = Convert.ToDouble(plano.Tables[0].Rows[0]["ValorPlano"].ToString());
                    parcela.TB016_ValorBruto        = Convert.ToDouble(plano.Tables[0].Rows[0]["ValorPlano"].ToString());
                    parcela.TB037_Id                = TB037_Id;
                    parcela.TB037_Comissao          = TB037_Comissao;

                    

                    parcela.TB015_id                = Convert.ToInt64(plano.Tables[0].Rows[0]["TB015_id"].ToString());
                    parcela.TB015_Plano             = plano.Tables[0].Rows[0]["TB015_Plano"].ToString();
                    parcela.TB016_LoteExportacao    = exportacao;


                    var planoProdutoN = new ProdutoNegocios();
                    var planoProdutoL = planoProdutoN.ProdutoPlano(parcela.TB015_id);

                    /*##########################################################################################################################################################################*/
                    if (aberturacontrato > 0)
                    {
                        if (parcelas.Count == 0)
                        {
                            /*Adesão*/
                            //parcela.TB016_Vencimento = dtContratoInicio.Value.AddDays(2);
                            var produtoAdesao = new ProdutoNegocios().ProdutoID(15);

                            var parcelaItemAdesao =
                                new ParcelaProdutosController
                                {
                                    TB017_Item                      = produtoAdesao.TB014_Produto,
                                    TB017_ValorUnitario             = produtoAdesao.TB014_ValorUnitario,
                                    TB017_IdProteus                 = produtoAdesao.TB014_IdProtheus
                                };
                            parcela.TB016_Entrada                   = 1;
                            parcela.TB016_ValorAdesao               = produtoAdesao.TB014_ValorUnitario;
                            parcela.TB016_ValorOutrosDesconto       = 0;
                            parcelaItemAdesao.TB017_ValorDesconto   = 0;
                            parcelaItemAdesao.TB017_ValorFinal      = parcela.TB016_ValorAdesao;
                            parcelaItemAdesao.TB017_TipoS           = "1";


                           // ComissaoAdesao                          = ComissaoAdesao + parcelaItemAdesao.TB017_ValorFinal;
                            if (Convert.ToInt16(pontoDevendaEmpresa.Tb002FamiliarAdesaoFormaS) == 1)
                            {
                                /*Valor Fixo*/
                                ComissaoAdesao = ComissaoAdesao+ pontoDevendaEmpresa.Tb002FamiliarAdesaoValor;
                            }
                            else
                            {
                                /*Aliquota*/

                                ComissaoAdesao = ComissaoAdesao + (pontoDevendaEmpresa.Tb002ParceiroAdesaoAliquota / parcelaItemAdesao.TB017_ValorFinal *100);
                            }

                            parcelaItensL.Add(parcelaItemAdesao);
                        }
                    }
                    foreach (var produto in planoProdutoL)
                    {
                        var parcelaItem = new ParcelaProdutosController
                        {
                            TB017_id            = produto.TB014_id,
                            TB017_IdProteus     = produto.TB014_IdProtheus,
                            TB017_Item          = produto.TB014_Produto,
                            TB017_Maior         = produto.TB014_Maiores,
                            TB017_Menor         = produto.TB014_Menores,
                            TB017_Isento        = produto.TB014_Isentos
                        };

                        if (produto.TB014_Menores > 0)
                        {
                            produto.TB014_ValorUnitario = produto.TB014_ValorUnitario * filtro.TB015_Menores;
                            parcelaItem.TB017_Item = parcelaItem.TB017_Item + "( * " + filtro.TB015_Menores + ")";
                        }

                        if (produto.TB014_Isentos > 0)
                        {
                            parcelaItem.TB017_Item = parcelaItem.TB017_Item + "( * " + filtro.TB015_Isentos + ")";
                            produto.TB014_ValorUnitario = produto.TB014_ValorUnitario * filtro.TB015_Isentos;
                        }

                        parcelaItem.TB017_ValorUnitario = produto.TB014_ValorUnitario;
                        if (produto.TB014_ValorMultiplo == 1)
                        {
                            parcelaItem.TB017_ValorUnitario = (parcelaItem.TB017_ValorUnitario * parcelaItem.TB017_Maior) +
                                                              (parcelaItem.TB017_ValorUnitario * parcelaItem.TB017_Menor);
                            produto.TB014_ValorUnitario = (produto.TB014_ValorUnitario * parcelaItem.TB017_Maior) +
                                                          (produto.TB014_ValorUnitario * parcelaItem.TB017_Menor);
                        }
                        parcelaItem.TB017_TipoS = "2";

                        if (aberturacontrato > 0 && parcelas.Count == 0 && produto.TB014_ValorUnitario > 0)
                        {
                            double preco = 0;
                            var data = parcela.TB016_Vencimento;// dtContratoInicio.Value; // DateTime.Now;
                            var ultimoDia = DateTime.DaysInMonth(data.Year, data.Month);
                            var diaAtual = data.Day;// DateTime.Now.Day;
                            var diasParaFinalDoMes = ultimoDia - diaAtual;
                            if (diaAtual < ultimoDia)
                            {
                                var valorMes = produto.TB014_ValorUnitario;
                                valorMes = Convert.ToDouble(valorMes.ToString(CultureInfo.InvariantCulture).Replace(".", ","));

                                var valorDia = valorMes / ultimoDia;
                                preco = preco + valorDia * diasParaFinalDoMes;

                                parcelaItem.TB017_ValorDesconto = produto.TB014_ValorUnitario - preco;
                            }
                        }
                        else
                        {
                            parcelaItem.TB017_ValorDesconto = 0;
                        }

                        parcelaItem.TB017_ValorDesconto = Convert.ToDouble(Format("{0:0.##}", parcelaItem.TB017_ValorDesconto));

                        parcelaItem.TB017_ValorFinal = produto.TB014_ValorUnitario - parcelaItem.TB017_ValorDesconto;// Convert.ToDouble(v1 + "," + v2);
                        parcelaItem.TB017_ValorFinal = Convert.ToDouble(Format("{0:0.##}", parcelaItem.TB017_ValorFinal));





                        //ComissaoMensalidade = ComissaoMensalidade;

                        if (Convert.ToInt16(pontoDevendaEmpresa.Tb002FamiliarMensalidadeFormaS) == 1)
                        {
                            /*Valor Fixo*/
                            ComissaoMensalidade = ComissaoMensalidade + pontoDevendaEmpresa.Tb002FamiliarMensalidadeValor;
                        }
                        else
                        {
                            /*Aliquota*/
                            ComissaoMensalidade = ComissaoMensalidade + (pontoDevendaEmpresa.Tb002FamiliarMensalidadeAliquota / parcelaItem.TB017_ValorFinal * 100);
                        }





                        parcelaItensL.Add(parcelaItem);

                        double valor = 0;
                        foreach (var t in parcelaItensL)
                        {
                            valor = valor + t.TB017_ValorFinal;
                        }
                        parcela.TB016_Valor = valor;

                        parcela.TB002_ComissaoAdesao        = ComissaoAdesao;
                        parcela.TB002_ComissaoMensalidade   = ComissaoMensalidade;

                        //Pesquisar todos os demais produtos do Plano e Incluir em ParcelaItem
                        parcela.ParcelaProduto_L = parcelaItensL;
                    }
                    parcelas.Add(parcela);
                }
                return parcelas;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController gerarParcelaParceiro(long contrato,DateTime vencimento, long reponsavel, int ciclo, long TB037_Id, double TB037_Comissao, int exportacao, int aberturacontrato,int TB013_Tipo)
        {
            try
            {
                var retorno                 = new ContratoParceiroDAO().contratoParceiroDadosParcela(contrato);
                var parcelas                = new List<ParcelaController>();
                double ComissaoAdesao       = 0;
                double ComissaoMensalidade  = 0;

                var pontoDevendaEmpresa     = new PontoDeVendaNegocios().PontoDeVendaEmpresa(retorno.TB002_id);
                var parcela                 = new ParcelaController
                {
                    Unidade                 = new UnidadeController
                    {
                        Municipio           = new MunicipioController { Estado = new EstadoController() }
                    }
                };

                var Pessoa                  = new PessoaController();
                Pessoa.Municipio            = new MunicipioController();
                Pessoa.Municipio.Estado     = new EstadoController();
                parcela.Pessoa              = Pessoa;
                var contratoparceiro        = new ContratosDao().ContratoSelect(contrato);
                parcela.Titular             = contratoparceiro.Titular;
    
                    var parcelaItensL                               = new List<ParcelaProdutosController>();
                    parcela.Plano                                   = new PlanoController();
                    parcela.Empresa                                 = new EmpresaController { TB001_id = pontoDevendaEmpresa.Empresa.TB001_id };
                    parcela.Unidade                                 = new UnidadeNegocios().UnidadeSelect(retorno.Unidade.TB020_id); 
                    parcela.TB033_Parcela                           = 1;
                    parcela.TB016_TotalParcelas                     = 1;
                    parcela.TB016_Emissao                           = DateTime.Now;
                    parcela.TB016_Vencimento                        = vencimento;
                    parcela.TB016_DiaVencimento                     = vencimento.Day;
                    parcela.TB016_DiaFechamento                     = vencimento.Day - 2;
                    parcela.TB016_Beneficiario                      = pontoDevendaEmpresa.Empresa.TB001_RazaoSocial;
                    parcela.TB016_BeneficiarioCidade                = pontoDevendaEmpresa.Empresa.Cidade;
                    parcela.TB016_BeneficiarioCPFCNPJ               = pontoDevendaEmpresa.Empresa.TB001_CNPJ;
                    parcela.TB016_BeneficiarioEndereco              = pontoDevendaEmpresa.Empresa.TB001_Logradouro;
                    parcela.TB016_BeneficiarioUF                    = pontoDevendaEmpresa.Empresa.TB001_UF;
                    parcela.TB016_CadastradoEm                      = DateTime.Now;
                    parcela.TB016_CadastradoPor                     = reponsavel;
                    parcela.TB016_AlteradoEm                        = DateTime.Now;
                    parcela.TB016_AlteradoPor                       = reponsavel;
                    parcela.Pessoa.Municipio.TB006_Municipio        = parcela.Unidade.Municipio.TB006_Municipio.TrimEnd().ToUpper();
                    parcela.Pessoa.Municipio.Estado.TB005_Estado    = parcela.Unidade.Estado.TB005_Estado;
                    parcela.TB016_CPFCNPJ                           = parcela.Unidade.TB020_Documento.Replace(".", "").Replace(",", "").Replace("/", "")
                                                                    .Replace("-", "").Trim();
                    parcela.TB016_Pagador                           = parcela.Unidade.TB020_RazaoSocial.TrimEnd().ToUpper().Replace("'", "/")
                                                                    .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                    parcela.TB016_PagadorCEP                        = parcela.Unidade.TB020_Cep.Replace(".","").Replace("-", "");
                    parcela.TB016_PagadorCidade                     = parcela.Unidade.Municipio.TB006_Municipio.TrimEnd().ToUpper().Replace("'", "/")
                                                                    .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                    parcela.TB016_PagadorUF                         = parcela.Unidade.Estado.TB005_Sigla.ToUpper().Replace("'", "/")
                                                                    .Replace("*", "").Replace("%", "/").Replace("&", "E").Replace("#", "");
                    parcela.TB012_id = contrato;
                    parcela.TB016_EnderecoPagador                   = parcela.Unidade.TB020_Logradouro.TrimEnd().ToUpper() + ", " +
                                                                    parcela.Unidade.TB020_Numero.TrimEnd().ToUpper();
                    parcela.TB012_CicloContrato                     = ciclo;
                    parcela.TB016_FormaPagamentoS                   = "1";
                    parcela.TB016_EmitirBoleto                      = 1;
                    parcela.TB016_StatusS                           = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(0));
                    var filtro                                      = new PlanoController();
                    var objPontoDeVenda                             = new PontoDeVendaController();
                    filtro.PontoDeVenda                             = objPontoDeVenda;
                    filtro.TB015_Maiores                            = 0;
                    filtro.TB015_Menores                            = 0;
                    filtro.TB015_Isentos                            = 0;
                    filtro.PontoDeVenda.TB002_id                    = retorno.TB002_id;

                    var plano                                       = new PlanoNegocios().PlanoVendaContezino(filtro, 0, 1, TB013_Tipo);
               
                    parcela.Plano.TB015_Maiores                     = filtro.TB015_Maiores;
                    parcela.Plano.TB015_Menores                     = filtro.TB015_Menores;
                    parcela.Plano.TB015_Isentos                     = filtro.TB015_Isentos;
                    parcela.Plano.TB015_IOF                         = Convert.ToDouble(plano.Tables[0].Rows[0]["TB015_IOF"].ToString());
                    parcela.TB016_TipoVencimento                    = Convert.ToInt16(plano.Tables[0].Rows[0]["TB015_TipoVencimento"].ToString());
                    parcela.TB016_EspecieDocumento                  = plano.Tables[0].Rows[0]["TB015_EspecieDocumento"].ToString();
                    parcela.TB016_BoletoDesc1                       = plano.Tables[0].Rows[0]["TB015_BoletoDesc1"].ToString();
                    parcela.TB016_BoletoDesc2                       = plano.Tables[0].Rows[0]["TB015_BoletoDesc2"].ToString();
                    parcela.TB016_BoletoDesc3                       = plano.Tables[0].Rows[0]["TB015_BoletoDesc3"].ToString();
                    parcela.TB016_BoletoDesc4                       = plano.Tables[0].Rows[0]["TB015_BoletoDesc4"].ToString();
                    parcela.TB016_BoletoDesc5                       = plano.Tables[0].Rows[0]["TB015_BoletoDesc5"].ToString();
                    parcela.TB016_Multa                             = Convert.ToDouble(plano.Tables[0].Rows[0]["TB015_Multa"].ToString());
                    parcela.TB016_Juros                             = Convert.ToDouble(plano.Tables[0].Rows[0]["TB015_Juros"].ToString());
                    parcela.TB016_TipoSacadoS                       = "1";
                    parcela.TB031_TipoVencimento                    = 1;
                    parcela.TB016_EspecieDocumento                  = plano.Tables[0].Rows[0]["TB015_EspecieDocumento"].ToString();
                    parcela.TB016_Valor                             = Convert.ToDouble(plano.Tables[0].Rows[0]["ValorPlano"].ToString());
                    parcela.TB016_ValorBruto                        = Convert.ToDouble(plano.Tables[0].Rows[0]["ValorPlano"].ToString());
                    parcela.TB037_Id                                = TB037_Id;
                    parcela.TB037_Comissao                          = TB037_Comissao;
                    parcela.TB015_id                                = Convert.ToInt64(plano.Tables[0].Rows[0]["TB015_id"].ToString());
                    parcela.TB015_Plano                             = plano.Tables[0].Rows[0]["TB015_Plano"].ToString();
                    parcela.TB016_LoteExportacao                    = exportacao;

                    var planoProdutoN                               = new ProdutoNegocios();
                    var planoProdutoL                               = planoProdutoN.ProdutoPlano(parcela.TB015_id);

                    foreach (var produto in planoProdutoL)
                    {
                        var parcelaItem                     = new ParcelaProdutosController
                        {
                            TB017_id                        = produto.TB014_id,
                            TB017_IdProteus                 = produto.TB014_IdProtheus,
                            TB017_Item                      = produto.TB014_Produto,
                            TB017_Maior                     = produto.TB014_Maiores,
                            TB017_Menor                     = produto.TB014_Menores,
                            TB017_Isento                    = produto.TB014_Isentos,
                            TB017_TipoS                     = produto.TB014_TipoS
                        };
                        parcelaItem.TB017_ValorUnitario     = produto.TB014_ValorUnitario;
          
                        parcelaItem.TB017_ValorDesconto     = 0;
                        parcelaItem.TB017_ValorDesconto     = Convert.ToDouble(Format("{0:0.##}", parcelaItem.TB017_ValorDesconto));
                        parcelaItem.TB017_ValorFinal        = produto.TB014_ValorUnitario - parcelaItem.TB017_ValorDesconto;// Convert.ToDouble(v1 + "," + v2);
                        parcelaItem.TB017_ValorFinal        = Convert.ToDouble(Format("{0:0.##}", parcelaItem.TB017_ValorFinal));
                  

                        if (Convert.ToInt16(pontoDevendaEmpresa.Tb002ParceiroMensalidadeFormaS) == 1)
                        {
                            /*Valor Fixo*/
                            ComissaoMensalidade             = ComissaoMensalidade + pontoDevendaEmpresa.Tb002ParceiroMensalidadeValor;
                        }
                        else
                       {
                            /*Aliquota*/
                            ComissaoMensalidade             = ComissaoMensalidade + (pontoDevendaEmpresa.Tb002ParceiroMensalidadeAliquota / parcelaItem.TB017_ValorFinal * 100);
                       }
                        parcelaItensL.Add(parcelaItem);
                        double valor = 0;
                        foreach (var t in parcelaItensL)
                        {
                            valor = valor + t.TB017_ValorFinal;
                        }
                        parcela.TB016_Valor                 = valor;
                        parcela.TB002_ComissaoAdesao        = ComissaoAdesao;
                        parcela.TB002_ComissaoMensalidade   = ComissaoMensalidade;

                        //Pesquisar todos os demais produtos do Plano e Incluir em ParcelaItem
                        parcela.ParcelaProduto_L = parcelaItensL;
                        
                }
                parcelas.Add(parcela);
                new ParcelaNegocios().parceiroParcelaInsert(parcelas);
                return parcelas[0];
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> ListarCiclosAtivosContrato(long tb012Id)
        {
            try
            {
                return new ParcelaDao().ListarCiclosAtivosContrato(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> FamiliarListaParcelasContrato(long tb012Id, long tb012CicloContrato, int tb016Status)
        {
            try
            {

                return new ParcelaDao().FamiliarListaParcelasContrato(tb012Id, tb012CicloContrato, tb016Status);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> parceiroListaParcelasContrato(long tb012Id, long tb012CicloContrato, int tb016Status)
        {
            try
            {

                return new ParcelaDao().parceiroListaParcelasContrato(tb012Id, tb012CicloContrato, tb016Status);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> FamiliarListaTodasContrato(long tb012Id)
        {
            try
            {

                return new ParcelaDao().FamiliarListaTodasContrato(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> ParcelasContratoExistente(long tb012Id)
        {
            try
            {

                return new ParcelaDao().ParcelasContratoExistente(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> BoletosParaImpressao(long tb012Id, long tb012CicloContrato)

        {
            try
            {
                return new ParcelaDao().BoletosParaImpressao(tb012Id, tb012CicloContrato);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> GerarCobrancasParcela(List<ParcelaController> parcelas, long tb011Id)
        {
            try
            {

                var parcelasRetorno = new List<ParcelaController>();


                for (int i = 0; i < parcelas.Count; i++)
                {
                    var parcelaInsert = new ParcelaDao().GerarCobrancasParcela(parcelas[i], parcelas.Count);


                    var produtos = new List<ParcelaProdutosController>();
                    for (var x = 0; x < parcelas[i].ParcelaProduto_L.Count; x++)
                    {
                        parcelas[i].ParcelaProduto_L[x].TB016_id = parcelaInsert.TB016_id;
                        ParcelaProdutosController produtoInsert = new ParcelaDao().ParcelaProdutoInsert(parcelas[i].ParcelaProduto_L[x]);
                        produtos.Add(produtoInsert);
                    }

                    parcelaInsert.ParcelaProduto_L = produtos;
                    parcelasRetorno.Add(parcelaInsert);

                    /*Gravando Log*/

                    var logC = new LogController
                    {
                        TB012_Id = parcelas[i].TB012_id,
                        TB011_Id = tb011Id,
                        TB000_IdTabela = 16,
                        TB000_Tabela = "Parcela",
                        TB000_Data = DateTime.Now,
                        TB000_Descricao = MensagensLog.L0009.Replace("$ID", parcelaInsert.TB016_id.ToString())
                            .Replace("$DATAEMISSAO", parcelaInsert.TB016_Emissao.ToString("dd/MM/yyyy"))
                            .Replace("$VENCIMENTO", parcelaInsert.TB016_Vencimento.ToString("dd/MM/yyyy"))
                            .Replace("$VALOR", parcelaInsert.TB016_Valor.ToString(CultureInfo.InvariantCulture))
                    };

                    new LogNegocios().LogInsert(logC);

                    /*Se a entrada for paga na abertura do contrato, status do contrato passa para 1 - Ativo*/
                    if (parcelas[0].TB016_EmitirBoleto != 1) continue;
                    if (parcelas[0].TB016_FormaPagamentoS == "2" || parcelas[0].TB016_FormaPagamentoS == "3" || parcelas[0].TB016_FormaPagamentoS == "4" || parcelas[0].TB016_FormaPagamentoS == "5")
                    {

                        new ContratoNegocios().contratoAtivar(parcelas[0].TB012_id, tb011Id);
                    }
                }

                return parcelasRetorno;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        //public ParcelaController ParcelaEntrada(long tb012Id)
        //{
        //    try
        //    {
        //        return new ParcelaDao().ParcelaEntrada(tb012Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //}
        public List<ParcelaController> ParcelasParaEmissaoBoleto(long tb012Id, long tb011Id, int Negociacao)
        {
            try
            {
                var emitirParcelas = new ParcelaDao().ParcelasParaEmissaoBoleto(tb012Id);
                var parcelasEmitidas = new List<ParcelaController>();

                WSBoleto.ClubeConteza ws = new WSBoleto.ClubeConteza();
                //WSBoleto.ParametrosSaida parametros;

                foreach (var parcela in emitirParcelas)
                {
                    //Emitir Boleto
                    var dataEmissao = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                    var vencimento = parcela.TB016_Vencimento.Year.ToString() + parcela.TB016_Vencimento.Month.ToString().PadLeft(2, '0') + parcela.TB016_Vencimento.Day.ToString().PadLeft(2, '0');
                    double valor = parcela.TB016_Valor + parcela.TB016_ValorAdesao;

                    //Math.Truncate(parcelaItem.TB017_ValorDesconto);


                    var endereco = parcela.Contrato.TB012_Logradouro.TrimEnd() + " - " + parcela.Contrato.TB012_Numero.TrimEnd();
                    var cnpjcpf = parcela.Pessoa.TB013_CPFCNPJ.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");

                    //parametros = ws.GerarBoleto(1, parcela.Banco.TB018_Banco, parcela.Empresa.TB001_id, dataEmissao, parcela.TB016_TipoVencimento.ToString(), vencimento, parcela.TB016_EspecieDocumento, valor, 0, parcela.TB016_IOF, parcela.Municipio.TB006_id, parcela.Municipio.TB006_Municipio.TrimEnd(), parcela.Estado.TB005_Sigla.Trim(), parcela.Contrato.TB004_Cep, endereco, parcela.Contrato.TB012_Bairro.TrimEnd(), parcela.Pessoa.TB013_NomeCompleto, cnpjcpf, parcela.TB016_id, parcela.TB016_BoletoDesc1, parcela.TB016_BoletoDesc2, parcela.TB016_BoletoDesc3, parcela.TB016_BoletoDesc4, parcela.TB016_BoletoDesc5);
                    //var v1 = 1;
                    //var v2 = parcela.Banco.TB018_Banco;
                    //var v3 = parcela.Empresa.TB001_id;
                    //var v4 = dataEmissao;
                    //var v5 = parcela.TB016_TipoVencimento.ToString();
                    //var v6 = vencimento;
                    //var v7 = parcela.TB016_EspecieDocumento;
                    //var v8 = valor;
                    ////var v30 = 0;
                    //var v10 = parcela.TB016_IOF;
                    //var v11 = parcela.Municipio.TB006_id;
                    //var v12 = parcela.Municipio.TB006_Municipio.TrimEnd();
                    //var v13 = parcela.Estado.TB005_Sigla.Trim();
                    //var v14 = parcela.Contrato.TB004_Cep;
                    //var v15 = endereco.Replace("'", "/").Replace("$", "/").Replace("& ", "E").Replace("%", "/").Replace("*", "/")
                    //    .Replace("#", "/");
                    //var v16 = parcela.Contrato.TB012_Bairro.TrimEnd().Replace("'", "/").Replace("$", "/").Replace("& ", "E")
                    //    .Replace("%", "/").Replace("*", "/").Replace("#", "/");
                    //var v17 = parcela.Pessoa.TB013_NomeCompleto.Replace("'", "/").Replace("$", "/").Replace("& ", "E")
                    //    .Replace("%", "/").Replace("*", "/").Replace("#", "/");
                    //var v18 = cnpjcpf;
                    //var v19 = parcela.TB016_id;
                    //var v20 = parcela.TB016_BoletoDesc1;
                    //var v21 = parcela.TB016_BoletoDesc2;
                    //var v22 = parcela.TB016_BoletoDesc3;
                    //var v23 = parcela.TB016_BoletoDesc4;
                    //var v24 = parcela.TB016_BoletoDesc5;



                    var parametros = ws.GerarBoleto(1, parcela.Banco.TB018_Banco, parcela.Empresa.TB001_id, dataEmissao, parcela.TB016_TipoVencimento.ToString(), vencimento, parcela.TB016_EspecieDocumento, valor, 0, parcela.TB016_IOF, parcela.Municipio.TB006_id, parcela.Municipio.TB006_Municipio.TrimEnd(), parcela.Estado.TB005_Sigla.Trim(), parcela.Contrato.TB004_Cep, endereco.Replace("'", "/").Replace("$", "/").Replace("& ", "E").Replace("%", "/").Replace("*", "/").Replace("#", "/"), parcela.Contrato.TB012_Bairro.TrimEnd().Replace("'", "/").Replace("$", "/").Replace("& ", "E").Replace("%", "/").Replace("*", "/").Replace("#", "/"), parcela.Pessoa.TB013_NomeCompleto.Replace("'", "/").Replace("$", "/").Replace("& ", "E").Replace("%", "/").Replace("*", "/").Replace("#", "/"), cnpjcpf, parcela.TB016_id, parcela.TB016_BoletoDesc1, parcela.TB016_BoletoDesc2, parcela.TB016_BoletoDesc3, parcela.TB016_BoletoDesc4, parcela.TB016_BoletoDesc5);

                    if (parametros.Erro != 0) continue;
                    var boletoParcela = new ParcelaController
                    {
                        TB016_id = parcela.TB016_id,
                        TB016_Emissao = DateTime.Now,
                        TB016_EnderecoPagador =
                            parcela.Contrato.TB012_Logradouro + " " + parcela.Contrato.TB012_Numero,
                        TB016_Valor = parcela.TB016_Valor,
                        TB016_Banco = parcela.Banco.TB018_Banco.ToString(),
                        TB016_NBoleto = parametros.NumeroBoleto,
                        TB016_NossoNumero = parametros.NossoNumero,
                        TB016_Agencia = parametros.Agencia,
                        TB016_ContaCorrente = parametros.Conta,
                        TB016_Carteira = parametros.Carteira,
                        TB016_Boleto = parametros.HTML
                    };
                    //BoletoParcela.TB016_TotalParcelas = TotalParcelas;

                    //BoletoParcela.TB016_DocumentoBanco = Parametros.NumeroBoleto;
                    /*Gravar Log*/
                    var logN = new LogNegocios();
                    var logC = new LogController
                    {
                        TB012_Id = tb012Id,
                        TB011_Id = tb011Id,
                        TB000_IdTabela = 12,
                        TB000_Tabela = "Contratos",
                        TB000_Data = DateTime.Now,
                        TB000_Descricao = MensagensLog.L0010.Replace("$ID", parcela.TB016_id.ToString())
                    };

                    logN.LogInsert(logC);






                    //MessageBox.Show(Format(MensagensDoSistema._0078, "\n", "\n"), @"Aviso",
                    //      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (Negociacao > 0)
                    {
                        var anotacao = new AnotacoesController
                        {
                            Tb012Id = tb012Id,
                            Tb011Id = tb011Id,
                            Tb026Data = DateTime.Now,
                            Tb026Cod = "L0074",
                            TB026_Negociacao = 1,
                            Tb026Anotacao = Format(MensagensLog.L0074, parcela.TB016_id)
                        };

                        var retorno = new AnotacoesNegocios().Anotacaoinsert(anotacao);

                    }


                    if (new ParcelaDao().ParcelaVinculaComBoleto(boletoParcela))
                    {
                        parcelasEmitidas.Add(boletoParcela);
                    }
                }

                return parcelasEmitidas;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController ParcelaPesquisaId(long tb016Id)
        {
            try
            {
                var parcela = new ParcelaDao().ParcelaPesquisaID(tb016Id);
                parcela.ParcelaProduto_L = new ProdutoDAO().ProdutosParcelaID(tb016Id);
                return parcela;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool BaixaBoletoImportacao(ParcelaController boleto, long tb011Id)
        {
            try
            {

                var logC = new LogController();
                var pessoaN = new PessoaNegocios();
                var contratoTitular = new ParcelaDao().ParcelaPesquisaID(boleto.TB016_id);

                logC.TB012_Id = contratoTitular.TB012_id;
                logC.TB011_Id = tb011Id;
                logC.TB000_IdTabela = 16;
                logC.TB000_Tabela = "Parcelas";
                logC.TB000_Data = DateTime.Now;
                logC.TB000_Descricao = MensagensLog.L0011.Replace("$ID", boleto.TB016_id.ToString());
                new LogNegocios().LogInsert(logC);

                pessoaN.GerarCartoes(Convert.ToInt64(contratoTitular.Pessoa.TB013_id), tb011Id);

                var contratoD = new ContratosDao();
                contratoD.ContratoAtivar(contratoTitular.TB012_id, tb011Id);

                logC.TB000_IdTabela = 12;
                logC.TB000_Tabela = "Contratos";
                logC.TB000_Data = DateTime.Now;
                logC.TB000_Descricao = MensagensLog.L0013.Replace("$ID", contratoTitular.TB012_id.ToString());
                new LogNegocios().LogInsert(logC);



                return new ParcelaDao().BaixaBoletoImportacao(boleto);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public bool atualizarnossonumero(long TB016_id, string TB016_NossoNumero, long TB016_AlteradoPor, long tb012Id)
        {
            try
            {
                if (new ParcelaDao().atualizarnossonumero(TB016_id, TB016_NossoNumero, TB016_AlteradoPor))
                {
                    var logC = new LogController
                    {
                        TB012_Id = tb012Id,
                        TB011_Id = TB016_AlteradoPor,
                        TB011_Ref = 0,
                        TB000_IdTabela = 16,
                        TB000_Tabela = "Parcela",
                        TB000_Data = DateTime.Now,
                        TB000_Descricao = Format(MensagensLog.L0051, TB016_id)
                    };

                    new LogNegocios().LogInsert(logC);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<ParcelaController> ParcelasBoletos(string vQuery)
        {
            try
            {
                return new ParcelaDao().ParcelasBoletos(vQuery);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaProdutosController ParcelaProdutoPesquisaId(long tb017Id)
        {
            try
            {

                return new ParcelaDao().ParcelaProdutoPesquisaId(tb017Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> ParcelasEmAberto(long tb012Id)
        {
            try
            {
                return new ParcelaDao().ParcelasEmAberto(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> GravarCodigoBarraCarne(long tb012Id)
        {
            try
            {

                return new ParcelaDao().GravarCodigoBarraCarne(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public Boolean ParcelasCancelar(List<ParcelaController> parcelasCancelar, long tb011Id, long tb012Id, string titular, string cpf)
        {
            try
            {

                /*Enviar E-mail*/

                //The From address (Email ID)
                string strFromAddress = ParametrosNegocios.EmailSaidaLogin;//sender
                string strName = ParametrosNegocios.NomeConta;
                //The To address (Email ID)
                string strToAddress = "ti@clubeconteza.com.br";//recipient


                if (Environment.UserName == "fabia" & Environment.MachineName == "FGE")
                {
                    strToAddress = "ti@clubeconteza.com.br";//recipient
                }
                var emailMsg = new MailMessage();

                //Specifying From,Sender & Reply to address
                emailMsg.From = new MailAddress(strFromAddress, strName);
                emailMsg.Sender = new MailAddress(strFromAddress, strName);
                //email_msg.ReplyTo = new MailAddress(str_from_address, str_name);

                //The To Email id
                emailMsg.To.Add(strToAddress);

                emailMsg.Subject = "Cancelamento de Parcelas";//Subject of email

                StringBuilder html = new StringBuilder();
                html.Append("<!DOCTYPE html>");
                html.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
                html.Append("<head runat='server' >");
                html.Append("<meta http-equiv = 'Content-Type' content= 'text/html; charset=utf-8'/>");
                html.Append("<title> Cancelamento Parcela </title>");
                html.Append("<style>");
                html.Append("body");
                html.Append("{");
                html.Append("margin: 0px");
                html.Append("}");
                html.Append(".container");
                html.Append("{");
                html.Append("width: 100 %;");
                html.Append("height: 100 %;");
                html.Append("background: #EDEDED;");
                html.Append("display: flex;");
                html.Append("flex-direction: row;");
                html.Append("justify-content: center;");
                html.Append("align-items: center");
                html.Append("}");
                html.Append(".box {");
                html.Append("width: 760px;");
                html.Append("height: 300px;");
                html.Append("background: #FFFFFF;");
                html.Append("}");
                html.Append(".auto-style1 {");
                html.Append("width: 150px;");
                html.Append("}");
                html.Append("</style>");
                html.Append("</head>");
                html.Append("<body>");
                html.Append("<div class='container'>");
                html.Append("<div class='box'>");
                html.Append("<div style='background-color:#0071C5; font-size: 26px; font-weight: bold; color: #FFFFFF;'>");
                html.Append("Clube Conteza");
                html.Append("</div>");
                html.Append("<br/>");
                html.Append("<div style='background-color:#F3F3F3; text-align: center;'>");
                html.Append("Cancelamento de parcelas");
                html.Append("</div>");
                html.Append("<br/>");
                html.Append("<div>");
                html.Append("Informamos que o contrato");
                html.Append(tb012Id);
                html.Append(" em nome de ");
                html.Append(titular.TrimEnd().ToUpper());
                html.Append(", com o CPF ");
                html.Append(cpf);
                html.Append(", foi alterado, resultando no cancelamento das seguintes parcelas.");
                html.Append("</div>");
                html.Append("<br/>");
                html.Append("<div>");
                html.Append("<table style='padding: 2px; margin: inherit; border: medium solid #000000; width:100%; table-layout: auto; border-spacing: inherit; border-collapse: collapse;'>");
                html.Append("<tr>");
                html.Append("<td style='border-style:solid;background-color:#4472C4;'>Parcela</td>");
                html.Append("<td style='border-style:solid;background-color:#4472C4;'>Vencimento </td>");
                html.Append("<td style='border-style:solid;background-color:#4472C4;'> Valor </td>");
                html.Append("</tr>");

                int linha = 0;
                foreach (ParcelaController parcela in parcelasCancelar)
                {

                    /*Cancelar Efetivamente a Parcela*/
                    ParcelaDao parcelaD = new ParcelaDao();
                    if (parcelaD.ParcelasCancelar(parcela.TB016_id))
                    {
                        /*Grava Log*/
                        //ParcelaDAO DAO = new ParcelaDAO();
                        /*Cancelar Parcelas*/
                        /*Gravando Log*/
                        LogNegocios logN = new LogNegocios();
                        LogController logC = new LogController();

                        logC.TB012_Id = tb012Id;
                        logC.TB011_Id = tb011Id;
                        logC.TB000_IdTabela = 16;
                        logC.TB000_Tabela = "Parcela";
                        logC.TB000_Data = DateTime.Now;
                        logC.TB000_Descricao = MensagensLog.L0014.Replace("$ID", parcela.TB016_id.ToString());
                        logN.LogInsert(logC);
                        /*Inclui no Email de alerta*/

                        html.Append("<tr>");
                        if (linha == 0)
                        {
                            html.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            html.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }
                        html.Append(parcela.TB016_id);
                        html.Append("</td>");
                        if (linha == 0)
                        {
                            html.Append("<td style='border-style: solid; background-color:#D9E2F3'>");
                        }
                        else
                        {
                            html.Append("<td style='border-style: solid; background-color:#FFFFFF'>");
                        }

                        // ReSharper disable once FormatStringProblem
                        html.Append(Format("{0:" + "" + "MM/dd/yyyy}", parcela.TB016_Vencimento.ToString("dd/MM/yyyy")));
                        html.Append("</td>");
                        if (linha == 0)
                        {
                            html.Append("<td style='border-style: solid; background-color:#D9E2F3;text-align:right'>");
                        }
                        else
                        {
                            html.Append("<td style='border-style: solid; background-color:#FFFFFF;text-align:right'>");
                        }

                        html.Append("R$ ");
                        html.Append(parcela.TB016_Valor);
                        html.Append("</td>");
                        html.Append("</tr>");

                        if (linha == 0)
                        {
                            linha = 1;
                        }
                        else
                        {
                            linha = 0;
                        }
                    }
                }
                html.Append("</table>");
                html.Append("</div>");
                html.Append("<div>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</div>");
                html.Append("</body>");
                html.Append("</html>");

                emailMsg.IsBodyHtml = true;

                emailMsg.Body = html.ToString();//body
                                                //Create an object for SmtpClient class
                var mailClient = new SmtpClient();

                //Providing Credentials (Username & password)
                var networkCdr = new NetworkCredential();
                networkCdr.UserName = strFromAddress;
                networkCdr.Password = ParametrosNegocios.EmailSaidaSenha;

                mailClient.Credentials = networkCdr;

                //Specify the SMTP Port
                mailClient.Port = ParametrosNegocios.EmailSaidaPorta;

                //Specify the name/IP address of Host
                mailClient.Host = ParametrosNegocios.EmailSaidaSMTP;

                //Uses Secure Sockets Layer(SSL) to encrypt the connection
                mailClient.EnableSsl = true;

                //Now Send the message
                mailClient.Send(emailMsg);

                // MessageBox.Show("Email Sent Successfully");

                /*Apos enviar email, gravar log*/

                return true;
            }
            catch (Exception)
            {
                //throw ex;
                return false;
            }
        }
        public List<ParcelaController> ParcelasVencidasContrato(long tb012Id, DateTime vencimento)
        {
            try
            {

                return new ParcelaDao().ParcelasVencidasContrato(tb012Id, vencimento);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController ParcelaPagamento(long tb016Id)
        {
            try
            {

                return new ParcelaDao().ParcelaPagamento(tb016Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> ListarBandeiraCartao()
        {
            try
            {
                return new ParcelaDao().ListarBandeiraCartao();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> ListaParcelamentoPossivelPorBandeira(long tb032Id, long tb001Id)
        {
            try
            {

                return new ParcelaDao().ListaParcelamentoPossivelPorBandeira(tb032Id, tb001Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController SelectFormaParamento(long tb031Id)
        {
            try
            {

                return new ParcelaDao().SelectFormaParamento(tb031Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool ParcelaInserirPagamentoCredParcela(ParcelaController pagamento, int cancelamento)
        {
            try
            {

                if (new ParcelaDao().ParcelaInserirPagamentoCredParcela(pagamento, cancelamento))
                {
                    var logN = new LogNegocios();
                    var logC = new LogController
                    {
                        TB012_Id = pagamento.TB012_id,
                        TB011_Id = pagamento.TB016_AlteradoPor,
                        TB011_Ref = 0,
                        TB000_IdTabela = 12,
                        TB000_Tabela = "TB012_Contratos",
                        TB000_Data = DateTime.Now
                    };


                    logC.TB000_Descricao = logC.TB000_Descricao = MensagensLog.L0015.Replace("$Contrato", pagamento.TB012_id.ToString());
                    logN.LogInsert(logC);

                    logC.TB000_IdTabela = 13;
                    logC.TB000_Tabela = "TB013_Pessoa";
                    logC.TB000_Descricao = logC.TB000_Descricao = MensagensLog.L0017;
                    logN.LogInsert(logC);

                    logC.TB000_IdTabela = 16;
                    logC.TB000_Tabela = "TB016_Parcela";
                    logC.TB000_Descricao = Format(MensagensLog.L0035, pagamento.TB016_id.ToString());
                    logN.LogInsert(logC);


                    new PessoaNegocios().GerarCartoes(pagamento.TB012_id, pagamento.TB016_AlteradoPor);

                    if (new PessoaDao().AtivarMembrosDoContrato(pagamento.TB012_id))
                    {
                        logC.TB000_IdTabela = 13;
                        logC.TB000_Tabela = "TB013_Pessoa";
                        logC.TB000_Descricao = logC.TB000_Descricao = MensagensLog.L0038;
                        logN.LogInsert(logC);
                        return true;
                    }

                    /*Parcela de cancelamento*/
                    // ParcelaController ParcelaPesquisaID = new ParcelaDAO().ParcelaPesquisaID()

                }


            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;

            }
            return false;
        }
        public List<ParcelaController> ParcelasListarParaBaixa(int tb016Status, long tb012Id)
        {
            try
            {
                return new ParcelaDao().ParcelasListarParaBaixa(tb016Status, tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        //public ParcelaController SelectTransacaoParcela(long tb016Id)
        //{
        //    try
        //    {
        //        var retorno = new ParcelaDao().ParcelaPesquisaID(tb016Id);
        //        retorno.Contrato = new ContratoNegocios().contratoSelect(retorno.TB012_id);


        //        return retorno;
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //}
        //public List<ParcelaProdutosController> ListaProdutosPorParcela(long tb016Id)
        //{
        //    try
        //    {

        //        return new ParcelaDao().ListaProdutosPorParcela(tb016Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //}
        //public ParcelaController ProximaParcela(long tb016Id, long tb012Id, int tb016Status)
        //{
        //    try
        //    {
        //        return new ParcelaDao().ProximaParcela(tb016Id, tb012Id, tb016Status);
        //    }
        //    catch (Exception ex)
        //    {
        //        // ReSharper disable once PossibleIntendedRethrow
        //        throw ex;
        //    }
        //}
        //public bool AlterarPlanoParcela(long tb016Id, long tb015Id, long tb012Id, long tb011Id)
        //{
        //    try
        //    {
        //        var produtos = new ProdutoDAO().ProdutoPlano(tb015Id);
        //        if (!new ParcelaDao().AlterarPlanoParcela(tb016Id, tb015Id, tb011Id, produtos)) return false;

        //        var logC = new LogController();

        //        logC.TB012_Id = tb012Id;
        //        logC.TB011_Id = tb011Id;
        //        logC.TB011_Ref = 0;
        //        logC.TB000_IdTabela = 12;
        //        logC.TB000_Tabela = "Contrato";
        //        logC.TB000_Data = DateTime.Now;
        //        logC.TB000_Descricao = Format(MensagensLog.L0039, tb016Id, tb015Id);
        //        new LogNegocios().LogInsert(logC);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool ParcelasUnir(long deleteTb016Id, long updadeTb016Id, long tb011Id, long tb012Id)
        //{
        //    try
        //    {

        //        double tb016Valor = 0;

        //        var produtos = new ParcelaDao().ListaProdutosPorParcela(deleteTb016Id);
        //        int i;
        //        for (i = 0; i < produtos.Count; i++)
        //        {
        //            tb016Valor = tb016Valor + Convert.ToDouble(produtos[i].TB017_ValorFinal);
        //        }

        //        if (!new ParcelaDao().ParcelasUnir(deleteTb016Id, updadeTb016Id, tb016Valor, tb011Id)) return false;
        //        var logC = new LogController
        //        {
        //            TB012_Id = tb012Id,
        //            TB011_Id = tb011Id,
        //            TB011_Ref = 0,
        //            TB000_IdTabela = 16,
        //            TB000_Tabela = "Parcela",
        //            TB000_Data = DateTime.Now,
        //            TB000_Descricao = Format(MensagensLog.L0040, deleteTb016Id.ToString(),
        //                updadeTb016Id.ToString())
        //        };

        //        new LogNegocios().LogInsert(logC);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool ParcelaAlterarDataEmissao(long tb016Id, long tb012Id, long tb011Id, DateTime tb016Emissao)
        //{
        //    try
        //    {
        //        if (!new ParcelaDao().ParcelaAlterarDataEmissao(tb016Id, tb011Id, tb016Emissao)) return false;
        //        var logC = new LogController
        //        {
        //            TB012_Id = tb012Id,
        //            TB011_Id = tb011Id,
        //            TB011_Ref = 0,
        //            TB000_IdTabela = 16,
        //            TB000_Tabela = "Parcela",
        //            TB000_Data = DateTime.Now,
        //            TB000_Descricao = Format(MensagensLog.L0041, tb016Id.ToString())
        //        };


        //        new LogNegocios().LogInsert(logC);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool ParcelaAlterarDataVencimento(long tb016Id, long tb012Id, long tb011Id, DateTime tb016Vencimento)
        //{
        //    try
        //    {
        //        if (!new ParcelaDao().ParcelaAlterarDataVencimento(tb016Id, tb011Id, tb016Vencimento)) return false;

        //        var logC = new LogController
        //        {
        //            TB012_Id = tb012Id,
        //            TB011_Id = tb011Id,
        //            TB011_Ref = 0,
        //            TB000_IdTabela = 16,
        //            TB000_Tabela = "Parcela",
        //            TB000_Data = DateTime.Now,
        //            TB000_Descricao = Format(MensagensLog.L0042, tb016Id.ToString())
        //        };

        //        new LogNegocios().LogInsert(logC);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool ParcelaAlterarValorAdesao(long tb016Id, long tb012Id, long tb011Id, double tb016ValorAdesao)
        //{
        //    try
        //    {

        //        if (new ParcelaDao().ParcelaAlterarValorAdesao(tb016Id, tb011Id, tb016ValorAdesao))
        //        {

        //            var logC = new LogController
        //            {
        //                TB012_Id = tb012Id,
        //                TB011_Id = tb011Id,
        //                TB011_Ref = 0,
        //                TB000_IdTabela = 16,
        //                TB000_Tabela = "Parcela",
        //                TB000_Data = DateTime.Now,
        //                TB000_Descricao = Format(MensagensLog.L0043, tb016Id.ToString())
        //            };

        //            new LogNegocios().LogInsert(logC);
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool ParcelaAlterarDataVPagamento(long tb016Id, long tb012Id, long tb011Id, DateTime tb016DataPagamento)
        //{
        //    try
        //    {
        //        if (!new ParcelaDao().ParcelaAlterarDataVPagamento(tb016Id, tb011Id, tb016DataPagamento)) return false;

        //        var logC = new LogController
        //        {
        //            TB012_Id = tb012Id,
        //            TB011_Id = tb011Id,
        //            TB011_Ref = 0,
        //            TB000_IdTabela = 16,
        //            TB000_Tabela = "Parcela",
        //            TB000_Data = DateTime.Now,
        //            TB000_Descricao = Format(MensagensLog.L0044, tb016Id)
        //        };

        //        new LogNegocios().LogInsert(logC);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool ParcelaAlterarFormaPagamento(long tb016Id, long tb012Id, long tb011Id, short tb016FormaPagamento)
        //{
        //    try
        //    {
        //        if (!new ParcelaDao().ParcelaAlterarFormaPagamento(tb016Id, tb011Id, tb016FormaPagamento)) return false;
        //        var logC = new LogController
        //        {
        //            TB012_Id = tb012Id,
        //            TB011_Id = tb011Id,
        //            TB011_Ref = 0,
        //            TB000_IdTabela = 16,
        //            TB000_Tabela = "Parcela",
        //            TB000_Data = DateTime.Now,
        //            TB000_Descricao = Format(MensagensLog.L0045, tb016Id.ToString())
        //        };

        //        new LogNegocios().LogInsert(logC);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool ParcelaProdutoDescricaoItem(long tb017Id, long tb012Id, long tb011Id, string tb017Item)
        //{
        //    try
        //    {
        //        if (!new ParcelaDao().ParcelaProdutoDescricaoItem(tb017Id, tb017Item)) return false;
        //        var logC = new LogController
        //        {
        //            TB012_Id = tb012Id,
        //            TB011_Id = tb011Id,
        //            TB011_Ref = 0,
        //            TB000_IdTabela = 17,
        //            TB000_Tabela = "ParcelaProduto",
        //            TB000_Data = DateTime.Now,
        //            TB000_Descricao = MensagensLog.L0046
        //        };

        //        new LogNegocios().LogInsert(logC);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool ParcelaProdutoAlterarDesconto(long tb017Id, long tb016Id, long tb012Id, long tb011Id, double tb017ValorUnitario, double tb017ValorDesconto, double tb017ValorFinal)
        //{
        //    try
        //    {
        //        if (!new ParcelaDao().ParcelaProdutoAlterarDesconto(tb017Id, tb017ValorUnitario, tb017ValorDesconto,
        //            tb017ValorFinal)) return false;

        //        var logC = new LogController
        //        {
        //            TB012_Id = tb012Id,
        //            TB011_Id = tb011Id,
        //            TB011_Ref = 0,
        //            TB000_IdTabela = 17,
        //            TB000_Tabela = "ParcelaProduto",
        //            TB000_Data = DateTime.Now,
        //            TB000_Descricao = Format(MensagensLog.L0047, tb017Id, tb016Id)
        //        };

        //        new LogNegocios().LogInsert(logC);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool ParcelaAlterarValorParcela(long tb016Id, long tb012Id, long tb011Id, double tb016ValorAdesao)
        //{
        //    try
        //    {
        //        if (!new ParcelaDao().ParcelaAlterarValorParcela(tb016Id, tb011Id, tb016ValorAdesao)) return false;

        //        var logC = new LogController
        //        {
        //            TB012_Id = tb012Id,
        //            TB011_Id = tb011Id,
        //            TB011_Ref = 0,
        //            TB000_IdTabela = 16,
        //            TB000_Tabela = "Parcela",
        //            TB000_Data = DateTime.Now,
        //            TB000_Descricao = Format(MensagensLog.L0048, tb016Id)
        //        };

        //        new LogNegocios().LogInsert(logC);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public ParcelaController ValorParcela(long tb016Id)
        {
            try
            {

                return new ParcelaDao().ValorParcela(tb016Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool ParcelaBaixaFinanceiro(ParcelaController parcela, int cancelar)
        {
            try
            {
                if (!new ParcelaDao().ParcelaBaixaFinanceiro(parcela)) return false;
                if (cancelar == 0)
                {
                    var pessoaN = new PessoaNegocios();
                    pessoaN.GerarCartoes(parcela.TB012_id, parcela.TB016_AlteradoPor);
                }
                else
                {
                    new ContratosDao().Contratocancelar(parcela.TB012_id, parcela.TB016_AlteradoPor);
                }

                var logC = new LogController
                {
                    TB012_Id = parcela.TB012_id,
                    TB011_Id = parcela.TB016_AlteradoPor,
                    TB011_Ref = 0,
                    TB000_IdTabela = 16,
                    TB000_Tabela = "Parcela",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = Format(MensagensLog.L0049, parcela.TB016_id)
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<ParcelaController> ParcelasEmAbertoPorData(long tb012Id)
        {
            try
            {

                return new ParcelaDao().ParcelasEmAbertoPorData(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController UltimaParcelaContrato(long tb012Id)
        {
            try
            {

                return new ParcelaDao().UltimaParcelaContrato(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController UltimaParcelaRenovacao(long tb012Id)
        {
            try
            {

                return new ParcelaDao().UltimaParcelaRenovacao(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public string ParcelasExportarBoletos(List<ParcelaController> export, Int32 tb012CicloContrato, string path)
        {
            try
            {


                var ultimoLonte = new ParcelaDao().UltimaLoteExportacao();
                ultimoLonte++;

                var nomeArquivo = "B_" + ultimoLonte.ToString() + ".txt";

                foreach (ParcelaController contrato in export)
                {
                    var s = File.AppendText(path + @"\" + nomeArquivo);
                    var gravar = new ParcelaDao().ParcelasExportarBoletos(contrato.TB012_id, tb012CicloContrato);
                    if (gravar.Count <= 0) continue;
                    foreach (var parcela in gravar)
                    {
                        var linha = new StringBuilder();

                        linha.Append(parcela.TB012_id.ToString().PadLeft(9, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_id.ToString().PadLeft(9, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_Parcela.ToString().PadLeft(2, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_Emissao.ToString("yyyy-MM-dd").PadLeft(10, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_Vencimento.ToString("yyyy-MM-dd").PadLeft(10, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_Pagador.PadLeft(150, ' '));
                        linha.Append("#");
                        linha.Append(parcela.Titular.TB013_TipoS.PadLeft(1, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_CPFCNPJ.PadLeft(14, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_EnderecoPagador.PadLeft(500, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_PagadorCEP.PadLeft(8, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_PagadorCidade.PadLeft(150, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_PagadorUF.PadLeft(5, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_Valor.ToString(CultureInfo.CurrentCulture).PadLeft(8, ' '));
                        linha.Append("#");
                        linha.Append(value: parcela.TB016_Abatimento.ToString(CultureInfo.CurrentCulture).PadLeft(8, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_IOF.ToString(CultureInfo.CurrentCulture).PadLeft(8, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_BoletoDesc1.PadLeft(70, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_BoletoDesc2.PadLeft(70, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_BoletoDesc3.PadLeft(70, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_BoletoDesc4.PadLeft(70, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_BoletoDesc5.PadLeft(70, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_EspecieDocumento.PadLeft(2, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_Banco.PadLeft(10, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_Agencia.PadLeft(15, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_ContaCorrente.PadLeft(15, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_Carteira.PadLeft(2, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_Beneficiario.PadLeft(50, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_BeneficiarioEndereco.PadLeft(150, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_BeneficiarioCPFCNPJ.PadLeft(14, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_BeneficiarioCidade.PadLeft(150, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_BeneficiarioUF.PadLeft(2, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_NBoleto.PadLeft(70, ' '));
                        linha.Append("#");
                        linha.Append(parcela.TB016_NossoNumero.PadLeft(15, ' '));
                        linha.Append("#");
                        linha.Append(ultimoLonte.ToString().PadLeft(8, ' '));
                        linha.Append("#");
                        linha.Append(parcela.Municipio.TB006_Lote.ToString().PadLeft(9, ' '));
                        linha.Append("#");
                        linha.Append(parcela.Titular.TB013_Bairro.PadLeft(100, ' '));
                        linha.Append("#");
                        s.WriteLine(linha);

                        /*Atualiza Parcela*/
                    }


                    s.Close();
                }
                return path + @"\" + nomeArquivo;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }


        }
        public bool ParcelaCancela(long tb016Id, long tb011Id, long tb012Id)
        {
            try
            {
                if (new ParcelaDao().ParcelaCancela(tb016Id, tb011Id))
                {
                    var logC = new LogController
                    {
                        TB012_Id = tb012Id,
                        TB011_Id = tb011Id,
                        TB011_Ref = 0,
                        TB000_IdTabela = 16,
                        TB000_Tabela = "Parcela",
                        TB000_Data = DateTime.Now,
                        TB000_Descricao = Format(MensagensLog.L0051, tb016Id)
                    };

                    new LogNegocios().LogInsert(logC);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<ParcelaController> ParcelasListarAtivasPorContrato(long tb012Id, int tb012CicloContrato)
        {
            try
            {

                return new ParcelaDao().ParcelasListarAtivasPorContrato(tb012Id, tb012CicloContrato);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController ParcelaId(long tb016Id)
        {
            try
            {

                return new ParcelaDao().ParcelaId(tb016Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool EmitirBoletoAvulsto(ParcelaController parcela, long tb011Id, long tb012Id)
        {
            try
            {
                var ws = new WSBoleto.ClubeConteza();

                //Emitir Boleto
                var dataEmissao = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                var vencimento = parcela.TB016_Vencimento.Year.ToString() + parcela.TB016_Vencimento.Month.ToString().PadLeft(2, '0') + parcela.TB016_Vencimento.Day.ToString().PadLeft(2, '0');


                double valor = parcela.TB016_Valor + parcela.TB016_ValorAdesao;

                var endereco = parcela.Contrato.TB012_Logradouro.TrimEnd() + " - " + parcela.Contrato.TB012_Numero.TrimEnd();
                // ReSharper disable once UnusedVariable
                var iof = parcela.TB016_IOF.ToString(CultureInfo.InvariantCulture).Replace(",", ".");
                var cnpjcpf = parcela.Pessoa.TB013_CPFCNPJ.Trim().Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");


                var v1 = 1;
                var v2 = parcela.Banco.TB018_Banco;
                var v3 = parcela.Empresa.TB001_id;
                var v4 = dataEmissao;
                var v5 = parcela.TB016_TipoVencimento.ToString();
                var v6 = vencimento;
                var v7 = parcela.TB016_EspecieDocumento;
                var v8 = valor;
                //var v30 = 0;
                var v10 = parcela.TB016_IOF;
                var v11 = parcela.Municipio.TB006_id;
                var v12 = parcela.Municipio.TB006_Municipio.TrimEnd();
                var v13 = parcela.Estado.TB005_Sigla.Trim();
                var v14 = parcela.Contrato.TB004_Cep;
                var v15 = endereco.Replace("'", "/").Replace("$", "/").Replace("& ", "E").Replace("%", "/").Replace("*", "/")
                    .Replace("#", "/");
                var v16 = parcela.Contrato.TB012_Bairro.TrimEnd().Replace("'", "/").Replace("$", "/").Replace("& ", "E")
                    .Replace("%", "/").Replace("*", "/").Replace("#", "/");
                var v17 = parcela.Pessoa.TB013_NomeCompleto.Replace("'", "/").Replace("$", "/").Replace("& ", "E")
                    .Replace("%", "/").Replace("*", "/").Replace("#", "/");
                var v18 = cnpjcpf;
                var v19 = parcela.TB016_id;
                var v20 = parcela.TB016_BoletoDesc1;
                var v21 = parcela.TB016_BoletoDesc2;
                var v22 = parcela.TB016_BoletoDesc3;
                var v23 = parcela.TB016_BoletoDesc4;
                var v24 = parcela.TB016_BoletoDesc5;



                var parametros = ws.GerarBoleto(v1, v2, v3, v4, v5.ToString(), v6, v7, v8, 0, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24);

                if (parametros.Erro == 0)
                {
                    var boletoParcela = new ParcelaController
                    {
                        TB016_id = parcela.TB016_id,
                        TB016_Emissao = DateTime.Now,
                        TB016_EnderecoPagador =
                            parcela.Contrato.TB012_Logradouro.Replace("'", "/").Replace("$", "/").Replace("& ", "E")
                                .Replace("%", "/").Replace("*", "/").Replace("#", "/") + " " + parcela.Contrato
                                .TB012_Numero.Replace("'", "/").Replace("$", "/").Replace("& ", "E")
                                .Replace("%", "/").Replace("*", "/").Replace("#", "/"),
                        TB016_Valor = parcela.TB016_Valor,
                        TB016_Banco = parcela.Banco.TB018_Banco.ToString(),
                        TB016_NBoleto = parametros.NumeroBoleto,
                        TB016_NossoNumero = parametros.NossoNumero,
                        TB016_Agencia = parametros.Agencia,
                        TB016_ContaCorrente = parametros.Conta,
                        TB016_Carteira = parametros.Carteira,
                        TB016_Boleto = parametros.HTML
                    };
                    /*Gravar Log*/

                    var logC = new LogController
                    {
                        TB012_Id = tb012Id,
                        TB011_Id = tb011Id,
                        TB000_IdTabela = 12,
                        TB000_Tabela = "Contratos",
                        TB000_Data = DateTime.Now,
                        TB000_Descricao = MensagensLog.L0010.Replace("$ID", parcela.TB016_id.ToString())
                    };

                    new LogNegocios().LogInsert(logC);

                    if (new ParcelaDao().ParcelaVinculaComBoleto(boletoParcela))
                    {
                        // ParcelasEmitidas.Add(BoletoParcela);
                    }
                }
                // }

                if (parametros.Erro > 0)
                {
                    return false;
                }
                    return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool AlterarVencimentoParcela(long tb016Id, DateTime tb016Vencimento, long tb011Id, long tb012Id)
        {
            try
            {
                if (!new ParcelaDao().AlterarVencimentoParcela(tb016Id, tb016Vencimento, tb011Id)) return false;

                var logC = new LogController
                {
                    TB012_Id = tb012Id,
                    TB011_Id = tb011Id,
                    TB011_Ref = 0,
                    TB000_IdTabela = 16,
                    TB000_Tabela = "Parcela",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = Format(MensagensLog.L0052, tb016Id,
                        tb016Vencimento.ToString("dd/MM/yyyy"))
                };


                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public long CorporativoParcelaInsert(ParcelaController parcela)
        {
            long retorno;
            try
            {
                retorno = new ParcelaDao().CorporativoParcelaInsert(parcela);

                if (retorno > 0)
                {

                    var logC = new LogController
                    {
                        TB012_Id = parcela.TB012_id,
                        TB011_Id = parcela.TB016_AlteradoPor,
                        TB011_Ref = 0,
                        TB000_IdTabela = 16,
                        TB000_Tabela = "Parcela",
                        TB000_Data = DateTime.Now,
                        TB000_Descricao = Format(MensagensLog.L0054, retorno,
                            parcela.TB012_id)
                    };

                    new LogNegocios().LogInsert(logC);
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }

            return retorno;
        }
        public ParcelaController UltimaParcelaContratoCiclo(long tb012Id, int tb012CicloContrato)
        {
            try
            {

                return new ParcelaDao().UltimaParcelaContratoCiclo(tb012Id, tb012CicloContrato);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public double CorporativoValorUnitarioParcela(long tb012Id)
        {
            try
            {

                return new ParcelaDao().CorporativoValorUnitarioParcela(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool CorporativoAdesaoParcelaInsert(List<ParcelaProdutosController> adesaoL, double valorAdesao, double tb016Id, long tb012Id)
        {
            try
            {
                return new ParcelaDao().CorporativoAdesaoParcelaInsert(adesaoL, valorAdesao, tb016Id, tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool FamiliarParcelaInsert(List<ParcelaController> parcelas)
        {
            try
            {
                if (new ParcelaDao().FamiliarParcelaInsert(parcelas))
                {
                    var logC = new LogController
                    {
                        TB012_Id = parcelas[0].TB012_id,
                        TB011_Id = parcelas[0].TB033_AlteradoPor,
                        TB011_Ref = 0,
                        TB000_IdTabela = 16,
                        TB000_Tabela = "Parcela",
                        TB000_Data = DateTime.Now
                    };


                    string parcelaI = null;
                    int i;
                    for (i = 0; i < parcelas.Count; i++)
                    {
                        parcelaI = parcelaI + "," + parcelas[i].TB033_Parcela + " [Com vencimento em] " + parcelas[i].TB016_Vencimento.ToString("ss/MM/yyyy");
                        if (parcelas[i].TB016_ParcelaCancelamento == 1)
                        {

                            new ContratosDao().ContratoInativar(parcelas[0].TB012_id, parcelas[0].TB033_AlteradoPor, parcelas[0].TB016_Valor);
                        }
                    }

                    logC.TB000_Descricao = Format(MensagensLog.L0055, parcelaI);

                    new LogNegocios().LogInsert(logC);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool parceiroParcelaInsert(List<ParcelaController> parcelas)
        {
            try
            {
                if (new ParcelaDao().parceiroParcelaInsert(parcelas))
                {
                    var logC = new LogController
                    {
                        TB012_Id = parcelas[0].TB012_id,
                        TB011_Id = parcelas[0].TB033_AlteradoPor,
                        TB011_Ref = 0,
                        TB000_IdTabela = 16,
                        TB000_Tabela = "Parcela",
                        TB000_Data = DateTime.Now
                    };


                    string parcelaI = null;
                    int i;
                    for (i = 0; i < parcelas.Count; i++)
                    {
                        parcelaI = parcelaI + "," + parcelas[i].TB033_Parcela + " [Com vencimento em] " + parcelas[i].TB016_Vencimento.ToString("ss/MM/yyyy");
                        if (parcelas[i].TB016_ParcelaCancelamento == 1)
                        {

                            new ContratosDao().ContratoInativar(parcelas[0].TB012_id, parcelas[0].TB033_AlteradoPor, parcelas[0].TB016_Valor);
                        }
                    }

                    logC.TB000_Descricao = Format(MensagensLog.L0055, parcelaI);

                    new LogNegocios().LogInsert(logC);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController ParcelaProximaParcelaId(long tb012Id, long tb016Id)
        {
            try
            {

                return new ParcelaDao().ParcelaProximaParcelaID(tb012Id, tb016Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool ParcelaUnir(ParcelaController parcela01, long parcela02, long tb011Id)
        {
            try
            {
                if (!new ParcelaDao().ParcelaUnir(parcela01, parcela02)) return false;
                var logC = new LogController
                {
                    TB012_Id = parcela01.TB012_id,
                    TB011_Id = tb011Id,
                    TB011_Ref = 0,
                    TB000_IdTabela = 16,
                    TB000_Tabela = "Parcela",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = Format(MensagensLog.L0040, parcela01.TB016_id,
                        parcela02)
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public long ParcelaIdVencimento(long tb012Id, DateTime vencimento)
        {
            try
            {
                return new ParcelaDao().ParcelaIdVencimento(tb012Id, vencimento);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool AlterarFormaPagamento(long tb016Id, short tb016FormaPagamento, long tb011Id, long tb012Id, DateTime tb016Vencimento)
        {
            try
            {
                if (new ParcelaDao().AlterarFormaPagamento(tb016Id, tb016FormaPagamento, tb011Id, tb016Vencimento))
                {
                    var logC = new LogController
                    {
                        TB012_Id = tb012Id,
                        TB011_Id = tb011Id,
                        TB011_Ref = 0,
                        TB000_IdTabela = 16,
                        TB000_Tabela = "Parcela",
                        TB000_Data = DateTime.Now,
                        TB000_Descricao = Format(MensagensLog.L0056, tb016Id)
                    };


                    new LogNegocios().LogInsert(logC);
                    return true;
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool ParcelasAlteracaoDesconto(long tb016Id, long tb017Id, double desconto, double valorfinal, long tb011Id, long tb012Id)
        {
            try
            {
                if (new ParcelaDao().ParcelasAlteracaoDesconto(tb016Id, tb017Id, desconto, valorfinal, tb011Id, tb012Id))
                {
                    var logC = new LogController
                    {
                        TB012_Id = tb012Id,
                        TB011_Id = tb011Id,
                        TB011_Ref = 0,
                        TB000_IdTabela = 16,
                        TB000_Tabela = "Parcela",
                        TB000_Data = DateTime.Now,
                        TB000_Descricao = Format(MensagensLog.L0060, tb017Id, tb016Id, desconto)
                    };

                    new LogNegocios().LogInsert(logC);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool ParcelaAlterarStauts(long tb016Id, long tb011Id, int status, long tb012Id)
        {
            try
            {
                if (new ParcelaDao().ParcelaAlterarStauts(tb016Id, tb011Id, status))
                {
                    var logC = new LogController
                    {
                        TB012_Id = tb012Id,
                        TB011_Id = tb011Id,
                        TB011_Ref = 0,
                        TB000_IdTabela = 16,
                        TB000_Tabela = "Parcela",
                        TB000_Data = DateTime.Now,
                        TB000_Descricao = Format(MensagensLog.L0062, tb016Id, status)
                    };

                    new LogNegocios().LogInsert(logC);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool ParcelasAlterarCiclo(long tb016Id, int ciclo, long tb011Id, long tb012Id)
        {
            try
            {
                if (!new ParcelaDao().ParcelasAlterarCiclo(tb016Id, ciclo, tb011Id)) return false;
                var logC = new LogController
                {
                    TB012_Id = tb012Id,
                    TB011_Id = tb011Id,
                    TB011_Ref = 0,
                    TB000_IdTabela = 16,
                    TB000_Tabela = "Parcela",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = Format(MensagensLog.L0063, tb016Id)
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool ParcelaAlterarVencimento(long tb016Id, long tb011Id, DateTime tb016Vencimento, long tb012Id)
        {
            try
            {
                if (!new ParcelaDao().ParcelaAlterarVencimento(tb016Id, tb011Id, tb016Vencimento)) return false;
                var logC = new LogController
                {
                    TB012_Id = tb012Id,
                    TB011_Id = tb011Id,
                    TB011_Ref = 0,
                    TB000_IdTabela = 16,
                    TB000_Tabela = "Parcela",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = Format(MensagensLog.L0066, tb016Vencimento.ToString("dd/MM/yyyy"))
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool AbonarAdesaoContrato(long tb016Id, long tb017Id, long tb012Id, double valorAdesao, long tb011Id)
        {
            try
            {
                if (!new ParcelaDao().AbonarAdesaoContrato(tb016Id, tb017Id, tb012Id, valorAdesao)) return false;
                var logC = new LogController
                {
                    TB012_Id = tb012Id,
                    TB011_Id = tb011Id,
                    TB011_Ref = 0,
                    TB000_IdTabela = 16,
                    TB000_Tabela = "Parcela",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = Format(MensagensLog.L0067, tb016Id)
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool SetarParcelaVencida( long AlteradoPor)
        {
            try
            {
                return new ParcelaDao().SetarParcelaVencida( AlteradoPor);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> ParcelasListarInadimplenciaAnual(DateTime DataReferencia)
        {
            try
            {
                return new ParcelaDao().ParcelasListarInadimplenciaAnual(DataReferencia);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> Inadimplencia(long TB037_Id, string query)
        {
            try
            {
                return new ParcelaDao().Inadimplencia(TB037_Id, query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ParcelaController> ListaParcelasVencidas(long tb012Id)

        {
            try
            {

                return new ParcelaDao().ListaParcelasVencidas(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController NegociacaoSimulacao(long tb012Id)
        {
            try
            {

                return new ParcelaDao().NegociacaoSimulacao(tb012Id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool  ParcelasVencidas(long TB012_id)
        {
            try
            {
                return new ParcelaDao().ParcelasVencidas(TB012_id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> ListaBoletosParaEnvio(long tb012Id,string TB011_ftpServidor,string TB011_ftpUsuario, string TB011_ftpSenha)
        {
            try
            {
                var Parcelas = new ParcelaDao().ListaBoletosParaEnvio(tb012Id);

                foreach (ParcelaController parcela in Parcelas)
                {

                    string vArq = @"C:\temp\" + parcela.TB016_id.ToString() + ".htm";

                    WebBrowser browser = new WebBrowser();
                    browser.ScriptErrorsSuppressed = true; //not necessesory you can remove it
                    browser.DocumentText = parcela.TB016_Boleto;
                    browser.Document.OpenNew(true);
                    browser.Document.Write(parcela.TB016_Boleto);
                    browser.Refresh();
                    browser.Document.Title = parcela.TB016_id.ToString();

                    File.WriteAllText(vArq, browser.Document.Body.Parent.OuterHtml, Encoding.GetEncoding(browser.Document.Encoding));

                    //Report vPdf = new Report(new PdfFormatter());
                    ////FontDef vDef = new FontDef(vPdf, FontDef.StandardFont.TimesRoman);
                    //Page vPage = new Page(vPdf);
                    //FontDef vDef = new FontDef(vPdf, FontDef.StandardFont.TimesRoman);
                    //FontProp vDrop = new FontProp(vDef, 10);

                    //vPage.AddCB_MM(5, new RepString(temp)); // Centraliza 
                    //vPdf.Save(vArq);


                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(TB011_ftpServidor + "/portal/boletos/" + parcela.TB016_id.ToString() + ".htm");
                    request.Method = WebRequestMethods.Ftp.UploadFile;

                    // Get network credentials.
                    request.Credentials = new NetworkCredential(TB011_ftpUsuario, TB011_ftpSenha);

                    // Read the file's contents into a byte array.
                    byte[] bytes = System.IO.File.ReadAllBytes(vArq);

                    // Write the bytes into the request stream.
                    request.ContentLength = bytes.Length;
                    using (Stream request_stream = request.GetRequestStream())
                    {
                        request_stream.Write(bytes, 0, bytes.Length);
                        request_stream.Close();
                    }

                }
                return Parcelas;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public ParcelaController ParceladoArquivoRemessa(string cpf, DateTime vencimento,long tb011Id, double TB016_ValorPago, DateTime TB016_DataPagamento,string banco,string agencia,string contacorrente)
        {
            try
            {
                ParcelaController retorno = new ParcelaDao().ParceladoArquivoRemessa(cpf, vencimento);

                if(retorno.TB016_id>0)
                {
                    /*Se Parcela esta em <3 ou 4 - Atualizar parcela*/
                    if (Convert.ToInt16(retorno.TB016_StatusS) < 3 || Convert.ToInt16(retorno.TB016_StatusS) == 4)
                    {
                        retorno.TB016_ValorPago = TB016_ValorPago;
                        retorno.TB016_DataPagamento = TB016_DataPagamento;
                        retorno.TB016_AlteradoPor = tb011Id;
                        retorno.TB016_Banco = banco;
                        retorno.TB016_Agencia = agencia;
                        retorno.TB016_ContaCorrente = contacorrente;
                        if (new ParcelaDao().parcelaBaixaViaPlanilhaRemessa_AtivarContrato(retorno))
                        {
                            var logC = new LogController
                            {
                                TB012_Id = retorno.TB012_id,
                                TB011_Id = tb011Id,
                                TB011_Ref = 0,
                                TB000_IdTabela = 16,
                                TB000_Tabela = "Parcela",
                                TB000_Data = DateTime.Now,
                                TB000_Descricao = Format(MensagensLog.L0075, retorno.TB016_id.ToString())
                                 
                            };

                            new LogNegocios().LogInsert(logC);
                        }
                    }

                }
         

                retorno.TB016_StatusS = Enum.GetName(typeof(ParcelaController.TB016_StatusE), Convert.ToInt16(retorno.TB016_StatusS));
                return retorno;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public bool produtoIncluirManualmente(ParcelaProdutosController produto, ParcelaController parcela, long idProduto)
        {
            try
            {
                if (!new ParcelaDao().produtoIncluirManualmente(produto, parcela)) return false;
                var logC = new LogController
                {
                    TB012_Id = parcela.TB012_id,
                    TB011_Id = parcela.TB016_AlteradoPor,
                    TB011_Ref = 0,
                    TB000_IdTabela = 16,
                    TB000_Tabela = "Parcela",
                    TB000_Data = DateTime.Now,
                    TB000_Descricao = Format(MensagensLog.L0081, idProduto.ToString(), parcela.TB016_id.ToString())
                };

                new LogNegocios().LogInsert(logC);
                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public long validarPendenciaFinanceira(long TB012_id)
        {
            try
            {
                return new ParcelaDao().validarPendenciaFinanceira(TB012_id);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
        public List<ParcelaController> negociadoresLista()
        {
            try
            {

                return new ParcelaDao().negociadoresLista();
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public long BuscarNossoNumeroPorIdParcela(long idParcela)
        {
            try
            {
                return new ParcelaDao().BuscarNossoNumeroPorIdParcela(idParcela);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AlterarNossoNumeroPorParcela(long idParcela, long nossoNumero)
        {
            try
            {
                return new ParcelaDao().AlterarNossoNumeroPorParcela(idParcela, nossoNumero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
