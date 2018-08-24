namespace ConectaLoja.Models
{
    public class Observacao
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int MasterID { get; set; }
        public int TipoID { get; set; }
    }
}
