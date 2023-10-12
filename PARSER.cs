namespace HULK;
public class Parser
{
    public List<Token> Tokens;
    //Para llevar la posicion de la lista
    private int current = 0;
    public List<ERROR> errores = new List<ERROR>();
    public Parser(List<Token> tokens)
    {
        Tokens = tokens;
        Parse();
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

    //Consume el token actual y lo devuelve aumentando el valor del current

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
    //Retorna verdadero si el Token es PuntoYComa , o sea que esta al final de la linea

    private bool IAE()
    {
        return peek().Type == TokenType.Final;
    }
    //Retorna el token en la posicion actual

    private Token peek()
    {
        return Tokens[current];
    }
    //Metodo para parsear una lista de tokens
    public Expresion Parse()
    {
        while (match(TokenType.function))
        {
            current++;

            Token nombre = consume(TokenType.Identificador, "An identifier was expected as the function name");

            string name = (string)nombre.Value;

            if (Funciones.ContainsFuncion(name))
            {
                errores.Add(new ERROR(ERROR.ErrorType.SyntaxError, "function " + name + " cannot be redefined"));
            }

            consume(TokenType.ParentesisAbierto, "Expected '(' after expression " + previous().Type + " " + previous().Value);

            List<object> argument = new List<object>();

            while (match(TokenType.Identificador))
            {
                argument.Add(advance().Value);

                if (!match(TokenType.ParentesisCerrado))
                {
                    consume(TokenType.Coma, "Expected ',' before expression " + peek().Type + " " + peek().Value);
                }
            }

            consume(TokenType.ParentesisCerrado, "Missing ')' after expression " + previous().Type + " " + previous().Value);

            consume(TokenType.Flecha, "Expect '=>' to declare the function");

            Funciones.nullfunctions(name);

            Expresion funcionCuerpo = expression();
            // if (match(TokenType.PuntoYComa))

            consume(TokenType.PuntoYComa, "Expected ';' at the end of expression declaration after " + previous().Type + " " + previous().Value);

            Expresion expres = new Expresion.Funcion(name, argument, funcionCuerpo);

            if (match(TokenType.Final))
                return expres;

            errores.Add(new ERROR(ERROR.ErrorType.SyntaxError, "Expression was not declared correctly after " + previous().Type + " " + previous().Value));

            return null!;
        }
        Expresion expr = expression();

        consume(TokenType.PuntoYComa, "Expected ';' at the end of expression declaration after " + previous().Type + " " + previous().Value);

        if (match(TokenType.Final))

            return expr;

        else
            errores.Add(new ERROR(ERROR.ErrorType.SyntaxError, "Expression was not declared correctly after " + previous().Type + " " + previous().Value));


        return null!;
    }
    private Expresion expression()
    {
        return logical();
    }

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

    private Expresion unary()
    {
        TokenType[] a = { TokenType.Resta, TokenType.Negacion };

        while (match(a))
        {
            Token operador = advance();

            Expresion rigth = unary();

            return new Expresion.ExprUnaria(operador, rigth);
        }

        return IFORLET();
    }

    private Expresion IFORLET()
    {
        if (match(TokenType.If))
        {
            current++;

            Expresion condicion = primary();

            Expresion ifcuerpo = expression();

            consume(TokenType.Else, "Expect 'else' after if statement");

            Expresion elsecuerpo = expression();

            Expresion expr = new Expresion.If(condicion, ifcuerpo, elsecuerpo);

            return expr;
        }

        if (match(TokenType.Let))
        {
            current++;

            List<Expresion.ExprAsignar> letCuerpo = Asign();

            consume(TokenType.In, "Expected 'in' after arguments in let-in statement");

            Expresion inCuerpo = expression();

            return new Expresion.LetIn(letCuerpo, inCuerpo);
        }

        if (match(TokenType.Identificador))
        {
            if (Funciones.ContainsFuncion(peek().Value))
            {
                string name = (string)advance().Value;

                consume(TokenType.ParentesisAbierto, "Expected '(' after expression " + previous().Type + " " + previous().Value);

                TokenType[] a = { TokenType.Coma, TokenType.ParentesisCerrado };

                List<Expresion> argument = new List<Expresion>();

                while (!match(a))
                {
                    Expresion expresion = expression();

                    if (!match(TokenType.ParentesisCerrado))
                    {
                        consume(TokenType.Coma, "Expected ',' before expression " + peek().Type + " " + peek().Value);
                    }

                    argument.Add(expresion);
                }

                consume(TokenType.ParentesisCerrado, "Missing ')' after expression " + previous().Type + " " + previous().Value);

                Expresion.Funcion funcion = Funciones.GetFuncion(name);

                return new Expresion.ExprLLamadaFuncion(name, argument, funcion);
            }

            Expresion.ExprVariable expr = new Expresion.ExprVariable(advance());

            return expr;
        }

        return primary();
    }

    private List<Expresion.ExprAsignar> Asign()
    {
        List<Expresion.ExprAsignar> answer = new List<Expresion.ExprAsignar>();

        while (!match(TokenType.In))
        {
            Token nombre = advance();

            consume(TokenType.Igual, "Expected '=' before expression" + peek().Type + " " + peek().Value + " in the let-in declaration");

            Expresion expr = expression();

            if (!match(TokenType.In))

                consume(TokenType.Coma, "Expected ',' before expression " + peek().Type + " " + peek().Value);

            answer.Add(new Expresion.ExprAsignar(nombre, expr));
        }

        return answer;
    }

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

            consume(TokenType.ParentesisCerrado, "Missing ) after expression " + previous().Type + " " + previous().Value);

            return expr;
        }
        if(match(TokenType.Final)) return null!;
        errores.Add(new ERROR(ERROR.ErrorType.SyntaxError, "Invalid syntax in " + peek().Type + " " + peek().Value));
        return null!;
    }

    private Token consume(TokenType type, string mensaje)
    {
        if (check(type)) return advance();

        errores.Add(new ERROR(ERROR.ErrorType.SyntaxError, mensaje));

        return null!;
    }

}