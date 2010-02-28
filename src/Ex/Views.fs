module Ex.Views

    open System
    open System.Collections.Generic

    type ID = Guid       
        
    type ProductAvailability = {
        ProductID : ID
        Qty : Quantity
    }           

    let Product1 = new Guid("0b2b5fc1-0f3f-4512-9278-fa38339fe0d2")
    let Product2 = new Guid("70533a32-e928-4df2-bda1-7e51007b6206")

    let internal productAvailabilityView = 
        List<ProductAvailability>([{              
                                        ProductID = Product1                                            
                                        Qty = 12.0m                                            
                                  };{                         
                                        ProductID = Product2
                                        Qty = 1234.0m
                                  }])

    let addProductAvailability productAvailability =
        productAvailabilityView.Add(productAvailability)

    let getProductAvailability productID = 
        productAvailabilityView
        |> Seq.find(fun x -> x.ProductID = productID)