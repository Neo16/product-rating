describe("Manage categoires test", () => {
    it("should visit category list", () => {
        cy.visit("/manage-categories");

        cy.get('.ngx-datatable').find('.datatable-body-row').its('length').should('be.gt', 0);
        cy.get('.ngx-datatable').should('contain', "Name");
        cy.get('.ngx-datatable').should('contain', "Num Of Products");
        cy.get('.ngx-datatable').should('contain', "Attribute Names");
        cy.get('.ngx-datatable').should('contain', "Parent Name");
    });

    it("should add a new category", () => {
        cy.visit("/manage-categories/new");

        //type category name
        cy.get('input[name="category-name"]')
            .clear()
            .type('Digital camera');

        //add an attribute
        cy.get('a').contains('New attribute').click();
        cy.wait(200);

        cy.get('input[name="name"]')
            .clear()
            .type('Max ISO');

        cy.get('.attribute-item select')
            .select('2');

        //save category 
        cy.get('button').contains('Save Category').click();

        //check 
        cy.get('.ngx-datatable').should('contain', "Digital camera");
    });

    it("should edit category", () => {
        cy.get('input[name="name"]')
            .clear()
            .type('Digital camera');
        cy.get('button').contains('Filter').click();
        cy.wait(200);

        cy.get('a').contains('Edit').first().click();

        //type category name
        cy.get('input[name="category-name"]')
            .clear()
            .type('Camcorder');

        //save category 
        cy.get('button').contains('Update Category').click();

        //check 
        cy.get('.ngx-datatable').should('contain', "Camcorder");
    });

    it("should delete category", () => {
        cy.get('input[name="name"]')
            .clear()
            .type('Camcorder');
        cy.wait(200);

        cy.get('button').contains('Filter').click();

        cy.get('a').contains('Delete').first().click();

        cy.wait(200);
        cy.get('button').contains('Ok').click();
        cy.wait(200);

        //check     
        cy.get('.ngx-datatable').should('not.contain', 'Digital camera');
    });  

});