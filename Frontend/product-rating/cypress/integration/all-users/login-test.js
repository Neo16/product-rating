describe("Login test", () => {
    it("should visit login page and log in", () => {    
      //Custom login/logout command, defined in commands.js
      cy.logoutIfLoggedIn();
      cy.login("user@productrating.com", "Asdf123!");

      //successful login should redirect to:
      cy.url().should('include', '/search');
    });
  });