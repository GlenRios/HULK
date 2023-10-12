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
        break;
      Run(input);
    }

  }
  public static void Run(string input)
  {
    Tokenizador tokens = new Tokenizador(input);
    //foreach (var token in tokens.Tokens)
    //{
    //  Console.WriteLine(token.Type + " " + token.Grupo + " " + token.Value);
    //}
    Parser parser = new Parser(tokens.Tokens);
    Expresion expresion = parser.Parse();
    Dictionary<object , object> xd= new Dictionary<object, object>();
    Evaluador evaluador = new Evaluador(expresion);
    object respuesta = evaluador.Run(expresion,xd);
    System.Console.WriteLine(respuesta);
  }
}
