using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PedidoLoja
{
    public double ValorTotal { get; set; }
    public string Nome { get; set; }
    public int codCliente { get; set; }
    public List<Itens> itens { get; set; }
}

public class Itens
{
    public string produto { get; set; }
    public int codigo { get; set; }
    public double valor { get; set; }
    public double quantidade { get; set; }
}
