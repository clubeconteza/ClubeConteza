using Controller;
using DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Negocios
{
    public class SicoobNegocios
    {
        public ParametrosBoletos Emissao240(
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

            StringBuilder Dados = new StringBuilder();
            ParametrosBoletos RetornoBoleto = new ParametrosBoletos();

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
                Parametros.Append("&&");
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
                Parametros.Append(valorIOF);


                Parametros.Append("&nomeSacado=");
                Parametros.Append(nomeSacado);
                Parametros.Append("&cpfCGC=");
                Parametros.Append(cpfCGC);
                Parametros.Append("&endereco=");
                Parametros.Append(endereco);
                Parametros.Append("&bairro=");
                Parametros.Append(bairro);
                Parametros.Append("&cidade=");
                Parametros.Append(cidade);
                Parametros.Append("&cep=");
                Parametros.Append(cep);
                Parametros.Append("&uf=");
                Parametros.Append(uf);
                Parametros.Append("&codMunicipio=1009");


                Parametros.Append("&descInstrucao1=");
                Parametros.Append(Instrucao1.TrimEnd());

                Parametros.Append("&descInstrucao2=");
                Parametros.Append(Instrucao2.TrimEnd());

                Parametros.Append("&descInstrucao3=");
                Parametros.Append(Instrucao3.TrimEnd());

                Parametros.Append("&descInstrucao4=");
                Parametros.Append(Instrucao4.TrimEnd());

                Parametros.Append("&descInstrucao5=");
                Parametros.Append(Instrucao5.TrimEnd());

                Parametros.Append("&seuNumero=");
                Parametros.Append(Cobranca);


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
                        byteBuffer = Encoding.UTF8.GetBytes(urlEncoded.ToString());

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

                        //if (Linha.ToString().Contains("Nosso"))
                        //{
                        //    //string Variavel = Linha.ToString();
                        //    //Variavel = Variavel.Replace("<tr>", "");
                        //    //Variavel = Variavel.Replace("</tr>", "");
                        //    //Variavel = Variavel.Replace("dth='100%' border='0' cellspacing='2' cellpadding='1'>", "");
                        //    //Variavel = Variavel.Replace("<td scope='col'><span class='fonteForm'>Nosso N&uacute;mero</span></td>", "");
                        //    //Variavel = Variavel.Replace("<td align='center'", "");
                        //    //Variavel = Variavel.Replace("</td>", "");
                        //    //Variavel = Variavel.Replace(" ", "");

                        //    //int posicaoEncontrada = Variavel.IndexOf(">", 5, StringComparison.Ordinal);

                        //    //if (Variavel.Length > posicaoEncontrada + 5)
                        //    //{

                        //    //    if (NossoNumero.Length == 1)
                        //    //    {
                        //    //        NossoNumero = Variavel.Substring(posicaoEncontrada + 1, 5);
                        //    //        RetornoBoleto.NumeroBoleto = NossoNumero;
                        //    //    }
                        //    //}
                        //}

                        Linha.Replace("src='sicooblogo.gif'", "src='https://geraboleto.sicoobnet.com.br/geradorBoleto/sicooblogo.gif'");
                        Linha.Replace("linhaPontilhada.JPG", "https://geraboleto.sicoobnet.com.br/geradorBoleto/linhaPontilhada.JPG");
                        Linha.Replace("src='sicooblogo.gif'", "src='https://geraboleto.sicoobnet.com.br/geradorBoleto/sicooblogo.gif'");
                        Dados.Append(Linha.ToString());
                        count = reader.Read(charBuffer, 0, charBuffer.Length);
                    }

                    Dados.Replace("img1.JPG", "https://geraboleto.sicoobnet.com.br/geradorBoleto/img1.JPG");
                    Dados.Replace("img2.JPG", "https://geraboleto.sicoobnet.com.br/geradorBoleto/img2.JPG");

                    Dados.Replace("'", "\"");

                    RetornoBoleto.HTML = Dados.ToString();
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
