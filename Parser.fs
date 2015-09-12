namespace PocketCalculator

open FParsec
open System

module Parser = 
    exception ParserException of string

    type Number = 
        Int of int64 
        | Decimal of decimal

    let private numberToDecimal (number:Number):decimal = 
        match number with
        | Int(value) -> decimal value
        | Decimal(value) -> value

    let private exec (parser) (input:string) = 
        match run parser input with 
        | Success(result, _, _) -> result
        | Failure(errorMessage, _, _) -> 
            printfn "%s" errorMessage
            raise (ParserException(errorMessage))

    let private numberLiteralToNumber (value:NumberLiteral) : Number = 
        if value.IsInteger
        then Int(int64 value.String)
        else Decimal(decimal value.String)
    
    let private pnumber : Parser<decimal, unit> =
        let numberFormat = NumberLiteralOptions.AllowFraction
        numberLiteral numberFormat "number" 
        |>> numberLiteralToNumber 
        |>> numberToDecimal
    
    let private pCircleConstant : Parser<decimal,unit> =
        let pi = 3.14159265358979323846264338327m
        pstring "π" <|> pstring "\pi"
        |>> fun _ -> pi

    let private pEulersNumber : Parser<decimal,unit> =
        let e = 2.7182818284590452353602874713527m
        pstring "e" <|> pstring "\e"
        |>> fun _ -> e

    let private pGoldenRatio : Parser<decimal,unit> =
        let phi = 1.61803398874989484820458683436m // (1 + sqrt(5))/2
        pstring "ϕ" <|> pstring "\phi"
        |>> fun _ -> phi

    let private pConstant = 
        many1 (pCircleConstant <|> pEulersNumber <|> pGoldenRatio)

    let private pNumberOrConstant : Parser<decimal,unit> =
        pnumber .>>. opt pConstant 
        |>> fun (n, pi) -> match (pi) with
                           | None -> n
                           | Some(c) ->  c |> List.fold (fun a b -> a*b) n
    
    let private unary (f) (value:decimal):decimal = 
        decimal(f(double(value)))

    let private binary (f) (left:decimal) (right:decimal):decimal = 
        decimal(f(double(left), double(right)))

    let private sin = unary Math.Sin
    let private sinh = unary Math.Sinh
    let private asin = unary Math.Asin
    let private cos = unary Math.Cos
    let private cosh = unary Math.Cosh
    let private acos = unary Math.Acos
    let private tan = unary Math.Tan
    let private tanh = unary Math.Tanh
    let private atan = unary Math.Atan
    let private pow = binary Math.Pow

    let private opp = new OperatorPrecedenceParser<decimal,unit,unit>()
    opp.AddOperator(InfixOperator("+", spaces, 1, Associativity.Left, fun x y -> x + y))
    opp.AddOperator(InfixOperator("-", spaces, 1, Associativity.Left, fun x y -> x - y))
    opp.AddOperator(InfixOperator("*", spaces, 2, Associativity.Left, fun x y -> x * y))
    opp.AddOperator(InfixOperator("/", spaces, 2, Associativity.Left, fun x y -> x / y))
    opp.AddOperator(InfixOperator("^", spaces, 3, Associativity.Left, fun x y -> pow x y))
    opp.AddOperator(PrefixOperator("-", spaces, 1, true, fun x -> -x))
    opp.AddOperator(PrefixOperator("sin", spaces, 1, true, fun x -> sin x))
    opp.AddOperator(PrefixOperator("cos", spaces, 1, true, fun x -> cos x))
    opp.AddOperator(PrefixOperator("tan", spaces, 1, true, fun x -> tan x))
    opp.AddOperator(PrefixOperator("asin", spaces, 1, true, fun x -> asin x))
    opp.AddOperator(PrefixOperator("acos", spaces, 1, true, fun x -> acos x))
    opp.AddOperator(PrefixOperator("atan", spaces, 1, true, fun x -> atan x))
    opp.AddOperator(PrefixOperator("sinh", spaces, 1, true, fun x -> sinh x))
    opp.AddOperator(PrefixOperator("cosh", spaces, 1, true, fun x -> cosh x))
    opp.AddOperator(PrefixOperator("tanh", spaces, 1, true, fun x -> tanh x))
    opp.AddOperator(PostfixOperator("%", spaces, 1, true, fun x -> x/100m))
    
    let expr = opp.ExpressionParser

    opp.TermParser <-
        pNumberOrConstant 
        <|> between (pchar '(') (pchar ')') expr

    let calculator (input) =
        exec expr input
