describe("should test manage user features", () => {
    it("should login as admin", () => {
        cy.logoutIfLoggedIn();
        cy.wait(200);
        //log in as admin             
        cy.login("admin@productrating.com", "Asdf123!");
        cy.wait(400);
        cy.url().should('include', '/search');
    });

    it("should list and filter users", () => {
        //navigate to manage users page
        cy.visit("/manage-users");

        //check if table is there and there are results 
        cy.get('.ngx-datatable').find('.datatable-body-row').its('length').should('be.gt', 0);
        cy.get('.ngx-datatable').should('contain', "Email");
        cy.get('.ngx-datatable').should('contain', "Role");
        cy.get('.ngx-datatable').should('contain', "Is Locked Out");

        //filter for a certain user
        cy.get('input[name="email"]')
            .clear()
            .type('user@productrating.com');

        cy.get('button').contains('Filter').click();
        cy.wait(100);

        //check if only one result is there 
        cy.get('.ngx-datatable').find('.datatable-body-row').its('length').should('eq', 1);
    });

    it("should lock out a user", () => {
        cy.get('a').contains('Lock out').click();
        // logout 
        cy.logoutIfLoggedIn();
        //check - try to log in with locked out user
        cy.wait(500);
        cy.login("user@productrating.com", "Asdf123!");

        //should stay on login page
        cy.url().should('include', '/login');
    });

    it("should re-enable a user", () => {
        cy.wait(500);
        cy.login("admin@productrating.com", "Asdf123!");

        cy.visit("/manage-users");
        //filter for a certain user
        cy.get('input[name="email"]')
            .clear()
            .type('user@productrating.com');

        cy.get('button').contains('Filter').click();
        cy.wait(100);

        cy.get('a').contains('End lock').click();
    });

    it("should delete a comment", () => {
        //visist a product detail page 
        cy.visit("/search");
        cy.get('.product-cell').first().find('a').click({ force: true });
        cy.wait(200);

        cy.get('.review-item-container').its('length').then(numOfComments => {

            //long press a comment to delete
            cy.get('.review-item-container').first().trigger('mousedown').wait(700);
            cy.get('.review-item-container').first().trigger('mouseleave');
            cy.wait(200);

            //confirm delete 
            cy.get('button').contains('Ok').click();
            cy.wait(200);

            //check - should be less comments then at frist 
            cy.get('.review-item-container').its('length').should('lt',numOfComments);
        })



    });
});