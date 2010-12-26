module Ex.Views
    
    open System
    open System.Collections.Generic    

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