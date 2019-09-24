describe("Review test", () => {
    it("should add a text review", () => {
        //Navigate a product's detail page     
        cy.visit("/search");
        cy.get('.product-cell').first().find('a').click({ force: true });

        //Click on "add review" button
        cy.contains("Add review").click();
        cy.wait(200);

        // Write a comment 
        cy.get('textarea[name="text"]')
            .type('New UI test comment.');

        //Submit
        cy.contains("Submit review").click();

        //check         
        cy.get('.review-item-list-container').should('contain', 'New UI test comment.');
    });
    it("should delete a text review", () => {

        //delete          
        cy.get('.review-body').contains('New UI test comment.')
            .first()
            .siblings('.review-like-container')
            .find('.ng-fa-icon[ng-reflect-icon-prop="fas,trash"]')
            .click();

        //check 
        cy.get('.review-item-list-container').not('contain', 'New UI test comment.');
    });
    it("should edit a text review", () => {
        //start edit           
        cy.get('.review-item-list-container')
            .find('.ng-fa-icon[ng-reflect-icon-prop="fas,pencil-alt"]')
            .first()
            .click();
        cy.wait(200);

        //type edited comment 
        var random = Math.floor((Math.random() * 10000));
        cy.get('.review-item-list-container')
            .find('textarea')
            .clear()
            .type(random);

        //click save
        cy.get('.review-item-list-container')
            .find('.ng-fa-icon[ng-reflect-icon-prop="fas,save"]')
            .click();

        //check
        cy.get('.review-item-list-container').should('contain', random);
    });
    it("should upvota then downvote a text review", () => {

        //find a review, that's not written by the user logged in 
        //TODO: add indicator poperty to find reviews that the user didnt up/downvote yet 
        cy.get('.review-like-container:not([isMine])')            
            .first().then(($score) => {
                //original score
                const score = parseInt($score.text());

                //upvote 
                cy.get('button[test="upvote"]').first().click();
                //check
                cy.get('.review-like-container').first().should(($upvotedScore) => {
                    expect(parseInt($upvotedScore.text())).to.eq(score+1);                   
                })

                //downvote 
                cy.get('button[test="downvote"]').first().click();
                cy.get('button[test="downvote"]').first().click();
                //check
                cy.get('.review-like-container').first().should(($downvotedScore) => {
                    expect(parseInt($downvotedScore.text())).to.eq(score-1);                   
                })

                //upvote again, to get back to original state 
                cy.get('button[test="upvote"]').first().click();
            })

    });
});