using System.ComponentModel;
using System.Numerics;

namespace HULK;
public class Funciones
{
    public static Dictionary<string, Expresion.Funcion> funciones = new Dictionary<string, Expresion.Funcion>();
    
    public Funciones(string name, Expresion.Funcion funcion)
    {
        funciones.Add(name, funcion);
    }
    
    public static bool ContainsFuncion(object name)
    {
        foreach (var nombre in funciones)
        {
            if (funciones.ContainsKey((string)name))
                return true;
        }
        return false;
    }
    
    public static Expresion.Funcion GetFuncion(string name)
    {
        foreach (var nombre in funciones)
        {
            if (funciones.ContainsKey(name))
                return funciones[name];
        }
        throw new ERROR(ERROR.ErrorType.SemanticError , " Function "+ name + " is not defined");
    }
    
    public static void nullfunctions(string name, Expresion.Funcion funcion= null!)
    {
        if(funciones.ContainsKey(name))
          funciones[name]=funcion;

        else funciones.Add(name,funcion);
    }
    
    public static void FuncionesEspeciales()
    {
        nullfunctions("print");
        nullfunctions("cos");
        nullfunctions("sin");
        nullfunctions("sqrt");
        nullfunctions("log");
    }
    
}