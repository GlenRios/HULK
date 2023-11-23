
namespace HULK;

public class Tokenizador
{
    //en esta lista iremos guardando todos los tokens
    public List<Token> Tokens;
    //esta lista es para los errores lexicos
    public List<ERROR> errores;
    //en el constructor se le pasa como parametro el string insertado por el usuario 
    public Tokenizador(string input)
    {
        Tokens = new List<Token>();

        errores = new List<ERROR>();

        MakeTokens(input);
    }

    //Este metodo comienza a iterar por el string verificando si cada 
    //caracter coincide con algun tipo de token y creandolo para guardarlo en la lista.
    //cuando llega al final guarda un token de tipo final para poder comprobar en el parser si estamos al final de la lista de tokens.
    public void MakeTokens(string x)
    {
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] == ' ') continue;

            if (x[i] == ';')
            {
                Tokens.Add(new Token(TokenType.PuntoYComa, ""));
                continue;
            }

            if (x[i] == '@')
            {
                Tokens.Add(new Token(TokenType.Concatenar, ""));
                continue;
            }

            if (x[i] == '(')
            {
                Tokens.Add(new Token(TokenType.ParentesisAbierto, ""));
                continue;
            }

            if (x[i] == ')')
            {
                Tokens.Add(new Token(TokenType.ParentesisCerrado, ""));
                continue;
            }

            if (x[i] == '*')
            {
                Tokens.Add(new Token(TokenType.Multiplicacion, ""));
                continue;
            }

            if (x[i] == '/')
            {
                Tokens.Add(new Token(TokenType.Division, ""));
                continue;
            }

            if (x[i] == '%' && (i != x.Length - 1))
            {
                Tokens.Add(new Token(TokenType.Modulo, ""));
                continue;
            }

            if (x[i] == '+')
            {
                Tokens.Add(new Token(TokenType.Suma, ""));
                continue;
            }

            if (x[i] == '-')
            {
                Tokens.Add(new Token(TokenType.Resta, ""));
                continue;
            }

            if (x[i] == ',')
            {
                Tokens.Add(new Token(TokenType.Coma, ""));
                continue;
            }

            if (x[i] == '^')
            {
                Tokens.Add(new Token(TokenType.Pow, ""));
                continue;
            }

            if (x[i] == '!')
            {
                if (x[i + 1] == '=')
                {
                    Tokens.Add(new Token(TokenType.NoIgual, ""));
                    i++;
                    continue;
                }

                else Tokens.Add(new Token(TokenType.Negacion, ""));
                continue;
            }

            if (x[i] == '|')
            {
                if (x[i + 1] == '|')
                {
                    Tokens.Add(new Token(TokenType.Or, ""));
                    i++;
                    continue;
                }

                else
                {
                    ERROR error = new ERROR(ERROR.ErrorType.LexicalError, x[i] + " is not a valid token");
                    errores.Add(error);
                    continue;
                }
            }

            if (x[i] == '&')
            {
                if (x[i + 1] == '&')
                {
                    Tokens.Add(new Token(TokenType.And, ""));
                    i++;
                    continue;
                }

                else
                {
                    errores.Add(new ERROR(ERROR.ErrorType.LexicalError, x[i] + " is not a valid token"));
                    continue;
                }
            }

            if (x[i] == '>')
            {
                if (x[i + 1] == '=')
                {
                    Tokens.Add(new Token(TokenType.MayorIgual, ""));
                    i++;
                    continue;
                }

                else
                {
                    Tokens.Add(new Token(TokenType.Mayor, ""));
                    continue;
                }
            }

            if (x[i] == '<')
            {
                if (x[i + 1] == '=')
                {
                    Tokens.Add(new Token(TokenType.MenorIgual, ""));
                    i++;
                    continue;
                }

                else
                {
                    Tokens.Add(new Token(TokenType.Menor, ""));
                    continue;
                }
            }

            if (x[i] == '=')
            {
                if (x[i + 1] == '=')
                {
                    Tokens.Add(new Token(TokenType.IgualIgual, ""));
                    i++;
                    continue;
                }

                if (x[i + 1] == '>')
                {
                    Tokens.Add(new Token(TokenType.Flecha, ""));
                    i++;
                    continue;
                }

                else
                {
                    Tokens.Add(new Token(TokenType.Igual, ""));
                    continue;
                }
            }

            if (x[i] == '"')
            {
                string a = "";
                for (int j = i + 1; j < x.Length; j++)
                {

                    if (x[j] == '"')
                    {
                        i = j;
                        break;
                    }

                    else
                    {
                        a += x[j];
                    }

                    if (j == x.Length - 1 && x[j] != '"')
                    {
                        ERROR error = new ERROR(ERROR.ErrorType.LexicalError, "String " + a + " was declared incorrectly");
                        errores.Add(error);
                    }

                    continue;
                }

                Tokens.Add(new Token(TokenType.String, a));
                continue;
            }

            if (x[i] == '0' || x[i] == '1' || x[i] == '2' || x[i] == '3' || x[i] == '4' || x[i] == '5' || x[i] == '6' || x[i] == '7' || x[i] == '8' || x[i] == '9')
            {
                string numero = "";
                numero = numero + x[i];
                int ContadorDePuntos = 0;
                bool valido = true;

                for (int j = i + 1; j < x.Length; j++)
                {

                    if (x[j] == '0' || x[j] == '1' || x[j] == '2' || x[j] == '3' || x[j] == '4' || x[j] == '5' || x[j] == '6' || x[j] == '7' || x[j] == '8' || x[j] == '9')
                    {
                        numero += x[j];
                        i = j;
                        continue;
                    }

                    if (x[j] == '.')
                    {
                        numero += x[j];
                        i = j;
                        ContadorDePuntos++;
                        continue;
                    }

                    if (x[j] == 'i')
                    {
                        if (x[j + 1] == 'n')
                        {
                            i = j;
                            break;
                        }

                        numero += x[j];
                        valido = false;
                    }

                    if (x[j] == '_' || x[j] == 'A' || x[j] == 'a' || x[j] == 'B' || x[j] == 'b' || x[j] == 'C' || x[j] == 'c' || x[j] == 'D' || x[j] == 'd' || x[j] == 'E' || x[j] == 'e' || x[j] == 'F' || x[j] == 'f' || x[j] == 'G' || x[j] == 'g' || x[j] == 'H' || x[j] == 'h' || x[j] == 'I' || x[j] == 'J' || x[j] == 'j' || x[j] == 'K' || x[j] == 'k' || x[j] == 'L' || x[j] == 'l' || x[j] == 'M' || x[j] == 'm' || x[j] == 'N' || x[j] == 'n' || x[j] == 'O' || x[j] == 'o' || x[j] == 'P' || x[j] == 'p' || x[j] == 'Q' || x[j] == 'q' || x[j] == 'R' || x[j] == 'r' || x[j] == 'S' || x[j] == 's' || x[j] == 'T' || x[j] == 't' || x[j] == 'U' || x[j] == 'u' || x[j] == 'V' || x[j] == 'v' || x[j] == 'W' || x[j] == 'W' || x[j] == 'X' || x[j] == 'x' || x[j] == 'Y' || x[j] == 'y' || x[j] == 'Z' || x[j] == 'z')
                    {
                        valido = false;
                        numero += x[j];
                        i = j;
                        continue;
                    }

                    break;
                }

                if (valido == false)
                {
                    ERROR error = new ERROR(ERROR.ErrorType.LexicalError, numero + " is not a valid token");
                    errores.Add(error);
                    continue;
                }

                if (ContadorDePuntos > 1)
                {
                    ERROR error = new ERROR(ERROR.ErrorType.LexicalError, " Number " + numero + " was declared incorrectly");
                    errores.Add(error);
                    continue;
                }

                Tokens.Add(new Token(TokenType.Number, double.Parse(numero)));
                continue;
            }

            if (x[i] == 'A' || x[i] == 'a' || x[i] == 'B' || x[i] == 'b' || x[i] == 'C' || x[i] == 'c' || x[i] == 'D' || x[i] == 'd' || x[i] == 'E' || x[i] == 'e' || x[i] == 'F' || x[i] == 'f' || x[i] == 'G' || x[i] == 'g' || x[i] == 'H' || x[i] == 'h' || x[i] == 'I' || x[i] == 'i' || x[i] == 'J' || x[i] == 'j' || x[i] == 'K' || x[i] == 'k' || x[i] == 'L' || x[i] == 'l' || x[i] == 'M' || x[i] == 'm' || x[i] == 'N' || x[i] == 'n' || x[i] == 'O' || x[i] == 'o' || x[i] == 'P' || x[i] == 'p' || x[i] == 'Q' || x[i] == 'q' || x[i] == 'R' || x[i] == 'r' || x[i] == 'S' || x[i] == 's' || x[i] == 'T' || x[i] == 't' || x[i] == 'U' || x[i] == 'u' || x[i] == 'V' || x[i] == 'v' || x[i] == 'W' || x[i] == 'W' || x[i] == 'X' || x[i] == 'x' || x[i] == 'Y' || x[i] == 'y' || x[i] == 'Z' || x[i] == 'z')
            {
                string a = "";
                a = a + x[i];

                for (int j = i + 1; j < x.Length; j++)
                {
                    if (x[j] == '_' || x[j] == 'A' || x[j] == 'a' || x[j] == 'B' || x[j] == 'b' || x[j] == 'C' || x[j] == 'c' || x[j] == 'D' || x[j] == 'd' || x[j] == 'E' || x[j] == 'e' || x[j] == 'F' || x[j] == 'f' || x[j] == 'G' || x[j] == 'g' || x[j] == 'H' || x[j] == 'h' || x[j] == 'I' || x[j] == 'i' || x[j] == 'J' || x[j] == 'j' || x[j] == 'K' || x[j] == 'k' || x[j] == 'L' || x[j] == 'l' || x[j] == 'M' || x[j] == 'm' || x[j] == 'N' || x[j] == 'n' || x[j] == 'O' || x[j] == 'o' || x[j] == 'P' || x[j] == 'p' || x[j] == 'Q' || x[j] == 'q' || x[j] == 'R' || x[j] == 'r' || x[j] == 'S' || x[j] == 's' || x[j] == 'T' || x[j] == 't' || x[j] == 'U' || x[j] == 'u' || x[j] == 'V' || x[j] == 'v' || x[j] == 'W' || x[j] == 'W' || x[j] == 'X' || x[j] == 'x' || x[j] == 'Y' || x[j] == 'y' || x[j] == 'Z' || x[j] == 'z' || x[j] == '0' || x[j] == '1' || x[j] == '2' || x[j] == '3' || x[j] == '4' || x[j] == '5' || x[j] == '6' || x[j] == '7' || x[j] == '8' || x[j] == '9')
                    {
                        a += x[j];
                        i = j;
                        continue;
                    }

                    break;
                }

                if (a == "true")
                {
                    Tokens.Add(new Token(TokenType.True, ""));
                    continue;
                }

                if (a == "false")
                {
                    Tokens.Add(new Token(TokenType.False, ""));
                    continue;
                }

                if (a == "PI")
                {
                    Tokens.Add(new Token(TokenType.PI, Math.PI));
                    continue;
                }

                if (a == "EULER")
                {
                    Tokens.Add(new Token(TokenType.EULER, Math.E));
                    continue;
                }

                if (a == "if")
                {
                    Tokens.Add(new Token(TokenType.If, ""));
                    continue;
                }

                if (a == "else")
                {
                    Tokens.Add(new Token(TokenType.Else, ""));
                    continue;
                }

                if (a == "let")
                {
                    Tokens.Add(new Token(TokenType.Let, ""));
                    continue;
                }

                if (a == "in")
                {
                    Tokens.Add(new Token(TokenType.In, ""));
                    continue;
                }

                if (a == "function")
                {
                    Tokens.Add(new Token(TokenType.function, ""));
                    continue;
                }

                Tokens.Add(new Token(TokenType.Identificador, a));
                continue;
            }

            errores.Add(new ERROR(ERROR.ErrorType.LexicalError, " '" + x[i] + "' is not a valid token" + i));
        }

        Tokens.Add(new Token(TokenType.Final, ""));

        if (Tokens.Count < 3)
        {
            throw new ERROR(ERROR.ErrorType.SyntaxError, " Invalid expression , valid expressions cannot have less than three tokens");
        }

    }
}