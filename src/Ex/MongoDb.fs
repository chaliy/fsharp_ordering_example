namespace Ex

open MongoDB.Driver

module MongoDB =

    type Context =    
        abstract member Item : string -> MongoDB.Driver.Database with get    
        inherit System.IDisposable

    let connect() =
        let mongo = Mongo()
        ignore (mongo.Connect())
     
        { new Context with
            member this.get_Item(name) = mongo.[name]
            member this.Dispose() = ignore (mongo.Disconnect()) }


[<AutoOpen>]
module MongoDBEx =

    open System.Globalization
    open Microsoft.FSharp.Reflection    
    
    let doc a =
        
        let t = a.GetType()
        if (false = FSharpType.IsRecord t) then
            failwith "Require record" 
            
        let prepVal (x : obj) =
            match x with
            | :? System.Guid as x -> x.ToString() :> obj
            | :? System.Decimal as x -> x.ToString(CultureInfo.InvariantCulture) :> obj
            | x -> x

        let prepName x =
            match x with
            | "ID" -> "_id"     
            | x -> x
       
        let d = new Document()

        (FSharpValue.GetRecordFields a)
        |> Seq.zip(FSharpType.GetRecordFields t)
        |> Seq.map(fun (f, v) -> (prepName f.Name, prepVal v))        
        |> Seq.iter(fun (n, v) -> d.Add(n, v)) 
        
        d