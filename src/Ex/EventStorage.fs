module Ex.EventStorage

    open Ex.Views
    open Ex.Orders

    // Handlers
    let productAvailability (e : Event) =
        let productPicked = e.Envelope :?> ProductPicked
        let product = Views.getProductAvailability(productPicked.Product)
        Views.addProductAvailability({              
                                        ProductID = productPicked.Product
                                        Qty = product.Qty - productPicked.Qty                        
                                     });
        ()

    let productAvailability2 (e : Event) =
        ()

    let handlers = [        
        productAvailability
        productAvailability2
    ]   

    let push evt =
        handlers
        |> Seq.iter(fun x -> x(evt))

