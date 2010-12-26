module Ex.Events

    open System
    
    type OrderRef = Ref
    type ProductRef = Ref
        
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

