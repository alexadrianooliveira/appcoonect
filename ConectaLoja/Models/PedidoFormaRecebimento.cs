using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConectaLoja.Models
{
    public class PedidoFormaRecebimento
    {
        private int _id = 0;
        private string _Nome = "";
        private double _valor = 0;

        public int ID { get { return _id; } set { _id = value; } }
        public string Nome { get { return _Nome; } set { _Nome = value; } }
        public double Valor { get { return _valor; } set { _valor = value; } }
    }
}
