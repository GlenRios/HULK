namespace HULK;
    public enum TokenType
    {
        //Tipos de Variables
        String,
        Number,
        True,
        False,
        Identificador,
        //Operadores
        Suma,
        Resta,
        Concatenar,
        Multiplicacion,
        Division,
        Pow,
        Modulo,
        //Comparadores
        IgualIgual,
        NoIgual,
        MayorIgual,
        MenorIgual,
        Mayor,
        Menor,
        //Asignacion
        Igual,        
        Flecha,
        //Operador Booleano
        Negacion,
        And,
        Or,
        //Palabras Reservadas
        If,
        Else,
        function,
        Let,
        In,
        //Constantes 
        PI,
        EULER,
        //Separadores
        Coma,
        PuntoYComa,
        ParentesisAbierto,
        ParentesisCerrado,
        //Final de el coodigo
        Final,
    }