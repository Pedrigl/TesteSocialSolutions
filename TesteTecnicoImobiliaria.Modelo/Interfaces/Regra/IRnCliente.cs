using TesteTecnicoImobiliaria.Modelo.ViewModels;

namespace TesteTecnicoImobiliaria.Modelo.Interfaces.Regra
{
    public interface IRnCliente
    {
        ClienteViewModel SelecionarCliente(int id);
        void SalvarCliente(ClienteViewModel cliente);
        List<ClienteViewModel> ListarClientes();
        void DesativarCliente(int id);
        void AtivarCliente(int id);
        bool VerificarSeCnpjEValido(string cnpj);
        bool VerificarSeCpfEValido(string cpf);
    }
}
