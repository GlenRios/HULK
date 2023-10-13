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
        throw new Exception("Error mango");
    }
    
    public static void nullfunctions(string name, Expresion.Funcion funcion= null!)
    {
        if(funciones.ContainsKey(name))
          funciones[name]=funcion;

        else funciones.Add(name,funcion);
    }

    public static void Borrar(string name)
    {
        funciones.Remove(name);
    }
    
    public static void FuncionesEspeciales()
    {
        nullfunctions("print");
        nullfunctions("cos");
        nullfunctions("sen");
        nullfunctions("sqrt");
        nullfunctions("log");
    }
    
}