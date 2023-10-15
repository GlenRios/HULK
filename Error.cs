
using System.Data;
using System.Net;
using System.Reflection.Metadata;

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
        if(type==ErrorType.SyntaxError)
        {
            System.Console.WriteLine(type+ mensaje);
        }

        if(type==ErrorType.SemanticError)
        {
            System.Console.WriteLine(type+ mensaje);
        }
    }
}