
namespace HULK;
//Esta clase es creada para las funciones propias del lenguaje y las que se declaran nuevas tambien
public class Funciones
{
   //en este diccionario guardaremos nuestras funciones con el nombre como key y la Funcion como value
    public static Dictionary<string, Expresion.Funcion> funciones = new Dictionary<string, Expresion.Funcion>();
    
    public Funciones(string name, Expresion.Funcion funcion)
    {
        funciones.Add(name, funcion);
    }
    
   //se utiliza en el parser para comprobar que la funcion no esta ya definida 
    public static bool ContainsFuncion(object name)
    {
        foreach (var nombre in funciones)
        {
            if (funciones.ContainsKey((string)name))
                return true;
        }
        return false;
    }
    
   // en el caso de los llamados de funcion este metodo sirve para devolver la funcion cuyo nombre es name 
    public static Expresion.Funcion GetFuncion(string name)
    {
        foreach (var nombre in funciones)
        {
            if (funciones.ContainsKey(name))
                return funciones[name];
        }
        throw new ERROR(ERROR.ErrorType.SemanticError , " Function "+ name + " is not defined");
    }
    
    //este metodo se utiliza mas que nada para modificar el value en el diccionario de el key name
    //por ejemplo en las funciones recursivas en el cuerpo de tu funcion la llamas a ella misma pero como estas creando el cuerpo precisamente
    //necesitas guardarla en un primer momento vacia y despues actualizar su valor 
    public static void AddFuncion(string name, Expresion.Funcion funcion= null!)
    {
        if(funciones.ContainsKey(name))
          funciones[name]=funcion;

        else funciones.Add(name,funcion);
    }
    
    //cuando se comienza a ejecutar el proyecto estas funciones como son propias del lenguaje e guardan desde un inicio y no se pueden modificar
    public static void FuncionesEspeciales()
    {
        AddFuncion("print");
        AddFuncion("cos");
        AddFuncion("sin");
        AddFuncion("sqrt");
        AddFuncion("log");
    }
    
}