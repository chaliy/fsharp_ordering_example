module Ex.Views

    open System
    open System.Collections.Generic

    type ID = Guid       

//    type OrderPosted = {
//        Details : OrderDetails
//    }

//    type OrderPaid = {
//        Order : OrderRef
//        Amount : Amount
//    }

//    type InventoryMoved = {
//        Inventory : InventoryRef
//        Qty : Quantity
//    }

//    type MonthlyPaidReport = {
//        Date : Month
//        Total : Amount
//    }

//    type PaidOrder = {
//        OrderNumber : Nubmer
//        PaidData : Date
//        CustomerName : FullName
//        Total : Amount        
//    }
        
//    let handleOrderPaid(evt) =
//        // Prepare data
//        let order = Orders.get(evt.Order)
//        let customer = Customers.get(order.Customer)
//        // Store denormalized data to PaidOrders table
//        PaidOrders.add({
//                            OrderNumber = order.Number
//                            PaidDate = evt.PaidDate
//                            CustomerName = customer.Name
//                            Total = order.Total
//                       });

//    let handleOrderPaidMonthly(evt) =        
//        // Store denormalized data
//        MonthlyPaidReport.get(amoun
//        MonthlyPaidReport.add({
//                            OrderNumber = order.Number
//                            PaidDate = evt.PaidDate
//                            CustomerName = customer.Name
//                            Total = order.Total
//                       });

    let get<'a> (id:ID) = 
        Activator.CreateInstance<'a>()

    let add<'a> (r:'a) = 
        ()

    let merge<'a> (r:'a) = 
        ()

    type ProductAvailability = {
        ProductID : ID
        Qty : Quantity
    }

    type MonthlyPaidReport = {        
        Month : Month
        Total : Amount
    }

    let Product1 = new Guid("0b2b5fc1-0f3f-4512-9278-fa38339fe0d2")
    let Product2 = new Guid("70533a32-e928-4df2-bda1-7e51007b6206")
        
        
    let storage = 
        List<ProductAvailability>([{              
                                        ProductID = Product1                                            
                                        Qty = 12                                            
                                  };{                         
                                        ProductID = Product2
                                        Qty = 1234
                                  }])        