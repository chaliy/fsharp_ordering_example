namespace Ex

type Amount = decimal
type Quantity = int
type Number = string
type Name = string
type Ref = System.Guid
type ID = System.Guid
type Event = {
    Envelope : obj
}
type Month = {
    Year : int
    Month : int
}    

// Sequence Numbers
module Numbers =
    let next() : Number =
        "12" 
        
[<AutoOpen>]
module Helpers =    
    let month (d:System.DateTime) =
        { Year = d.Year
          Month = d.Month }

    let event (evt:obj) = { Envelope = evt }