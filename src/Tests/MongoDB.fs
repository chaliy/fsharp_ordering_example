module ``MongoDB Extensions Specification``

    open FsSpec        

    type FakeRec = {
        String : string
    }

    module ``Describe converting Record to Document`` =

        let fake = {String = "Test"}
        let res = MongoDB.Cnvt.toDocument(fake)
    
        let ``return at least something...`` = spec {                        
            res.should_not_be_null                
        }
        
        let ``covert string field`` = spec {            
            let resultString = (res.["String"] :?> string)
            resultString.should_be_equal_to "Test"
        }              