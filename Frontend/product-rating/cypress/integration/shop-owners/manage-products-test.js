describe("Manage products test", () => {
  it("should visit product list", () => {

    cy.logoutIfLoggedIn();
    cy.wait(200);
    cy.login("webshop-owner@productrating.com", "Asdf123!");
    cy.wait(400);

    cy.visit("/manage-products");

    cy.get('body').should('contain', "Name");
    cy.get('body').should('contain', "Brand Name");
    cy.get('body').should('contain', "Category Name");
    cy.get('body').should('contain', "Created At");
  });

  it("should add a new product", () => {
    cy.visit("/manage-products/new");
    cy.wait(400);

    //type product name
    cy.get('input[name="product-name"]')
      .type('P30 lite');

    //choose category 
    cy.get('select[name="categoryId"]')
      .selectNth(4);

    //select a brand 
    cy.get('select[name="brandId"]')
      .selectNth(1);

    //Add start/end of production 
    cy.get('input[name="startOfProduction"]')
      .type('2015-09-11');
    cy.get('input[name="endOfProduction"]')
      .type('2019-09-11');

    //upload picture
    const fixturePath = "p30.jpg"; // relative to the cypress/fixtures directory
    const mimeType = 'image/jpeg';
    const filename = "p30.jpg";

    cy.get("input[type='file']")
      .then((subject) => {
        cy.fixture(fixturePath, 'base64').then((base64String) => {
          Cypress.Blob.base64StringToBlob(base64String, mimeType)
            .then(function (blob) {
              var testfile = new File([blob], filename, { type: mimeType });
              var dataTransfer = new DataTransfer();
              var fileInput = subject[0];

              dataTransfer.items.add(testfile);
              fileInput.files = dataTransfer.files;

              cy.wrap(subject).trigger('change', { force: true });
              cy.wait(400);

              //save product 
              cy.get('button').contains('Save Product').click();

              //check 
              cy.wait(200);

              cy.get('input[name="name"]')
                .clear()
                .type('P30 lite');
              cy.get('button').contains('Filter').click();

              cy.get('.ngx-datatable').should('contain', "P30 lite");
            });
        });
      });
  });

  it("should edit a new product", () => {
    cy.visit("/manage-products/");
    cy.wait(200);

    //Filter for product 
    cy.get('input[name="name"]')
      .clear()
      .type('P30 lite');
    cy.get('button').contains('Filter').click();
    cy.wait(200);

    //Navigate to edit page  
    cy.get('a').contains('Edit').click();
    cy.wait(200);

    //edit product name
    cy.get('input[name="product-name"]')
      .clear()
      .type('P30 lite edited');

    //click save 
    cy.get('button').contains('Save Product').click();

    //check 
    cy.get('input[name="name"]')
      .clear()
      .type('P30 lite');
    cy.get('button').contains('Filter').click();

    cy.get('.ngx-datatable').should('contain', "P30 lite edited");
  });


  it("should delete a product", () => {

    cy.get('input[name="name"]')
      .clear()
      .type('P30 lite');
    cy.get('button').contains('Filter').click();

    cy.get('a').contains('Delete').first().click();

    cy.wait(200);
    cy.get('button').contains('Ok').click();
    cy.wait(400);

    //check     
    cy.get('.ngx-datatable').should('not.contain', 'P30 lite edited');
  });
});

