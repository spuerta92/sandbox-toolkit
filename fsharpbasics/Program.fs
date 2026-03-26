open System

let sign num =
   if num > 0 then "positive"
   elif num < 0 then "negative"
   else "zero"

let main() =
    // ---
    let x = 10 // immutable by default, type inference
    let mutable y = 20  // explictly set variable are mutable
    y <- 30
    let z:float = 20.26 // explictly declare type

    Console.WriteLine(x)
    Console.WriteLine(y)
    Console.WriteLine(z)
    Console.WriteLine("sign 5: {0}", (sign 5))

    // ---
    let a : int32 = 20
    let b : int32 = 10
    let c : int32 = 15
    let d : int32 = 5

    let mutable e : int32 = 0
    e <- (a + b) * c / d // ( 30 * 15 ) / 5
    printfn "Value of (a + b) * c / d is : %d" e

    e <- ((a + b) * c) / d // (30 * 15 ) / 5
    printfn "Value of ((a + b) * c) / d is : %d" e

    e <- (a + b) * (c / d) // (30) * (15/5)
    printfn "Value of (a + b) * (c / d) is : %d" e

    e <- a + (b * c) / d // 20 + (150/5)
    printfn "Value of a + (b * c) / d is : %d" e

main()