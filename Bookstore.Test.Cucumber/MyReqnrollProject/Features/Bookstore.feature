Feature: Order management

    Scenario: Creating a new order
        Given the book ID is 101
        And the customer name is "Lasse"
        When the order is submitted
        Then the response status should be 201
        
    Scenario: Creating an order with an invalid book ID
        Given the book ID is 9999
        And the customer name is "Lasse"
        When the order is submitted
        Then the response status should be 404

    Scenario: Creating an order with a missing customer name
        Given the book ID is 101
        And the customer name is ""
        When the order is submitted
        Then the response status should be 400

    Scenario: Creating an order with a duplicate request
        Given the book ID is 101
        And the customer name is "Lasse"
        And the order has already been submitted
        When the order is submitted
        Then the response status should be 409

    Scenario: Creating an order with a non-numeric book ID
        Given the book ID is "ABC"
        And the customer name is "Lasse"
        When the order is submitted
        Then the response status should be 400

    Scenario: Creating an order with a very long customer name
        Given the book ID is 101
        And the customer name is "Lasse" repeated 50 times
        When the order is submitted
        Then the response status should be 400
