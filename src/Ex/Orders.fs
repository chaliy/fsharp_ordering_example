module Ex.Orders

    open System
    open Views
    
    type CustomerRef = Ref
    type OrderRef = Ref
    type ProductRef = Ref        
    
    // Candidates
    type OrderLineCandidate = {
        Product: ProductRef
        Quantity: Quantity        
    }

    type OrderCandidate = {        
        Customer : CustomerRef
        Lines: OrderLineCandidate list
    }

    // Model

    type OrderStatus =
    | Pending    
    | Proved 
    | NotEnoughInventory       
        
    type OrderLine = {
        Quantity : Quantity
        Amount : Amount
        Product : ProductRef
    }       
         
    type OrderDetails = {
        Number : Number
        Total : Amount
        Lines : OrderLine list        
    }    

    type OrderCreated = {    
        Details : OrderDetails
    }

    type OrderPaid = {
        Order : OrderRef
        PaidDate : DateTime
        Total : Amount
    }

    type OrderRevoked = {
        Order : OrderRef
        PaidDate : DateTime
        Total : Amount
        Reason : string
    }

    type ProductPicked = {
        Product : ProductRef
        Qty : Quantity
    }     
            
    let event (evt:obj) = { Envelope = evt }
    
    // Should fire this events
    //  - OrderAccepted
    //  - ProductPicked
    let accept(o : OrderCandidate) = seq {
                
        let is_enough_inventory() =
            o.Lines
            |> Seq.map(fun l -> (l, get l.Product))
            |> Seq.forall(fun (l, p) -> p.Qty > l.Quantity)
       
        yield event( { Details = {
                                    Number = Numbers.next()
                                    Total = 12.0m                                    
                                    Lines = []
                                 } } )

        if is_enough_inventory() then            
            yield! o.Lines
                   |> Seq.map(fun l -> event ( { Product = l.Product
                                                 Qty = l.Quantity } ) )        
     }