module Ex.Orders

    open System
    open Views
    open Events       
    
    // Candidates
    type OrderLineCandidate = {
        ProductID: ID
        Quantity: Quantity        
    }

    type OrderCandidate = {        
        CustomerID : ID
        Lines: OrderLineCandidate list
    }               
    
    // Should fire this events
    //  - OrderAccepted
    //  - ProductPicked
    let accept(o : OrderCandidate) = seq {
                
        let is_enough_inventory() =
            o.Lines
            |> Seq.map(fun l -> (l, get l.ProductID))
            |> Seq.forall(fun (l, p) -> p.Qty > l.Quantity)
                        
        yield event( (*OrderCreated*) { Details = {
                                                        Number = Numbers.next()
                                                        Total = 12.0m                                    
                                                        Lines = []
                                                   } } )

        if is_enough_inventory() then            
            yield! o.Lines
                   |> Seq.map(fun l -> event ( { Product = l.ProductID
                                                 Qty = l.Quantity } ) )        
     }