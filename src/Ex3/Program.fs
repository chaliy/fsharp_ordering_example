module Model =

    // Inventory

    type ProductID = {
        SKU : string
    }    

    // Purchasing

    type OrderID = {
        OrderNumber : string
    }

    type DeliveryMethod =
    | MethodA
    | MethodB

    type OrderItem = {
        Product : ProductID
        Quantity : int
        Price : decimal
        DeliveryMethod : DeliveryMethod
    }

    type Order = {
        Number : string
        Date : System.DateTime    
        Items : OrderItem list
    }
    
    type Events =
    // Stocks event
    | StocksReserved of OrderID * ProductID * int
    | ApplyStocks of OrderID * ProductID * int    
    // Purchsing
    | OrderPlaced of Order
    | OrderPayed of OrderID * System.DateTime
    | OrderDelivered of OrderID * System.DateTime


// Logic
open Model

let bus = new Event<Events>()
let readBus = bus.Publish
let promote evt = bus.Trigger(evt)

module Invnetory =
    
    let reserveStocks orderID productID qty =
        promote(StocksReserved(orderID, productID, qty))

module Purchasing =
  
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
|> Event.choose(function | Model.OrderPlaced(o) -> Some(o) | _  -> None )
|> Event.add(fun order -> 
    order.Items
    |> Seq.iter(fun item -> Invnetory.reserveStocks { OrderNumber = order.Number } item.Product item.Quantity) )

