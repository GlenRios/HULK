
using System.Data;
using System.Net;

namespace HULK;
public class ERROR: Exception
{
    public enum ErrorType
    {
        LexicalError,
        SyntaxError,
        SemanticError,
    }

    public ErrorType Tipo;
    public string Mensaje;

    public ERROR(ErrorType tipo, string mensaje)
    {
        Tipo = tipo;
        Mensaje = mensaje;
    }

    
}