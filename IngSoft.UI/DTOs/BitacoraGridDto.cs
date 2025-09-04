using System;

namespace IngSoft.UI.DTOs
{
    internal class BitacoraGridDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Origen { get; set; }
        public string TipoEvento { get; set; }
        public string Usuario { get; set; }
    }
}
