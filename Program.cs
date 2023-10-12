namespace HULK;
class Program
{
  static void Main(string[] args)
  {

    Funciones.FuncionesEspeciales();
    while (true)
    {
      System.Console.Write(">");
      string input = Console.ReadLine();
      if (input == null!)
      {
        Console.WriteLine("Se ha ingresado una linea vacia");
        break;
      }
      else Run(input);
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
        System.Console.WriteLine(error.Tipo + " " + error.Mensaje);
      }
    }

    else
    {
      Parser parser = new Parser(tokens.Tokens);

      Expresion expresion = parser.Parse();

      foreach (var error in parser.errores)
      {
        System.Console.WriteLine(error.Tipo + " " + error.Mensaje);
      }
      Dictionary<object, object> xd = new Dictionary<object, object>();
      //Evaluador evaluador = new Evaluador(expresion);
      //object respuesta = evaluador.Run(expresion, xd);
      //System.Console.WriteLine(respuesta);
    }
  }
}
