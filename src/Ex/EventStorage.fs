module Ex.EventStorage

    open Ex.Views
    open Ex.Orders

    // Handler per view 
    
    type View<'d, 'r> =    
        abstract member Map : obj -> 'd seq
        abstract member Reduce : 'd seq -> 'r seq

    type ProductAvailabilityDelta = {
        ProductID : ID
        Delta : Quantity
    }
        
    let productAvailability3 = 
        { new View<ProductAvailabilityDelta, ProductAvailability> with
             member this.Map(evt) = 
                seq { match evt with
                      | :? ProductPicked as productPicked ->
                          yield {
                                    ProductID = productPicked.Product
                                    Delta = productPicked.Qty                        
                                }
                      | _ -> () }
             member this.Reduce(details) = details 
                                           |> Seq.groupBy(fun x -> x.ProductID)
                                           |> Seq.map(fun (id, pp) -> (id, pp 
                                                                           |> Seq.sumBy(fun p -> p.Delta)))
                                           |> Seq.map(fun (id, qty) -> {
                                                                            ProductID = id
                                                                            Qty = qty                
                                                                       } ) }
        

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

