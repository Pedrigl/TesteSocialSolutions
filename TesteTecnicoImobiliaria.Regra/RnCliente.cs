using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TesteTecnicoImobiliaria.Modelo.Interfaces;
using TesteTecnicoImobiliaria.Modelo.Interfaces.Regra;
using TesteTecnicoImobiliaria.Modelo.Models;
using TesteTecnicoImobiliaria.Modelo.ViewModels;

namespace TesteTecnicoImobiliaria.Regra
{
    internal class RnCliente : IRnCliente
    {
        private readonly IMapper mapper;
        private readonly IClienteDAL clienteDAL;

        public RnCliente(IMapper mapper, IClienteDAL clienteDAL)
        {
            this.mapper = mapper;
            this.clienteDAL = clienteDAL;
        }

        public ClienteViewModel SelecionarCliente(int id)
        {
            ClienteModel clienteModel = clienteDAL.SelecionarCliente(id);
            var cliente = mapper.Map<ClienteViewModel>(clienteModel);

            return cliente;
        }

        public void AtivarCliente(int id)
        {
            clienteDAL.AtivarCliente(id);
        }

        public void DesativarCliente(int id)
        {
            clienteDAL.DesativarCliente(id);
        }

        public List<ClienteViewModel> ListarClientes()
        {
            var retorno = new List<ClienteViewModel>();
            var clientes = clienteDAL.ListarClientes();
            retorno = mapper.Map<List<ClienteViewModel>>(clientes);

            return retorno;
        }

        public void SalvarCliente(ClienteViewModel cliente)
        {

            ClienteModel clienteModel = mapper.Map<ClienteModel>(cliente);
            
            if (cliente.Id == 0)
            {
                clienteDAL.CadastrarCliente(clienteModel);
            }
            else
            {
                clienteDAL.AtualizarCliente(clienteModel);
            }
        }

        public bool VerificarSeCnpjEValido(string cnpj)
        {
            int[] multiplicadoresPrimeiroDigito = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadoresSegundoDigito = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            if (cnpj.Length != 14 || !long.TryParse(cnpj, out long cnpjNumerico))
            {
                return false;
            }

            string cnpjSemDigitosVerificadores = cnpj.Substring(0, 12);

            int soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(cnpjSemDigitosVerificadores[i].ToString()) * multiplicadoresPrimeiroDigito[i];
            }

            int resto = soma % 11;
            int primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            cnpjSemDigitosVerificadores += primeiroDigitoVerificador;

            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(cnpjSemDigitosVerificadores[i].ToString()) * multiplicadoresSegundoDigito[i];
            }

            resto = soma % 11;
            int segundoDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith(primeiroDigitoVerificador.ToString() + segundoDigitoVerificador.ToString());
        }


        public bool VerificarSeCpfEValido(string cpf)
        {
            int[] multiplicadoresPrimeiroDigito = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadoresSegundoDigito = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || !long.TryParse(cpf, out long cpfNumerico))
            {
                return false;
            }

            string cpfSemDigitosVerificadores = cpf.Substring(0, 9);

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpfSemDigitosVerificadores[i].ToString()) * multiplicadoresPrimeiroDigito[i];
            }

            int resto = soma % 11;
            int primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            cpfSemDigitosVerificadores += primeiroDigitoVerificador;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpfSemDigitosVerificadores[i].ToString()) * multiplicadoresSegundoDigito[i];
            }

            resto = soma % 11;
            int segundoDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(primeiroDigitoVerificador.ToString() + segundoDigitoVerificador.ToString());
        }

    }
}
