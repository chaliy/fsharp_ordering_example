
open Invoicing

let get<'a> ref = async {
    return System.Activator.CreateInstance<'a>()
}

let promote msg =
    ()

type InvoicePaid = { Invoice : EntityRef<Invoice>
                     Amount : decimal }
type InvoiceAlreadyPaid = { Invoice : EntityRef<Invoice> }

type Events =
| InvoicePaid of InvoicePaid
| InvoiceAlreadyPaid

let invoicePaid invoiceRef amount = async {

    let! invoice = get<Invoice> invoiceRef

    if invoice.IsPaid then
        promote (Events.InvoiceAlreadyPaid)
    
    promote (Events.InvoicePaid({ Invoice = invoiceRef
                                  Amount = amount }))
}