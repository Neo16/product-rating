describe("First test", () => {
    it("should visit login page", () => {
      cy.visit("/account/login");
      cy.get('input[name="username"]')
      .clear()
      .type('user@productrating.com');

      cy.get('input[name="password"]')
      .clear()
      .type('Asdf123!');

      cy.get('.btn-primary').click();

      cy.url().should('include', '/search');
    });
  });