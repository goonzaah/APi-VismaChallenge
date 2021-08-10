namespace Core.Exceptions
{
    public class InvalidProductException : BusinessException
    {
        public InvalidProductException()
            : base("Error al obtener el producto.") { }
    }
}
