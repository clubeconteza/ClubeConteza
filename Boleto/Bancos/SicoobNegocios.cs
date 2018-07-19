using Boleto.Controller;
using Boleto.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Boleto.Bancos
{
    public class SicoobNegocios
    {
        public ParametrosSaida Emissao240(
            BancoController Banco_F,
            string dataEmissao,
            string codTipoVencimento,
            string dataVencimentoTit,
            string codEspDocumento,
            string valorTitulo,
            string valorAbatimento,
            string valorIOF,
            string codMunicipio,
            string nomeSacado,
            string cpfCGC,
            string endereco,
            string bairro,
            string cidade,
            string cep,
            string uf,
            string Cobranca,
            string Instrucao1,
            string Instrucao2,
            string Instrucao3,
            string Instrucao4,
            string Instrucao5
            )
        {

            StringBuilder   Dados           = new StringBuilder();
            StringBuilder   BoletoCarne     = new StringBuilder();
            ParametrosSaida RetornoBoleto   = new ParametrosSaida();
            
            RetornoBoleto.ErroDesc = "SEM ERRO.";

            try
            {
                StringBuilder Parametros = new StringBuilder();

                /*Pesquisa no banco*/
                BancoDAO Banco_D = new BancoDAO();
                BancoController Banco = Banco_D.SP_S_TB018_BancosBoleto(Banco_F);

                String url = Banco.TB018_url;
                Parametros.Append("numContaCorrente=");
                Parametros.Append(Banco.TB018_ContaCorrente);
                Parametros.Append("&");

                Parametros.Append("coopCartao=");
                Parametros.Append(Banco.TB018_Cartao);
                Parametros.Append("&");

                Parametros.Append("numCliente=");
                Parametros.Append(Banco.TB018_Cliente);
                Parametros.Append("&");

                Parametros.Append("chaveAcessoWeb=");
                Parametros.Append(Banco.TB018_chaveAcesso);
                Parametros.Append("&");

                /*Dados */
                Parametros.Append("dataEmissao=");
                Parametros.Append(dataEmissao);
                Parametros.Append("&");
                Parametros.Append("codTipoVencimento=");
                Parametros.Append(codTipoVencimento);
                Parametros.Append("&dataVencimentoTit=");
                Parametros.Append(dataVencimentoTit);
                Parametros.Append("&codEspDocumento=");
                Parametros.Append(codEspDocumento.Trim());
                Parametros.Append("&valorTitulo=");
                Parametros.Append(valorTitulo);
                Parametros.Append("&valorAbatimento=");
                Parametros.Append(valorAbatimento);
                Parametros.Append("&valorIOF=");
                Parametros.Append(valorIOF.TrimEnd());
                Parametros.Append("&nomeSacado=");
                Parametros.Append(nomeSacado.TrimEnd());
                Parametros.Append("&cpfCGC=");
                Parametros.Append(cpfCGC.TrimEnd());
                Parametros.Append("&endereco=");
                Parametros.Append(endereco.TrimEnd());
                Parametros.Append("&bairro=");
                Parametros.Append(bairro);
                Parametros.Append("&cidade=");
                Parametros.Append(cidade.TrimEnd());
                Parametros.Append("&cep=");
                Parametros.Append(cep);
                Parametros.Append("&uf=");
                Parametros.Append(uf.TrimEnd());
                Parametros.Append("&codMunicipio=1009");
                //Parametros.Append(codMunicipio);
               

                if (Instrucao1.TrimEnd() != "NULO")
                {
                    Parametros.Append("&descInstrucao1=");
                    Parametros.Append(Instrucao1.TrimEnd());
                }

                if (Instrucao2.TrimEnd() != "NULO")
                {
                    Parametros.Append("&descInstrucao2=");
                    Parametros.Append(Instrucao2.TrimEnd());
                }

                if (Instrucao3.TrimEnd() != "NULO")
                {
                    Parametros.Append("&descInstrucao3=");
                    Parametros.Append(Instrucao3.TrimEnd());
                }

                if (Instrucao4.TrimEnd() != "NULO")
                {
                    Parametros.Append("&descInstrucao4=");
                    Parametros.Append(Instrucao4.TrimEnd());
                }

                    
                if(Instrucao5.TrimEnd()  != "NULO")
                {
                    Parametros.Append("&descInstrucao5=");
                    Parametros.Append(Instrucao5.TrimEnd());
                }

                Parametros.Append("&seuNumero=");
                Parametros.Append(Cobranca);
                Parametros.Append("&percTaxaMulta=2.0");
               
                Parametros.Append("&percTaxaMora=1.0");
                //Parametros.Append(1);

                String query = Parametros.ToString();

                // Declarações necessárias
                Stream requestStream = null;
                WebResponse response = null;
                StreamReader reader = null;

                try
                {
                    WebRequest request = WebRequest.Create(url);
                    request.Method = WebRequestMethods.Http.Post;

                    // Neste ponto, você está setando a propriedade ContentType da página
                    // para urlencoded para que o comando POST seja enviado corretamente
                    request.ContentType = "application/x-www-form-urlencoded";

                    StringBuilder urlEncoded = new StringBuilder();

                    // Separando cada parâmetro
                    Char[] reserved = { '?', '=', 'a' };

                    // alocando o bytebuffer
                    byte[] byteBuffer = null;

                    // caso a URL seja preenchida
                    if (query != null)
                    {
                        int i = 0, j;
                        // percorre cada caractere da url atraz das palavras reservadas para separação
                        // dos parâmetros
                        while (i < query.Length)
                        {
                            j = query.IndexOfAny(reserved, i);
                            if (j == -1)
                            {
                                urlEncoded.Append(query.Substring(i, query.Length - i));
                                break;
                            }
                            urlEncoded.Append(query.Substring(i, j - i));
                            urlEncoded.Append(query.Substring(j, 1));
                            i = j + 1;
                        }
                        // codificando em UTF8 (evita que sejam mostrados códigos malucos em caracteres especiais
                        byteBuffer =  Encoding.UTF8.GetBytes(urlEncoded.ToString());

                        request.ContentLength = byteBuffer.Length;
                        requestStream = request.GetRequestStream();
                        requestStream.Write(byteBuffer, 0, byteBuffer.Length);
                        requestStream.Close();
                    }
                    else
                    {
                        request.ContentLength = 0;
                    }

                    // Dados recebidos
                    response = request.GetResponse();
                    Stream responseStream = response.GetResponseStream();

                    //Codifica os caracteres especiais para que possam ser exibidos corretamente
                    System.Text.Encoding encoding = System.Text.Encoding.Default;

                    //Preenche o reader
                    reader = new StreamReader(responseStream, encoding);

                    Char[] charBuffer = new Char[256];
                    int count = reader.Read(charBuffer, 0, charBuffer.Length);

                    // Lê cada byte para preencher meu stringbuilder
                    while (count > 0)
                    {
                        StringBuilder Linha = new StringBuilder();
                        Linha.Append(new String(charBuffer, 0, count));

                        Linha.Replace("src='sicooblogo.gif'", "src='https://geraboleto.sicoobnet.com.br/geradorBoleto/sicooblogo.gif'");
                        Linha.Replace("linhaPontilhada.JPG", "https://geraboleto.sicoobnet.com.br/geradorBoleto/linhaPontilhada.JPG");
                        Linha.Replace("src='sicooblogo.gif'", "src='https://geraboleto.sicoobnet.com.br/geradorBoleto/sicooblogo.gif'");

                        Dados.Append(Linha.ToString());

                        count = reader.Read(charBuffer, 0, charBuffer.Length);
                    }

                    string vNumeroBoleto = string.Empty;
                    string vNossoNumero = string.Empty;

                    var dadosBoleto = Dados.ToString();

                    var inicioPosicaoLinhaDigitavel = dadosBoleto.IndexOf("class='fonteMedia'>") + 19;
                    var fimPosicaoLinhaDigitavel = dadosBoleto.IndexOf("</", inicioPosicaoLinhaDigitavel);

                    var linhaDigitavel = dadosBoleto.Substring(inicioPosicaoLinhaDigitavel, (fimPosicaoLinhaDigitavel - inicioPosicaoLinhaDigitavel)).Trim();
                    linhaDigitavel = linhaDigitavel.Replace("  ", " ");

                    vNumeroBoleto = linhaDigitavel;

                    vNossoNumero = vNumeroBoleto.Replace(" ", "");
                    vNossoNumero = vNossoNumero.Substring(23, 8);
                    vNossoNumero = vNossoNumero.Replace(".", "");
                    vNossoNumero = vNossoNumero.TrimStart('0');

                    Dados.Replace("img1.JPG", "https://geraboleto.sicoobnet.com.br/geradorBoleto/img1.JPG");
                    Dados.Replace("img2.JPG", "https://geraboleto.sicoobnet.com.br/geradorBoleto/img2.JPG");
                    BoletoCarne.Replace("img1.JPG", "https://geraboleto.sicoobnet.com.br/geradorBoleto/img1.JPG");
                    BoletoCarne.Replace("img2.JPG", "https://geraboleto.sicoobnet.com.br/geradorBoleto/img2.JPG");

                    Dados.Replace("'", "\"");
                    BoletoCarne.Replace("'", "\"");
                    RetornoBoleto.Agencia       = Banco.TB018_Agencia;
                    RetornoBoleto.Conta         = Banco.TB018_Cliente;
                    RetornoBoleto.Carteira      = vNumeroBoleto.Substring(4,1);
                    RetornoBoleto.NossoNumero   = vNossoNumero;
                    RetornoBoleto.NumeroBoleto  = vNumeroBoleto;
                    RetornoBoleto.HTML          = Dados.ToString();
                    //RetornoBoleto.BoletoCarne   = Dados.ToString();
                }
                catch (Exception ex)
                {
                    // Ocorreu algum erro
                    RetornoBoleto.Erro = 1;
                    RetornoBoleto.ErroDesc = ex.Message;
                }
                finally
                {
                    // Fecha tudo
                    if (requestStream != null)
                        requestStream.Close();
                    if (response != null)
                        response.Close();
                    if (reader != null)
                        reader.Close();
                }
            }
            catch (Exception ex)
            {
                RetornoBoleto.Erro = 1;
                RetornoBoleto.ErroDesc = ex.Message;
            }

            return RetornoBoleto;
        }
    }
}