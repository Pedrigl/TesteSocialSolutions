using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TesteTecnicoImobiliaria.Modelo.Interfaces.Regra;
using TesteTecnicoImobiliaria.Modelo.ViewModels;

namespace TesteTecnicoImobiliaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IRnCliente rnCliente;
        private readonly IRnImovel rnImovel;

        public ClienteController(IRnCliente rnCliente, IRnImovel rnImovel)
        {
            this.rnCliente = rnCliente;
            this.rnImovel = rnImovel;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public IEnumerable<ClienteViewModel> Get()
        {
            return rnCliente.ListarClientes();
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public ClienteViewModel Get(int id)
        {
            return rnCliente.SelecionarCliente(id);
        }

        // POST api/<ClienteController>
        [HttpPost]
        public void Post([FromBody] ClienteViewModel cliente)
        {
            var cnpjValido = !cliente.CNPJ.IsNullOrEmpty() ? rnCliente.VerificarSeCnpjEValido(cliente.CNPJ) : false;
            var cpfValido = !cliente.CPF.IsNullOrEmpty() ? rnCliente.VerificarSeCpfEValido(cliente.CPF) : false;

            if(cnpjValido || cpfValido)
                rnCliente.SalvarCliente(cliente);
            
        }

        // POST api/<ClienteController>/Ativar/5
        [HttpPost]
        [Route("Ativar/{id}")]
        public void AtivarCliente(int id)
        {
            rnCliente.AtivarCliente(id);
        }

        // POST api/<ClienteController>/Desativar/5
        [HttpPost]
        [Route("Desativar/{id}")]
        public void DesativarCliente(int id)
        {
            var cliente = rnCliente.SelecionarCliente(id);
            if(cliente != null)
            {
                var imoveis = rnImovel.ListarImoveis();
                if(imoveis.Any(x => x.IdCliente == id))
                {
                    throw new Exception("Não é possível desativar um cliente que possui imóveis cadastrados.");
                }
            }
            
            rnCliente.DesativarCliente(id);
        }
    }
}
