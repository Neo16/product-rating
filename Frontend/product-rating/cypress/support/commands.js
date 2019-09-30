// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add("login", (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add("drag", { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add("dismiss", { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This is will overwrite an existing command --
// Cypress.Commands.overwrite("visit", (originalFn, url, options) => { ... })

Cypress.Commands.add(
  'selectNth',
  { prevSubject: 'element' },
  (subject, pos) => {
    cy.wrap(subject)
      .children('option')
      .eq(pos)
      .then(e => {
        cy.wrap(subject).select(e.val())
      })
  }
)

//overwrite localstorage clearing, so cypress stays logged in between tests 
Cypress.LocalStorage.clear = function (keys, ls, rs) {
  return;
}

Cypress.Commands.add('login', (email, password) => {
  cy.visit("/account/login");
  cy.wait(200);  
  cy.get('input[name="username"]')  
    .type("a")
    .clear()      
    .type(email, {delay: 15});
  cy.get('input[name="password"]')    
    .type(password);
  cy.get('.btn-primary').click();
})

Cypress.Commands.add('logoutIfLoggedIn', () => {
  cy.get('body').then(body => {
    if (body.find('a[test="logged-in"]').length > 0) {
      cy.get('a[test="logged-in"]').click({ force: true });
      cy.wait(200);
      cy.get('a').contains('Log out').click({ force: true });
    }
    cy.wait(200);
  });
})