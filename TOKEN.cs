namespace HULK;
//En esta clase definimos las propiedades que van a tener nuestros tokens
public class Token
{
    public TokenType Type;
    public object Value;
    public Token(TokenType type, object value)
    {
        Type = type;
        Value = value;
    }
}