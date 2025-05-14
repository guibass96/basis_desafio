using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;
using MediatR;

namespace CadastroLivros.Application.Command.Books.UpdateBook
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int Edition { get; set; }
        public string PublicationYear { get; set; }
        public List<BookPurchaseOptionEntity> PurchaseOptions { get; set; } = new();

    }
}
