describe("Manage products test", () => {
    it("should visit product list", () => {
      cy.visit("/manage-products");     

      cy.get('body').should('contain', "Name");      
      cy.get('body').should('contain', "Brand Name");
      cy.get('body').should('contain', "Category Name");
      cy.get('body').should('contain', "Created At");
    });
  });