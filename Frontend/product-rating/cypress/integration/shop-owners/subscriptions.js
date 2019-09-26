describe("Subscriptions test", () => {
    it("should list subscriptions", () => {
        cy.visit("/profile");
        cy.wait(200);

        cy.get('a').contains('Subscriptions').click();
        cy.wait(50);

        cy.get('.items').find('.subscription-item').its('length').should('be.gt', 0);
    });

    it("should add a new subscription", () => {
        //add a new item
        cy.get('button').contains('Add').click();

        cy.get('input[name="url"]')
            .type('test-subscription.com');

        cy.get('input[name="dayLimit"]')
            .type('5000');
        cy.get('button').contains('Require').click();

        //check 
        cy.get('.items').should('contain', 'test-subscription.com');
        cy.get('.items').should('contain', '5000');
    });    


    it("should delete a subscription", () => {        
        //delete the last item (should be the one just added)
        cy.get('.subscription-item').last().find('.ng-fa-icon').click();

        //check     
        cy.get('.items').should('not.contain', 'test-subscription.com');
    });    
});