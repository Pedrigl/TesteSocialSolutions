using Microsoft.AspNetCore.Mvc;
using TesteTecnicoImobiliaria.WEB.Models;

namespace TesteTecnicoImobiliaria.WEB.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteViewModel _clienteViewModel;
        public ClientesController(ClienteViewModel clienteViewModel)
        {
            _clienteViewModel = new ClienteViewModel();
        }
        public IActionResult Index()
        {
            _clienteViewModel.Clientes = new List<ClienteModel>();
            _clienteViewModel.Clientes.Add(new ClienteModel()
            {
                Id = 1,
                Nome = "Cliente 1",
                Email = ""
            });

            return View(_clienteViewModel);
        }

    }
}
