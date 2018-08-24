using System;
using System.Collections.Generic;


namespace ConectaLoja.Models
{
    public class Pedido
    {
        private int _id = 0;
        private int _masterid = 0;
        private DateTime _data;
        private double _totalvenda = 0;
        private double _desconto = 0;
        private double _acrescimo = 0;
        private double _totalrecebido = 0;
        private double _troco = 0;
        private string _email = "";
        private string _cpf = "";
        private DateTime _datacontabil;
        private int _enderecoentregaid = 0;
        private string _observaoentrega = "";
        private List<PedidoItem> _Itens = new List<PedidoItem>();
        private List<PedidoFormaRecebimento> _formaRecebe = new List<PedidoFormaRecebimento>();
        private Endereco _endereco = new Endereco();
        private Cliente _cliente = new Cliente();
        private int _idStatus = 0;
        private string _nomeStatus = "";
        private DateTime _dtStatus;

        public int ID { get { return _id; } set { _id = value; } }
        public int Masterid { get { return _masterid; } set { _masterid = value; } }
        public DateTime Data { get { return _data; } set { _data = value; } }
        public double Totalvenda { get { return _totalvenda; } set { _totalvenda = value; } }
        public double Desconto { get { return _desconto; } set { _desconto = value; } }
        public double Acrescimo { get { return _acrescimo; } set { _acrescimo = value; } }
        public double Totalrecebido { get { return _totalrecebido; } set { _totalrecebido = value; } }
        public double Troco { get { return _troco; } set { _troco = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Cpf { get { return _cpf; } set { _cpf = value; } }
        public DateTime Datacontabil { get { return _datacontabil; } set { _datacontabil = value; } }
        public int Enderecoentregaid { get { return _enderecoentregaid; } set { _enderecoentregaid = value; } }
        public string Observaoentrega { get { return _observaoentrega; } set { _observaoentrega = value; } }
        public List<PedidoItem> Itens { get { return _Itens; } set { _Itens = value; } }
        public Endereco EnderecoEntrega { get { return _endereco; } set { _endereco = value; } }
        public Cliente Cliente { get { return _cliente; } set { _cliente = value; } }
        public List<PedidoFormaRecebimento> FormasRecebimento { get { return _formaRecebe; } set { _formaRecebe = value; } }
        public int StatusID { get { return _idStatus; } set { _idStatus = value; } }
        public string StatusNome { get { return _nomeStatus; } set { _nomeStatus = value; } }
        public DateTime StatusData { get { return _dtStatus; } set { _dtStatus = value; } }
    }    
}
