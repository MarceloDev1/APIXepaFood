﻿namespace Domain.Requests
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Localizacao { get; set; }
        public string Telefone { get; set; }
        public bool Feirante { get; set; }
    }
}