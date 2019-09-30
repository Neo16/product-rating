describe("User profile test", () => {
    // it("should change nickname and nationality", () => {
    //     cy.visit("/profile");

    //     //start editing profile data  
    //     cy.get('button').contains('Edit profile ').click();     

    //     //.... TODO
    // });

    it("should change password", () => {
        //Navigate to /profile/security tab
        cy.visit("/profile");
        cy.wait(200);
        cy.get('a').contains('Security').click();
        cy.wait(200);

        cy.get('input[name="currentPassword"]')
            .clear()
            .type('Asdf123!');

        cy.get('input[name="newPassword"]')
            .clear()
            .type('Asdf123.');

        cy.get('input[name="newPasswordAgain"]')
            .clear()
            .type('Asdf123.');

        cy.get('button').contains('Update password').click();
        //check 
        cy.get("body").should('contain', 'Success');

        // logout 
        cy.get('a').contains('Logged in as').click({ force: true });
        cy.wait(200);
        cy.get('a').contains('Log out').click({ force: true });

        //log back in with new password
        cy.visit("/account/login");
        cy.get('input[name="username"]')
            .clear()
            .type('user@productrating.com');

        cy.get('input[name="password"]')
            .clear()
            .type('Asdf123.');

        cy.get('.btn-primary').click();

        //check
        cy.url().should('include', '/search')
    });

    it("should change back password", () => {
        //Navigate to /profile/security tab
        cy.visit("/profile");
        cy.wait(200);
        cy.get('a').contains('Security').click();
        cy.wait(200);

        cy.get('input[name="currentPassword"]')
            .clear()
            .type('Asdf123.');

        cy.get('input[name="newPassword"]')
            .clear()
            .type('Asdf123!');

        cy.get('input[name="newPasswordAgain"]')
            .clear()
            .type('Asdf123!');

        cy.get('button').contains('Update password').click();
    });
});