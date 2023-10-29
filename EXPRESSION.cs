
namespace HULK;
public abstract class Expresion
{

    public class ExprUnaria : Expresion

    {
        public Token Token;
        public Expresion Derecha;
        public ExprUnaria(Token token, Expresion derecha)
        {
            Token = token;
            Derecha = derecha;
        }
        public object VisitExprUnaria(object derecha)
        {
            if (Token.Type == TokenType.Resta)
            {
                if (derecha is double)
                {
                    return -1 * (double)derecha;
                }

                Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '-' cannot be used before " + derecha));
            }

            if (Token.Type == TokenType.Negacion)
            {
                if (derecha is bool)
                {
                    return !(bool)derecha;
                }

                Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '!' cannot be used before " + derecha));
            }

            return null!;
        }
    }

    public class ExprBinaria : Expresion
    {
        public Expresion Izquierda;
        public Token Operador;
        public Expresion Derecha;

        public ExprBinaria(Expresion izquierda, Token operador, Expresion derecha)
        {
            Izquierda = izquierda;
            Operador = operador;
            Derecha = derecha;
        }

        public object VisitExprBinaria(object izquierda, object derecha)
        {
            if (Operador.Type == TokenType.IgualIgual)
            {
                return IgualIgual(izquierda, derecha);
            }

            if (Operador.Type == TokenType.NoIgual)
            {
                return NoIgual(izquierda, derecha);
            }

            if (Operador.Type == TokenType.MenorIgual)
            {
                return MenorIgual(izquierda, derecha);
            }

            if (Operador.Type == TokenType.MayorIgual)
            {
                return MayorIgual(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Menor)
            {
                return Menor(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Mayor)
            {
                return Mayor(izquierda, derecha);
            }

            if (Operador.Type == TokenType.And)
            {
                return And(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Or)
            {
                return Or(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Suma)
            {
                return Suma(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Resta)
            {
                return Resta(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Concatenar)
            {
                return Concatenar(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Multiplicacion)
            {
                return Multiplicacion(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Division)
            {
                return Division(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Pow)
            {
                return Pow(izquierda, derecha);
            }

            if (Operador.Type == TokenType.Modulo)
            {
                return Modulo(izquierda, derecha);
            }

            return null!;
        }
        public object IgualIgual(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)
            {
                if ((double)izquierda == (double)derecha) return true;
                else return false;
            }

            if (izquierda is bool && derecha is bool)
            {
                if ((bool)izquierda == (bool)derecha) return true;
                else return false;
            }

            if (izquierda is string && derecha is string)
            {
                if ((string)izquierda == (string)derecha) return true;
                else return false;
            }

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '==' cannot be used between " + izquierda + " and  " + derecha));
            return null!;
        }

        public object NoIgual(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)
            {
                if ((double)izquierda != (double)derecha) return true;
                else return false;
            }

            if (izquierda is bool && derecha is bool)
            {
                if ((bool)izquierda != (bool)derecha) return true;
                else return false;
            }

            if (izquierda is string && derecha is string)
            {
                if ((string)izquierda != (string)derecha) return true;
                else return false;
            }

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '!=' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object MenorIgual(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return (double)izquierda <= (double)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '<=' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object MayorIgual(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return (double)izquierda >= (double)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '>=' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object Menor(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return (double)izquierda < (double)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '<' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object Mayor(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return (double)izquierda > (double)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '>' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object And(object izquierda, object derecha)
        {
            if (izquierda is bool && derecha is bool)

                return (bool)izquierda && (bool)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '&&' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object Or(object izquierda, object derecha)
        {
            if (izquierda is bool && derecha is bool)

                return (bool)izquierda || (bool)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '||' cannot be used between " + izquierda + " and " + derecha));
            return null!;
        }

        public object Suma(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return (double)izquierda + (double)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '+' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object Resta(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return (double)izquierda - (double)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '-' cannot be used between " + izquierda + " and " + derecha));
            return null!;


        }

        public object Multiplicacion(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return (double)izquierda * (double)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '*' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object Division(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return (double)izquierda / (double)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '/' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object Pow(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return Math.Pow((double)izquierda, (double)derecha);

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '^' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object Modulo(object izquierda, object derecha)
        {
            if (izquierda is double && derecha is double)

                return (double)izquierda % (double)derecha;

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '%' cannot be used between " + izquierda + " and " + derecha));
            return null!;

        }

        public object Concatenar(object izquierda, object derecha)
        {
            string resultado = "";
            if (izquierda is string)
            {
                resultado += izquierda;
            }
            if (izquierda is double)
            {
                resultado += (double)izquierda;
            }
            if (izquierda is bool)
            {
                resultado += (bool)izquierda;
            }
            if (derecha is string)
            {
                resultado += derecha;
                return resultado;
            }
            if (derecha is double)
            {
                resultado += (double)derecha;
                return resultado;
            }
            if (derecha is bool)
            {
                resultado += (bool)derecha;
                return resultado;
            }

            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Operator '@' cannot be used between " + izquierda + " and " + derecha));
            return null!;
        }
    }

    public class ExprLiteral : Expresion
    {
        public object literal;
        public ExprLiteral(object literal)
        {
            this.literal = literal;
        }

        public object VisitExprLiteral(ExprLiteral expr)
        {
            return expr.literal;
        }

    }

    public class ExprAsignar : Expresion
    {
        public Token Nombre;
        public Expresion Valor;

        public ExprAsignar(Token nombre, Expresion valor)
        {
            Nombre = nombre;
            Valor = valor;
        }
    }

    public class ExprVariable : Expresion
    {
        public Token Nombre;
        public ExprVariable(Token nombre)
        {
            Nombre = nombre;
        }
        public object VisitExprVariable(Dictionary<object, object> asign, Token variable)
        {
            if (asign is null)
            {
                Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Variable " + variable.Value + " does not have a value assigned"));
            }

            else
            {
                foreach (object x in asign)
                {
                    if (asign.ContainsKey(variable.Value))

                        return asign[variable.Value];
                }
            }
            
            Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Variable " + variable.Value + " does not have a value assigned"));
            return null!;

        }
    }

    public class ExprLLamadaFuncion : Expresion
    {
        public string Identificador;
        public List<Expresion> Argumento;
        public Funcion funcion;
        public ExprLLamadaFuncion(string identificador, List<Expresion> argumento, Funcion funcion)
        {
            Identificador = identificador;
            Argumento = argumento;
            this.funcion = funcion;
        }

        public object VisitExprLlamada(ExprLLamadaFuncion call, Dictionary<object, object> valor)
        {
            List<object> valores = new List<object>();

            foreach (Expresion args in call.Argumento)
            {
                valores.Add(Evaluador.GetValue(args, valor));
            }

            switch (Identificador)
            {
                case "sin":
                    return Sin(valores);

                case "cos":
                    return Cos(valores);

                case "print":
                    return Print(valores);

                case "log":
                    return Log(valores);

                case "sqrt":
                    return Sqrt(valores);

                default:
                    Funcion funcion = Funciones.GetFuncion(Identificador);

                    return VisitFuncion(valores, funcion);
            }
        }

        public object Sin(List<object> argument)
        {
            if (argument.Count != 1)
            {
                throw new ERROR(ERROR.ErrorType.SyntaxError, " Function 'sin' only receives one parameter as an argument");
            }

            else
            {
                if (argument[0] is double)
                {
                    return Math.Sin((double)argument[0]);
                }

                Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " function sin only receives numbers as parameters"));

            }

            return null!;

        }

        public object Cos(List<object> argument)
        {
            if (argument.Count != 1)
            {
                throw new ERROR(ERROR.ErrorType.SyntaxError, " Function 'cos' only receives one parameter as an argument");
            }

            else
            {
                if (argument[0] is double)
                {
                    return Math.Cos((double)argument[0]);
                }

                Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " function 'cos' only receives numbers as parameters"));

            }

            return null!;

        }

        public object Sqrt(List<object> argument)
        {
            if (argument.Count != 1)
            {
                throw new ERROR(ERROR.ErrorType.SyntaxError, " Function 'sqrt' only receives one parameter as an argument");
            }

            else
            {
                if (argument[0] is double)
                {
                    return Math.Sqrt((double)argument[0]);
                }

                Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " function 'sqrt' only receives numbers as parameters"));

            }

            return null!;

        }

        public object Log(List<object> argument)
        {
            if (argument.Count != 2)
            {
                throw new ERROR(ERROR.ErrorType.SyntaxError, " Function 'log' only receives two parameter as arguments");
            }

            else
            {
                if (argument[0] is double && argument[1] is double)
                {
                    return Math.Log((double)argument[0], (double)argument[1]);
                }

                Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " Function 'log' only receives numbers as arguments"));
            }

            return null!;

        }

        public object Print(List<object> argument)
        {
            if (argument.Count != 1)
            {
                throw new ERROR(ERROR.ErrorType.SyntaxError, " Function 'print' only receives one parameter as an argument");
            }

            else
            {
                Console.WriteLine(argument[0]);
                return argument[0];
            }
        }

        public object VisitFuncion(List<object> valores, Funcion funcion)
        {
            Dictionary<object, object> value = new Dictionary<object, object>();

            if (funcion.Parametros.Count == valores.Count)
            {
                for (int i = 0; i < valores.Count; i++)
                {
                    value.Add(funcion.Parametros[i], valores[i]);
                }

                return Evaluador.GetValue(funcion.Cuerpo, value);
            }

            else
            {
                Evaluador.errores.Add(new ERROR(ERROR.ErrorType.SemanticError, " function " + funcion.Identificador + " only receives " + funcion.Parametros.Count + " parameter as arguments"));
                return null!;
            }

        }

    }

    public class Funcion : Expresion
    {
        public string Identificador;
        public List<object> Parametros;
        public Expresion Cuerpo;
        public Funcion(string identificador, List<object> parametros, Expresion cuerpo)
        {
            Identificador = identificador;
            Parametros = parametros;
            Cuerpo = cuerpo;
        }

    }

    public class If : Expresion
    {
        public Expresion Condicion;
        public Expresion IfCuerpo;
        public Expresion ElseCuerpo;
        public If(Expresion condicion, Expresion ifCuerpo, Expresion elseCuerpo)
        {
            Condicion = condicion;
            IfCuerpo = ifCuerpo;
            ElseCuerpo = elseCuerpo;
        }
    }

    public class LetIn : Expresion
    {
        public List<ExprAsignar> LetCuerpo;
        public Expresion InCuerpo;
        public LetIn(List<ExprAsignar> letCuerpo, Expresion inCuerpo)
        {
            LetCuerpo = letCuerpo;
            InCuerpo = inCuerpo;
        }

    }

}