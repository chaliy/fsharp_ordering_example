let bus = new Event<obj>()
let readBus = bus.Publish
let promote evt =    
    ()

module Identifiers =

    type ProductID = {
        SKU : string
    }

    type OrderID = {
        OrderNumber : string
    }

module Invnetory =

    type StocksEvents =
    | StocksReserved of Identifiers.OrderID * Identifiers.ProductID * int
    | ApplyStocks of Identifiers.OrderID * Identifiers.ProductID * int
        
    let reserveStocks orderID productID qty =
        promote(StocksReserved(orderID, productID, qty))

module Purchasing =
  
    type DeliveryMethod =
    | MethodA
    | MethodB

    type OrderItem = {
        Product : Identifiers.ProductID
        Quantity : int
        Price : decimal
        DeliveryMethod : DeliveryMethod
    }

    type Order = {
        Number : string
        Date : System.DateTime    
        Items : OrderItem list
    }
    
    type OrderEvents =    
    | OrderPlaced of Order
    | OrderPayed of Identifiers.OrderID * System.DateTime
    | OrderDelivered of Identifiers.OrderID * System.DateTime

    let validatePlaceOrder order = true
    
    let placeOrder order =
        promote(OrderPlaced(order))
        // record to Db
        ()

    let payOrder orderId = 
        // apply stocks
        // notify everybody
        // record to Db
        ()

    let delieverOrder orderId =         
        // notify everybody
        // record to Db
        ()

    let cancel orderNumber =
        // release stocks
        // record to bd
        ()


// For all order placed 
readBus 
|> Event.choose(function
                 | :? Purchasing.OrderEvents as x -> 
                    match x with
                    | Purchasing.OrderEvents.OrderPlaced(o) -> Some(o)
                    | _  -> None
                 | _  -> None )
|> Event.add(fun order -> 
    order.Items
    |> Seq.iter(fun item -> Invnetory.reserveStocks { OrderNumber = order.Number } item.Product item.Quantity) )

