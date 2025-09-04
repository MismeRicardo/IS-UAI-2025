namespace IngSoft.Domain
{
    public class Usuario: Entity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string UserName { get; set; }
    }
}
