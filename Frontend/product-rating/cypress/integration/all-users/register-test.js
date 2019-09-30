describe("Register test test", () => {
    it("should register a user ", () => {

        cy.logoutIfLoggedIn();
        cy.wait(300);
        cy.visit('/register');

        cy.get('input[name="email"]')
            .type("onwner2@productrating.com");
        cy.get('input[name="password"]')
            .type("Asdf123!");
        cy.get('input[name="nickName"]')
            .type("Mike");
        cy.get('input[name="nationality"]')
            .type("Hungarian");
        cy.get('select[name="type"]')
            .selectNth(1);

        cy.get('button').contains('Register').click();
        cy.wait(500);

        cy.login("onwner2@productrating.com", "Asdf123!");
        cy.wait(600);

        //Check 
        //successful login should redirect to:
        cy.url().should('include', '/search');

        //Should have manage product menu 
        cy.get('a').contains('Manage Products');

    });
});