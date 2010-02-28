namespace Ex

type Amount = decimal
type Quantity = int
type Number = string
type Name = string
type Ref = System.Guid
type Event = {
    Envelope : obj
}

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

// Sequence Numbers
module Numbers =
    let next() : Number =
        "12"   