﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroLivros.Application.Queries.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
