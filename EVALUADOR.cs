
namespace HULK;
public class Evaluador
{
    public Expresion Parser;

    public static List<ERROR> errores = new List<ERROR>();

    public Evaluador(Expresion parser)
    {
        Parser = parser;
    }
    public object Run(Expresion expr, Dictionary<object, object> asign)
    {
        return GetValue(expr, asign);
    }

    //este metodo es el utilizado para evaluar , luego de parsear tendremos una expresion creada que estara conformada por 
    //mas expresiones dentro , inicialmente cuando llamamos a este metodo por primera vez le pasaremos un diccionario vacio
    //y la expresion conformada en el parser , luego comprobara que tipo de expresion es y en dependencia de cual sea procedera a llamar 
    //al metodo correspondiente para cada tipo . Como puede notarse los llamados son recursivos por lo cual este metodo ira evaluando desde
    //lo mas profundo o interno hasta lo mas externo de la expresion.
    public static object GetValue(Expresion expr, Dictionary<object, object> asig)
    {
        if (expr is Expresion.ExprUnaria)
        {
            Expresion.ExprUnaria unaria = (Expresion.ExprUnaria)expr;
            return unaria.VisitExprUnaria(GetValue(unaria.Derecha, asig));
        }

        if (expr is Expresion.ExprBinaria)
        {
            Expresion.ExprBinaria binaria = (Expresion.ExprBinaria)expr;
            return binaria.VisitExprBinaria(GetValue(binaria.Izquierda, asig), GetValue(binaria.Derecha, asig));
        }

        if (expr is Expresion.ExprLiteral)
        {
            Expresion.ExprLiteral literal = (Expresion.ExprLiteral)expr;
            return literal.VisitExprLiteral(literal);
        }

        if (expr is Expresion.If)
        {
            Expresion.If If = (Expresion.If)expr;
            object x = GetValue(If.Condicion, asig);
            if (!(x is bool))
            {
                errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " if condition must return a bool"));
            }

            else
            {
                if ((bool)x == true)
                {
                    return GetValue(If.IfCuerpo, asig);
                }

                else return GetValue(If.ElseCuerpo, asig);
            }

        }

        if (expr is Expresion.LetIn)
        {
            Expresion.LetIn let = (Expresion.LetIn)expr;
            Dictionary<object, object> answ = DictLetIn(let.LetCuerpo, asig);
            return GetValue(let.InCuerpo, answ);
        }

        if (expr is Expresion.ExprVariable)
        {
            Expresion.ExprVariable variable = (Expresion.ExprVariable)expr;
            return variable.VisitExprVariable(asig, variable.Nombre);
        }

        if (expr is Expresion.Funcion)
        {
            return null!;
        }

        if (expr is Expresion.ExprLLamadaFuncion)
        {
            Expresion.ExprLLamadaFuncion call = (Expresion.ExprLLamadaFuncion)expr;
            return call.VisitExprLlamada(call, asig);
        }

        return null!;
    }

    //este metodo esta creado para el caso especifico del LetCuerpo pues como tedremos asignacion de valores , debemos comprobar
    //que esta variable no tiene ya un valor asignado o si el valor que se le asigno en la declaracion es valido.
    private static Dictionary<object, object> DictLetIn(List<Expresion.ExprAsignar> asignar, Dictionary<object, object> asig)
    {
        Dictionary<object, object> resp = new Dictionary<object, object>();

        foreach (var x in asig)
        {
            if (resp.ContainsKey(x)) continue;
            else resp.Add(x.Key, x.Value);
        }

        foreach (var expresion in asignar)
        {
            if (resp.ContainsKey(expresion.Nombre.Value) && !asig.ContainsKey(expresion.Nombre.Value))
            {
                errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " variable " + expresion.Nombre.Value + " already has a value assigned"));
            }

            else resp[expresion.Nombre.Value] = GetValue(expresion.Valor, resp);
        }

        return resp;
    }
}