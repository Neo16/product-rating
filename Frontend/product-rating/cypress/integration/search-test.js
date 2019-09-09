describe("Search test", () => {
    it("should visit main page and search for a certain product", () => {
      cy.visit("/search") ;

      // Write 'Laptop 20' to free text search box
      cy.get('input[name="textFilter"]')
      .clear()
      .type('Laptop 20');     

      //Click search 
      cy.contains('Search').click();      

      //Check if only the correct result is there
      cy.get('.product-list-container').find('.product-cell').should('have.length', 1);
      cy.get('.product-cell').should('contain', 'Lenovo Dummy Laptop 20');
    });

    it("should search for category", () => {  
        
        cy.get('input[name="textFilter"]')
        .clear();

        // Choose a category 
        cy.contains('Consumer electronics').click();     
        // Choose a sub-category 
        cy.contains('Phones').click();   
        //Click search 
        cy.contains('Search').click(); 
        
        cy.wait(500);
        
        //Navigate to one of the result product detail page
        cy.get('.product-cell').find('a').click({ force: true });             

        // product should be a phone
        cy.get('body').should('contain', 'category: Phones');
      });
  });