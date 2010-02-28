module Ex.Orders

    open System
    
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
        Status: OrderStatus
        Lines : OrderLine list        
    }    

    type OrderCreated = {    
        Order : OrderDetails
    }

    type ProductPicked = {
        Product : ProductRef
        Qty : Quantity
    }    

    let event (evt:obj) = { Envelope = evt }
    
    let accept(o : OrderCandidate) = seq {
                
        let is_enough_inventory() =
            o.Lines
            |> Seq.map(fun l -> (l, Views.getProductAvailability(l.Product)))
            |> Seq.forall(fun (l, p) -> p.Qty > l.Quantity)
                
        yield event { Order = {
                                Number = Numbers.next()
                                Total = 12.0m
                                Status = OrderStatus.NotEnoughInventory
                                Lines = []
                              } }

        if is_enough_inventory() then
            yield! o.Lines
                   |> Seq.map(fun l -> event { Product = l.Product
                                               Qty = l.Quantity })        
     }