using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConectaLoja.Models
{
    public class Cliente
    {
        private int _id = 0;
        private string _nome = "";
        private string _email = "";
        private string _cpf = "";
        private string _telefone = "";
        private string _senha = "";
        private int _usuarioID = 0;

        public int ID { get { return _id; } set { _id = value; } }
        public string Nome { get { return _nome; } set { _nome = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string CPF { get { return _cpf; } set { _cpf = value; } }
        public string Telefone { get { return _telefone; } set { _telefone = value; } }
        public string Senha { get { return _senha; } set { _senha = value; } }
        public int UsuarioID { get { return _usuarioID; } set { _usuarioID = value; } }
    }
}
