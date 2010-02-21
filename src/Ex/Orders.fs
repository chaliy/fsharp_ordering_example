module Ex.Orders    

    open System
    open Inventory
    open Uow
    open Ctx
    
    type OrderRef = Ref
    
    // Candidates
    type OrderLineCandidate = {
        Product: ID
        Quantity: Quantity        
    }

    type OrderCandidate = {        
        Customer : ID
        Lines: OrderLineCandidate list
    }

    // Model
    type OrderLine = {
        Quantity : Quantity
        Amount : Amount
        Product : ProductRef
    }       
         
    type Order = {
        Number : Number
        Total : Amount        
        Lines : OrderLine list        
    }
    
    type Event =        
        | NotEnoughInventoryToAcceptOrder of OrderCandidate
        | OrderCreated of Order

    let accept(o : OrderCandidate, ctx : Context, uow : UnitOfWork<Event>) =
        //let oldOrders = PersistenceModel.allOrders ctx
        let products = Views.allProducts ctx
        
        // Ensure there is enough inventory
        let enough =
            o.Lines
            |> Seq.map(fun l -> (l, 
                                 products 
                                 |> Seq.find(fun p -> p.ID = l.Product))
                                )
            |> Seq.forall(fun (l, p) -> p.AvailableQty > l.Quantity)

        if enough then
            let order = {
                Number = Numbers.next ctx
                Total = 12.0m
                Lines = []
            } 
            uow.Add(Event.OrderCreated(order))
        else
            uow.Add(Event.NotEnoughInventoryToAcceptOrder(o))