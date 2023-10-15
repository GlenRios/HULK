using System.Runtime.Intrinsics.Arm;

namespace HULK;
class Program
{
  static void Main(string[] args)
  {
    System.Console.WriteLine("[HULK , Idioma de La Universidad de La Habana para Kompilers]");
    Funciones.FuncionesEspeciales();
    while (true)
    {
      System.Console.Write(">");
      
      var input = Console.ReadLine();
      if (input == null!)
      {
        Console.WriteLine("Se ha ingresado una linea vacia");
        break;
      }

      else Run((string)input);
    }

  }
  public static void Run(string input)
  {
    Tokenizador tokens = new Tokenizador(input);
    /*foreach (var token in tokens.Tokens)
    {
      Console.WriteLine(token.Type + " " + token.Grupo + " " + token.Value);
    }*/

    if (tokens.errores.Count != 0)
    {
      foreach (var error in tokens.errores)
      {
        System.Console.WriteLine(error.Type + " " + error.Mensaje);
      }
    }

    else
    {
      Parser parser = new Parser(tokens.Tokens);

      Expresion expresion = parser.Parse();

      if (ERROR.hadError == true) return;

      Dictionary<object, object> xd = new Dictionary<object, object>();

      Evaluador evaluador = new Evaluador(expresion);

      object respuesta = evaluador.Run(expresion, xd);

      if(ERROR.hadError==true) return;

      System.Console.WriteLine(respuesta);

    }
  }
}
