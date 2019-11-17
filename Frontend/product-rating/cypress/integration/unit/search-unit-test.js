describe("Search test", () => {
  it("should visit main page and search for a certain product", () => {

    cy.server({
      method: 'POST',
      url: 'products/find*'
    })
    cy.route({
      response: 'fixture:search-results.json'
    })

    cy.visit("/search");

    // Write 'Product 2' to free text search box
    cy.get('input[name="textFilter"]')
      .clear()
      .type('Product 2');

    //mock filtered results 
    cy.route({
      response: 'fixture:search-result-filtered.json'
    }).as('search');

    //Click search     
    cy.contains('Search').click();
    cy.wait('@search');

    // check search request textFilter
    cy.get('@search').should((xhr) => {   
      expect(xhr.request.body.textFilter).to.eq('Product 2');   
    })

    //Check if only the correct result is there
    cy.get('.product-list-container').find('.product-cell').should('have.length', 1);
    cy.get('.product-cell').should('contain', 'Product 2');
  });
});