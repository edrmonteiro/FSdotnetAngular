define('Conclude login', () => {
    const username = 'a@a';
    const password = '123456';
    it ('Sucesso', () => {
        cy.login('a@a', '123456')
        cy.contains('world');
    })



    
})


