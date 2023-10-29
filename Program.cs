using System.Net.NetworkInformation;
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
      Console.Write(">");

      var input = Console.ReadLine();

      if (input == null)
      {
        Console.WriteLine("An empty line has been entered");
      }

      else
      {
        if (input.Length == 0)
        {
          Console.WriteLine("An empty line has been entered");
        }

        else
        {
          Run(input);
        }
      }
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

      Dictionary<object, object> value = new Dictionary<object, object>();

      Evaluador evaluador = new Evaluador(expresion);

      object respuesta = evaluador.Run(expresion, value);

      if (Evaluador.errores.Count != 0)
      {
        foreach (ERROR x in Evaluador.errores)
        {
          System.Console.WriteLine(x.Type + " " + x.Mensaje);
        }
      }

      else
      {
        if (respuesta != null!)
          Console.WriteLine(respuesta);

      }
    }
  }
}

