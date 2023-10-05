namespace LogsAllianceApi.Models.ViewModel
{
    public class FiltersHistorico
    {
        public string Tipo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Evento { get; set; } = string.Empty;

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

    }
}
