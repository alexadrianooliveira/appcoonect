using System.ComponentModel.DataAnnotations;
namespace ConectaLoja.Models
{
    public class Endereco
    {
        private int _id = 0;
        private int _cadastroid = 0;
        private string _logradouro = "";
        private string _logradouronumero = "";
        private string _bairro = "";
        private string _cep = "";
        private int _masterid = 0;
        private int _cidadeid = 0;
        private string _complemento = "";
        private string _cidadeNome = "";


        public int Id { get { return _id; } set { _id = value; } }
        public int Cadastroid { get { return _cadastroid; } set { _cadastroid = value; } }
        public string Logradouro { get { return _logradouro; } set { _logradouro = value; } }
        public string Logradouronumero { get { return _logradouronumero; } set { _logradouronumero = value; } }
        public string Bairro { get { return _bairro; } set { _bairro = value; } }
        public string Cep { get { return _cep; } set { _cep = value; } }
        public int Masterid { get { return _masterid; } set { _masterid = value; } }
        public int Cidadeid { get { return _cidadeid; } set { _cidadeid = value; } }
        public string Complemento { get { return _complemento; } set { _complemento = value; } }
        public string Cidade { get { return _cidadeNome; } set { _cidadeNome = value; } }
    }
}