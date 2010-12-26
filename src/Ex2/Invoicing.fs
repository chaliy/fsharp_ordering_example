module Invoicing

type EntityRef<'a>(id : string) =
    let a = ()

type Product = {
    SKU : string
}

type DeliveryService =
| ServiceA
| ServiceB

type DeliveryMethod =
| MethodA
| MethodB


type OrderItem = {
    Product : EntityRef<Product>
    DeliveryMethod : DeliveryMethod
    DeliveryService : DeliveryService
}

type Order = {
    Number : string
    Date : System.DateTime    
    Items : OrderItem list
}

type ShipmentItem = {
    Product : EntityRef<Product>
    Quantity : int
}

type Shipment = {
    Number : string
    Date : System.DateTime
    DeliveryMethod : DeliveryMethod
    DeliveryService : DeliveryService
    Items : ShipmentItem list
}

type InvoiceItem = {
    Product : EntityRef<Product>
}

type Invoice = {
    Number : string
    Date : System.DateTime
    IsPaid : bool
    Items : InvoiceItem list
}