
namespace HULK;
//En esta clase voy a definir lo que es un objeto de tipo ERROR , deriva de Exception 
//porque en la parte de los errores sintacticos cuando mi programa encuentra 1 yo necesito que se detenga y no siga analizando.
public class ERROR : Exception
{
    public enum ErrorType
    {
        LexicalError,
        SyntaxError,
        SemanticError,
    }

    public string Mensaje;
    public ErrorType Type;
    public static bool hadError = false;

    public ERROR(ErrorType type, string mensaje)
    {
        Type = type;

        Mensaje = mensaje;

        hadError = true;
    }
}