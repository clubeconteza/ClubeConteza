using Controller;
using DAO;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class ComissaoNegocios
    {

        public bool ComissaoProcessamento(long tb011Id)
        {
            try
            {
                var parcelasAdesao = new ParcelaDao().ParcelasComissaoNaoProcessadaAdesao();

                var comissaoPontoDeVenda = new List<ComissaoController>();

                #region Comissão ADESÃO Ponto de Venda




                for (int i = 0; i < parcelasAdesao.Count; i++)
                {
                    var pontoDeVenda = new PontoDeVendaDao().PontoDeVenda(parcelasAdesao[i].Contrato.TB002_id);

                    var objComissaoPontoDeVenda = new ComissaoController();
                    objComissaoPontoDeVenda.Tb035DataReferencia = parcelasAdesao[i].TB016_DataPagamento;
                    objComissaoPontoDeVenda.Tb002Id = pontoDeVenda.TB002_id;
                    objComissaoPontoDeVenda.Tb011Id = tb011Id;
                    objComissaoPontoDeVenda.Tb012Id = parcelasAdesao[i].TB012_id;
                    objComissaoPontoDeVenda.Tb016Id = parcelasAdesao[i].TB016_id;
                    objComissaoPontoDeVenda.Tb035StatusS = "1";
                    /*Familiar*/
                    if (parcelasAdesao[i].Contrato.TB012_TipoContrato == 1)
                    {
                        //Adesão
                        if (Convert.ToInt16(parcelasAdesao[i].Produto.TB017_TipoS) == 1)
                        {
                            if (Convert.ToInt16(pontoDeVenda.Tb002FamiliarAdesaoFormaS) == 1)//Fixo
                            {
                                objComissaoPontoDeVenda.Tb035FamiliarAdesao = pontoDeVenda.Tb002FamiliarAdesaoValor;
                                comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                            }
                            else
                            {
                                if (Convert.ToInt16(pontoDeVenda.Tb002FamiliarAdesaoFormaS) == 2)//Aliquota
                                {
                                    objComissaoPontoDeVenda.Tb035FamiliarAdesao = (parcelasAdesao[i].Produto.TB017_ValorFinal * pontoDeVenda.Tb002FamiliarMensalidadeAliquota) / 100;
                                    comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                                }
                            }
                        }

                    }
                    /*Parceiro#############################################################################################################################################################*/
                    if (parcelasAdesao[i].Contrato.TB012_TipoContrato == 2)
                    {
                        //Adesão
                        if (Convert.ToInt16(parcelasAdesao[i].Produto.TB017_TipoS) == 1)
                        {
                            if (Convert.ToInt16(pontoDeVenda.Tb002ParceiroAdesaoFormaS) == 1)//Fixo
                            {
                                objComissaoPontoDeVenda.Tb035ParceiroAdesao = pontoDeVenda.Tb002ParceiroAdesaoValor;
                                comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                            }
                            else
                            {
                                if (Convert.ToInt16(pontoDeVenda.Tb002ParceiroAdesaoFormaS) == 2)//Aliquota
                                {
                                    objComissaoPontoDeVenda.Tb035ParceiroAdesao = (parcelasAdesao[i].Produto.TB017_ValorFinal * pontoDeVenda.Tb002ParceiroAdesaoAliquota) / 100;
                                    comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                                }
                            }
                        }

                    }
                    /*Corporativo*/
                    if (parcelasAdesao[i].Contrato.TB012_TipoContrato == 3)
                    {
                        //Adesão
                        if (Convert.ToInt16(parcelasAdesao[i].Produto.TB017_TipoS) == 1)
                        {
                            if (Convert.ToInt16(pontoDeVenda.Tb002CorporativoAdesaoFormaS) == 1)//Fixo
                            {
                                objComissaoPontoDeVenda.Tb035CorporativoAdesao = pontoDeVenda.Tb002CorporativoAdesaoValor;
                                comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                            }
                            else
                            {
                                if (Convert.ToInt16(pontoDeVenda.Tb002CorporativoAdesaoFormaS) == 2)//Aliquota
                                {
                                    objComissaoPontoDeVenda.Tb035CorporativoAdesao = (parcelasAdesao[i].Produto.TB017_ValorFinal * pontoDeVenda.Tb002CorporativoAdesaoAliquota) / 100;
                                    comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                                }
                            }
                        }
                    }

                }
                #endregion

                /***************************/

                #region Comissão MENSALIDADE Ponto de Venda
                var parcelasMensalidade = new ParcelaDao().ParcelasComissaoNaoProcessadaMemsalidade();



                for (int i = 0; i < parcelasMensalidade.Count; i++)
                {
                    var pontoDeVenda = new PontoDeVendaDao().PontoDeVenda(parcelasMensalidade[i].Contrato.TB002_id);

                    var objComissaoPontoDeVenda = new ComissaoController();
                    objComissaoPontoDeVenda.Tb035DataReferencia = parcelasMensalidade[i].TB016_DataPagamento;
                    objComissaoPontoDeVenda.Tb002Id = pontoDeVenda.TB002_id;
                    objComissaoPontoDeVenda.Tb011Id = tb011Id;
                    objComissaoPontoDeVenda.Tb012Id = parcelasMensalidade[i].TB012_id;
                    objComissaoPontoDeVenda.Tb016Id = parcelasMensalidade[i].TB016_id;
                    objComissaoPontoDeVenda.Tb035StatusS = "1";


                    /*Familiar*/
                    if (parcelasMensalidade[i].Contrato.TB012_TipoContrato == 1)
                    {
                        
                        if (Convert.ToInt16(parcelasMensalidade[i].Produto.TB017_TipoS) == 2)
                        {
                            if (Convert.ToInt16(pontoDeVenda.Tb002FamiliarAdesaoFormaS) == 1)//Fixo
                            {
                                objComissaoPontoDeVenda.Tb035FamiliarMensalidade = pontoDeVenda.Tb002FamiliarMensalidadeValor * parcelasMensalidade[i].TB016_ParcelasAgrupadas;
                                comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                            }
                            else
                            {
                                if (Convert.ToInt16(pontoDeVenda.Tb002FamiliarAdesaoFormaS) == 2)//Aliquota
                                {
                                    objComissaoPontoDeVenda.Tb035FamiliarMensalidade = (parcelasMensalidade[i].Produto.TB017_ValorFinal * pontoDeVenda.Tb002FamiliarMensalidadeAliquota) / 100;
                                    comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                                }
                            }
                        }

                    }
                    /*Parceiro#############################################################################################################################################################*/
                    if (parcelasMensalidade[i].Contrato.TB012_TipoContrato == 2)
                    {
              
                        if (Convert.ToInt16(parcelasMensalidade[i].Produto.TB017_TipoS) == 2)
                        {
                            if (Convert.ToInt16(pontoDeVenda.Tb002ParceiroAdesaoFormaS) == 1)//Fixo
                            {
                                objComissaoPontoDeVenda.Tb035ParceiroMensalidade = pontoDeVenda.Tb002FamiliarAdesaoValor * parcelasMensalidade[i].TB016_ParcelasAgrupadas;
                                comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                            }
                            else
                            {
                                if (Convert.ToInt16(pontoDeVenda.Tb002ParceiroAdesaoFormaS) == 2)//Aliquota
                                {
                                    objComissaoPontoDeVenda.Tb035ParceiroMensalidade = (parcelasMensalidade[i].Produto.TB017_ValorFinal * pontoDeVenda.Tb002ParceiroMensalidadeAliquota) / 100;
                                    comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                                }
                            }
                        }

                    }
                    /*Corporativo*/
                    if (parcelasMensalidade[i].Contrato.TB012_TipoContrato == 3)
                    {

                        if (Convert.ToInt16(parcelasMensalidade[i].Produto.TB017_TipoS) == 2)
                        {
                            if (Convert.ToInt16(pontoDeVenda.Tb002CorporativoAdesaoFormaS) == 1)//Fixo
                            {
                                objComissaoPontoDeVenda.Tb035CorporativoMensalidade = pontoDeVenda.Tb002CorporativoAdesaoValor * parcelasMensalidade[i].TB016_ParcelasAgrupadas;
                                comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                            }
                            else
                            {
                                if (Convert.ToInt16(pontoDeVenda.Tb002CorporativoAdesaoFormaS) == 2)//Aliquota
                                {
                                    objComissaoPontoDeVenda.Tb035CorporativoMensalidade = (parcelasMensalidade[i].Produto.TB017_ValorFinal * pontoDeVenda.Tb002CorporativoAdesaoAliquota) / 100;
                                    comissaoPontoDeVenda.Add(objComissaoPontoDeVenda);
                                }
                            }
                        }
                    }

                }
                #endregion
                /****************************/

                return true;
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }
    }
}
