# HULK: Idioma de la Universidad de La Habana para Kompilers

El lenguaje HULK (simplificado)

HULK es un lenguaje de programación imperativo, funcional, estática y fuertemente tipado. Casi todas las instrucciones en HULK son expresiones. En particular, el subconjunto de HULK que usted implementar se compone solamente de expresiones que pueden escribirse en una línea.

Para ver las reglas gramaticales y conceptos basicos del lenguaje HULK visitar la Orientacion del Proyecto  (https://github.com/matcom/programming/tree/main/projects/hulk).

## ¿Cómo funciona el intérprete de HULK?
El interprete de HULK es una aplicacion de consola, donde el usuario puede introducir una expresión de HULK , presionar ENTER e inmediatamente verá el resultado de evaluar la expresión (si lo hubiere).

Cada línea que comienza con > representa una entrada del usuario , e inmediatamente después se imprime el resultado de evaluar esa expresión.

Para construir la lógica de nuestro intérprete nos hemos basado en la siguente estructura.
### Lexer o Analizador Léxico
Es quien identifica y extrae los llamados tokens (en el informe se explica como hacemos todo esto) , que son los elementos mínimos de caracteres con significado coherente para un lenguaje de programación. Para este proyecto hemos definido en un archivo TokenType los diferentes tipos de tokens que podemos encontrar en nuestro código , cualquier caracter fuera de los definidos sería un token inválido.

#### Errores Léxicos
Si el usuario inserta un token inválido , o sea, que no pertenece a ninguno de los tipos definidos aparecerán en consola los mensajes con los errores. Además en caso de haber alguno el programa se detendrá y no seguirá ejecutandose en una segunda Fase que sería el Parser o Analizador sintáctico.

### Parser o Analizador Sintáctico
Es quien procesa los tokens de acuerdo a las reglas de la gramática del lenguaje de programación (como dónde es permitido un identificador). Por lo general su resultado termina en lo que se llama Árbol de sintaxis.

#### Árbol de Sintaxis
Es una estructura ramificada que representa al código en cuanto a estructura . Los llamados nodos del Árbol pueden representar a expresiones como funciones, declaraciones , etc. Se usa a su vez para realizar el análisis semántico.

En el informe podrán ver la estructura que presentan algunos tipos de expresiones.

#### Errores Sintácticos.
En este caso , a diferencia del anterior, una vez encontremos algún error en cuanto a estructura del código , este no seguirá siendo analizado y aparecerá en pantalla el error y la posición en la que se encuentra.

### Analizador Semántico
Este analizador realiza una comprobación de mayor nivel al código fuente , como por ejemplo , si un tipo de datos es válido en la posición donde se encuentra ( sumar un token de tipo string con otro de tipo number) estructuralmente está correcto pero es imposible realizar dicha operación en HULK . Este analizador trabaja sobre el árbol de sintaxis que se genera en el analizador sintáctico y puede agregarle información adicional.

#### Errores Semánticos
Similar a los errores sintácticos una vez el analizador se tope con un error de este tipo (como sumar un token String + token Number) aparecerá en consola un mensaje con el tipo de error y alguna especificación sobre donde se encuentra.



