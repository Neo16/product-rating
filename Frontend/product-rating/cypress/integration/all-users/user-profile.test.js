describe("User profile test", () => {
    it("should change nickname and nationality", () => {
        cy.wait(400);
        cy.visit("/profile");
        cy.wait(500);

        //start editing profile data  
        cy.get('button').contains('Edit profile').click();
        cy.wait(300);

        cy.get('input[name="nickName"]')
            .clear()
            .type('Fekete Péter');

        cy.get('input[name="nationality"]')
            .clear()
            .type('Dutch');

        cy.get('button').contains('Save profile').click();

        //check 
        cy.wait(300);
        cy.get('.username').should('contain', 'Fekete Péter')
    });

    it("should change password", () => {
        //Navigate to /profile/security tab
        cy.visit("/profile");
        cy.wait(400);
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
        cy.logoutIfLoggedIn();
        cy.wait(200);

        //log back in with new password
        cy.visit("/account/login");
        cy.login("user@productrating.com", "Asdf123.");
        cy.wait(400);

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