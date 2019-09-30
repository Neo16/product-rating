describe("should test admin review features", () => { 
    it("should delete a comment", () => {
        cy.logoutIfLoggedIn();
        cy.wait(200);
        //log in as admin             
        cy.login("admin@productrating.com", "Asdf123!");
        cy.wait(400);

        //visist a product detail page 
        cy.visit("/search");
        cy.get('.product-cell').first().find('a').click({ force: true });
        cy.wait(200);

        cy.get('.review-item-container').its('length').then(numOfComments => {

            //long press a comment to delete
            cy.get('.review-item-container').first().trigger('mousedown').wait(700);
            cy.get('.review-item-container').first().trigger('mouseleave', {force: true});
            cy.wait(200);

            //confirm delete 
            cy.get('button').contains('Ok').click();
            cy.wait(200);

            //check - should be less comments then at frist 
            cy.get('.review-item-container').its('length').should('lt',numOfComments);
        });
    });
});