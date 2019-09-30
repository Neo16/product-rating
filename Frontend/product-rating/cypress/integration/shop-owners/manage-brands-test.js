describe("Manage brands test", () => {
    it("should login as a shop owner", () => {
       cy.logoutIfLoggedIn();
       cy.wait(200);
       cy.login("webshop-owner@productrating.com", "Asdf123!");
    });

    it("should visit brand list", () => {
        cy.visit("/manage-brands");

        cy.get('.ngx-datatable').find('.datatable-body-row').its('length').should('be.gt', 0);
        cy.get('.ngx-datatable').should('contain', "Name");
        cy.get('.ngx-datatable').should('contain', "Num Of Products");
        cy.get('.ngx-datatable').should('contain', "Categories");
    });

    it("should add a new brand", () => {
        cy.visit("/manage-brands/new");

        //type brand name
        cy.get('input[name="brand-name"]')
            .clear()
            .type('Nikon');

        //save brand 
        cy.get('button').contains('Create Brand').click();

        //check 
        cy.get('.ngx-datatable').should('contain', "Nikon");
    });

    it("should edit a brand", () => {
        //Clear filter
        cy.get('input[name="name"]')
            .clear()   
            .type('Nikon');       
        cy.get('button').contains('Filter').click();
        cy.wait(200);

        //Navigate to an edit page
        cy.get('a').contains('Edit').first().click();
        cy.wait(200);

        //update brand name 
        cy.get('input[name="brand-name"]')
            .clear()
            .type('Canon');

        //save brand 
        cy.get('button').contains('Update Brand').click();
        cy.wait(200);

        //check     
        cy.get('.ngx-datatable').should('contain', 'Canon');
    });

    it("should delete a brand", () => {
        cy.get('input[name="name"]')
            .clear()
            .type('Canon');
        cy.get('button').contains('Filter').click();

        cy.get('a').contains('Delete').first().click();

        cy.wait(200);
        cy.get('button').contains('Ok').click();
        cy.wait(200);

        //check     
        cy.get('.ngx-datatable').should('not.contain', 'Nikon');
    });    
});