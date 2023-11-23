
namespace HULK;
//En esta clase crearemos objectos de algun tipo especifico de expresion 
public abstract class Expresion
{

    //las expresiones unarias estan formadas por un operador logico (!) o aritmetico (-) y una expression derecha.
    public class ExprUnaria : Expresion

    {
        public Token Token;
        public Expresion Derecha;
        public ExprUnaria(Token token, Expresion derecha)
        {
            Token = token;
            Derecha = derecha;
        }
        //Este metodo se usa en el evaluador , comprueba segun el operador que la expresion a la derecha sea valida y evalua;
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

    //las expresiones de tipo binaria estan formadas por una expresion izquierda , un operador tanto logico como aritmetico y una expression derecha.
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

        //este metodo se utiliza en el evaluador y segun el operador llama a otros metodos que combruebaan que la operacion sea valida
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

    //los literales son expresiones que contienen un object literal y un literal puede ser basicamente un numero o un string , etc
    public class ExprLiteral : Expresion
    {
        public object literal;
        public ExprLiteral(object literal)
        {
            this.literal = literal;
        }
        //este metodo se utiliza en el evaluador y devuelve el literal(valor) de mi expresion literal
        public object VisitExprLiteral(ExprLiteral expr)
        {
            return expr.literal;
        }

    }

    //las expresiones de asignacion e utilizan en este lenguaje para crear expresiones de la forma Token(variable) y el valor que se le asigna a esa variable
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

    //las expresiones variable estan formadas por un unico token que hace funcion de variable en el codigo
    public class ExprVariable : Expresion
    {
        public Token Nombre;
        public ExprVariable(Token nombre)
        {
            Nombre = nombre;
        }
        //este metodo se utiliza en el evaluador dado un diccionario con las variables y sus valores y 
        //la variable especifica que se necesita saber te devulve si la variable esta en el diccionario el valor que se le asigna.
        //de no estar la variable en el diccionario retorna un error porque la variable no tiene un valor asignado.
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

    //las llamadas de funcion tiene un string como nombre , una lista de expresiones como argumento y una funcion que 
    //coincide con el nombre de esta.
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

        //este metodo se usa en el evaluador y recibe como parametros un diccionario de variables con sus valores y la llamada funcion
        //crea una lista donde a cada argumento en call.Argumento lo evalua y guarda sus valores como object en dicha lista.
        //luego revisa cual es la funcion a la que se le hizo la llamada y en dependencia del nombre  se evalua con los valores de la lista creada.
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

        //si la funcion es de tipo sin comprueba que el count de la lista de argument sea 1 y luego evalua en dicho valor.
        //si es diferente de 1 lanza un error pues sin solo recibe un argumento.
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

        //si la funcion es de tipo cos comprueba que el count de la lista de argument sea 1 y luego evalua en dicho valor.
        //si es diferente de 1 lanza un error pues cos solo recibe un argumento.
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

        //si la funcion es de tipo sqrt comprueba que el count de la lista de argument sea 1 y luego evalua en dicho valor.
        //si es diferente de 1 lanza un error pues sqrt solo recibe un argumento.
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

        //si la funcion es de tipo slog comprueba que el count de la lista de argument sea 2 y luego evalua en dicho valor.
        //si es diferente de 2 lanza un error pues log solo recibe un argumento.
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

        //si la funcion es de tipo print comprueba que el count de la lista de argument sea 1 y luego evalua en dicho valor.
        //si es diferente de 1 lanza un error pues print solo recibe un argumento.
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

        //por default si es una funcion declarada por el usuario entonces va a comprobar que la cantidad de parametros que recibe la funcion
        //por definicion sea == a la cantidad de argumentos que tiene la llamada (si no es igual lanza un error )luego creara un 
        //diccionario donde a cada parametro le asignara el valor del argumento en el mismo orden y evaluara
        // el cuerpo de la funcion que no es mas que una expresion con dicho diccionario.
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

//esta clase se utiliza para crear nuevas funciones que seran declaradas por el usuario y las cuales tendran un nombre , una lista de parametros
//que seran object y un cuerpo.
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

    //las expresiones de tipo if estan formadas por una condicion de if , un cuerpo de if y un cuerpo de else.
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

//Las expresiones de tipo let_in estan conformadas por una lista de expresiones de asignacion y un inCuerpo que es una expresion.
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