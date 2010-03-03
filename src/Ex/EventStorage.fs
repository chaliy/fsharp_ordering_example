module Ex.EventStorage
    
    open Ex
    open Ex.Views
    open Ex.Orders    

    // Handler per view 
    
    type View<'k, 'd, 'r> =    
        abstract member Map : obj -> ('k * 'd) seq
        abstract member Reduce : 'k -> ('d seq) -> 'r seq
        
        
    type View2<'k, 'd, 'r> = {
        Map : obj -> ('k * 'd) seq
        Reduce : 'k -> ('d seq) -> 'r seq            
    }               
    
    // This is problematic, because this guy will work 
    // only on the whole set of events.    
    let productAvailability3 = 
        { Map = fun evt ->
                seq { match evt with
                      | :? ProductPicked as productPicked ->
                          yield (productPicked.Product, productPicked.Qty)
                      | _ -> () }

         Reduce = fun id details ->
                seq { yield {
                                ProductID = id
                                Qty = details |> Seq.sumBy(fun x -> x)           
                            } } }               
        

    let productAvailability (evt: obj) = 

        match evt with
        | :? ProductPicked as productPicked -> 
            let product = get productPicked.Product
            merge {
                      ProductID = productPicked.Product
                      Qty = product.Qty - productPicked.Qty                        
                  }
        | _ -> ()

//     let monthlyPaidReport2 = function                         
//          // if order paid push total
//          | :? OrderPaid as x ->
//              push (month x.PaidDate, x.Total)
//          // if revoked push negative total
//          | :? OrderRevoked as x ->
//              push (month x.PaidDate, -x.Total)
//          | _ -> ()

    let monthlyPaidReport = 
        { new View<Month, Amount, MonthlyPaidReport> with

             member this.Map evt =
                seq { match evt with
                      | :? OrderPaid as x ->
                          yield (month x.PaidDate, x.Total)
                      | :? OrderRevoked as x ->
                          yield (month x.PaidDate, -x.Total)
                      | _ -> () }

             member this.Reduce month details = 
                seq { yield {
                                Month = month
                                Total = details |> Seq.sumBy(fun x -> x)           
                            } } }
        

    let productAvailability2 (e : obj) =
        ()

    let handlers = [        
        productAvailability
        productAvailability2
    ]   

    let Push evt =
        handlers
        |> Seq.iter(fun x -> x(evt.Envelope) )