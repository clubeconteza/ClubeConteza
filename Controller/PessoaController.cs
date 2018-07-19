using System;
using System.Collections.Generic;

namespace Controller
{
    public class PessoaController
    {
        public long     TB040_id                    { get; set; }
        public long     TB013_id                    { get; set; }
        public int      TB012_EraContezino          { get; set; }
        public long     TB012_Corporativo           { get; set; }
        public long     TB012_Parceiro              { get; set; }       
        public string   TB013_Matricula             { get; set; }
        public string   TB013_IdProtheus            { get; set; }
        public string   TB013_CPFCNPJ               { get; set; }
        public int      TB013_ListaNegra            { get; set; }
        public string   TB013_NomeCompleto          { get; set; }
        public string   TB013_NomeExibicao          { get; set; }
        public string   TB013_NomeExibicaoDetalhes  { get; set; }
        public string   TB013_RG                    { get; set; }
        public string   TB013_InformacoesPortal     { get; set; }
        public string   TB013_RGOrgaoEmissor        { get; set; }
        public DateTime TB013_DataNascimento        { get; set; }
        public int      TB013_DeclaroSerMaiorCapaz  { get; set; }
        public string   TB004_Cep                   { get; set; }
        public string   TB013_Logradouro            { get; set; }
        public string   TB013_Numero                { get; set; }
        public string   TB013_Bairro                { get; set; }
        public string   TB013_Complemento           { get; set; }
        public DateTime TB013_CadastradoEm          { get; set; }
        public Int64    TB013_CadastradoPor         { get; set; }
        public string   TB013_CadastradoPorNome     { get; set; }
        public string   TB013_AlteradoPorNome       { get; set; }
        public DateTime TB013_AlteradoEm            { get; set; }
        public Int64    TB013_AlteradoPor           { get; set; }
        public double   TB013_CodigoDependente      { get; set; }
        public string   TB013_Cartao                { get; set; }
        public DateTime TB013_CartaoEntregueEm      { get; set; }
        public int      TB013_CartaoLote            { get; set; }
        public string   TB013_CartaoEntreguePara    { get; set; }
        public string   TB013_SexoS                 { get; set; }
        public int      TB013_Idade                 { get; set; }
        public string   TB002_Ponto                 { get; set; }
        public string   TB013_CancelamentoDescricao { get; set; }
        public string   Celular                     { get; set; }
        public string   fixo                        { get; set; }
        public string   email                       { get; set; }


        public long     IdCelular                   { get; set; }
        public long     IdFixo                      { get; set; }
        public long     IdEmail                     { get; set; }
        public int      Selecionado                 { get; set; }

        public int      TB012_ProximoCodDependente  { get; set; }
        public int      TB013_CorporativoAtivado    { get; set; }
        public string   TB013_MaeNome               { get; set; }
        public DateTime TB013_MaeDataNascimento     { get; set; }
        public string   TB013_PaiNome               { get; set; }
        public DateTime TB013_PaiDataNascimento     { get; set; }
        public string   TB013_CartaoChip            { get; set; }           
        public string   TB013_CartaoChipStatusS     { get; set; }
        public enum TB013_CartaoChipStatusSE
        {
            Solicitado      = 1,
            Gerado          = 2,
            Entregue        = 3,
            Bloqueado       = 4,
            Desbloqueado    = 5,
            Cancelado       = 6
        }

        public enum TB013_SexoE
        {
            Feminino = 1,
            Masculino = 2//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         = 2
        }

        public string TB013_TipoS { get; set; }
        public enum TB013_TipoE
        {
            Fisica      = 1,
            Juridica    = 2
        }

        public string TB013_StatusS { get; set; }
        public enum TB013_StatusE
        {
            Cadastrado = 0,
            Ativo = 1,
            Inativo = 2, //Não Alerar esta codigo.    
            Inativar = 3
        }

        public string TB033_Senha { get; set; }

        /*Parceiro TB034*/

        public long     TB034_Id            { get; set; }
        public string   TB034_Parceiro      { get; set; }
        public string   TB034_CNPJ          { get; set; }
        public string   TB034_Senha         { get; set; }
        public int      TB034_ImpContezinos { get; set; }
        public int      TB034_ImpParceiros  { get; set; }

        public string   TB013_CarteirinhaStatusS { get; set; }

        public enum     TB013_CarteirinhaStatusE
        {
            Pendente    = 0,
            Gerado      = 1,
            Impressão   = 2,
            Encaminhado = 3,
            Entregue    = 4,
            Disponivel  = 5,
            Manual      = 6,
            Financeiro  = 7,
            Legado      = 8
        }

        public int      TB013_CartaoSolicitado { get; set; }
        
        public string   TB013_CancelamentoMotivoS { get; set; }
        public enum     TB013_CancelamentoMotivoE
        {
            Obto = 1,
            Idade = 2,
            Outros = 3
        }


        public string   TB010_Perfil        { get; set; }

        public string   TB009_Contato       { get; set; }

        
        public List<ContatoController>  Contatos { get; set; }
        public ContatoController        Contato { get; set; }
        public ContratosController      Contrato { get; set; }
        public Int64                    TB012_Id { get; set; }
        public MunicipioController      Municipio { get; set; }

        public PaisController           Pais { get; set; }

        public EstadoController         Estado { get; set; }

        public PontoDeVendaController   PontoDeVenda { get; set; }

    }
}
