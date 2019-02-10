namespace Tests

open NUnit.Framework

module Calculator =
    let calcul opt state = 
        match state with
        | n1 :: n2 :: tail -> 
            let result = opt n2 n1
            printfn "n1 %i, n2 %i, result %i" n1 n2 result
            result :: tail
        | _ -> invalidOp "calcul"

    let (|Number|_|) n = 
        match System.Int32.TryParse n with
        | true, v -> Some v
        | _ -> None

    let private agg (state:int list) (element:string) =
        match element with
        | Number(n) -> n :: state
        | "+" -> calcul (+) state
        | "-" -> calcul (-) state
        | "/" -> calcul (/) state
        | "*" -> calcul (*) state
        | s -> invalidOp <| sprintf "Agg: %s" s

    let calculate (st : string) = 
        st.Split([|' '|])
        |> Seq.fold agg []
        |> Seq.head

type CalculatorShould () =
    [<TestCase("1 1 +")>]
    member __.``Return 2 for expression '1 1 +'`` expression =
        expression
        |> Calculator.calculate
        |> fun v -> Assert.AreEqual(2, v)

    [<TestCase("1 1 1 + +")>]
    member __.``Return 3 for expression '1 1 1 + +'`` expression =
        expression
        |> Calculator.calculate
        |> fun v -> Assert.AreEqual(3, v)

    [<TestCase("1 1 1 - +")>]
    member __.``Return 1 for expression '1 1 1 - +'`` expression =
        expression
        |> Calculator.calculate
        |> fun v -> Assert.AreEqual(1, v)

    [<TestCase("2 3 11 + 5 - *")>]
    member __.``Return 18 for expression '2 3 11 + 5 - *'`` expression =
        expression
        |> Calculator.calculate
        |> fun v -> Assert.AreEqual(18, v)

    [<TestCase("15 7 1 1 + - / 3 * 2 1 1 + + -")>]
    member __.``Return 5 for expression '15 7 1 1 + - / 3 * 2 1 1 + + -'`` expression =
        expression
        |> Calculator.calculate
        |> fun v -> Assert.AreEqual(5, v)
