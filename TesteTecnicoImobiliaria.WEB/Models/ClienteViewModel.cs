using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TesteTecnicoImobiliaria.WEB.Models
{
    public class ClienteViewModel
    {
        public List<ClienteModel>  Clientes { get; set; }
        public ClienteModel Cliente { get; set; }
    }

    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? CPF { get; set; }
        public string? CNPJ { get; set; }
        public bool Ativo { get; set; }
    }

}
