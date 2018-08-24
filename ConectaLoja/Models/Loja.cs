using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaLoja.Models
{
    public class Loja
    {
        private int _masterID = 0;
        string _email = "";
        string _senha = "";

        public int MasterID { get { return _masterID; } set { _masterID = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Senha { get { return _senha; } set { _senha = value; } }
    }
}
