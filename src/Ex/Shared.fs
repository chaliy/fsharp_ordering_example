namespace Ex

type Amount = decimal
type Quantity = decimal
type Number = string
type Name = string
type Ref = System.Guid
type ID = System.Guid

// Context
module Ctx =
    open System

    type Context = {
        ID : Guid
    }

    let current() = {ID = Guid.NewGuid()}

// Unit of Work
module Uow =
    open System         

    type UnitOfWork<'a>(ctx) =
        member x.Add(ev:'a) = ()
        member x.Context = ctx
        member x.Submit() = ()

    let create<'a>() = new UnitOfWork<'a>(Ctx.current())

//    let create<'a>() =
//        { new IObserver<'a> with
//            member this.OnNext(value : 'a) = ()
//            member this.OnError(error : exn ) = ()
//            member this.OnCompleted() = () }

// Sequence Numbers
module Numbers =
    let next (ctx) : Number =
        "12"   