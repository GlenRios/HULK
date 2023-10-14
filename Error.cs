
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

    public string Mensaje;
    public ErrorType Type;
    public static bool hadError= false;

    public ERROR(ErrorType type, string mensaje)
    {
        Type=type;
        Mensaje=mensaje;
        hadError=true;
    }
    public ERROR(string mensaje)
    {
        Mensaje = mensaje;
        hadError=true;
    }


    
}