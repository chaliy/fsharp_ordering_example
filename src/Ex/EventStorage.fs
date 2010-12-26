module Ex.EventStorage
    
    open Ex.Events
    open Ex.Views        

    // Handler per view 
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
    
    let handlers = [        
        productAvailability        
    ]   

    let Push evt =
        handlers
        |> Seq.iter(fun x -> x(evt.Envelope) )