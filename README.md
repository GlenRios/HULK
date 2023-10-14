# HULK: Idioma de la Universidad de La Habana para Kompilers

El lenguaje HULK (simplificado)

HULK es un lenguaje de programación imperativo, funcional, estática y fuertemente tipado. Casi todas las instrucciones en HULK son expresiones. En particular, el subconjunto de HULK que usted implementar se compone solamente de expresiones que pueden escribirse en una línea.

## EXPRESIONES BÁSICAS
Todas las instrucciones en HULK terminan en ;. La instrucción más simple en HULK que hace algo es la siguiente:

print("Hello World");

## HULK además tiene EXPRESIONES ARITMÉTICAS:

print((((1 + 2) ^ 3) * 4) / 5);

## Y FUNCIONES MATEMÁTICAS básicas:

print(sin(2 * PI) ^ 2 + cos(3 * PI / log(4, 64)));

## FUNCIONES
En HULK existen las funciones inline.Un ejemplo de este tipo de funciones sería

function tan(x) => sin(x) / cos(x);

Una vez definida una función, puede usarse en una expresión cualquiera:

print(tan(PI/2));

El cuerpo de una función inline es una expresión cualquiera, que por supuesto puede incluir otras funciones y expresiones básicas, o cualquier combinación.

## VARIABLES
En HULK es posible declarar variables usando la expresión let-in, que funciona de la siguiente forma:

let x = PI/2 in print(tan(x));

En general, una expresión let-in consta de una o más declaraciones de variables, y un cuerpo, que puede ser cualquier expresión donde además se pueden utilizar las variables declaradas en el let. Fuera de una expresión let-in las variables dejan de existir.

Por ejemplo, con dos variables:

let number = 42, text = "The meaning of life is" in print(text @ number);

Que es equivalente a:

let number = 42 in (let text = "The meaning of life is" in (print(text @ number)));

El valor de retorno de una expresión let-in es el valor de retorno del cuerpo, por lo que es posible hacer:

print(7 + (let x = 2 in x * x));

Que da como resultado 11.

La expresión let-in permite hacer mucho más, pero para este proyecto usted solo necesita implementar las funcionalidades anteriores.

## CONDICIONALES
Las condiciones en HULK se implementan con la expresión if-else, que recibe una expresión booleana entre paréntesis, y dos expresiones para el cuerpo del if y el else respectivamente. Siempre deben incluirse ambas partes:

let a = 42 in if (a % 2 == 0) print("Even") else print("odd");

Como if-else es una expresión, se puede usar dentro de otra expresión (al estilo del operador ternario en C#):

let a = 42 in print(if (a % 2 == 0) "even" else "odd");

## RECURSIÓN
Dado que HULK tiene funciones compuestas, por definición tiene también soporte para recursión. Un ejemplo de una función recursiva en HULK es la siguiente:

function fib(n) => if (n > 1) fib(n-1) + fib(n-2) else 1;

Usted debe garantizar que su implementación permite este tipo de definiciones recursivas.

## EL INTÉRPRETE
Su intérprete de HULK será una aplicación de consola, donde el usuario puede introducir una expresión de HULK, presionar ENTER, e immediatamente se verá el resultado de evaluar expresión (si lo hubiere) Este es un ejemplo de una posible interacción:


'''

       > let x = 42 in print(x);
       
       42
       
       > function fib(n) => if (n > 1) fib(n-1) + fib(n-2) else 1;
       
       > fib(5);
      
       13
       
       > let x = 3 in fib(x+1);
       
       8
       
       > print(fib(6));
       
       21
'''


Cada línea que comienza con > representa una entrada del usuario, e immediatamente después se imprime el resultado de evaluar esa expresión, si lo hubiere.

Note que cuando una expresión tiene valor de retorno (como en el caso de un llamado a una función), directamente se imprime el valor retornado, aunque no haya una instrucción print.

Todas las funciones declaradas anteriormente son visibles en cualquier expresión subsiguiente. Las funciones no pueden redefinirse

## ERRORES

