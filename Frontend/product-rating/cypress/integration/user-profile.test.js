describe("User profile test", () => {
    it("should change nickname and nationality", () => {
        cy.visit("/profile");

        //start editing profile data  
        cy.get('button').contains('Edit profile ').click();     

        //.... TODO
    });
});