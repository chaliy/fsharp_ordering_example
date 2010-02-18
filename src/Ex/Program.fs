﻿// Order product scenario
// - Accept order data
// - Rules
// --- Ensure that customer didn't over orderd, 1m total should not be more than 1000
// --- Ensure there is enough inventory
// - Assign new order number
// - Events
// --- Store Order for reference
// - Event handlers
// --- Move products from store to temp storage
// --- Update monthly reports

open Ex
open Ex.Orders
open Microsoft.FSharp.Control

//let uow = Uow.create()
//let ctx = Ctx.current()
//let order = {
//                 Customer = System.Guid.NewGuid()
//                 Lines = [{
//                            Product = PersistenceModel.Product1
//                            Quantity = 10.0m
//                         }] 
//             }    
//Orders.accept(order, ctx, uow)
//
//uow.Submit()

//printfn "%s" "Hello word!"

open Microsoft.FSharp.Reflection

FSharpType.GetRecordFields(typeof<PersistenceModel.Order>)
|> Array.map(fun x -> x.Name)
|> Array.iter(printfn "%s")