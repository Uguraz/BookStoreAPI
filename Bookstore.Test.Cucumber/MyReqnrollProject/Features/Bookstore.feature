Feature: Order management

    Scenario: Creating a new order
        Given a valid book with ID 101 exists
        And the customer name is Lasse
        When the order is submitted with book ID 101 and customer name Lasse
        Then the response status should be 201
        
    Scenario: Creating an order with an invalid book ID
        Given the customer name is Lasse for an invalid book ID
        When the order is submitted with book ID -1 and customer name Lasse
        Then the response status should be 404

    Scenario: Creating an order with a missing customer name
        Given a valid book with ID 101 exists 
        When the order is submitted with book ID 101 and empty customer name
        Then the response status should be 400

    Scenario: Creating an order with a duplicate request
        Given a valid book with ID 101 exists
        And the customer name is Lasse for duplicate request
        And the order has already been submitted
        When the duplicate order is submitted
        Then the response status should be 409

    Scenario: Creating an order with a very long customer name
        Given a valid book with ID 101 exists
        And the customer name is Lasse repeated 50 times
        When the order is submitted
        Then the response status should be 400
