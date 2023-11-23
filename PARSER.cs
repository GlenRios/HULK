
using System.Linq.Expressions;

namespace HULK;
public class Parser
{
    public List<Token> Tokens;
    //Para llevar la posicion de la lista
    private int current = 0;

    public Parser(List<Token> tokens)
    {
        Tokens = tokens;
    }

    // Retorna verdadero si los tokens que se insertan en la llamada coincide con el de la posicion en que vamos en la lista
    private bool match(TokenType[] types)
    {
        for (int i = 0; i < types.Length; i++)
        {
            if (check(types[i]))
            {
                return true;
            }
        }
        return false;
    }
    private bool match(TokenType type)
    {
        if (check(type))
        {
            return true;
        }
        else return false;
    }
    //Retorna verdadero si el TokenType que se le inserta es del mismo tipo del que estamos parados en la lista
    private bool check(TokenType type)
    {
        //if (IAE()) return false;
        return peek().Type == type;
    }

    //Consume el token actual y lo devuelve ademas aumenta el valor del current
    private Token advance()
    {
        if (!IAE()) current++;
        return previous();
    }

    //Retorna el Token en la posicion anterior
    private Token previous()
    {
        return Tokens[current - 1];
    }
    //Retorna verdadero si el Token es Final , o sea que esta al final de la linea
    private bool IAE()
    {
        return peek().Type == TokenType.Final;
    }
    //Retorna el token en la posicion actual
    private Token peek()
    {
        return Tokens[current];
    }

    //este es el metodo principal que comprueba inicialmente si estamos en precencia de una declaracion de funcion, si no llama a expression()
    //una vez retornado un valor consume ; porque es lo esperado al final de la linea y luego el token Final.
    public Expresion Parse()
    {
        while (match(TokenType.function))
        {
            current++;

            Token nombre = consume(TokenType.Identificador, "Identifier was expected as the function name in " + current);

            string name = (string)nombre.Value;

            consume(TokenType.ParentesisAbierto, "Expected '(' after identifier " + previous().Value + " in " + current);

            List<object> argument = new List<object>();

            while (match(TokenType.Identificador))
            {
                argument.Add(advance().Value);

                if (!match(TokenType.ParentesisCerrado))
                {
                    consume(TokenType.Coma, "Expected ',' or ')' after expression " + previous().Type + " " + previous().Value + " in " + current);
                }

            }

            consume(TokenType.ParentesisCerrado, "Missing ')' after the function arguments in " + current);

            consume(TokenType.Flecha, "Expected '=>' in the function declaration in " + current);

            if (Funciones.ContainsFuncion(name))
            {
                throw new Exception("Functions cannot be redefined");
            }

            Funciones.AddFuncion(name);

            Expresion funcionCuerpo = expression();

            consume(TokenType.PuntoYComa, " Expected ';' at the end of the expression after " + previous().Type + " " + previous().Value + " in " + current);

            Expresion.Funcion expres = new Expresion.Funcion(name, argument, funcionCuerpo);

            Funciones.AddFuncion(name, expres);

            if (match(TokenType.Final))
                return expres;

            else
            {
                throw new ERROR(ERROR.ErrorType.SyntaxError, " Invalid expression after ';' in " + current);
            }

        }

        Expresion expr = expression();

        consume(TokenType.PuntoYComa, "Expected ';' at the end of the expression after " + previous().Type + " " + previous().Value + " in " + current);

        if (match(TokenType.Final))

            return expr;

        else

            throw new ERROR(ERROR.ErrorType.SyntaxError, " Invalid espression after ';' in " + current);

    }
   
    //simplemete se expande a logical()
    private Expresion expression()
    {
        return logical();
    }

    //se expande a equality primero y luego de retornado un valor para expr comprueba si el token en el que estamos es de tipo && or ||
    //si lo es creara una nueva expresion bianria y la retornara ,si no seguira retornando el llamado.
    private Expresion logical()
    {
        Expresion expr = equality();

        TokenType[] a = { TokenType.And, TokenType.Or };

        while (match(a))
        {
            Token operador = advance();
            Expresion rigth = equality();
            expr = new Expresion.ExprBinaria(expr, operador, rigth);
        }

        return expr;
    }

    //se expande a comparison primero y luego de retornado un valor para expr comprueba si el token en el que estamos es de tipo == or !=
    //si lo es creara una nueva expresion bianria y la retornara, si no seguira retornando la expr que llevaba.
    private Expresion equality()
    {
        Expresion expr = comparison();

        TokenType[] a = { TokenType.IgualIgual, TokenType.NoIgual };

        while (match(a))
        {
            Token operador = advance();
            Expresion rigth = comparison();
            expr = new Expresion.ExprBinaria(expr, operador, rigth);
        }

        return expr;
    }


    //se expande a concat primero y luego de retornado un valor para expr comprueba si el token en el que estamos es de tipo > or <
    // or => or <= si lo es creara una nueva expresion bianria y la retornara, si no seguira retornando la expr que llevaba.
    private Expresion comparison()
    {
        Expresion expr = concat();

        TokenType[] a = { TokenType.Mayor, TokenType.MayorIgual, TokenType.Menor, TokenType.MenorIgual };

        while (match(a))
        {
            Token operador = advance();
            Expresion rigth = concat();
            expr = new Expresion.ExprBinaria(expr, operador, rigth);
        }

        return expr;
    }

    //se expande a Term primero y luego de retornado un valor para expr comprueba si el token en el que estamos es de tipo @
    //si lo es creara una nueva expresion bianria y la retornara, si no seguira retornando la expr que llevaba.
    private Expresion concat()
    {
        Expresion expr = Term();

        while (match(TokenType.Concatenar))
        {
            Token operador = advance();
            Expresion rigth = Term();
            expr = new Expresion.ExprBinaria(expr, operador, rigth);
        }

        return expr;
    }

    //se expande a factor primero y luego de retornado un valor para expr comprueba si el token en el que estamos es de tipo + or -
    //si lo es creara una nueva expresion bianria y la retornara, si no seguira retornando la expr que llevaba.
    private Expresion Term()
    {
        Expresion expr = factor();

        TokenType[] a = { TokenType.Resta, TokenType.Suma };

        while (match(a))
        {
            Token operador = advance();
            Expresion rigth = factor();
            expr = new Expresion.ExprBinaria(expr, operador, rigth);
        }

        return expr;
    }

    //se expande a pow primero y luego de retornado un valor para expr comprueba si el token en el que estamos es de tipo * or /
    //si lo es creara una nueva expresion bianria y la retornara, si no seguira retornando la expr que llevaba.
    private Expresion factor()
    {
        Expresion expr = pow();

        TokenType[] a = { TokenType.Division, TokenType.Multiplicacion };

        while (match(a))
        {
            Token operador = advance();
            Expresion rigth = pow();
            expr = new Expresion.ExprBinaria(expr, operador, rigth);
        }

        return expr;
    }

    //se expande a mod primero y luego de retornado un valor para expr comprueba si el token en el que estamos es de tipo ^
    //si lo es creara una nueva expresion bianria y la retornara, si no seguira retornando la expr que llevaba.
    private Expresion pow()
    {
        Expresion expr = mod();

        while (match(TokenType.Pow))
        {
            Token operador = advance();
            Expresion rigth = mod();
            expr = new Expresion.ExprBinaria(expr, operador, rigth);
        }

        return expr;
    }

    //se expande a unary primero y luego de retornado un valor para expr comprueba si el token en el que estamos es de tipo %
    //si lo es creara una nueva expresion bianria y la retornara, si no seguira retornando la expr que llevaba.
    private Expresion mod()
    {
        Expresion expr = unary();

        while (match(TokenType.Modulo))
        {
            Token operador = advance();
            Expresion rigth = unary();
            expr = new Expresion.ExprBinaria(expr, operador, rigth);
        }

        return expr;
    }

    //comprueba que el token sobre el que estamos sea de tipo - o ! , si lo es llamariamos expresion para crear la expresion derecha 
    //de la unary expr y luego retornaria una expr unaria , si el token no es de ese tipo entonces retorna Stm
    private Expresion unary()
    {
        TokenType[] a = { TokenType.Resta, TokenType.Negacion };

        while (match(a))
        {
            Token operador = advance();
            Expresion rigth = unary();
            return new Expresion.ExprUnaria(operador, rigth);
        }

        return Stm();
    }
    
    //comprueba si el token en el que estamos e de tipo if , si lo es crea mi expresion de tipo if comprobando que este bien estructurada
    //y se retorna dicha expresion.
    
    //si el token no es de tipo if comprueba si es de tipo let , si lo es se crea una expresion de tipo let donde para recoger el letCuerpo
    //se llama al metodo Asign(), siempre comprobando que la estructura sea correcta y se retorna dicha expresion.
    
    //si el token no es de tipo let entonces comprueba si es de tipo identificador, si lo es , tenemos dos posibles casos , en el que sea un llamado
    //de funcion donde comprobaremos primero que nuestra funcion exita o este declarada por el usuario, luego se crearia una expresion de tipo llamado 
    //funcion y se retornaria , comprobando que la estructura sea correcta. Si es un simple identificador entonces retornara una expresion de tipo
    //variable.

    //si no es ninguna de las anteriores retorna primary()
    private Expresion Stm()
    {
        if (match(TokenType.If))
        {
            current++;

            Expresion condicion = primary();

            Expresion ifcuerpo = expression();

            consume(TokenType.Else, " Expected else after if statement in " + current);

            Expresion elsecuerpo = expression();

            Expresion expr = new Expresion.If(condicion, ifcuerpo, elsecuerpo);

            return expr;
        }

        if (match(TokenType.Let))
        {
            current++;

            List<Expresion.ExprAsignar> letCuerpo = Asign();

            consume(TokenType.In, " Expected 'in' after let statement in " + current);

            Expresion inCuerpo = expression();

            return new Expresion.LetIn(letCuerpo, inCuerpo);
        }

        if (match(TokenType.Identificador))
        {
            if (Funciones.ContainsFuncion(peek().Value))
            {
                string name = (string)advance().Value;

                consume(TokenType.ParentesisAbierto, " Expected '(' in " + current);

                TokenType[] a = { TokenType.Coma, TokenType.ParentesisCerrado };

                List<Expresion> argument = new List<Expresion>();

                while (!match(a))
                {
                    Expresion expresion = expression();

                    if (!match(TokenType.ParentesisCerrado))
                    {
                        consume(TokenType.Coma, " Expected ',' or ')' after expression " + previous().Type + " " + previous().Value + " in " + current);
                    }

                    argument.Add(expresion);
                }
                consume(TokenType.ParentesisCerrado, " Missing ')' after expression in " + current);

                return new Expresion.ExprLLamadaFuncion(name, argument, Funciones.GetFuncion(name));
            }

            Expresion.ExprVariable expr = new Expresion.ExprVariable(advance());

            return expr;
        }

        return primary();
    }

   //se usa para crear la lista de expresiones de asign para el letCuerpo.
    private List<Expresion.ExprAsignar> Asign()
    {
        List<Expresion.ExprAsignar> answer = new List<Expresion.ExprAsignar>();

        while (!match(TokenType.In))
        {
            Token name = new Token(TokenType.Identificador, "");

            if (match(TokenType.Identificador))
            {
                name = advance();
            }

            else
            {
                throw new ERROR(ERROR.ErrorType.SyntaxError, " Expect a variable name after " + previous().Type + " " + previous().Value + " in " + current);
            }

            consume(TokenType.Igual, " Expect '=' after variable name in " + current);

            Expresion expr = expression();

            if (!match(TokenType.In))
            {
                consume(TokenType.Coma, " Expect ',' or 'in' after expression " + previous().Type + " " + previous().Value + " in " + current);

                if (match(TokenType.In))
                {
                    throw new ERROR(ERROR.ErrorType.LexicalError, " Invalid token 'in' after ',' in " + current);
                }

            }

            answer.Add(new Expresion.ExprAsignar(name, expr));
        }

        return answer;
    }

    //primary es el ultimo metodo en ser llamado y donde e recogen las expresiones mas singulares , como expresiones literales o expresiones entre
    //parentesis .
    //primero comprueba si el token es de tipo false ,de serlo retorna una expression literal cuyo valor es false.
    //luego si el token es de tipo true hace lo mismo.
    //si son las constantes PI o E tambien retorna el mismo tipo de expresion
    //Luego comprueba si es number o string y de serlo hace lo mismo que con los anteriores.
    //si es ParentesisAbierto entonces recoge llama a expresion en el token siguiente y luego de recibido un valor para dicha expresion cmprueba 
    //que el token en que esta es de tipo parentesisCerrado , si no lo es lanza un error , si lo es retorna dicha expresion creada.
    //finalmente si no es nada de lo anterior entonces lanza un error pues el token en esa posicion no esta bien colocado por lo 
    //que la expresion es invalida.
    private Expresion primary()
    {
        if (match(TokenType.False))
        {
            current++;

            return new Expresion.ExprLiteral(false);
        }

        if (match(TokenType.True))
        {
            current++;

            return new Expresion.ExprLiteral(true);
        }

        if (match(TokenType.PI))
        {
            current++;

            return new Expresion.ExprLiteral(Math.PI);
        }

        if (match(TokenType.EULER))
        {
            current++;

            return new Expresion.ExprLiteral(Math.E);
        }

        TokenType[] a = { TokenType.Number, TokenType.String };

        if (match(a))
        {
            return new Expresion.ExprLiteral(advance().Value);
        }

        if (match(TokenType.ParentesisAbierto))
        {
            current++;

            Expresion expr = expression();

            consume(TokenType.ParentesisCerrado, " missing ')' after expresion in " + current);

            return expr;
        }

        throw new ERROR(ERROR.ErrorType.LexicalError, " Invalid token " + peek().Type + " " + peek().Value + " in " + current);

    }
    
    //este metodo es de los mas importantes y usados , que lo que hace es comprobar si el token sobre el que estoy es del mismo tipo del esperado
    //si lo es llama a advance() si no lanza un error con un mensaje sobre el tipo y la posicion.
    private Token consume(TokenType type, string mensaje)
    {
        if (check(type)) return advance();
        //ERROR error = new ERROR(ERROR.ErrorType.SyntaxError, mensaje);
        throw new ERROR(ERROR.ErrorType.SyntaxError, mensaje);
    }

}