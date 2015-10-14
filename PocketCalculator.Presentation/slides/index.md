- title : FParsec
- description : Introduction to Parsec for F# 
- author : Johann Dirry
- theme : moon
- transition : default

***

## FParsec 

An introduction to the parser combinator library FParsec 

***

### Overview

- [FParsec](http://www.quanttec.com/fparsec/) is a port of Haskell's [Parsec](https://wiki.haskell.org/Parsec)
- provides a custom DSL to combine parsers 
- parsers are combined to generate a parser

---

#### What can it do? 

- recursive-descent text parser for formal LL-type grammars
  - possible symbols must folow deterministically from previous symbols
- support for state and backtracking 

---

#### Alternatives: [FsLex, FsYacc](http://fsprojects.github.io/FsLexYacc/)

- Port of the UNIX Tools `lex` and `yacc`
- FsLex and FsYacc provide a custom DSL in F# 


    let digit = ['0'-'9']
    let whitespace = [' ' '\t' ]
    let newline = ('\n' | '\r' '\n')
    rule token = parse
        | whitespace     { token lexbuf }
        | newline        { newline lexbuf; token lexbuf }
        | "while"        { WHILE }


---

#### Alternatives: [ANTLR4](http://www.antlr.org/)

- available for many programming languages
- C, Python, Java, C#, etc. 
- relies on custom grammar definition 


    grammar Expr;		
    prog:	(expr NEWLINE)* ;
    expr:	expr ('*'|'/') expr
        |	expr ('+'|'-') expr
        |	INT
        |	'(' expr ')'
        ;
    INT     : [0-9]+ ;

--- 

#### Alternatives: Ship your own 

- build a state machine that parses the input
- usually involving regular expressions 
- potentially fastest approach
- hard to do, much work, hard to change 

***

### What is a Parser? 

Takes an input state `s` and returns an output state `s'`, as well as an output `'t`. 

    parser: Parser<'t, unit>

A parser may also have an [user state](http://www.quanttec.com/fparsec/users-guide/parsing-with-user-state.html) `'u`: 

    parser: Parser<'t, 'u>

FParsec comes with a set of predefined parsers for primitives. 


---

### What is a Parser Combinator? 

- A parser generator takes one or more parsers and combines them to a new parser 


    combinator: Parser<'a,'u> -> Parser<'b,'u> -> Parser<'c,'u>

***

### Parser Combinators: (preturn)

creates a parser `p` which returns `x`


    let p = preturn x

---

### Parser Combinators: (>>=)

applies an function `f` to the result of the parser `p` 


    let pf = p >>= f


it is the formal basis for all other combinators

technically, no other combinators are needed 

---

### Parser Combinator: (>>%)

ignores the result of the parser `p` and returns `x`


    let px = p >>% x

---

### Parser Combinators: (>>.)

ignores the result of the parser `p1` and returns the result of the parser `p2`

    let p = p1 >>. p2


---

### Parser Combinators: (.>>)

ignores the result of the parser `p2` and returns the result of the parser `p1`

    let p = p1 .>> p2


---

### Parser Combinators: (.>>.)

combines the result of the parser `p1` and the result of the parser `p2` into a tuple

    let p = p1 .>>. p2

---

### Parser Combinators: (|>>)

takes the result of the parser `p1` and applies the function `f` on it

    let p = p1 |>> f


---

### Parser Combinators: (opt)

returns an option type with `Some` representing the result of `p1`, otherwise it returns `None` 

    let p = opt p1


***

### Operator Precedence Parser 

Used to parse operator expressions. Uses a list of operators to parse expressions and a term parser. 

    let private opp = new OperatorPrecedenceParser<decimal, unit, unit>()
    opp.AddOperator ...
    ...
    let expr = opp.ExpressionParser
    opp.TermParser <- between (pchar '(') (pchar ')') expr


---

### Operator Types

* PrefixOperator 

    `sin 5`

* InfixOperator 

    `2 + 3`

* PostfixOperator 

    `5!`

* TernaryOperator 

    `true ? 5 : 7`

---

### Operator Options 

* Textual representation
* Preamble to ignore
* Precedence 
* Associativity on `InfixOperator`
* Mapping function 
* Support for user state


    InfixOperator("+", spaces, 1, Associativity.Left, fun x y -> x + y) 


*** 

## DEMO

*** 

## Q&A

http://www.quanttec.com/fparsec/ 