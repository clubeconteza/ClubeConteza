using Controller;
using Controller.Enums;
using DAO;
using DAO.Infrastructure;
using FluentValidation.Results;
using Negocios.Utilities.Format;
using Negocios.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Negocios
{
    public class UnidadesViewModelNegocios
    {
        public ContratosModelDAO ContratosModel { get; set; }
        public EstadosModelDAO EstadosModel { get; set; }
        public MunicipiosModelDAO MunicipiosModel { get; set; }
        public PaisModelDAO PaisModel { get; set; }
        public PessoasModelDAO PessoasModel { get; set; }
        public UnidadesModelDAO UnidadesModel { get; set; }

        public ContratosModelController Contratos
        {
            get
            {
                return ContratosModel.Contratos;
            }
            set
            {
                ContratosModel.Contratos = value;
            }
        }

        public PessoasModelController Pessoas
        {
            get
            {
                return PessoasModel.Pessoas;
            }
            set
            {
                PessoasModel.Pessoas = value;
            }
        }

        public UnidadesModelController Unidades
        {
            get
            {
                return UnidadesModel.Unidades;
            }
            set
            {
                UnidadesModel.Unidades = value;
            }
        }

        public UnidadesViewModelNegocios()
        {
            var conexao = new ConexaoFabrica().CriarConexao();
            ContratosModel = new ContratosModelDAO(conexao);
            EstadosModel = new EstadosModelDAO(conexao);
            MunicipiosModel = new MunicipiosModelDAO(conexao);
            UnidadesModel = new UnidadesModelDAO(conexao);
            PaisModel = new PaisModelDAO(conexao);
            PessoasModel = new PessoasModelDAO(conexao);
        }

        public DataTable ListarCorporativo(string filtroSelecionado, string pesquisa)
        {
            var filtro = Utilities.Utility.GetEnumByDescription<CorporativoFiltro>(filtroSelecionado);
            if (filtro == CorporativoFiltro.Status && !string.IsNullOrEmpty(pesquisa))
            {
                pesquisa = ListarUnidadesStatusFiltro().Exists(x => x.ToUpper() == pesquisa.ToUpper()) ? ((int)Utilities.Utility.GetEnumByDescription<UnidadesStatus>(pesquisa)).ToString() : "-1";
            }
            var lista = UnidadesModel.ListarCorporativo(filtro, pesquisa.Trim());
            foreach (DataRow linha in lista.Rows)
            {
                linha.SetField(CorporativoFiltro.Cnpj.ToString(), new CNPJFormatter().Format(linha.Field<string>(CorporativoFiltro.Cnpj.ToString()).Trim()));
                linha.SetField(CorporativoFiltro.Status.ToString(), Utilities.Utility.GetDescription((UnidadesStatus)int.Parse(linha.Field<string>(CorporativoFiltro.Status.ToString()))));
            }
            return lista;
        }

        public List<string> ListarCorporativoFiltro()
        {
            var lista = new List<string>();
            Utilities.Utility.EnumToList<CorporativoFiltro>().ForEach(item => lista.Add(Utilities.Utility.GetDescription(item)));
            return lista;
        }

        public List<string> ListarUnidadesStatusFiltro()
        {
            var lista = new List<string>();
            Utilities.Utility.EnumToList<UnidadesStatus>().ForEach(item => lista.Add(Utilities.Utility.GetDescription(item)));
            return lista;
        }

        public List<string> ListarContratosStatus()
        {
            var lista = new List<string>();
            Utilities.Utility.EnumToList<ContratosStatus>().ForEach(item => lista.Add(Utilities.Utility.GetDescription(item)));
            return lista;
        }

        public List<string> ListarPessoasSexoTitular()
        {
            var lista = new List<string>();
            Utilities.Utility.EnumToList<PessoasSexo>().ForEach(item => lista.Add(Utilities.Utility.GetDescription(item)));
            return lista;
        }

        public bool SelecionarFiltroTodos(string filtroSelecionado)
        {
            var filtro = Utilities.Utility.GetEnumByDescription<CorporativoFiltro>(filtroSelecionado);
            return filtro != CorporativoFiltro.Todos;
        }

        public string SelecionarFiltroTodos(string filtroSelecionado, string pesquisa)
        {
            return SelecionarFiltroTodos(filtroSelecionado) ? pesquisa : string.Empty;
        }

        public bool SelecionarFiltroStatus(string filtroSelecionado)
        {
            var filtro = Utilities.Utility.GetEnumByDescription<CorporativoFiltro>(filtroSelecionado);
            return filtro == CorporativoFiltro.Status;
        }

        public int BuscarSexoPessoaTitular(string sexoSelecionado)
        {
            var sexo = Utilities.Utility.GetEnumByDescription<PessoasSexo>(sexoSelecionado);
            return (int)sexo;
        }

        public List<KeyValuePair<long, string>> ListarPais()
        {
            var lista = PaisModel.ListarPais().AsEnumerable().ToDictionary(linha => linha.Field<long>(0), linha => linha.Field<string>(1).Trim()).ToList();
            lista.Insert(0, new KeyValuePair<long, string>(0, "Selecione"));
            return lista;
        }

        public List<KeyValuePair<long, string>> ListarEstadosPorPais(long idPais)
        {
            var lista = EstadosModel.ListarEstadosPorPais(idPais).AsEnumerable().ToDictionary(linha => linha.Field<long>(0), linha => linha.Field<string>(1).Trim()).ToList();
            lista.Insert(0, new KeyValuePair<long, string>(0, "Selecione"));
            return lista;
        }

        public List<KeyValuePair<long, string>> ListarMunicipiosPorEstado(long idEstado)
        {
            var lista = MunicipiosModel.ListarMunicipiosPorEstado(idEstado).AsEnumerable().ToDictionary(linha => linha.Field<long>(0), linha => linha.Field<string>(1).Trim()).ToList();
            lista.Insert(0, new KeyValuePair<long, string>(0, "Selecione"));
            return lista;
        }

        public Dictionary<string, string> SalvarCorporativoEmpresa()
        {
            var validarContrato = new ContratosValidator();
            var validarPessoa = new PessoasValidator();
            var validarUnidade = new UnidadesValidator();
            var resultadoContrato = validarContrato.Validate(Contratos);
            var resultadoPessoa = validarPessoa.Validate(Pessoas);
            var resultadoUnidade = validarUnidade.Validate(Unidades);
            var falhas = resultadoUnidade.Errors.Union(resultadoContrato.Errors).Union(resultadoPessoa.Errors);
            var resultado = new ValidationResult(falhas);

            if (resultado.IsValid)
            {
                try
                {
                    ContratosModel.SalvarCorporativo();
                    UnidadesModel.SalvarCorporativo();
                    //Retornar para a lista ou manter no cadastro como edição
                    return RetornoMensagem("Sucesso", "Contrato Corporativo salvo com sucesso!", "64");
                }
                catch (Exception)
                {
                    return RetornoMensagem("Erro", "Não gravado!", "16");
                }
            }
            else
            {
                return RetornoMensagem("Aviso", resultado.Errors.First().ErrorMessage, "48");
            }
        }

        private Dictionary<string, string> RetornoMensagem(string titulo, string mensagem, string icone)
        {
            var retorno = new Dictionary<string, string>();
            retorno.Add("Titulo", titulo);
            retorno.Add("Mensagem", mensagem);
            retorno.Add("Icone", icone);
            return retorno;
        }
    }
}
