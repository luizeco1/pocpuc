namespace PoCGastos.Models
{
    public class ContextoGasto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal? LimiteMensal { get; set; }

        public decimal TotalGasto { get; set; } = 0M;
        public int QuantidadeLancamentos { get; set; } = 0;
        public bool Ativo { get; set; } = true;
    }
}