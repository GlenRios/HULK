using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.Arm;

namespace HULK;
class Program
{
  static void Main(string[] args)
  {
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    System.Console.WriteLine("[HULK , Idioma de La Universidad de La Habana para Kompilers]");
    Funciones.FuncionesEspeciales();
    while (true)
    {
      Console.Write(">");

      var input = Console.ReadLine();

      if (input == null)
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("An empty line has been entered");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
      }

      else
      {
        if (input.Length == 0)
        {
          Console.ForegroundColor = ConsoleColor.DarkRed;
          Console.WriteLine("An empty line has been entered");
          Console.ForegroundColor = ConsoleColor.DarkYellow;
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
    try
    {
      Tokenizador tokens = new Tokenizador(input);/*foreach (var token in tokens.Tokens)
    {
      Console.WriteLine(token.Type + " " + token.Grupo + " " + token.Value);
    }*/

      if (tokens.errores.Count != 0)
      {
        foreach (var error in tokens.errores)
        {
          Console.ForegroundColor = ConsoleColor.DarkRed;
          System.Console.WriteLine(error.Type + " " + error.Mensaje);
          Console.ForegroundColor = ConsoleColor.DarkYellow;
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
            Console.ForegroundColor = ConsoleColor.DarkRed;
            System.Console.WriteLine(x.Type + " " + x.Mensaje);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
          }
        }

        else
        {
          if (respuesta != null!)
            Console.WriteLine(respuesta);

        }
      }
    }

    catch (ERROR x)
    {
      Console.ForegroundColor = ConsoleColor.DarkRed;
      System.Console.WriteLine(x.Type + " " + x.Mensaje);
      Console.ForegroundColor = ConsoleColor.DarkYellow;
    }

  }
}

