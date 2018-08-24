using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConectaLoja.Models
{
    public class PedidoItem
    {
        private int _id = 0;
        private int _vendaid = 0;
        private List<Observacao> _obs  = new List<Observacao>();
        private DateTime _data;
        private int _produtoid = 0;
        private string _produtoNome = "";
        private double _quantidade = 0;
        private double _total = 0;
        private double _acrescimo = 0;
        private double _desconto = 0;
        private double _valorunitario = 0;

        public int Id { get { return _id; } set { _id = value; } }
        public int Vendaid { get { return _vendaid; } set { _vendaid = value; } }
        public DateTime Data { get { return _data; } set { _data = value; } }
        public int Produtoid { get { return _produtoid; } set { _produtoid = value; } }
        public string ProdutoNome { get { return _produtoNome; } set { _produtoNome = value; } }
        public double Quantidade { get { return _quantidade; } set { _quantidade = value; } }
        public double Total { get { return _total; } set { _total = value; } }
        public double Acrescimo { get { return _acrescimo; } set { _acrescimo = value; } }
        public double Desconto { get { return _desconto; } set { _desconto = value; } }
        public double Valorunitario { get { return _valorunitario; } set { _valorunitario = value; } }
        public List<Observacao> Observacoes { get { return _obs; } set { _obs = value; } }
    }
}
