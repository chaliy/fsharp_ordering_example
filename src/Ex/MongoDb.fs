module MongoDB

    open MongoDB.Driver

    type Context =    
        abstract member Item : string -> MongoDB.Driver.Database with get    
        inherit System.IDisposable

    module Server =       
        let Connect() =
            let mongo = Mongo()
            ignore (mongo.Connect())
         
            { new Context with
                member this.get_Item(name) = mongo.[name]
                member this.Dispose() = ignore (mongo.Disconnect()) }

    module Cnvt =

        open System.Globalization
        open Microsoft.FSharp.Reflection 
        open MongoDB.Driver   
        
        let toDocument a =
            
            let t = a.GetType()
            if (false = FSharpType.IsRecord t) then
                failwith "Requires record" 
                        
            let d = new Document()

            (FSharpValue.GetRecordFields a)
            |> Seq.zip(FSharpType.GetRecordFields t)
            |> Seq.map(fun (f, v) -> (( match f.Name with
                                          | "ID" -> "_id"     
                                          | x -> x ),
                                      ( match v with
                                          | :? System.Guid as x -> x.ToString() :> obj
                                          | :? System.Decimal as x -> x.ToString(CultureInfo.InvariantCulture) :> obj
                                          | x -> x )))        
            |> Seq.iter(fun (n, v) -> d.Add(n, v)) 
            
            d

    type MongoDB.Driver.Database with       
        member x.For<'a>() =        
            x.[typeof<'a>.Name]            

        member x.Insert<'a> (a: 'a) =
            let col = x.For<'a>()
            col.Insert(Cnvt.toDocument a)