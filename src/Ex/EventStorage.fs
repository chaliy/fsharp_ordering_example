module Ex.EventStorage

    open Ex.Views
    open Ex.Orders

    // Handler per view 
    
    type View<'d, 'r> =    
        abstract member Map : obj -> (ID * 'd) seq
        abstract member Reduce : ID -> ('d seq) -> 'r seq
    
    let productAvailability3 = 
        { new View<Quantity, ProductAvailability> with

             member this.Map evt =
                seq { match evt with
                      | :? ProductPicked as productPicked ->
                          yield (productPicked.Product, productPicked.Qty)                                
                      | _ -> () }

             member this.Reduce id details = 
                seq { yield {
                                ProductID = id
                                Qty = details |> Seq.sumBy(fun x -> x)           
                            } } }               
        

    let productAvailability (evt: obj) = 

        match evt with
        | :? ProductPicked as productPicked -> 
            let product = get productPicked.Product
            add {
                      ProductID = productPicked.Product
                      Qty = product.Qty - productPicked.Qty                        
                }
        | _ -> ()    
        

    let productAvailability2 (e : obj) =
        ()

    let handlers = [        
        productAvailability
        productAvailability2
    ]   

    let Push evt =
        handlers
        |> Seq.iter(fun x -> x(evt.Envelope))

