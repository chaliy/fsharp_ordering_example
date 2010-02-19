namespace Ex

open System

module PersistenceModel =

    type ProductRef = Ref

    // Orders
    type OrderLine = {
        Quantity : Quantity
        Amount : Amount
        Product : ProductRef
    }
        
    type Order = {
        ID : ID
        Number : Number
        Total : Amount                
        Lines : OrderLine list        
    }

    let Order1 = Guid.NewGuid()

    let allOrders(ctx) = [{                         
                            ID = Order1
                            Number = "123"
                            Total = 12.0m
                            Lines = []
                         }]
    
    // Customers
    type Customer = {
        ID : ID
        FullName : Name
    }
    
    // Inventory
    type Product = {
        ID : ID
        Title : string
        AvailableQty : Quantity
        Price : Amount        
    }

    let Product1 = new Guid("0b2b5fc1-0f3f-4512-9278-fa38339fe0d2")
    let Product2 = Guid.NewGuid()
    
    let allProducts(ctx) = [{              
                                ID = Product1
                                Title = "123"
                                AvailableQty = 12.0m
                                Price = 150.0m
                           };{                         
                                ID = Product2
                                Title = "1234"
                                AvailableQty = 5.0m
                                Price = 250.0m
                           }]